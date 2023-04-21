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
using OSiNOSSv = inos.common.Service;
using inos.common.Model;
using inos.common.Service;
using System.Diagnostics;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System.IO;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
        #region // khi clone hệ thống đã có các hàm này:
    //    #region // Mst_GovIDType:
    //    private void Mst_GovIDType_CheckDB(
    //       ref ArrayList alParamsCoupleError
    //       , object objGovIDType
    //       , string strFlagExistToCheck
    //       , string strFlagActiveListToCheck
    //       , out DataTable dtDB_Mst_GovIDType
    //       )
    //    {
    //        // GetInfo:
    //        string strSqlExec = CmUtils.StringUtils.Replace(@"
				//	select top 1
				//		t.*
				//	from Mst_GovIDType t --//[mylock]
				//	where (1=1)
				//		and t.GovIDType = @objGovIDType
				//	;
				//");
    //        dtDB_Mst_GovIDType = _cf.db.ExecQuery(
    //            strSqlExec
    //            , "@objGovIDType", objGovIDType
    //            ).Tables[0];
    //        dtDB_Mst_GovIDType.TableName = "Mst_GovIDType";

    //        // strFlagExistToCheck:
    //        if (strFlagExistToCheck.Length > 0)
    //        {
    //            if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_GovIDType.Rows.Count < 1)
    //            {
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "Check.GovIDType", objGovIDType
    //                    });
    //                throw CmUtils.CMyException.Raise(
    //                    TError.ErridnInventory.Mst_GovIDType_CheckDB_GovIDTypeNotFound
    //                    , null
    //                    , alParamsCoupleError.ToArray()
    //                    );
    //            }
    //            if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_GovIDType.Rows.Count > 0)
    //            {
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "Check.GovIDType", objGovIDType
    //                    });
    //                throw CmUtils.CMyException.Raise(
    //                    TError.ErridnInventory.Mst_GovIDType_CheckDB_GovIDTypeExist
    //                    , null
    //                    , alParamsCoupleError.ToArray()
    //                    );
    //            }
    //        }

    //        // strFlagActiveListToCheck:
    //        if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_GovIDType.Rows[0]["FlagActive"])))
    //        {
    //            alParamsCoupleError.AddRange(new object[]{
    //                "Check.GovIDType", objGovIDType
    //                , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
    //                , "DB.FlagActive", dtDB_Mst_GovIDType.Rows[0]["FlagActive"]
    //                });
    //            throw CmUtils.CMyException.Raise(
    //                TError.ErridnInventory.Mst_GovIDType_CheckDB_FlagActiveNotMatched
    //                , null
    //                , alParamsCoupleError.ToArray()
    //                );
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_GovIDType_Get(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_GovIDType objRQ_Mst_GovIDType
    //        ////
    //        , out RT_Mst_GovIDType objRT_Mst_GovIDType
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_GovIDType.Tid;
    //        objRT_Mst_GovIDType = new RT_Mst_GovIDType();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovIDType.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_GovIDType_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_GovIDType_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //                "strFunctionName", strFunctionName
    //                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                });
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            #endregion

    //            #region // Refine and Check Input:
    //            List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

    //            List<Mst_GovIDType> lst_Mst_GovIDType = new List<Mst_GovIDType>();
    //            #endregion

    //            #region // WS_RptSv_Mst_GovIDType_Get:
    //            mdsResult = RptSv_Mst_GovIDType_Get(
    //                objRQ_Mst_GovIDType.Tid // strTid
    //                , objRQ_Mst_GovIDType.GwUserCode // strGwUserCode
    //                , objRQ_Mst_GovIDType.GwPassword // strGwPassword
    //                , objRQ_Mst_GovIDType.WAUserCode // strUserCode
    //                , objRQ_Mst_GovIDType.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          //// Filter:
    //                , objRQ_Mst_GovIDType.Ft_RecordStart // strFt_RecordStart
    //                , objRQ_Mst_GovIDType.Ft_RecordCount // strFt_RecordCount
    //                , objRQ_Mst_GovIDType.Ft_WhereClause // strFt_WhereClause
    //                                                     //// Return:
    //                , objRQ_Mst_GovIDType.Rt_Cols_Mst_GovIDType // Rt_Cols_Mst_GovIDType
    //                );
    //            #endregion

    //            #region // GetData:
    //            if (!CmUtils.CMyDataSet.HasError(mdsResult))
    //            {
    //                ////
    //                DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
    //                lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
    //                objRT_Mst_GovIDType.MySummaryTable = lst_MySummaryTable[0];

    //                ////
    //                DataTable dt_Mst_GovIDType = mdsResult.Tables["Mst_GovIDType"].Copy();
    //                lst_Mst_GovIDType = TUtils.DataTableCmUtils.ToListof<Mst_GovIDType>(dt_Mst_GovIDType);
    //                objRT_Mst_GovIDType.Lst_Mst_GovIDType = lst_Mst_GovIDType;
    //                /////
    //            }
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet RptSv_Mst_GovIDType_Get(
    //       string strTid
    //       , string strGwUserCode
    //       , string strGwPassword
    //       , string strWAUserCode
    //       , string strWAUserPassword
    //       , ref ArrayList alParamsCoupleError
    //       //// Filter:
    //       , string strFt_RecordStart
    //       , string strFt_RecordCount
    //       , string strFt_WhereClause
    //       //// Return:
    //       , string strRt_Cols_Mst_GovIDType
    //       )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        // bool bNeedTransaction = true;
    //        string strFunctionName = "RptSv_Mst_GovIDType_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_GovIDType_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////// Filter
				//, "strFt_RecordStart", strFt_RecordStart
    //            , "strFt_RecordCount", strFt_RecordCount
    //            , "strFt_WhereClause", strFt_WhereClause
				////// Return
				//, "strRt_Cols_Mst_GovIDType", strRt_Cols_Mst_GovIDType
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );

    //            // RptSv_Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, strWAUserCode
    //            //	, strWAUserPassword
    //            //	);

    //            //// Check Access/Deny:
    //            //Sys_Access_CheckDenyV30(
    //            //    ref alParamsCoupleError
    //            //    , strWAUserCode
    //            //    , strFunctionName
    //            //    );
    //            #endregion

    //            #region // Mst_GovIDType_GetX:
    //            DataSet dsGetData = null;
    //            {
    //                ////
    //                Mst_GovIDType_GetX(
    //                    strTid // strTid
    //                    , strGwUserCode // strGwUserCode
    //                    , strGwPassword // strGwPassword
    //                    , strWAUserCode // strWAUserCode
    //                    , strWAUserPassword // strWAUserPassword
    //                    , ref alParamsCoupleError // alParamsCoupleError
    //                    , dtimeSys // dtimeSys
    //                               ////
    //                    , strFt_RecordStart // strFt_RecordStart
    //                    , strFt_RecordCount // strFt_RecordCount
    //                    , strFt_WhereClause // strFt_WhereClause
    //                                        ////
    //                    , strRt_Cols_Mst_GovIDType // strRt_Cols_Mst_GovIDType
    //                                               ////
    //                    , out dsGetData // dsGetData
    //                    );
    //                ////
    //                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
    //            }
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Mst_GovIDType_Get(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_GovIDType objRQ_Mst_GovIDType
    //        ////
    //        , out RT_Mst_GovIDType objRT_Mst_GovIDType
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_GovIDType.Tid;
    //        objRT_Mst_GovIDType = new RT_Mst_GovIDType();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovIDType.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_GovIDType_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_GovIDType_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //                "strFunctionName", strFunctionName
    //                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                });
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            #endregion

    //            #region // Refine and Check Input:
    //            List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

    //            List<Mst_GovIDType> lst_Mst_GovIDType = new List<Mst_GovIDType>();
    //            #endregion

    //            #region // WS_Mst_GovIDType_Get:
    //            mdsResult = Mst_GovIDType_Get(
    //                objRQ_Mst_GovIDType.Tid // strTid
    //                , objRQ_Mst_GovIDType.GwUserCode // strGwUserCode
    //                , objRQ_Mst_GovIDType.GwPassword // strGwPassword
    //                , objRQ_Mst_GovIDType.WAUserCode // strUserCode
    //                , objRQ_Mst_GovIDType.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          //// Filter:
    //                , objRQ_Mst_GovIDType.Ft_RecordStart // strFt_RecordStart
    //                , objRQ_Mst_GovIDType.Ft_RecordCount // strFt_RecordCount
    //                , objRQ_Mst_GovIDType.Ft_WhereClause // strFt_WhereClause
    //                                                     //// Return:
    //                , objRQ_Mst_GovIDType.Rt_Cols_Mst_GovIDType // Rt_Cols_Mst_GovIDType
    //                );
    //            #endregion

    //            #region // GetData:
    //            if (!CmUtils.CMyDataSet.HasError(mdsResult))
    //            {
    //                ////
    //                DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
    //                lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
    //                objRT_Mst_GovIDType.MySummaryTable = lst_MySummaryTable[0];

    //                ////
    //                DataTable dt_Mst_GovIDType = mdsResult.Tables["Mst_GovIDType"].Copy();
    //                lst_Mst_GovIDType = TUtils.DataTableCmUtils.ToListof<Mst_GovIDType>(dt_Mst_GovIDType);
    //                objRT_Mst_GovIDType.Lst_Mst_GovIDType = lst_Mst_GovIDType;
    //                /////
    //            }
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet Mst_GovIDType_Get(
    //       string strTid
    //       , string strGwUserCode
    //       , string strGwPassword
    //       , string strWAUserCode
    //       , string strWAUserPassword
    //       , ref ArrayList alParamsCoupleError
    //       //// Filter:
    //       , string strFt_RecordStart
    //       , string strFt_RecordCount
    //       , string strFt_WhereClause
    //       //// Return:
    //       , string strRt_Cols_Mst_GovIDType
    //       )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        Stopwatch stopWatchFunc = new Stopwatch();
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        // bool bNeedTransaction = true;
    //        string strFunctionName = "Mst_GovIDType_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_GovIDType_Get;
    //        ArrayList alParamsCoupleSW = new ArrayList();
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////// Filter
				//, "strFt_RecordStart", strFt_RecordStart
    //            , "strFt_RecordCount", strFt_RecordCount
    //            , "strFt_WhereClause", strFt_WhereClause
				////// Return
				//, "strRt_Cols_Mst_GovIDType", strRt_Cols_Mst_GovIDType
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // SW:				
    //            stopWatchFunc.Start();
    //            alParamsCoupleSW.AddRange(new object[]{
    //                "strFunctionName", strFunctionName
    //                , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                });
    //            #endregion

    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );

    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, strWAUserCode
    //            //	, strWAUserPassword
    //            //	);

    //            //// Check Access/Deny:
    //            //Sys_Access_CheckDenyV30(
    //            //    ref alParamsCoupleError
    //            //    , strWAUserCode
    //            //    , strFunctionName
    //            //    );
    //            #endregion

    //            #region // Mst_GovIDType_GetX:
    //            DataSet dsGetData = null;
    //            {
    //                ////
    //                Mst_GovIDType_GetX(
    //                    strTid // strTid
    //                    , strGwUserCode // strGwUserCode
    //                    , strGwPassword // strGwPassword
    //                    , strWAUserCode // strWAUserCode
    //                    , strWAUserPassword // strWAUserPassword
    //                    , ref alParamsCoupleError // alParamsCoupleError
    //                    , dtimeSys // dtimeSys
    //                               ////
    //                    , strFt_RecordStart // strFt_RecordStart
    //                    , strFt_RecordCount // strFt_RecordCount
    //                    , strFt_WhereClause // strFt_WhereClause
    //                                        ////
    //                    , strRt_Cols_Mst_GovIDType // strRt_Cols_Mst_GovIDType
    //                                               ////
    //                    , out dsGetData // dsGetData
    //                    );
    //                ////
    //                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
    //            }
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            stopWatchFunc.Stop();
    //            alParamsCoupleSW.AddRange(new object[]{
    //                "strFunctionName", strFunctionName
    //                , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc.ElapsedMilliseconds
    //                });

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleSW // alParamsCoupleSW
    //                );
    //            #endregion
    //        }
    //    }

    //    private void Mst_GovIDType_GetX(
    //      string strTid
    //      , string strGwUserCode
    //      , string strGwPassword
    //      , string strWAUserCode
    //      , string strWAUserPassword
    //      , ref ArrayList alParamsCoupleError
    //      , DateTime dtimeSys
    //      //// Filter:
    //      , string strFt_RecordStart
    //      , string strFt_RecordCount
    //      , string strFt_WhereClause
    //      //// Return:
    //      , string strRt_Cols_Mst_GovIDType
    //      ////
    //      , out DataSet dsGetData
    //     )
    //    {
    //        #region // Temp:
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //DateTime dtimeSys = DateTime.UtcNow;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "Mst_GovIDType_Get";
    //        //string strErrorCodeDefault = TError.ErrHTCNM.Mst_GovIDType_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                });
    //        #endregion

    //        #region // Check:
    //        //// Refine:
    //        long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
    //        long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
    //        bool bGet_Mst_GovIDType = (strRt_Cols_Mst_GovIDType != null && strRt_Cols_Mst_GovIDType.Length > 0);

    //        //// drAbilityOfUser:
    //        //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

    //        #endregion

    //        #region // Build Sql:
    //        ////
    //        ArrayList alParamsCoupleSql = new ArrayList();
    //        alParamsCoupleSql.AddRange(new object[] {
    //                "@nFilterRecordStart", nFilterRecordStart
    //                , "@nFilterRecordEnd", nFilterRecordEnd
    //                });
    //        ////
    //        //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
    //        ////
    //        string strSqlGetData = CmUtils.StringUtils.Replace(@"
				//		---- #tbl_Mst_GovIDType_Filter_Draft:
				//		select distinct
				//			identity(bigint, 0, 1) MyIdxSeq
				//			, mgi.GovIDType
				//		into #tbl_Mst_GovIDType_Filter_Draft
				//		from Mst_GovIDType mgi --//[mylock]
				//		where (1=1)
				//			zzB_Where_strFilter_zzE
				//		order by mgi.GovIDType asc
				//		;

				//		---- Summary:
				//		select Count(0) MyCount from #tbl_Mst_GovIDType_Filter_Draft t --//[mylock]
				//		;

				//		---- #tbl_Mst_GovIDType_Filter:
				//		select
				//			t.*
				//		into #tbl_Mst_GovIDType_Filter
				//		from #tbl_Mst_GovIDType_Filter_Draft t --//[mylock]
				//		where
				//			(t.MyIdxSeq >= @nFilterRecordStart)
				//			and (t.MyIdxSeq <= @nFilterRecordEnd)
				//		;

				//		-------- Mst_GovIDType --------:
				//		zzB_Select_Mst_GovIDType_zzE
				//		----------------------------------------

				//		---- Clear for debug:
				//		--drop table #tbl_Mst_GovIDType_Filter_Draft;
				//		--drop table #tbl_Mst_GovIDType_Filter;
				//	"
    //            );
    //        ////
    //        string zzB_Select_Mst_GovIDType_zzE = "-- Nothing.";
    //        if (bGet_Mst_GovIDType)
    //        {
    //            #region // bGet_Mst_GovIDType:
    //            zzB_Select_Mst_GovIDType_zzE = CmUtils.StringUtils.Replace(@"
				//			---- Mst_GovIDType:
				//			select
				//				t.MyIdxSeq
				//				, mgi.*
				//			from #tbl_Mst_GovIDType_Filter t --//[mylock]
				//				inner join Mst_GovIDType mgi --//[mylock]
				//					on t.GovIDType = mgi.GovIDType
				//			order by t.MyIdxSeq asc
				//			;
				//		"
    //                );
    //            #endregion
    //        }
    //        ////
    //        string zzB_Where_strFilter_zzE = "";
    //        {
    //            Hashtable htSpCols = new Hashtable();
    //            {
    //                #region // htSpCols:
    //                ////
    //                TUtils.CUtils.MyBuildHTSupportedColumns(
    //                    _cf.db // db
    //                    , ref htSpCols // htSupportedColumns
    //                    , "Mst_GovIDType" // strTableNameDB
    //                    , "Mst_GovIDType." // strPrefixStd
    //                    , "mgi." // strPrefixAlias
    //                    );
    //                ////
    //                #endregion
    //            }
    //            zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
    //                htSpCols // htSpCols
    //                , strFt_WhereClause // strClause
    //                , "@p_" // strParamPrefix
    //                , ref alParamsCoupleSql // alParamsCoupleSql
    //                );
    //            zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
    //            alParamsCoupleError.AddRange(new object[]{
    //                    "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //                    });
    //        }
    //        ////
    //        strSqlGetData = CmUtils.StringUtils.Replace(
    //            strSqlGetData
    //            , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //            , "zzB_Select_Mst_GovIDType_zzE", zzB_Select_Mst_GovIDType_zzE
    //            );
    //        #endregion

    //        #region // Get Data:
    //        dsGetData = _cf.db.ExecQuery(
    //            strSqlGetData
    //            , alParamsCoupleSql.ToArray()
    //            );
    //        int nIdxTable = 0;
    //        dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
    //        if (bGet_Mst_GovIDType)
    //        {
    //            dsGetData.Tables[nIdxTable++].TableName = "Mst_GovIDType";
    //        }
    //        #endregion
    //    }
    //    #endregion

    //    #region // Mst_Province:
    //    private void Mst_Province_CheckDB(
    //        ref ArrayList alParamsCoupleError
    //        , object objProvinceCode
    //        , string strFlagExistToCheck
    //        , string strFlagActiveListToCheck
    //        , out DataTable dtDB_Mst_Province
    //        )
    //    {
    //        // GetInfo:
    //        string strSqlExec = CmUtils.StringUtils.Replace(@"
				//	select top 1
				//		t.*
				//	from Mst_Province t --//[mylock]
				//	where (1=1)
				//		and t.ProvinceCode = @objProvinceCode
				//	;
				//");
    //        dtDB_Mst_Province = _cf.db.ExecQuery(
    //            strSqlExec
    //            , "@objProvinceCode", objProvinceCode
    //            ).Tables[0];
    //        dtDB_Mst_Province.TableName = "Mst_Province";

    //        // strFlagExistToCheck:
    //        if (strFlagExistToCheck.Length > 0)
    //        {
    //            if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Province.Rows.Count < 1)
    //            {
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "Check.ProvinceCode", objProvinceCode
    //                    });
    //                throw CmUtils.CMyException.Raise(
    //                    TError.ErridnInventory.Mst_Province_CheckDB_ProvinceNotFound
    //                    , null
    //                    , alParamsCoupleError.ToArray()
    //                    );
    //            }
    //            if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Province.Rows.Count > 0)
    //            {
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "Check.ProvinceCode", objProvinceCode
    //                    });
    //                throw CmUtils.CMyException.Raise(
    //                    TError.ErridnInventory.Mst_Province_CheckDB_ProvinceExist
    //                    , null
    //                    , alParamsCoupleError.ToArray()
    //                    );
    //            }
    //        }

    //        // strFlagActiveListToCheck:
    //        if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Province.Rows[0]["FlagActive"])))
    //        {
    //            alParamsCoupleError.AddRange(new object[]{
    //                "Check.ProvinceCode", objProvinceCode
    //                , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
    //                , "DB.FlagActive", dtDB_Mst_Province.Rows[0]["FlagActive"]
    //                });
    //            throw CmUtils.CMyException.Raise(
    //                TError.ErridnInventory.Mst_Province_CheckDB_FlagActiveNotMatched
    //                , null
    //                , alParamsCoupleError.ToArray()
    //                );
    //        }
    //    }
    //    public DataSet RptSv_Mst_Province_Get(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        //// Filter:
    //        , string strFt_RecordStart
    //        , string strFt_RecordCount
    //        , string strFt_WhereClause
    //        //// Return:
    //        , string strRt_Cols_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        bool bNeedTransaction = true;
    //        string strFunctionName = "RptSv_Mst_Province_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.RptSv_Mst_Province_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////// Filter
				//, "strFt_RecordStart", strFt_RecordStart
    //            , "strFt_RecordCount", strFt_RecordCount
    //            , "strFt_WhereClause", strFt_WhereClause
				////// Return
				//, "strRt_Cols_Mst_Province", strRt_Cols_Mst_Province
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            if (bNeedTransaction) _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );

    //            // Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, strWAUserCode
    //            //	, strWAUserPassword
    //            //	);

    //            //// Check Access/Deny:
    //            //Sys_Access_CheckDenyV30(
    //            //    ref alParamsCoupleError
    //            //    , strWAUserCode
    //            //    , strFunctionName
    //            //    );
    //            #endregion

    //            #region // Check:
    //            //// Refine:
    //            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
    //            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
    //            bool bGet_Mst_Province = (strRt_Cols_Mst_Province != null && strRt_Cols_Mst_Province.Length > 0);

    //            //// drAbilityOfUser:
    //            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

    //            #endregion

    //            #region // Build Sql:
    //            ////
    //            ArrayList alParamsCoupleSql = new ArrayList();
    //            alParamsCoupleSql.AddRange(new object[] {
    //                "@nFilterRecordStart", nFilterRecordStart
    //                , "@nFilterRecordEnd", nFilterRecordEnd
    //                });
    //            ////
    //            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
    //            ////
    //            string strSqlGetData = CmUtils.StringUtils.Replace(@"
				//		---- #tbl_Mst_Province_Filter_Draft:
				//		select distinct
				//			identity(bigint, 0, 1) MyIdxSeq
				//			, mp.ProvinceCode
				//		into #tbl_Mst_Province_Filter_Draft
				//		from Mst_Province mp --//[mylock]
				//		where (1=1)
				//			zzB_Where_strFilter_zzE
				//		order by mp.ProvinceCode asc
				//		;

				//		---- Summary:
				//		select Count(0) MyCount from #tbl_Mst_Province_Filter_Draft t --//[mylock]
				//		;

				//		---- #tbl_Mst_Province_Filter:
				//		select
				//			t.*
				//		into #tbl_Mst_Province_Filter
				//		from #tbl_Mst_Province_Filter_Draft t --//[mylock]
				//		where
				//			(t.MyIdxSeq >= @nFilterRecordStart)
				//			and (t.MyIdxSeq <= @nFilterRecordEnd)
				//		;

				//		-------- Mst_Province --------:
				//		zzB_Select_Mst_Province_zzE
				//		----------------------------------------

				//		---- Clear for debug:
				//		--drop table #tbl_Mst_Province_Filter_Draft;
				//		--drop table #tbl_Mst_Province_Filter;
				//	"
    //                );
    //            ////
    //            string zzB_Select_Mst_Province_zzE = "-- Nothing.";
    //            if (bGet_Mst_Province)
    //            {
    //                #region // bGet_Mst_Province:
    //                zzB_Select_Mst_Province_zzE = CmUtils.StringUtils.Replace(@"
				//			---- Mst_Province:
				//			select
				//				t.MyIdxSeq
				//				, mp.*
				//			from #tbl_Mst_Province_Filter t --//[mylock]
				//				inner join Mst_Province mp --//[mylock]
				//					on t.ProvinceCode = mp.ProvinceCode
				//			order by t.MyIdxSeq asc
				//			;
				//		"
    //                    );
    //                #endregion
    //            }
    //            ////
    //            string zzB_Where_strFilter_zzE = "";
    //            {
    //                Hashtable htSpCols = new Hashtable();
    //                {
    //                    #region // htSpCols:
    //                    ////
    //                    TUtils.CUtils.MyBuildHTSupportedColumns(
    //                        _cf.db // db
    //                        , ref htSpCols // htSupportedColumns
    //                        , "Mst_Province" // strTableNameDB
    //                        , "Mst_Province." // strPrefixStd
    //                        , "mp." // strPrefixAlias
    //                        );
    //                    ////
    //                    #endregion
    //                }
    //                zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
    //                    htSpCols // htSpCols
    //                    , strFt_WhereClause // strClause
    //                    , "@p_" // strParamPrefix
    //                    , ref alParamsCoupleSql // alParamsCoupleSql
    //                    );
    //                zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //                    });
    //            }
    //            ////
    //            strSqlGetData = CmUtils.StringUtils.Replace(
    //                strSqlGetData
    //                , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //                , "zzB_Select_Mst_Province_zzE", zzB_Select_Mst_Province_zzE
    //                );
    //            #endregion

    //            #region // Get Data:
    //            DataSet dsGetData = _cf.db.ExecQuery(
    //                strSqlGetData
    //                , alParamsCoupleSql.ToArray()
    //                );
    //            int nIdxTable = 0;
    //            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
    //            if (bGet_Mst_Province)
    //            {
    //                dsGetData.Tables[nIdxTable++].TableName = "Mst_Province";
    //            }
    //            CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }
    //    public DataSet Mst_Province_Get(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        //// Filter:
    //        , string strFt_RecordStart
    //        , string strFt_RecordCount
    //        , string strFt_WhereClause
    //        //// Return:
    //        , string strRt_Cols_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        Stopwatch stopWatchFunc = new Stopwatch();
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        bool bNeedTransaction = true;
    //        string strFunctionName = "Mst_Province_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_Province_Get;
    //        ArrayList alParamsCoupleSW = new ArrayList();
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////// Filter
				//, "strFt_RecordStart", strFt_RecordStart
    //            , "strFt_RecordCount", strFt_RecordCount
    //            , "strFt_WhereClause", strFt_WhereClause
				////// Return
				//, "strRt_Cols_Mst_Province", strRt_Cols_Mst_Province
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // SW:				
    //            stopWatchFunc.Start();
    //            alParamsCoupleSW.AddRange(new object[]{
    //                "strFunctionName", strFunctionName
    //                , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                });
    //            #endregion

    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            if (bNeedTransaction) _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );

    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, strWAUserCode
    //            //	, strWAUserPassword
    //            //	);

    //            //// Check Access/Deny:
    //            //Sys_Access_CheckDenyV30(
    //            //    ref alParamsCoupleError
    //            //    , strWAUserCode
    //            //    , strFunctionName
    //            //    );
    //            #endregion

    //            #region // Check:
    //            //// Refine:
    //            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
    //            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
    //            bool bGet_Mst_Province = (strRt_Cols_Mst_Province != null && strRt_Cols_Mst_Province.Length > 0);

    //            //// drAbilityOfUser:
    //            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

    //            #endregion

    //            #region // Build Sql:
    //            ////
    //            ArrayList alParamsCoupleSql = new ArrayList();
    //            alParamsCoupleSql.AddRange(new object[] {
    //                "@nFilterRecordStart", nFilterRecordStart
    //                , "@nFilterRecordEnd", nFilterRecordEnd
    //                });
    //            ////
    //            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
    //            ////
    //            string strSqlGetData = CmUtils.StringUtils.Replace(@"
				//		---- #tbl_Mst_Province_Filter_Draft:
				//		select distinct
				//			identity(bigint, 0, 1) MyIdxSeq
				//			, mp.ProvinceCode
				//		into #tbl_Mst_Province_Filter_Draft
				//		from Mst_Province mp --//[mylock]
				//		where (1=1)
				//			zzB_Where_strFilter_zzE
				//		order by mp.ProvinceCode asc
				//		;

				//		---- Summary:
				//		select Count(0) MyCount from #tbl_Mst_Province_Filter_Draft t --//[mylock]
				//		;

				//		---- #tbl_Mst_Province_Filter:
				//		select
				//			t.*
				//		into #tbl_Mst_Province_Filter
				//		from #tbl_Mst_Province_Filter_Draft t --//[mylock]
				//		where
				//			(t.MyIdxSeq >= @nFilterRecordStart)
				//			and (t.MyIdxSeq <= @nFilterRecordEnd)
				//		;

				//		-------- Mst_Province --------:
				//		zzB_Select_Mst_Province_zzE
				//		----------------------------------------

				//		---- Clear for debug:
				//		--drop table #tbl_Mst_Province_Filter_Draft;
				//		--drop table #tbl_Mst_Province_Filter;
				//	"
    //                );
    //            ////
    //            string zzB_Select_Mst_Province_zzE = "-- Nothing.";
    //            if (bGet_Mst_Province)
    //            {
    //                #region // bGet_Mst_Province:
    //                zzB_Select_Mst_Province_zzE = CmUtils.StringUtils.Replace(@"
				//			---- Mst_Province:
				//			select
				//				t.MyIdxSeq
				//				, mp.*
				//			from #tbl_Mst_Province_Filter t --//[mylock]
				//				inner join Mst_Province mp --//[mylock]
				//					on t.ProvinceCode = mp.ProvinceCode
				//			order by t.MyIdxSeq asc
				//			;
				//		"
    //                    );
    //                #endregion
    //            }
    //            ////
    //            string zzB_Where_strFilter_zzE = "";
    //            {
    //                Hashtable htSpCols = new Hashtable();
    //                {
    //                    #region // htSpCols:
    //                    ////
    //                    TUtils.CUtils.MyBuildHTSupportedColumns(
    //                        _cf.db // db
    //                        , ref htSpCols // htSupportedColumns
    //                        , "Mst_Province" // strTableNameDB
    //                        , "Mst_Province." // strPrefixStd
    //                        , "mp." // strPrefixAlias
    //                        );
    //                    ////
    //                    #endregion
    //                }
    //                zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
    //                    htSpCols // htSpCols
    //                    , strFt_WhereClause // strClause
    //                    , "@p_" // strParamPrefix
    //                    , ref alParamsCoupleSql // alParamsCoupleSql
    //                    );
    //                zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //                    });
    //            }
    //            ////
    //            strSqlGetData = CmUtils.StringUtils.Replace(
    //                strSqlGetData
    //                , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //                , "zzB_Select_Mst_Province_zzE", zzB_Select_Mst_Province_zzE
    //                );
    //            #endregion

    //            #region // Get Data:
    //            DataSet dsGetData = _cf.db.ExecQuery(
    //                strSqlGetData
    //                , alParamsCoupleSql.ToArray()
    //                );
    //            int nIdxTable = 0;
    //            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
    //            if (bGet_Mst_Province)
    //            {
    //                dsGetData.Tables[nIdxTable++].TableName = "Mst_Province";
    //            }
    //            CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            stopWatchFunc.Stop();
    //            alParamsCoupleSW.AddRange(new object[]{
    //                "strFunctionName", strFunctionName
    //                , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //                , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc.ElapsedMilliseconds
    //                });

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleSW // alParamsCoupleSW
    //                );
    //            #endregion
    //        }
    //    }
    //    public DataSet Mst_Province_Create(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        ////
    //        , object objProvinceCode
    //        , object objProvinceName
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        string strFunctionName = "Mst_Province_Create";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_Province_Create;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            ////
				//, "objProvinceCode", objProvinceCode
    //            , "objProvinceName", objProvinceName
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Convert Input:
    //            #endregion

    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );
    //            #endregion

    //            #region // Refine and Check Input:
    //            ////
    //            string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
    //            string strProvinceName = string.Format("{0}", objProvinceName).Trim();

    //            // drAbilityOfUser:
    //            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
    //            ////
    //            DataTable dtDB_Mst_Province = null;
    //            {
    //                ////
    //                if (strProvinceCode == null || strProvinceCode.Length < 1)
    //                {
    //                    alParamsCoupleError.AddRange(new object[]{
    //                        "Check.strProvinceCode", strProvinceCode
    //                        });
    //                    throw CmUtils.CMyException.Raise(
    //                        TError.ErridnInventory.Mst_Province_Create_InvalidProvinceCode
    //                        , null
    //                        , alParamsCoupleError.ToArray()
    //                        );
    //                }
    //                Mst_Province_CheckDB(
    //                    ref alParamsCoupleError // alParamsCoupleError
    //                    , strProvinceCode // objProvinceCode
    //                    , TConst.Flag.No // strFlagExistToCheck
    //                    , "" // strFlagActiveListToCheck
    //                    , out dtDB_Mst_Province // dtDB_Mst_Province
    //                    );
    //                ////
    //                if (strProvinceName.Length < 1)
    //                {
    //                    alParamsCoupleError.AddRange(new object[]{
    //                        "Check.strProvinceName", strProvinceName
    //                        });
    //                    throw CmUtils.CMyException.Raise(
    //                        TError.ErridnInventory.Mst_Province_Create_InvalidProvinceName
    //                        , null
    //                        , alParamsCoupleError.ToArray()
    //                        );
    //                }
    //            }
    //            #endregion

    //            #region // SaveDB Mst_Province:
    //            {
    //                // Init:
    //                //ArrayList alColumnEffective = new ArrayList();
    //                string strFN = "";
    //                DataRow drDB = dtDB_Mst_Province.NewRow();
    //                strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode;
    //                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
    //                strFN = "ProvinceName"; drDB[strFN] = strProvinceName;
    //                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
    //                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
    //                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
    //                dtDB_Mst_Province.Rows.Add(drDB);

    //                // Save:
    //                _cf.db.SaveData(
    //                    "Mst_Province"
    //                    , dtDB_Mst_Province
    //                    //, alColumnEffective.ToArray()
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.CommitSafety(_cf.db);
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }
    //    public DataSet Mst_Province_Update(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        ////
    //        , object objProvinceCode
    //        , object objProvinceName
    //        , object objFlagActive
    //        ////
    //        , object objFt_Cols_Upd
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        string strFunctionName = "Mst_Province_Update";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_Province_Update;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				//, "objProvinceCode", objProvinceCode
    //            , "objProvinceName", objProvinceName
    //            , "objFlagActive", objFlagActive
				//////
				//, "objFt_Cols_Upd", objFt_Cols_Upd
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Convert Input:
    //            #endregion

    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );
    //            #endregion

    //            #region // Refine and Check Input:
    //            ////
    //            string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
    //            strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
    //            ////
    //            string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
    //            string strProvinceName = string.Format("{0}", objProvinceName).Trim();
    //            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
    //            ////
    //            bool bUpd_ProvinceName = strFt_Cols_Upd.Contains("Mst_Province.ProvinceName".ToUpper());
    //            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Province.FlagActive".ToUpper());

    //            ////
    //            DataTable dtDB_Mst_Province = null;
    //            {
    //                ////
    //                Mst_Province_CheckDB(
    //                     ref alParamsCoupleError // alParamsCoupleError
    //                     , strProvinceCode // objProvinceCode 
    //                     , TConst.Flag.Yes // strFlagExistToCheck
    //                     , "" // strFlagActiveListToCheck
    //                     , out dtDB_Mst_Province // dtDB_Mst_Province
    //                    );
    //                ////
    //                if (bUpd_ProvinceName && string.IsNullOrEmpty(strProvinceName))
    //                {
    //                    alParamsCoupleError.AddRange(new object[]{
    //                        "Check.strProvinceName", strProvinceName
    //                        });
    //                    throw CmUtils.CMyException.Raise(
    //                        TError.ErridnInventory.Mst_Province_Update_InvalidProvinceName
    //                        , null
    //                        , alParamsCoupleError.ToArray()
    //                        );
    //                }
    //                ////
    //            }
    //            #endregion

    //            #region // SaveDB Mst_Province:
    //            {
    //                // Init:
    //                ArrayList alColumnEffective = new ArrayList();
    //                string strFN = "";
    //                DataRow drDB = dtDB_Mst_Province.Rows[0];
    //                if (bUpd_ProvinceName) { strFN = "ProvinceName"; drDB[strFN] = strProvinceName; alColumnEffective.Add(strFN); }
    //                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
    //                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
    //                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

    //                // Save:
    //                _cf.db.SaveData(
    //                    "Mst_Province"
    //                    , dtDB_Mst_Province
    //                    , alColumnEffective.ToArray()
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.CommitSafety(_cf.db);
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }
    //    public DataSet Mst_Province_Delete(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        /////
    //        , object objProvinceCode
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        string strFunctionName = "Mst_Province_Delete";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_Province_Delete;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				//, "objProvinceCode", objProvinceCode
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );
    //            #endregion

    //            #region // Refine and Check Input:
    //            ////
    //            string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
    //            ////
    //            DataTable dtDB_Mst_Province = null;
    //            {
    //                ////
    //                Mst_Province_CheckDB(
    //                     ref alParamsCoupleError // alParamsCoupleError
    //                     , objProvinceCode // objProvinceCode
    //                     , TConst.Flag.Yes // strFlagExistToCheck
    //                     , "" // strFlagActiveListToCheck
    //                     , out dtDB_Mst_Province // dtDB_Mst_Province
    //                    );
    //                ////
    //            }
    //            #endregion

    //            #region // SaveDB Mst_Province:
    //            {
    //                // Init:
    //                dtDB_Mst_Province.Rows[0].Delete();

    //                // Save:
    //                _cf.db.SaveData(
    //                    "Mst_Province"
    //                    , dtDB_Mst_Province
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.CommitSafety(_cf.db);
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_Province_Get(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_Province objRQ_Mst_Province
    //        ////
    //        , out RT_Mst_Province objRT_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_Province.Tid;
    //        objRT_Mst_Province = new RT_Mst_Province();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_Province_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_Province_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            #endregion

    //            #region // Refine and Check Input:
    //            List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

    //            List<Mst_Province> lst_Mst_Province = new List<Mst_Province>();
    //            bool bGet_Mst_Province = (objRQ_Mst_Province.Rt_Cols_Mst_Province != null && objRQ_Mst_Province.Rt_Cols_Mst_Province.Length > 0);
    //            #endregion

    //            #region // WS_Mst_Province_Get:
    //            mdsResult = RptSv_Mst_Province_Get(
    //                objRQ_Mst_Province.Tid // strTid
    //                , objRQ_Mst_Province.GwUserCode // strGwUserCode
    //                , objRQ_Mst_Province.GwPassword // strGwPassword
    //                , objRQ_Mst_Province.WAUserCode // strUserCode
    //                , objRQ_Mst_Province.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          //// Filter:
    //                , objRQ_Mst_Province.Ft_RecordStart // strFt_RecordStart
    //                , objRQ_Mst_Province.Ft_RecordCount // strFt_RecordCount
    //                , objRQ_Mst_Province.Ft_WhereClause // strFt_WhereClause
    //                                                    //// Return:
    //                , objRQ_Mst_Province.Rt_Cols_Mst_Province // strRt_Cols_Mst_Province
    //                );
    //            #endregion

    //            #region // GetData:
    //            if (!CmUtils.CMyDataSet.HasError(mdsResult))
    //            {
    //                ////
    //                if (bGet_Mst_Province)
    //                {
    //                    ////
    //                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
    //                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
    //                    objRT_Mst_Province.MySummaryTable = lst_MySummaryTable[0];

    //                    ////
    //                    DataTable dt_Mst_Province = mdsResult.Tables["Mst_Province"].Copy();
    //                    lst_Mst_Province = TUtils.DataTableCmUtils.ToListof<Mst_Province>(dt_Mst_Province);
    //                    objRT_Mst_Province.Lst_Mst_Province = lst_Mst_Province;
    //                }
    //            }
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }
    //    public DataSet WAS_Mst_Province_Get(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_Province objRQ_Mst_Province
    //        ////
    //        , out RT_Mst_Province objRT_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_Province.Tid;
    //        objRT_Mst_Province = new RT_Mst_Province();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_Province_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Province_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            #endregion

    //            #region // Refine and Check Input:
    //            List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();

    //            List<Mst_Province> lst_Mst_Province = new List<Mst_Province>();
    //            bool bGet_Mst_Province = (objRQ_Mst_Province.Rt_Cols_Mst_Province != null && objRQ_Mst_Province.Rt_Cols_Mst_Province.Length > 0);
    //            #endregion

    //            #region // WS_Mst_Province_Get:
    //            mdsResult = Mst_Province_Get(
    //                objRQ_Mst_Province.Tid // strTid
    //                , objRQ_Mst_Province.GwUserCode // strGwUserCode
    //                , objRQ_Mst_Province.GwPassword // strGwPassword
    //                , objRQ_Mst_Province.WAUserCode // strUserCode
    //                , objRQ_Mst_Province.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          //// Filter:
    //                , objRQ_Mst_Province.Ft_RecordStart // strFt_RecordStart
    //                , objRQ_Mst_Province.Ft_RecordCount // strFt_RecordCount
    //                , objRQ_Mst_Province.Ft_WhereClause // strFt_WhereClause
    //                                                    //// Return:
    //                , objRQ_Mst_Province.Rt_Cols_Mst_Province // strRt_Cols_Mst_Province
    //                );
    //            #endregion

    //            #region // GetData:
    //            if (!CmUtils.CMyDataSet.HasError(mdsResult))
    //            {
    //                ////
    //                if (bGet_Mst_Province)
    //                {
    //                    ////
    //                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
    //                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
    //                    objRT_Mst_Province.MySummaryTable = lst_MySummaryTable[0];

    //                    ////
    //                    DataTable dt_Mst_Province = mdsResult.Tables["Mst_Province"].Copy();
    //                    lst_Mst_Province = TUtils.DataTableCmUtils.ToListof<Mst_Province>(dt_Mst_Province);
    //                    objRT_Mst_Province.Lst_Mst_Province = lst_Mst_Province;
    //                }
    //            }
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Mst_Province_Create(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_Province objRQ_Mst_Province
    //        ////
    //        , out RT_Mst_Province objRT_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_Province.Tid;
    //        objRT_Mst_Province = new RT_Mst_Province();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_Province_Create";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Province_Create;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            , "Mst_Province", TJson.JsonConvert.SerializeObject(objRQ_Mst_Province.Mst_Province)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_Province.WAUserCode
    //            //	, objRQ_Mst_Province.WAUserPassword
    //            //	);
    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_Province> lst_Mst_Province = new List<Mst_Province>();
    //            //List<Mst_ProvinceInGroup> lst_Mst_ProvinceInGroup = new List<Mst_ProvinceInGroup>();
    //            #endregion

    //            #region // Mst_Province_Create:
    //            mdsResult = Mst_Province_Create(
    //                objRQ_Mst_Province.Tid // strTid
    //                , objRQ_Mst_Province.GwUserCode // strGwUserCode
    //                , objRQ_Mst_Province.GwPassword // strGwPassword
    //                , objRQ_Mst_Province.WAUserCode // strUserCode
    //                , objRQ_Mst_Province.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_Province.Mst_Province.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_Province.Mst_Province.ProvinceName // objProvinceName
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Mst_Province_Update(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_Province objRQ_Mst_Province
    //        ////
    //        , out RT_Mst_Province objRT_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_Province.Tid;
    //        objRT_Mst_Province = new RT_Mst_Province();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_Province_Update";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Province_Update;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            , "Mst_Province", TJson.JsonConvert.SerializeObject(objRQ_Mst_Province.Mst_Province)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_Province.WAUserCode
    //            //	, objRQ_Mst_Province.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_Province> lst_Mst_Province = new List<Mst_Province>();
    //            //List<Mst_ProvinceInGroup> lst_Mst_ProvinceInGroup = new List<Mst_ProvinceInGroup>();
    //            #endregion

    //            #region // Mst_Province_Update:
    //            mdsResult = Mst_Province_Update(
    //                objRQ_Mst_Province.Tid // strTid
    //                , objRQ_Mst_Province.GwUserCode // strGwUserCode
    //                , objRQ_Mst_Province.GwPassword // strGwPassword
    //                , objRQ_Mst_Province.WAUserCode // strUserCode
    //                , objRQ_Mst_Province.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_Province.Mst_Province.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_Province.Mst_Province.ProvinceName // objProvinceName
    //                , objRQ_Mst_Province.Mst_Province.FlagActive // objFlagActive
    //                                                             ////
    //                , objRQ_Mst_Province.Ft_Cols_Upd// objFt_Cols_Upd
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Mst_Province_Delete(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_Province objRQ_Mst_Province
    //        ////
    //        , out RT_Mst_Province objRT_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_Province.Tid;
    //        objRT_Mst_Province = new RT_Mst_Province();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_Province_Delete";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Province_Delete;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////, "Mst_Province", TJson.JsonConvert.SerializeObject(objRQ_Mst_Province.Mst_Province)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_Province.WAUserCode
    //            //	, objRQ_Mst_Province.WAUserPassword
    //            //	);
    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_Province> lst_Mst_Province = new List<Mst_Province>();
    //            //List<Mst_ProvinceInGroup> lst_Mst_ProvinceInGroup = new List<Mst_ProvinceInGroup>();
    //            #endregion

    //            #region // Mst_Province_Delete:
    //            mdsResult = Mst_Province_Delete(
    //                objRQ_Mst_Province.Tid // strTid
    //                , objRQ_Mst_Province.GwUserCode // strGwUserCode
    //                , objRQ_Mst_Province.GwPassword // strGwPassword
    //                , objRQ_Mst_Province.WAUserCode // strUserCode
    //                , objRQ_Mst_Province.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_Province.Mst_Province.ProvinceCode // objProvinceCode
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_Province_Create(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_Province objRQ_Mst_Province
    //        ////
    //        , out RT_Mst_Province objRT_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_Province.Tid;
    //        objRT_Mst_Province = new RT_Mst_Province();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_Province_Create";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_Province_Create;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            , "Mst_Province", TJson.JsonConvert.SerializeObject(objRQ_Mst_Province.Mst_Province)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_Province.WAUserCode
    //            //	, objRQ_Mst_Province.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_Province> lst_Mst_Province = new List<Mst_Province>();
    //            //List<Mst_ProvinceInGroup> lst_Mst_ProvinceInGroup = new List<Mst_ProvinceInGroup>();
    //            #endregion

    //            #region // Mst_Province_Create:
    //            mdsResult = Mst_Province_Create(
    //                objRQ_Mst_Province.Tid // strTid
    //                , objRQ_Mst_Province.GwUserCode // strGwUserCode
    //                , objRQ_Mst_Province.GwPassword // strGwPassword
    //                , objRQ_Mst_Province.WAUserCode // strUserCode
    //                , objRQ_Mst_Province.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_Province.Mst_Province.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_Province.Mst_Province.ProvinceName // objProvinceName
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_Province_Update(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_Province objRQ_Mst_Province
    //        ////
    //        , out RT_Mst_Province objRT_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_Province.Tid;
    //        objRT_Mst_Province = new RT_Mst_Province();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_Province_Update";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_Province_Update;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            , "Mst_Province", TJson.JsonConvert.SerializeObject(objRQ_Mst_Province.Mst_Province)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_Province.WAUserCode
    //            //	, objRQ_Mst_Province.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_Province> lst_Mst_Province = new List<Mst_Province>();
    //            //List<Mst_ProvinceInGroup> lst_Mst_ProvinceInGroup = new List<Mst_ProvinceInGroup>();
    //            #endregion

    //            #region // Mst_Province_Update:
    //            mdsResult = Mst_Province_Update(
    //                objRQ_Mst_Province.Tid // strTid
    //                , objRQ_Mst_Province.GwUserCode // strGwUserCode
    //                , objRQ_Mst_Province.GwPassword // strGwPassword
    //                , objRQ_Mst_Province.WAUserCode // strUserCode
    //                , objRQ_Mst_Province.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_Province.Mst_Province.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_Province.Mst_Province.ProvinceName // objProvinceName
    //                , objRQ_Mst_Province.Mst_Province.FlagActive // objFlagActive
    //                                                             ////
    //                , objRQ_Mst_Province.Ft_Cols_Upd// objFt_Cols_Upd
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_Province_Delete(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_Province objRQ_Mst_Province
    //        ////
    //        , out RT_Mst_Province objRT_Mst_Province
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_Province.Tid;
    //        objRT_Mst_Province = new RT_Mst_Province();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_Province_Delete";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_Province_Delete;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////, "Mst_Province", TJson.JsonConvert.SerializeObject(objRQ_Mst_Province.Mst_Province)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_Province.WAUserCode
    //            //	, objRQ_Mst_Province.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_Province> lst_Mst_Province = new List<Mst_Province>();
    //            //List<Mst_ProvinceInGroup> lst_Mst_ProvinceInGroup = new List<Mst_ProvinceInGroup>();
    //            #endregion

    //            #region // Mst_Province_Delete:
    //            mdsResult = Mst_Province_Delete(
    //                objRQ_Mst_Province.Tid // strTid
    //                , objRQ_Mst_Province.GwUserCode // strGwUserCode
    //                , objRQ_Mst_Province.GwPassword // strGwPassword
    //                , objRQ_Mst_Province.WAUserCode // strUserCode
    //                , objRQ_Mst_Province.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_Province.Mst_Province.ProvinceCode // objProvinceCode
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }
    //    #endregion

    //    #region // Mst_District:
    //    private void Mst_District_CheckDB(
    //        ref ArrayList alParamsCoupleError
    //        , object objProvinceCode
    //        , object objDistrictCode
    //        , string strFlagExistToCheck
    //        , string strFlagActiveListToCheck
    //        , out DataTable dtDB_Mst_District
    //        )
    //    {
    //        // GetInfo:
    //        string strSqlExec = CmUtils.StringUtils.Replace(@"
				//	select top 1
				//		t.*
				//	from Mst_District t --//[mylock]
				//	where (1=1)
				//		and t.ProvinceCode = @objProvinceCode
				//		and t.DistrictCode = @objDistrictCode
				//	;
				//");
    //        dtDB_Mst_District = _cf.db.ExecQuery(
    //            strSqlExec
    //            , "@objProvinceCode", objProvinceCode
    //            , "@objDistrictCode", objDistrictCode
    //            ).Tables[0];
    //        dtDB_Mst_District.TableName = "Mst_District";

    //        // strFlagExistToCheck:
    //        if (strFlagExistToCheck.Length > 0)
    //        {
    //            if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_District.Rows.Count < 1)
    //            {
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "Check.ProvinceCode", objProvinceCode
    //                    , "Check.DistrictCode", objDistrictCode
    //                    });
    //                throw CmUtils.CMyException.Raise(
    //                    TError.ErridnInventory.Mst_District_CheckDB_DistrictNotFound
    //                    , null
    //                    , alParamsCoupleError.ToArray()
    //                    );
    //            }
    //            if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_District.Rows.Count > 0)
    //            {
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "Check.ProvinceCode", objProvinceCode
    //                    , "Check.DistrictCode", objDistrictCode
    //                    });
    //                throw CmUtils.CMyException.Raise(
    //                    TError.ErridnInventory.Mst_District_CheckDB_DistrictExist
    //                    , null
    //                    , alParamsCoupleError.ToArray()
    //                    );
    //            }
    //        }

    //        // strFlagActiveListToCheck:
    //        if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_District.Rows[0]["FlagActive"])))
    //        {
    //            alParamsCoupleError.AddRange(new object[]{
    //                "Check.ProvinceCode", objProvinceCode
    //                , "Check.DistrictCode", objDistrictCode
    //                , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
    //                , "DB.FlagActive", dtDB_Mst_District.Rows[0]["FlagActive"]
    //                });
    //            throw CmUtils.CMyException.Raise(
    //                TError.ErridnInventory.Mst_District_CheckDB_FlagActiveNotMatched
    //                , null
    //                , alParamsCoupleError.ToArray()
    //                );
    //        }
    //    }

    //    public DataSet Mst_District_Get(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        //// Filter:
    //        , string strFt_RecordStart
    //        , string strFt_RecordCount
    //        , string strFt_WhereClause
    //        //// Return:
    //        , string strRt_Cols_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        bool bNeedTransaction = true;
    //        string strFunctionName = "Mst_District_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_District_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////// Filter
				//, "strFt_RecordStart", strFt_RecordStart
    //            , "strFt_RecordCount", strFt_RecordCount
    //            , "strFt_WhereClause", strFt_WhereClause
				////// Return
				//, "strRt_Cols_Mst_District", strRt_Cols_Mst_District
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            if (bNeedTransaction) _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );

    //            //// Check Access/Deny:
    //            //Sys_Access_CheckDenyV30(
    //            //    ref alParamsCoupleError
    //            //    , strWAUserCode
    //            //    , strFunctionName
    //            //    );
    //            #endregion

    //            #region // Check:
    //            //// Refine:
    //            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
    //            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
    //            bool bGet_Mst_District = (strRt_Cols_Mst_District != null && strRt_Cols_Mst_District.Length > 0);

    //            //// drAbilityOfUser:
    //            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

    //            #endregion

    //            #region // Build Sql:
    //            ////
    //            ArrayList alParamsCoupleSql = new ArrayList();
    //            alParamsCoupleSql.AddRange(new object[] {
    //                "@nFilterRecordStart", nFilterRecordStart
    //                , "@nFilterRecordEnd", nFilterRecordEnd
    //                });
    //            ////
    //            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
    //            ////
    //            string strSqlGetData = CmUtils.StringUtils.Replace(@"
				//		---- #tbl_Mst_District_Filter_Draft:
				//		select distinct
				//			identity(bigint, 0, 1) MyIdxSeq
				//			, md.DistrictCode
				//		into #tbl_Mst_District_Filter_Draft
				//		from Mst_District md --//[mylock]
				//		where (1=1)
				//			zzB_Where_strFilter_zzE
				//		order by md.DistrictCode asc
				//		;

				//		---- Summary:
				//		select Count(0) MyCount from #tbl_Mst_District_Filter_Draft t --//[mylock]
				//		;

				//		---- #tbl_Mst_District_Filter:
				//		select
				//			t.*
				//		into #tbl_Mst_District_Filter
				//		from #tbl_Mst_District_Filter_Draft t --//[mylock]
				//		where
				//			(t.MyIdxSeq >= @nFilterRecordStart)
				//			and (t.MyIdxSeq <= @nFilterRecordEnd)
				//		;

				//		-------- Mst_District --------:
				//		zzB_Select_Mst_District_zzE
				//		----------------------------------------

				//		---- Clear for debug:
				//		--drop table #tbl_Mst_District_Filter_Draft;
				//		--drop table #tbl_Mst_District_Filter;
				//	"
    //                );
    //            ////
    //            string zzB_Select_Mst_District_zzE = "-- Nothing.";
    //            if (bGet_Mst_District)
    //            {
    //                #region // bGet_Mst_District:
    //                zzB_Select_Mst_District_zzE = CmUtils.StringUtils.Replace(@"
				//			---- Mst_District:
				//			select
				//				t.MyIdxSeq
				//				, md.*
				//			from #tbl_Mst_District_Filter t --//[mylock]
				//				inner join Mst_District md --//[mylock]
				//					on t.DistrictCode = md.DistrictCode
				//			order by t.MyIdxSeq asc
				//			;
				//		"
    //                    );
    //                #endregion
    //            }
    //            ////
    //            string zzB_Where_strFilter_zzE = "";
    //            {
    //                Hashtable htSpCols = new Hashtable();
    //                {
    //                    #region // htSpCols:
    //                    ////
    //                    TUtils.CUtils.MyBuildHTSupportedColumns(
    //                        _cf.db // db
    //                        , ref htSpCols // htSupportedColumns
    //                        , "Mst_District" // strTableNameDB
    //                        , "Mst_District." // strPrefixStd
    //                        , "md." // strPrefixAlias
    //                        );
    //                    ////
    //                    #endregion
    //                }
    //                zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
    //                    htSpCols // htSpCols
    //                    , strFt_WhereClause // strClause
    //                    , "@p_" // strParamPrefix
    //                    , ref alParamsCoupleSql // alParamsCoupleSql
    //                    );
    //                zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
    //                alParamsCoupleError.AddRange(new object[]{
    //                    "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //                    });
    //            }
    //            ////
    //            strSqlGetData = CmUtils.StringUtils.Replace(
    //                strSqlGetData
    //                , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
    //                , "zzB_Select_Mst_District_zzE", zzB_Select_Mst_District_zzE
    //                );
    //            #endregion

    //            #region // Get Data:
    //            DataSet dsGetData = _cf.db.ExecQuery(
    //                strSqlGetData
    //                , alParamsCoupleSql.ToArray()
    //                );
    //            int nIdxTable = 0;
    //            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
    //            if (bGet_Mst_District)
    //            {
    //                dsGetData.Tables[nIdxTable++].TableName = "Mst_District";
    //            }
    //            CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }
    //    public DataSet Mst_District_Create(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        ////
    //        , object objDistrictCode
    //        , object objProvinceCode
    //        , object objDistrictName
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        string strFunctionName = "Mst_District_Create";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_District_Create;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            ////
				//, "objDistrictCode", objDistrictCode
    //            , "objProvinceCode", objProvinceCode
    //            , "objDistrictName", objDistrictName
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Convert Input:
    //            #endregion

    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );
    //            #endregion

    //            #region // Refine and Check Input:
    //            ////
    //            ////
    //            string strDistrictCode = TUtils.CUtils.StdParam(objDistrictCode);
    //            string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
    //            string strDistrictName = string.Format("{0}", objDistrictName).Trim();

    //            // drAbilityOfUser:
    //            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
    //            ////
    //            DataTable dtDB_Mst_District = null;
    //            {
    //                ////
    //                if (strDistrictCode == null || strDistrictCode.Length < 1)
    //                {
    //                    alParamsCoupleError.AddRange(new object[]{
    //                        "Check.strDistrictCode", strDistrictCode
    //                        });
    //                    throw CmUtils.CMyException.Raise(
    //                        TError.ErridnInventory.Mst_District_Create_InvalidDistrictCode
    //                        , null
    //                        , alParamsCoupleError.ToArray()
    //                        );
    //                }
    //                Mst_District_CheckDB(
    //                    ref alParamsCoupleError // alParamsCoupleError
    //                    , strProvinceCode // objProvinceCode
    //                    , strDistrictCode // objDistrictCode
    //                    , TConst.Flag.No // strFlagExistToCheck
    //                    , "" // strFlagActiveListToCheck
    //                    , out dtDB_Mst_District // dtDB_Mst_District
    //                    );
    //                ////
    //                DataTable dtDB_Mst_Province = null;

    //                Mst_Province_CheckDB(
    //                    ref alParamsCoupleError // alParamsCoupleError
    //                    , strProvinceCode // objProvinceCode
    //                    , TConst.Flag.Yes // strFlagExistToCheck
    //                    , TConst.Flag.Active // strFlagActiveListToCheck
    //                    , out dtDB_Mst_Province // dtDB_Mst_Province
    //                    );
    //                ////
    //                if (strDistrictName.Length < 1)
    //                {
    //                    alParamsCoupleError.AddRange(new object[]{
    //                        "Check.strDistrictName", strDistrictName
    //                        });
    //                    throw CmUtils.CMyException.Raise(
    //                        TError.ErridnInventory.Mst_District_Create_InvalidDistrictName
    //                        , null
    //                        , alParamsCoupleError.ToArray()
    //                        );
    //                }
    //                ////
    //            }
    //            #endregion

    //            #region // SaveDB Mst_District:
    //            {
    //                // Init:
    //                //ArrayList alColumnEffective = new ArrayList();
    //                string strFN = "";
    //                DataRow drDB = dtDB_Mst_District.NewRow();
    //                strFN = "DistrictCode"; drDB[strFN] = strDistrictCode;
    //                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
    //                strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode;
    //                strFN = "DistrictName"; drDB[strFN] = strDistrictName;
    //                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
    //                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
    //                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
    //                dtDB_Mst_District.Rows.Add(drDB);

    //                // Save:
    //                _cf.db.SaveData(
    //                    "Mst_District"
    //                    , dtDB_Mst_District
    //                    //, alColumnEffective.ToArray()
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.CommitSafety(_cf.db);
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }
    //    public DataSet Mst_District_Update(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        ////
    //        , object objDistrictCode
    //        , object objProvinceCode
    //        , object objDistrictName
    //        , object objFlagActive
    //        ////
    //        , object objFt_Cols_Upd
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        string strFunctionName = "Mst_District_Update";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_District_Update;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				//, "objDistrictCode", objDistrictCode
    //            , "objProvinceCode", objProvinceCode
    //            , "objDistrictName", objDistrictName
    //            , "objFlagActive", objFlagActive
				//////
				//, "objFt_Cols_Upd", objFt_Cols_Upd
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Convert Input:
    //            #endregion

    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );

    //            #endregion

    //            #region // Refine and Check Input:
    //            ////
    //            string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
    //            strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
    //            ////
    //            string strDistrictCode = TUtils.CUtils.StdParam(objDistrictCode);
    //            string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
    //            string strDistrictName = string.Format("{0}", objDistrictName).Trim();
    //            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
    //            ////
    //            bool bUpd_DistrictName = strFt_Cols_Upd.Contains("Mst_District.DistrictName".ToUpper());
    //            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_District.FlagActive".ToUpper());

    //            ////
    //            DataTable dtDB_Mst_District = null;
    //            {
    //                ////
    //                Mst_District_CheckDB(
    //                     ref alParamsCoupleError // alParamsCoupleError
    //                     , strProvinceCode // objProvinceCode
    //                     , strDistrictCode // objDistrictCode 
    //                     , TConst.Flag.Yes // strFlagExistToCheck
    //                     , "" // strFlagActiveListToCheck
    //                     , out dtDB_Mst_District // dtDB_Mst_District
    //                    );
    //                ////
    //                DataTable dtDB_Mst_Province = null;

    //                Mst_Province_CheckDB(
    //                    ref alParamsCoupleError // alParamsCoupleError
    //                    , strProvinceCode // objProvinceCode
    //                    , TConst.Flag.Yes // strFlagExistToCheck
    //                    , TConst.Flag.Active // strFlagActiveListToCheck
    //                    , out dtDB_Mst_Province // dtDB_Mst_Province
    //                    );
    //                ////
    //                if (bUpd_DistrictName && string.IsNullOrEmpty(strDistrictName))
    //                {
    //                    alParamsCoupleError.AddRange(new object[]{
    //                        "Check.strDistrictName", strDistrictName
    //                        });
    //                    throw CmUtils.CMyException.Raise(
    //                        TError.ErridnInventory.Mst_District_Update_InvalidDistrictName
    //                        , null
    //                        , alParamsCoupleError.ToArray()
    //                        );
    //                }
    //                ////
    //            }
    //            #endregion

    //            #region // SaveDB Mst_District:
    //            {
    //                // Init:
    //                ArrayList alColumnEffective = new ArrayList();
    //                string strFN = "";
    //                DataRow drDB = dtDB_Mst_District.Rows[0];
    //                if (bUpd_DistrictName) { strFN = "DistrictName"; drDB[strFN] = strDistrictName; alColumnEffective.Add(strFN); }
    //                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
    //                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
    //                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

    //                // Save:
    //                _cf.db.SaveData(
    //                    "Mst_District"
    //                    , dtDB_Mst_District
    //                    , alColumnEffective.ToArray()
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.CommitSafety(_cf.db);
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }
    //    public DataSet Mst_District_Delete(
    //        string strTid
    //        , string strGwUserCode
    //        , string strGwPassword
    //        , string strWAUserCode
    //        , string strWAUserPassword
    //        , ref ArrayList alParamsCoupleError
    //        /////
    //        , object objProvinceCode
    //        , object objDistrictCode
    //        )
    //    {
    //        #region // Temp:
    //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        //int nTidSeq = 0;
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        string strFunctionName = "Mst_District_Delete";
    //        string strErrorCodeDefault = TError.ErridnInventory.Mst_District_Delete;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
    //            , "objProvinceCode", objProvinceCode
    //            , "objDistrictCode", objDistrictCode
    //            });
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
    //            _cf.db.BeginTransaction();

    //            // Write RequestLog:
    //            _cf.ProcessBizReq_OutSide(
    //                strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                , alParamsCoupleError // alParamsCoupleError
    //                );
    //            #endregion

    //            #region // Refine and Check Input:
    //            ////
    //            string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
    //            string strDistrictCode = TUtils.CUtils.StdParam(objDistrictCode);
    //            ////
    //            DataTable dtDB_Mst_District = null;
    //            {
    //                ////
    //                Mst_District_CheckDB(
    //                     ref alParamsCoupleError // alParamsCoupleError
    //                     , strProvinceCode // objProvinceCode
    //                     , objDistrictCode // objDistrictCode
    //                     , TConst.Flag.Yes // strFlagExistToCheck
    //                     , "" // strFlagActiveListToCheck
    //                     , out dtDB_Mst_District // dtDB_Mst_District
    //                    );
    //                ////
    //            }
    //            #endregion

    //            #region // SaveDB Mst_District:
    //            {
    //                // Init:
    //                dtDB_Mst_District.Rows[0].Delete();

    //                // Save:
    //                _cf.db.SaveData(
    //                    "Mst_District"
    //                    , dtDB_Mst_District
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            TDALUtils.DBUtils.CommitSafety(_cf.db);
    //            mdsFinal.AcceptChanges();
    //            return mdsFinal;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Rollback:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsFinal
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Rollback and Release resources:
    //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
    //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

    //            // Write ReturnLog:
    //            _cf.ProcessBizReturn_OutSide(
    //                ref mdsFinal // mdsFinal
    //                , strTid // strTid
    //                , strGwUserCode // strGwUserCode
    //                , strGwPassword // strGwPassword
    //                , strWAUserCode // objUserCode
    //                , strFunctionName // strFunctionName
    //                );
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_District_Get(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_District objRQ_Mst_District
    //        ////
    //        , out RT_Mst_District objRT_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_District.Tid;
    //        objRT_Mst_District = new RT_Mst_District();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_District.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_District_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_District_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_District.WAUserCode
    //            //	, objRQ_Mst_District.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
    //            List<Mst_District> lst_Mst_District = new List<Mst_District>();
    //            bool bGet_Mst_District = (objRQ_Mst_District.Rt_Cols_Mst_District != null && objRQ_Mst_District.Rt_Cols_Mst_District.Length > 0);
    //            #endregion

    //            #region // WS_Mst_District_Get:
    //            mdsResult = Mst_District_Get(
    //                objRQ_Mst_District.Tid // strTid
    //                , objRQ_Mst_District.GwUserCode // strGwUserCode
    //                , objRQ_Mst_District.GwPassword // strGwPassword
    //                , objRQ_Mst_District.WAUserCode // strUserCode
    //                , objRQ_Mst_District.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          //// Filter:
    //                , objRQ_Mst_District.Ft_RecordStart // strFt_RecordStart
    //                , objRQ_Mst_District.Ft_RecordCount // strFt_RecordCount
    //                , objRQ_Mst_District.Ft_WhereClause // strFt_WhereClause
    //                                                    //// Return:
    //                , objRQ_Mst_District.Rt_Cols_Mst_District // strRt_Cols_Mst_District
    //                );
    //            #endregion

    //            #region // GetData:
    //            if (!CmUtils.CMyDataSet.HasError(mdsResult))
    //            {
    //                ////
    //                DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
    //                lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
    //                objRT_Mst_District.MySummaryTable = lst_MySummaryTable[0];
    //                ////
    //                if (bGet_Mst_District)
    //                {
    //                    ////
    //                    DataTable dt_Mst_District = mdsResult.Tables["Mst_District"].Copy();
    //                    lst_Mst_District = TUtils.DataTableCmUtils.ToListof<Mst_District>(dt_Mst_District);
    //                    objRT_Mst_District.Lst_Mst_District = lst_Mst_District;
    //                }
    //            }
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Mst_District_Get(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_District objRQ_Mst_District
    //        ////
    //        , out RT_Mst_District objRT_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_District.Tid;
    //        objRT_Mst_District = new RT_Mst_District();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_District.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_District_Get";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_District_Get;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_District.WAUserCode
    //            //	, objRQ_Mst_District.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
    //            List<Mst_District> lst_Mst_District = new List<Mst_District>();
    //            bool bGet_Mst_District = (objRQ_Mst_District.Rt_Cols_Mst_District != null && objRQ_Mst_District.Rt_Cols_Mst_District.Length > 0);
    //            #endregion

    //            #region // WS_Mst_District_Get:
    //            mdsResult = Mst_District_Get(
    //                objRQ_Mst_District.Tid // strTid
    //                , objRQ_Mst_District.GwUserCode // strGwUserCode
    //                , objRQ_Mst_District.GwPassword // strGwPassword
    //                , objRQ_Mst_District.WAUserCode // strUserCode
    //                , objRQ_Mst_District.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          //// Filter:
    //                , objRQ_Mst_District.Ft_RecordStart // strFt_RecordStart
    //                , objRQ_Mst_District.Ft_RecordCount // strFt_RecordCount
    //                , objRQ_Mst_District.Ft_WhereClause // strFt_WhereClause
    //                                                    //// Return:
    //                , objRQ_Mst_District.Rt_Cols_Mst_District // strRt_Cols_Mst_District
    //                );
    //            #endregion

    //            #region // GetData:
    //            if (!CmUtils.CMyDataSet.HasError(mdsResult))
    //            {
    //                ////
    //                DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
    //                lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
    //                objRT_Mst_District.MySummaryTable = lst_MySummaryTable[0];
    //                ////
    //                if (bGet_Mst_District)
    //                {
    //                    ////
    //                    DataTable dt_Mst_District = mdsResult.Tables["Mst_District"].Copy();
    //                    lst_Mst_District = TUtils.DataTableCmUtils.ToListof<Mst_District>(dt_Mst_District);
    //                    objRT_Mst_District.Lst_Mst_District = lst_Mst_District;
    //                }
    //            }
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Mst_District_Create(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_District objRQ_Mst_District
    //        ////
    //        , out RT_Mst_District objRT_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_District.Tid;
    //        objRT_Mst_District = new RT_Mst_District();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_District.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_District_Create";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_District_Create;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            , "Mst_District", TJson.JsonConvert.SerializeObject(objRQ_Mst_District.Mst_District)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_District.WAUserCode
    //            //	, objRQ_Mst_District.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_District> lst_Mst_District = new List<Mst_District>();
    //            //List<Mst_DistrictInGroup> lst_Mst_DistrictInGroup = new List<Mst_DistrictInGroup>();
    //            #endregion

    //            #region // Mst_District_Create:
    //            mdsResult = Mst_District_Create(
    //                objRQ_Mst_District.Tid // strTid
    //                , objRQ_Mst_District.GwUserCode // strGwUserCode
    //                , objRQ_Mst_District.GwPassword // strGwPassword
    //                , objRQ_Mst_District.WAUserCode // strUserCode
    //                , objRQ_Mst_District.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_District.Mst_District.DistrictCode // objDistrictCode
    //                , objRQ_Mst_District.Mst_District.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_District.Mst_District.DistrictName // objDistrictName
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Mst_District_Update(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_District objRQ_Mst_District
    //        ////
    //        , out RT_Mst_District objRT_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_District.Tid;
    //        objRT_Mst_District = new RT_Mst_District();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_District.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_District_Update";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_District_Update;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            , "Mst_District", TJson.JsonConvert.SerializeObject(objRQ_Mst_District.Mst_District)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_District.WAUserCode
    //            //	, objRQ_Mst_District.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_District> lst_Mst_District = new List<Mst_District>();
    //            //List<Mst_DistrictInGroup> lst_Mst_DistrictInGroup = new List<Mst_DistrictInGroup>();
    //            #endregion

    //            #region // Mst_District_Update:
    //            mdsResult = Mst_District_Update(
    //                objRQ_Mst_District.Tid // strTid
    //                , objRQ_Mst_District.GwUserCode // strGwUserCode
    //                , objRQ_Mst_District.GwPassword // strGwPassword
    //                , objRQ_Mst_District.WAUserCode // strUserCode
    //                , objRQ_Mst_District.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_District.Mst_District.DistrictCode // objDistrictCode
    //                , objRQ_Mst_District.Mst_District.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_District.Mst_District.DistrictName // objDistrictName
    //                , objRQ_Mst_District.Mst_District.FlagActive // objFlagActive
    //                                                             ////
    //                , objRQ_Mst_District.Ft_Cols_Upd// objFt_Cols_Upd
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_Mst_District_Delete(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_District objRQ_Mst_District
    //        ////
    //        , out RT_Mst_District objRT_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_District.Tid;
    //        objRT_Mst_District = new RT_Mst_District();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_District.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_Mst_District_Delete";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_District_Delete;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////, "Mst_District", TJson.JsonConvert.SerializeObject(objRQ_Mst_District.Mst_District)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_District.WAUserCode
    //            //	, objRQ_Mst_District.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_District> lst_Mst_District = new List<Mst_District>();
    //            //List<Mst_DistrictInGroup> lst_Mst_DistrictInGroup = new List<Mst_DistrictInGroup>();
    //            #endregion

    //            #region // Mst_District_Delete:
    //            mdsResult = Mst_District_Delete(
    //                objRQ_Mst_District.Tid // strTid
    //                , objRQ_Mst_District.GwUserCode // strGwUserCode
    //                , objRQ_Mst_District.GwPassword // strGwPassword
    //                , objRQ_Mst_District.WAUserCode // strUserCode
    //                , objRQ_Mst_District.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_District.Mst_District.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_District.Mst_District.DistrictCode // objDistrictCode
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_District_Create(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_District objRQ_Mst_District
    //        ////
    //        , out RT_Mst_District objRT_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_District.Tid;
    //        objRT_Mst_District = new RT_Mst_District();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_District.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_District_Create";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_District_Create;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            , "Mst_District", TJson.JsonConvert.SerializeObject(objRQ_Mst_District.Mst_District)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_District.WAUserCode
    //            //	, objRQ_Mst_District.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_District> lst_Mst_District = new List<Mst_District>();
    //            //List<Mst_DistrictInGroup> lst_Mst_DistrictInGroup = new List<Mst_DistrictInGroup>();
    //            #endregion

    //            #region // Mst_District_Create:
    //            mdsResult = Mst_District_Create(
    //                objRQ_Mst_District.Tid // strTid
    //                , objRQ_Mst_District.GwUserCode // strGwUserCode
    //                , objRQ_Mst_District.GwPassword // strGwPassword
    //                , objRQ_Mst_District.WAUserCode // strUserCode
    //                , objRQ_Mst_District.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_District.Mst_District.DistrictCode // objDistrictCode
    //                , objRQ_Mst_District.Mst_District.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_District.Mst_District.DistrictName // objDistrictName
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_District_Update(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_District objRQ_Mst_District
    //        ////
    //        , out RT_Mst_District objRT_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_District.Tid;
    //        objRT_Mst_District = new RT_Mst_District();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_District.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_District_Update";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_District_Update;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
    //            , "Mst_District", TJson.JsonConvert.SerializeObject(objRQ_Mst_District.Mst_District)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_District.WAUserCode
    //            //	, objRQ_Mst_District.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_District> lst_Mst_District = new List<Mst_District>();
    //            //List<Mst_DistrictInGroup> lst_Mst_DistrictInGroup = new List<Mst_DistrictInGroup>();
    //            #endregion

    //            #region // Mst_District_Update:
    //            mdsResult = Mst_District_Update(
    //                objRQ_Mst_District.Tid // strTid
    //                , objRQ_Mst_District.GwUserCode // strGwUserCode
    //                , objRQ_Mst_District.GwPassword // strGwPassword
    //                , objRQ_Mst_District.WAUserCode // strUserCode
    //                , objRQ_Mst_District.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_District.Mst_District.DistrictCode // objDistrictCode
    //                , objRQ_Mst_District.Mst_District.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_District.Mst_District.DistrictName // objDistrictName
    //                , objRQ_Mst_District.Mst_District.FlagActive // objFlagActive
    //                                                             ////
    //                , objRQ_Mst_District.Ft_Cols_Upd// objFt_Cols_Upd
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }

    //    public DataSet WAS_RptSv_Mst_District_Delete(
    //        ref ArrayList alParamsCoupleError
    //        , RQ_Mst_District objRQ_Mst_District
    //        ////
    //        , out RT_Mst_District objRT_Mst_District
    //        )
    //    {
    //        #region // Temp:
    //        string strTid = objRQ_Mst_District.Tid;
    //        objRT_Mst_District = new RT_Mst_District();
    //        DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
    //        DateTime dtimeSys = DateTime.UtcNow;
    //        //DataSet mdsExec = null;
    //        //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_District.Tid);
    //        //int nTidSeq = 0;
    //        //bool bNeedTransaction = true;
    //        string strFunctionName = "WAS_RptSv_Mst_District_Delete";
    //        string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_District_Delete;
    //        alParamsCoupleError.AddRange(new object[]{
    //            "strFunctionName", strFunctionName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////, "Mst_District", TJson.JsonConvert.SerializeObject(objRQ_Mst_District.Mst_District)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // Init:
    //            // Sys_User_CheckAuthentication:
    //            //RptSv_Sys_User_CheckAuthentication(
    //            //	ref alParamsCoupleError
    //            //	, objRQ_Mst_District.WAUserCode
    //            //	, objRQ_Mst_District.WAUserPassword
    //            //	);

    //            #endregion

    //            #region // Refine and Check Input:
    //            //List<Mst_District> lst_Mst_District = new List<Mst_District>();
    //            //List<Mst_DistrictInGroup> lst_Mst_DistrictInGroup = new List<Mst_DistrictInGroup>();
    //            #endregion

    //            #region // Mst_District_Delete:
    //            mdsResult = Mst_District_Delete(
    //                objRQ_Mst_District.Tid // strTid
    //                , objRQ_Mst_District.GwUserCode // strGwUserCode
    //                , objRQ_Mst_District.GwPassword // strGwPassword
    //                , objRQ_Mst_District.WAUserCode // strUserCode
    //                , objRQ_Mst_District.WAUserPassword // strUserPassword
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                                          ////
    //                , objRQ_Mst_District.Mst_District.ProvinceCode // objProvinceCode
    //                , objRQ_Mst_District.Mst_District.DistrictCode // objDistrictCode
    //                );
    //            #endregion

    //            // Return Good:
    //            return mdsResult;
    //        }
    //        catch (Exception exc)
    //        {
    //            #region // Catch of try:
    //            // Return Bad:
    //            return TUtils.CProcessExc.Process(
    //                ref mdsResult
    //                , exc
    //                , strErrorCodeDefault
    //                , alParamsCoupleError.ToArray()
    //                );
    //            #endregion
    //        }
    //        finally
    //        {
    //            #region // Finally of try:
    //            // Write ReturnLog:
    //            //_cf.ProcessBizReturn(
    //            //	ref mdsResult // mdsFinal
    //            //	, strTid // strTid
    //            //	, strFunctionName // strFunctionName
    //            //	);
    //            #endregion
    //        }
    //    }
    //    #endregion 
        #endregion

        #region // Mst_Ward:
        private void Mst_Ward_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objWardCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Ward
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Ward t --//[mylock]
					where (1=1)
						and t.WardCode = @objWardCode
					;
				");
			dtDB_Mst_Ward = _cf.db.ExecQuery(
				strSqlExec
				, "@objWardCode", objWardCode
				).Tables[0];
			dtDB_Mst_Ward.TableName = "Mst_Ward";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Ward.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.WardCode", objWardCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Ward_CheckDB_WardNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Ward.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.WardCode", objWardCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Ward_CheckDB_WardExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Ward.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.WardCode", objWardCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Ward.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Ward_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		public DataSet WAS_Mst_Ward_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Ward objRQ_Mst_Ward
			////
			, out RT_Mst_Ward objRT_Mst_Ward
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Ward.Tid;
			objRT_Mst_Ward = new RT_Mst_Ward();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Ward.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Ward_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Ward_Get;
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
				List<Mst_Ward> lst_Mst_Ward = new List<Mst_Ward>();
				#endregion

				#region // WS_Mst_Ward_Get:
				mdsResult = Mst_Ward_Get(
					objRQ_Mst_Ward.Tid // strTid
					, objRQ_Mst_Ward.GwUserCode // strGwUserCode
					, objRQ_Mst_Ward.GwPassword // strGwPassword
					, objRQ_Mst_Ward.WAUserCode // strUserCode
					, objRQ_Mst_Ward.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Ward.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Ward.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Ward.Ft_WhereClause // strFt_WhereClause
													//// Return:
					, objRQ_Mst_Ward.Rt_Cols_Mst_Ward // strRt_Cols_Mst_Ward
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_Mst_Ward = mdsResult.Tables["Mst_Ward"].Copy();
					lst_Mst_Ward = TUtils.DataTableCmUtils.ToListof<Mst_Ward>(dt_Mst_Ward);
					objRT_Mst_Ward.Lst_Mst_Ward = lst_Mst_Ward;
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
		public DataSet Mst_Ward_Get(
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
			, string strRt_Cols_Mst_Ward
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Ward_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Ward_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_Ward", strRt_Cols_Mst_Ward
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

				#region // Mst_Ward_GetX:
				DataSet dsGetData = null;
				{
					Mst_Ward_GetX(
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
						, strRt_Cols_Mst_Ward // strRt_Cols_Mst_Ward
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

		public DataSet WAS_Mst_Ward_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Ward objRQ_Mst_Ward
			////
			, out RT_Mst_Ward objRT_Mst_Ward
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Ward.Tid;
			objRT_Mst_Ward = new RT_Mst_Ward();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Ward.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Ward_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Ward_Create;
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
				List<Mst_Ward> lst_Mst_Ward = new List<Mst_Ward>();
				#endregion

				#region // WS_Mst_Ward_Get:
				mdsResult = Mst_Ward_Create(
					objRQ_Mst_Ward.Tid // strTid
					, objRQ_Mst_Ward.GwUserCode // strGwUserCode
					, objRQ_Mst_Ward.GwPassword // strGwPassword
					, objRQ_Mst_Ward.WAUserCode // strUserCode
					, objRQ_Mst_Ward.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Ward.Mst_Ward.WardCode // objWardCode
					, objRQ_Mst_Ward.Mst_Ward.NetworkID // objNetworkID
					, objRQ_Mst_Ward.Mst_Ward.ProvinceCode // objProvinceCode
					, objRQ_Mst_Ward.Mst_Ward.DistrictCode // objDistrictCode
					, objRQ_Mst_Ward.Mst_Ward.WardName // objWardName
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
		public DataSet Mst_Ward_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objWardCode
			, object objNetworkID
			, object objProvinceCode
			, object objDistrictCode
			, object objWardName
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Ward_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Ward_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objWardCode", objWardCode
					, "objNetworkID", objNetworkID
					, "objProvinceCode", objProvinceCode
					, "objDistrictCode", objDistrictCode
					, "objWardName", objWardName
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

				#region // Mst_Ward_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_Ward_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objWardCode // objWardCode
						, objNetworkID // objNetworkID
						, objProvinceCode // objProvinceCode
						, objDistrictCode // objDistrictCode
						, objWardName // objWardName
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
		public DataSet WAS_Mst_Ward_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Ward objRQ_Mst_Ward
			////
			, out RT_Mst_Ward objRT_Mst_Ward
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Ward.Tid;
			objRT_Mst_Ward = new RT_Mst_Ward();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Ward.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Ward_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Ward_Update;
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
				List<Mst_Ward> lst_Mst_Ward = new List<Mst_Ward>();
				#endregion

				#region // Mst_Ward_Update:
				mdsResult = Mst_Ward_Update(
					objRQ_Mst_Ward.Tid // strTid
					, objRQ_Mst_Ward.GwUserCode // strGwUserCode
					, objRQ_Mst_Ward.GwPassword // strGwPassword
					, objRQ_Mst_Ward.WAUserCode // strUserCode
					, objRQ_Mst_Ward.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Ward.Mst_Ward.WardCode // objWardCode
					, objRQ_Mst_Ward.Mst_Ward.NetworkID // objNetworkID
					, objRQ_Mst_Ward.Mst_Ward.ProvinceCode // objProvinceCode
					, objRQ_Mst_Ward.Mst_Ward.DistrictCode // objDistrictCode
					, objRQ_Mst_Ward.Mst_Ward.WardName // objWardName
					, objRQ_Mst_Ward.Mst_Ward.FlagActive // objFlagActive
													  ////
					, objRQ_Mst_Ward.Ft_Cols_Upd // objFt_Cols_Upd
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
		public DataSet Mst_Ward_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
		   ////
		   , object objWardCode
			, object objNetworkID
			, object objProvinceCode
			, object objDistrictCode
			, object objWardName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Ward_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Ward_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objWardCode", objWardCode
					, "objNetworkID", objNetworkID
					, "objProvinceCode", objProvinceCode
					, "objDistrictCode", objDistrictCode
					, "objWardName", objWardName
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
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Mst_Ward_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_Ward_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objWardCode // objWardCode
						, objNetworkID // objNetworkID
						, objProvinceCode // objProvinceCode
						, objDistrictCode // objDistrictCode
						, objWardName // objWardName
						, objFlagActive // objFlagActive
									 ////
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
		public DataSet WAS_Mst_Ward_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Ward objRQ_Mst_Ward
			////
			, out RT_Mst_Ward objRT_Mst_Ward
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Ward.Tid;
			objRT_Mst_Ward = new RT_Mst_Ward();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Ward.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Ward_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Ward_Delete;
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
				List<Mst_Ward> lst_Mst_Ward = new List<Mst_Ward>();
				#endregion

				#region // Mst_Ward_Delete:
				mdsResult = Mst_Ward_Delete(
					objRQ_Mst_Ward.Tid // strTid
					, objRQ_Mst_Ward.GwUserCode // strGwUserCode
					, objRQ_Mst_Ward.GwPassword // strGwPassword
					, objRQ_Mst_Ward.WAUserCode // strUserCode
					, objRQ_Mst_Ward.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Ward.Mst_Ward.WardCode // objWardCode
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
		public DataSet Mst_Ward_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objWardCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Ward_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Ward_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objWardCode", objWardCode
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

				#region // Mst_Ward_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_Ward_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objWardCode // objWardCode
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
		private void Mst_Ward_GetX(
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
			, string strRt_Cols_Mst_Ward
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Ward_GetX";
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
			bool bGet_Mst_Ward = (strRt_Cols_Mst_Ward != null && strRt_Cols_Mst_Ward.Length > 0);

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
			//myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Mst_Ward_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mo.WardCode
					into #tbl_Mst_Ward_Filter_Draft
					from Mst_Ward mo --//[mylock]
					where (1=1)
						zzB_Where_strFilter_zzE
					order by mo.WardCode asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_Ward_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_Ward_Filter:
					select
						t.*
					into #tbl_Mst_Ward_Filter
					from #tbl_Mst_Ward_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_Ward -----:
					zzB_Select_Mst_Ward_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_Ward_Filter_Draft;
					--drop table #tbl_Mst_Ward_Filter;
					"
				);
			////
			string zzB_Select_Mst_Ward_zzE = "-- Nothing.";
			if (bGet_Mst_Ward)
			{
				#region // bGet_Mst_Ward:
				zzB_Select_Mst_Ward_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_Ward:
					select
						t.MyIdxSeq
						, mo.*
					from #tbl_Mst_Ward_Filter t --//[mylock]
						inner join Mst_Ward mo --//[mylock]
							on t.WardCode = mo.WardCode
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
						, "Mst_Ward" // strTableNameDB
						, "Mst_Ward." // strPrefixStd
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
				, "zzB_Select_Mst_Ward_zzE", zzB_Select_Mst_Ward_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Ward)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Ward";
			}
			#endregion
		}
		private void Mst_Ward_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objWardCode
			, object objNetworkID
			, object objProvinceCode
			, object objDistrictCode
			, object objWardName
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Ward_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objWardCode", objWardCode
				, "objNetworkID", objNetworkID
				, "objProvinceCode", objProvinceCode
				, "objDistrictCode", objDistrictCode
				, "objWardName", objWardName
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strWardCode = TUtils.CUtils.StdParam(objWardCode);
			string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
			string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
			string strDistrictCode = TUtils.CUtils.StdParam(objDistrictCode);
			string strWardName = TUtils.CUtils.StdStr(objWardName);

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_Ward = null;
			{

				////
				if (strWardCode == null || strWardCode.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strWardCode", strWardCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Ward_Create_InvalidWardCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				Mst_Ward_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strWardCode // objWardCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Ward // dtDB_Mst_Ward
					);


				////
				if (strWardName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strWardName", strWardName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Ward_Create_InvalidWardName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB Mst_Ward:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Ward.NewRow();
				strFN = "WardCode"; drDB[strFN] = strWardCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode;
				strFN = "DistrictCode"; drDB[strFN] = strDistrictCode;
				strFN = "WardName"; drDB[strFN] = strWardName;;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_Ward.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_Ward" // strTableName
					, dtDB_Mst_Ward // dtData
									//, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		private void Mst_Ward_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
		   //// 
		   , object objWardCode
			, object objNetworkID
			, object objProvinceCode
			, object objDistrictCode
			, object objWardName
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Ward_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objWardCode", objWardCode
				, "objNetworkID", objNetworkID
				, "objProvinceCode", objProvinceCode
				, "objDistrictCode", objDistrictCode
				, "objWardName", objWardName
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
			string strWardCode = TUtils.CUtils.StdParam(objWardCode);
			string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
			string strProvinceCode = TUtils.CUtils.StdParam(objProvinceCode);
			string strDistrictCode = TUtils.CUtils.StdParam(objDistrictCode);
			string strWardName = TUtils.CUtils.StdStr(objWardName);
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			////
			bool bUpd_ProvinceCode = strFt_Cols_Upd.Contains("Mst_Ward.ProvinceCode".ToUpper());
			bool bUpd_DistrictCode = strFt_Cols_Upd.Contains("Mst_Ward.DistrictCode".ToUpper());
			bool bUpd_WardName = strFt_Cols_Upd.Contains("Mst_Ward.WardName".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Ward.FlagActive".ToUpper());

			////
			DataTable dtDB_Mst_Ward = null;
			{
				////
				Mst_Ward_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strWardCode // strWardCode 
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Ward // dtDB_Mst_Ward
					);
				////
				if (bUpd_WardName && string.IsNullOrEmpty(strWardName))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strWardName", strWardName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Ward_UpdateX_InvalidWardName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // Save Mst_Ward:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Ward.Rows[0];
				if (bUpd_ProvinceCode) { strFN = "ProvinceCode"; drDB[strFN] = strProvinceCode; alColumnEffective.Add(strFN); }
				if (bUpd_DistrictCode) { strFN = "DistrictCode"; drDB[strFN] = strDistrictCode; alColumnEffective.Add(strFN); }
				if (bUpd_WardName) { strFN = "WardName"; drDB[strFN] = strWardName; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_Ward"
					, dtDB_Mst_Ward
					, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		private void Mst_Ward_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objWardCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Ward_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objWardCode", objWardCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strWardCode = TUtils.CUtils.StdParam(objWardCode);

			////
			DataTable dtDB_Mst_Ward = null;
			{
				////
				Mst_Ward_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strWardCode // strWardCode 
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Ward // dtDB_Mst_Ward
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_Ward.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_Ward"
					, dtDB_Mst_Ward
					);
			}
			#endregion
		}
		#endregion

		#region // Mst_Area:
		private void Mst_Area_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objAreaCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Area
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Area t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.AreaCode = @objAreaCode
					;
				");
			dtDB_Mst_Area = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objAreaCode", objAreaCode
				).Tables[0];
			dtDB_Mst_Area.TableName = "Mst_Area";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Area.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.AreaCode", objAreaCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Area_CheckDB_AreaNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Area.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.AreaCode", objAreaCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Area_CheckDB_AreaExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Area.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.AreaCode", objAreaCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Area.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Area_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		private void Mst_Area_UpdBU()
		{
			string strSqlPostSave = CmUtils.StringUtils.Replace(@"
                    declare @strAreaCode_Root nvarchar(100); select @strAreaCode_Root = 'ALL';

                    update t
                    set
	                    t.AreaBUCode = @strAreaCode_Root
	                    , t.AreaBUPattern = @strAreaCode_Root + '%'
	                    , t.AreaLevel = 1
                    from Mst_Area t
	                    left join Mst_Area t_Parent
		                    on t.AreaCodeParent = t_Parent.AreaCode
                    where (1=1)
	                    and t.AreaCode in (@strAreaCode_Root)
                    ;

                    declare @nDeepArea int; select @nDeepArea = 0;
                    while (@nDeepArea <= 6)
                    begin
	                    select @nDeepArea = @nDeepArea + 1;
	
	                    update t
	                    set
		                    t.AreaBUCode = IsNull(t_Parent.AreaBUCode + '.', '') + t.AreaCode
		                    , t.AreaBUPattern = IsNull(t_Parent.AreaBUCode + '.', '') + t.AreaCode + '%'
		                    , t.AreaLevel = IsNull(t_Parent.AreaLevel, 0) + 1
	                    from Mst_Area t
		                    left join Mst_Area t_Parent
			                    on t.AreaCodeParent = t_Parent.AreaCode
	                    where (1=1)
		                    and t.AreaCode not in (@strAreaCode_Root)
	                    ;
                    end;
                ");
			DataSet dsPostSave = _cf.db.ExecQuery(strSqlPostSave);
		}

		public DataSet WAS_Mst_Area_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Area objRQ_Mst_Area
			////
			, out RT_Mst_Area objRT_Mst_Area
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Area.Tid;
			objRT_Mst_Area = new RT_Mst_Area();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Area.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Area_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Area_Get;
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

                List<Mst_Area> lst_Mst_Area = new List<Mst_Area>();
				#endregion

				#region // WS_Mst_Area_Get:
				mdsResult = Mst_Area_Get(
					objRQ_Mst_Area.Tid // strTid
					, objRQ_Mst_Area.GwUserCode // strGwUserCode
					, objRQ_Mst_Area.GwPassword // strGwPassword
					, objRQ_Mst_Area.WAUserCode // strUserCode
					, objRQ_Mst_Area.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Area.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Area.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Area.Ft_WhereClause // strFt_WhereClause
													//// Return:
					, objRQ_Mst_Area.Rt_Cols_Mst_Area // strRt_Cols_Mst_Area
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_Area.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    DataTable dt_Mst_Area = mdsResult.Tables["Mst_Area"].Copy();
					lst_Mst_Area = TUtils.DataTableCmUtils.ToListof<Mst_Area>(dt_Mst_Area);
					objRT_Mst_Area.Lst_Mst_Area = lst_Mst_Area;
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
		public DataSet Mst_Area_Get(
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
			, string strRt_Cols_Mst_Area
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Area_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Area_Get;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
					, "strFt_RecordCount", strFt_RecordCount
					, "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_Area", strRt_Cols_Mst_Area
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

				#region // Mst_Area_GetX:
				DataSet dsGetData = null;
				{
					Mst_Area_GetX(
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
						, strRt_Cols_Mst_Area // strRt_Cols_Mst_Area
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

		public DataSet WAS_Mst_Area_Create(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Area objRQ_Mst_Area
			////
			, out RT_Mst_Area objRT_Mst_Area
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Area.Tid;
			objRT_Mst_Area = new RT_Mst_Area();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Area.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Area_Create";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Area_Create;
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
				List<Mst_Area> lst_Mst_Area = new List<Mst_Area>();
				#endregion

				#region // WS_Mst_Area_Get:
				mdsResult = Mst_Area_Create(
					objRQ_Mst_Area.Tid // strTid
					, objRQ_Mst_Area.GwUserCode // strGwUserCode
					, objRQ_Mst_Area.GwPassword // strGwPassword
					, objRQ_Mst_Area.WAUserCode // strUserCode
					, objRQ_Mst_Area.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Area.Mst_Area.OrgID // objOrgID
					, objRQ_Mst_Area.Mst_Area.AreaCode // objAreaCode
					, objRQ_Mst_Area.Mst_Area.NetworkID // objNetworkID
					, objRQ_Mst_Area.Mst_Area.AreaCodeParent // objAreaCodeParent
					, objRQ_Mst_Area.Mst_Area.AreaName // objAreaName
					, objRQ_Mst_Area.Mst_Area.AreaDesc // objAreaDesc
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
		public DataSet Mst_Area_Create(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgID
			, object objAreaCode
			, object objNetworkID
			, object objAreaCodeParent
			, object objAreaName
			, object objAreaDesc
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Area_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Area_Create;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objAreaCode", objAreaCode
					, "objNetworkID", objNetworkID
					, "objAreaCodeParent", objAreaCodeParent
					, "objAreaName", objAreaName
					, "objAreaDesc", objAreaDesc
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

				#region // Mst_Area_CreateX:
				//DataSet dsGetData = null;
				{
					Mst_Area_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objAreaCode // objAreaCode
						, objNetworkID // objNetworkID
						, objAreaCodeParent // objAreaCodeParent
						, objAreaName // objAreaName
						, objAreaDesc // objAreaDesc
						);
				}
				////
				//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
		public DataSet WAS_Mst_Area_Update(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Area objRQ_Mst_Area
			////
			, out RT_Mst_Area objRT_Mst_Area
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Area.Tid;
			objRT_Mst_Area = new RT_Mst_Area();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Area.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Area_Update";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Area_Update;
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
				List<Mst_Area> lst_Mst_Area = new List<Mst_Area>();
				#endregion

				#region // Mst_Area_Update:
				mdsResult = Mst_Area_Update(
					objRQ_Mst_Area.Tid // strTid
					, objRQ_Mst_Area.GwUserCode // strGwUserCode
					, objRQ_Mst_Area.GwPassword // strGwPassword
					, objRQ_Mst_Area.WAUserCode // strUserCode
					, objRQ_Mst_Area.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Area.Mst_Area.OrgID // objOrgID
					, objRQ_Mst_Area.Mst_Area.AreaCode // objAreaCode
					, objRQ_Mst_Area.Mst_Area.NetworkID // objNetworkID
					, objRQ_Mst_Area.Mst_Area.AreaCodeParent // objAreaCodeParent
					, objRQ_Mst_Area.Mst_Area.AreaName // objAreaName
					, objRQ_Mst_Area.Mst_Area.AreaDesc // objAreaDesc
					, objRQ_Mst_Area.Mst_Area.FlagActive // objFlagActive
													  ////
					, objRQ_Mst_Area.Ft_Cols_Upd // objFt_Cols_Upd
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
		public DataSet Mst_Area_Update(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
		   ////
		   , object objOrgID
			, object objAreaCode
			, object objNetworkID
			, object objAreaCodeParent
			, object objAreaName
			, object objAreaDesc
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Area_Update";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Area_Update;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objAreaCode", objAreaCode
					, "objNetworkID", objNetworkID
					, "objAreaCodeParent", objAreaCodeParent
					, "objAreaName", objAreaName
					, "objAreaDesc", objAreaDesc
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
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Mst_Area_UpdateX:
				//DataSet dsGetData = null;
				{
					Mst_Area_UpdateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objAreaCode // objAreaCode
						, objNetworkID // objNetworkID
						, objAreaCodeParent // objAreaCodeParent
						, objAreaName // objAreaName
						, objAreaDesc // objAreaDesc
						, objFlagActive // objFlagActive
									 ////
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
		public DataSet WAS_Mst_Area_Delete(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Area objRQ_Mst_Area
			////
			, out RT_Mst_Area objRT_Mst_Area
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Area.Tid;
			objRT_Mst_Area = new RT_Mst_Area();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Area.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Area_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Area_Delete;
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
				List<Mst_Area> lst_Mst_Area = new List<Mst_Area>();
				#endregion

				#region // Mst_Area_Delete:
				mdsResult = Mst_Area_Delete(
					objRQ_Mst_Area.Tid // strTid
					, objRQ_Mst_Area.GwUserCode // strGwUserCode
					, objRQ_Mst_Area.GwPassword // strGwPassword
					, objRQ_Mst_Area.WAUserCode // strUserCode
					, objRQ_Mst_Area.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Area.Mst_Area.OrgID // objOrgID
					, objRQ_Mst_Area.Mst_Area.AreaCode // objAreaCode
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
		public DataSet Mst_Area_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objOrgID
			, object objAreaCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Area_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Area_Delete;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
					, "objAreaCode", objAreaCode
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

				#region // Mst_Area_DeleteX:
				//DataSet dsGetData = null;
				{
					Mst_Area_DeleteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objOrgID // objOrgID
						, objAreaCode // objAreaCode
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
		private void Mst_Area_GetX(
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
			, string strRt_Cols_Mst_Area
			////
			, out DataSet dsGetData
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Mst_Area_GetX";
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
			bool bGet_Mst_Area = (strRt_Cols_Mst_Area != null && strRt_Cols_Mst_Area.Length > 0);

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
			//myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
			////
			string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Mst_Area_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mo.OrgID
                        , mo.AreaCode
					into #tbl_Mst_Area_Filter_Draft
					from Mst_Area mo --//[mylock]
					where (1=1)
						zzB_Where_strFilter_zzE
					order by mo.OrgID asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_Area_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_Area_Filter:
					select
						t.*
					into #tbl_Mst_Area_Filter
					from #tbl_Mst_Area_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_Area -----:
					zzB_Select_Mst_Area_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_Area_Filter_Draft;
					--drop table #tbl_Mst_Area_Filter;
					"
				);
			////
			string zzB_Select_Mst_Area_zzE = "-- Nothing.";
			if (bGet_Mst_Area)
			{
				#region // bGet_Mst_Area:
				zzB_Select_Mst_Area_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_Area:
					select
						t.MyIdxSeq
						, mo.*
					from #tbl_Mst_Area_Filter t --//[mylock]
						inner join Mst_Area mo --//[mylock]
							on t.OrgID = mo.OrgID
                                and t.AreaCode = mo.AreaCode
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
						, "Mst_Area" // strTableNameDB
						, "Mst_Area." // strPrefixStd
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
				, "zzB_Select_Mst_Area_zzE", zzB_Select_Mst_Area_zzE
				);
			#endregion

			#region // Get Data:
			dsGetData = _cf.db.ExecQuery(
				strSqlGetData
				, alParamsCoupleSql.ToArray()
				);
			int nIdxTable = 0;
			dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
			if (bGet_Mst_Area)
			{
				dsGetData.Tables[nIdxTable++].TableName = "Mst_Area";
			}
			#endregion
		}
		private void Mst_Area_CreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objAreaCode
			, object objNetworkID
			, object objAreaCodeParent
			, object objAreaName
			, object objAreaDesc
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Area_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
				, "objAreaCode", objAreaCode
				, "objNetworkID", objNetworkID
				, "objAreaCodeParent", objAreaCodeParent
				, "objAreaName", objAreaName
				, "objAreaDesc", objAreaDesc
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strAreaCode = TUtils.CUtils.StdParam(objAreaCode);
			string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
			string strAreaCodeParent = TUtils.CUtils.StdParam(objAreaCodeParent);
			string strAreaName = TUtils.CUtils.StdStr(objAreaName);
			string strAreaDesc = TUtils.CUtils.StdStr(objAreaDesc);

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
			////
			DataTable dtDB_Mst_Area = null;
			{

				////
				if (strOrgID == null || strOrgID.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strOrgID", strOrgID
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Area_Create_InvalidOrgID
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				Mst_Area_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strAreaCode // objAreaCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Area // dtDB_Mst_Area
					);
				////
				DataTable dtDB_Mst_Area_Parent = null;

				Mst_Area_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strAreaCodeParent // objAreaCode
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Area_Parent // dtDB_Mst_Area
					);
				////
				if (strAreaName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strAreaName", strAreaName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Area_Create_InvalidAreaName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // SaveDB Mst_Area:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Area.NewRow();
				strFN = "OrgID"; drDB[strFN] = strOrgID;
				strFN = "AreaCode"; drDB[strFN] = strAreaCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "AreaCodeParent"; drDB[strFN] = strAreaCodeParent;
				strFN = "AreaBUCode"; drDB[strFN] = "X";
				strFN = "AreaBUPattern"; drDB[strFN] = "X";
				strFN = "AreaLevel"; drDB[strFN] = 1;
				strFN = "AreaName"; drDB[strFN] = strAreaName;
				strFN = "AreaDesc"; drDB[strFN] = strAreaDesc;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Mst_Area.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Mst_Area" // strTableName
					, dtDB_Mst_Area // dtData
									//, alColumnEffective.ToArray()
					);
			}
			#endregion

			#region // Post Save:
			{
				Mst_Area_UpdBU();
			}
			#endregion
		}
		private void Mst_Area_UpdateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objAreaCode
			, object objNetworkID
			, object objAreaCodeParent
			, object objAreaName
			, object objAreaDesc
			, object objFlagActive
			////
			, object objFt_Cols_Upd
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Area_UpdateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
				, "objAreaCode", objAreaCode
				, "objNetworkID", objNetworkID
				, "objAreaCodeParent", objAreaCodeParent
				, "objAreaName", objAreaName
				, "objAreaDesc", objAreaDesc
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
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strAreaCode = TUtils.CUtils.StdParam(objAreaCode);
			string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
			string strAreaCodeParent = TUtils.CUtils.StdParam(objAreaCodeParent);
			string strAreaName = TUtils.CUtils.StdParam(objAreaName);
			string strAreaDesc = TUtils.CUtils.StdParam(objAreaDesc);
			string strFlagActive = TUtils.CUtils.StdParam(objFlagActive);
			////
			bool bUpd_AreaName = strFt_Cols_Upd.Contains("Mst_Area.AreaName".ToUpper());
			bool bUpd_AreaDesc = strFt_Cols_Upd.Contains("Mst_Area.AreaDesc".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Area.FlagActive".ToUpper());

			////
			DataTable dtDB_Mst_Area = null;
			{
				////
				Mst_Area_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strAreaCode // objAreaCode 
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Area // dtDB_Mst_Area
					);
				////
				DataTable dtDB_Mst_Area_Parent = null;

				Mst_Area_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strAreaCodeParent // objAreaCode
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Mst_Area_Parent // dtDB_Mst_Area
					);
				////
				if (bUpd_AreaName && string.IsNullOrEmpty(strAreaName))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strAreaName", strAreaName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Area_UpdateX_InvalidAreaName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

			#region // Save Mst_Area:
			{
				// Init:
				ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Mst_Area.Rows[0];
				if (bUpd_AreaName) { strFN = "AreaName"; drDB[strFN] = strAreaName; alColumnEffective.Add(strFN); }
				if (bUpd_AreaDesc) { strFN = "AreaDesc"; drDB[strFN] = strAreaDesc; alColumnEffective.Add(strFN); }
				if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

				// Save:
				_cf.db.SaveData(
					"Mst_Area"
					, dtDB_Mst_Area
					, alColumnEffective.ToArray()
					);
			}
			#endregion
		}
		private void Mst_Area_DeleteX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objOrgID
			, object objAreaCode
			////
			)
		{
			#region // Temp:
			string strFunctionName = "Mst_Area_DeleteX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
				, "objAreaCode", objAreaCode
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strAreaCode = TUtils.CUtils.StdParam(objAreaCode);

			////
			DataTable dtDB_Mst_Area = null;
			{
				////
				Mst_Area_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // strOrgID 
					, strAreaCode // objAreaCode
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Area // dtDB_Mst_Area
					);
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				dtDB_Mst_Area.Rows[0].Delete();

				// Save:
				_cf.db.SaveData(
					"Mst_Area"
					, dtDB_Mst_Area
					);
			}
			#endregion
		}

        public void Mst_Area_SaveX(
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
            string strFunctionName = "Mst_Area_SaveX";
            //string strErrorCodeDefault = TError.ErridNTVAN.Mst_Area_SaveAllX;
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

            #region // Refine and Check Input Mst_Area:
            ////

            ////
            DataTable dtInput_Mst_Area = null;
            {
                ////
                string strTableCheck = "Mst_Area";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Area_SaveX_Input_BrandTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Area = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_Area.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Area_SaveX_Input_BrandTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Area // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "AreaCode" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
                    , "", "AreaCodeParent" // arrstrCouple
                    , "", "AreaBUCode" // arrstrCouple
                    , "", "AreaBUPattern" // arrstrCouple
                    , "", "AreaLevel" // arrstrCouple
                    , "", "AreaName" // arrstrCouple
                    , "", "AreaDesc" // arrstrCouple
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
                    , "#input_Mst_Area" // strTableName
                    , new object[] {
                            "OrgID", TConst.BizMix.Default_DBColType
                            , "AreaCode", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "AreaCodeParent", TConst.BizMix.Default_DBColType
                            , "AreaBUCode", TConst.BizMix.Default_DBColType
                            , "AreaBUPattern", TConst.BizMix.Default_DBColType
                            , "AreaLevel", TConst.BizMix.Default_DBColType
                            , "AreaName", TConst.BizMix.Default_DBColType
                            , "AreaDesc", TConst.BizMix.Default_DBColType
                            , "FlagActive", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_Area // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- Mst_Area:
							    delete t
							    from Mst_Area t --//[mylock]
								    inner join #input_Mst_Area f --//[mylock]
									    on t.OrgID = f.OrgID
                                            and t.AreaCode = f.AreaCode
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
                        string zzzzClauseInsert_Mst_Area_zSave = CmUtils.StringUtils.Replace(@"
                                insert into Mst_Area
                                (
                                    OrgID
                                    , AreaCode
                                    , NetworkID
                                    , AreaCodeParent
                                    , AreaBUCode
                                    , AreaBUPattern
                                    , AreaLevel
                                    , AreaName
                                    , AreaDesc
                                    , FlagActive
                                    , LogLUDTimeUTC
                                    , LogLUBy
                                )
                                select
                                    t.OrgID
                                    , t.AreaCode
                                    , t.NetworkID
                                    , t.AreaCodeParent
                                    , t.AreaBUCode
                                    , t.AreaBUPattern
                                    , t.AreaLevel
                                    , t.AreaName
                                    , t.AreaDesc
                                    , t.FlagActive
                                    , t.LogLUDTimeUTC
                                    , t.LogLUBy
                                from #input_Mst_Area t --//[mylock]
                                ;
                            ");

                        /////

                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Mst_Area_zSave
							"
                            , "zzzzClauseInsert_Mst_Area_zSave", zzzzClauseInsert_Mst_Area_zSave
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
						        drop table #input_Mst_Area;
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
						        drop table #input_Mst_Area;
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

        public DataSet Mst_Area_Save(
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
            string strErrorCodeDefault = TError.ErridnInventory.Mst_Area_Save;
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
                    Mst_Area_SaveX(
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

        public DataSet WAS_Mst_Area_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_Area objRQ_Mst_Area
            ////
            , out RT_Mst_Area objRT_Mst_Area
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_Area.Tid;
            objRT_Mst_Area = new RT_Mst_Area();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Area.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_Area_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Area_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "FlagIsDelete", objRQ_Mst_Area.FlagIsDelete
                , "Lst_Mst_Area", TJson.JsonConvert.SerializeObject(objRQ_Mst_Area.Lst_Mst_Area)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Mst_Area> lst_Mst_Area = new List<Mst_Area>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Mst_Area = TUtils.DataTableCmUtils.ToDataTable<Mst_Area>(objRQ_Mst_Area.Lst_Mst_Area, "Mst_Area");
                    dsData.Tables.Add(dt_Mst_Area);
                    ////
                }
                #endregion

                #region // WS_Mst_Area_Create: 
                // Mst_Area_Save_Root_New20190704
                mdsResult = Mst_Area_Save(
                    objRQ_Mst_Area.Tid // strTid
                    , objRQ_Mst_Area.GwUserCode // strGwUserCode
                    , objRQ_Mst_Area.GwPassword // strGwPassword
                    , objRQ_Mst_Area.WAUserCode // strUserCode
                    , objRQ_Mst_Area.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_Area.FlagIsDelete // objFlagIsDelete
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

        #region // Customer_DynamicField:
        private void Customer_DynamicField_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objOrgID
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Customer_DynamicField
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Customer_DynamicField t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
					;
				");
            dtDB_Customer_DynamicField = _cf.db.ExecQuery(
                strSqlExec
                , "@objOrgID", objOrgID
                ).Tables[0];
            dtDB_Customer_DynamicField.TableName = "Customer_DynamicField";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Customer_DynamicField.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Customer_DynamicField_CheckDB_OrganNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Customer_DynamicField.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Customer_DynamicField_CheckDB_OrganExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Customer_DynamicField.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.OrgID", objOrgID
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Customer_DynamicField.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Customer_DynamicField_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet WAS_Customer_DynamicField_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Customer_DynamicField objRQ_Customer_DynamicField
            ////
            , out RT_Customer_DynamicField objRT_Customer_DynamicField
            )
        {
            #region // Temp:
            string strTid = objRQ_Customer_DynamicField.Tid;
            objRT_Customer_DynamicField = new RT_Customer_DynamicField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Customer_DynamicField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Customer_DynamicField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Customer_DynamicField_Get;
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
                List<Customer_DynamicField> lst_Customer_DynamicField = new List<Customer_DynamicField>();
                #endregion

                #region // WS_Customer_DynamicField_Get:
                mdsResult = Customer_DynamicField_Get(
                    objRQ_Customer_DynamicField.Tid // strTid
                    , objRQ_Customer_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Customer_DynamicField.GwPassword // strGwPassword
                    , objRQ_Customer_DynamicField.WAUserCode // strUserCode
                    , objRQ_Customer_DynamicField.WAUserPassword // strUserPassword
					, objRQ_Customer_DynamicField.AccessToken // strAccessToken
					, objRQ_Customer_DynamicField.NetworkID // strNetworkID
					, objRQ_Customer_DynamicField.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Customer_DynamicField.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Customer_DynamicField.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Customer_DynamicField.Ft_WhereClause // strFt_WhereClause
                                                                 //// Return:
                    , objRQ_Customer_DynamicField.Rt_Cols_Customer_DynamicField // strRt_Cols_Customer_DynamicField
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Customer_DynamicField = mdsResult.Tables["Customer_DynamicField"].Copy();
                    lst_Customer_DynamicField = TUtils.DataTableCmUtils.ToListof<Customer_DynamicField>(dt_Customer_DynamicField);
                    objRT_Customer_DynamicField.Lst_Customer_DynamicField = lst_Customer_DynamicField;
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
        public DataSet Customer_DynamicField_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Customer_DynamicField
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Customer_DynamicField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Customer_DynamicField_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Customer_DynamicField", strRt_Cols_Customer_DynamicField
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
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Customer_DynamicField_GetX:
				DataSet dsGetData = null;
                {
                    Customer_DynamicField_GetX(
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
                        , strRt_Cols_Customer_DynamicField // strRt_Cols_Customer_DynamicField
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

        public DataSet WAS_Customer_DynamicField_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Customer_DynamicField objRQ_Customer_DynamicField
            ////
            , out RT_Customer_DynamicField objRT_Customer_DynamicField
            )
        {
            #region // Temp:
            string strTid = objRQ_Customer_DynamicField.Tid;
            objRT_Customer_DynamicField = new RT_Customer_DynamicField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Customer_DynamicField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Customer_DynamicField_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Customer_DynamicField_Create;
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
                List<Customer_DynamicField> lst_Customer_DynamicField = new List<Customer_DynamicField>();
                #endregion

                #region // WS_Customer_DynamicField_Get:
                mdsResult = Customer_DynamicField_Create(
                    objRQ_Customer_DynamicField.Tid // strTid
                    , objRQ_Customer_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Customer_DynamicField.GwPassword // strGwPassword
                    , objRQ_Customer_DynamicField.WAUserCode // strUserCode
                    , objRQ_Customer_DynamicField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Customer_DynamicField.Customer_DynamicField.OrgID // objOrgID
                    , objRQ_Customer_DynamicField.Customer_DynamicField.Detail // objDetail
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
        public DataSet Customer_DynamicField_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objDetail
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Customer_DynamicField_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Customer_DynamicField_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
                    , "objDetail", objDetail
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

                #region // Customer_DynamicField_CreateX:
                //DataSet dsGetData = null;
                {
                    Customer_DynamicField_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
                        , objDetail // objDetail
                        );
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
        public DataSet WAS_Customer_DynamicField_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Customer_DynamicField objRQ_Customer_DynamicField
            ////
            , out RT_Customer_DynamicField objRT_Customer_DynamicField
            )
        {
            #region // Temp:
            string strTid = objRQ_Customer_DynamicField.Tid;
            objRT_Customer_DynamicField = new RT_Customer_DynamicField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Customer_DynamicField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Customer_DynamicField_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Customer_DynamicField_Update;
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
                List<Customer_DynamicField> lst_Customer_DynamicField = new List<Customer_DynamicField>();
                #endregion

                #region // Customer_DynamicField_Update:
                mdsResult = Customer_DynamicField_Update(
                    objRQ_Customer_DynamicField.Tid // strTid
                    , objRQ_Customer_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Customer_DynamicField.GwPassword // strGwPassword
                    , objRQ_Customer_DynamicField.WAUserCode // strUserCode
                    , objRQ_Customer_DynamicField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Customer_DynamicField.Customer_DynamicField.OrgID // objOrgID
                    , objRQ_Customer_DynamicField.Customer_DynamicField.Detail // objDetail
                    , objRQ_Customer_DynamicField.Customer_DynamicField.FlagActive // objFlagActive
                                                                                ////
                    , objRQ_Customer_DynamicField.Ft_Cols_Upd // objFt_Cols_Upd
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
        public DataSet Customer_DynamicField_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
           ////
           , object objOrgID
            , object objDetail
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Customer_DynamicField_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Customer_DynamicField_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
                    , "objDetail", objDetail
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
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Customer_DynamicField_UpdateX:
                //DataSet dsGetData = null;
                {
                    Customer_DynamicField_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
                        , objDetail // objDetail
                        , objFlagActive // objFlagActive
                                     ////
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
        public DataSet WAS_Customer_DynamicField_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Customer_DynamicField objRQ_Customer_DynamicField
            ////
            , out RT_Customer_DynamicField objRT_Customer_DynamicField
            )
        {
            #region // Temp:
            string strTid = objRQ_Customer_DynamicField.Tid;
            objRT_Customer_DynamicField = new RT_Customer_DynamicField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Customer_DynamicField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Customer_DynamicField_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Customer_DynamicField_Delete;
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
                List<Customer_DynamicField> lst_Customer_DynamicField = new List<Customer_DynamicField>();
                #endregion

                #region // Customer_DynamicField_Delete:
                mdsResult = Customer_DynamicField_Delete(
                    objRQ_Customer_DynamicField.Tid // strTid
                    , objRQ_Customer_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Customer_DynamicField.GwPassword // strGwPassword
                    , objRQ_Customer_DynamicField.WAUserCode // strUserCode
                    , objRQ_Customer_DynamicField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Customer_DynamicField.Customer_DynamicField.OrgID // objOrgID
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
        public DataSet Customer_DynamicField_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Customer_DynamicField_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Customer_DynamicField_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
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

                #region // Customer_DynamicField_DeleteX:
                //DataSet dsGetData = null;
                {
                    Customer_DynamicField_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
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
        private void Customer_DynamicField_GetX(
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
            , string strRt_Cols_Customer_DynamicField
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Customer_DynamicField_GetX";
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
            bool bGet_Customer_DynamicField = (strRt_Cols_Customer_DynamicField != null && strRt_Cols_Customer_DynamicField.Length > 0);

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
            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Customer_DynamicField_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mo.OrgID
					into #tbl_Customer_DynamicField_Filter_Draft
					from Customer_DynamicField mo --//[mylock]
					where (1=1)
						zzB_Where_strFilter_zzE
					order by mo.OrgID asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Customer_DynamicField_Filter_Draft t --//[mylock]
					;

					---- #tbl_Customer_DynamicField_Filter:
					select
						t.*
					into #tbl_Customer_DynamicField_Filter
					from #tbl_Customer_DynamicField_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Customer_DynamicField -----:
					zzB_Select_Customer_DynamicField_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Customer_DynamicField_Filter_Draft;
					--drop table #tbl_Customer_DynamicField_Filter;
					"
                );
            ////
            string zzB_Select_Customer_DynamicField_zzE = "-- Nothing.";
            if (bGet_Customer_DynamicField)
            {
                #region // bGet_Customer_DynamicField:
                zzB_Select_Customer_DynamicField_zzE = CmUtils.StringUtils.Replace(@"
					---- Customer_DynamicField:
					select
						t.MyIdxSeq
						, mo.*
					from #tbl_Customer_DynamicField_Filter t --//[mylock]
						inner join Customer_DynamicField mo --//[mylock]
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
                        , "Customer_DynamicField" // strTableNameDB
                        , "Customer_DynamicField." // strPrefixStd
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
                , "zzB_Select_Customer_DynamicField_zzE", zzB_Select_Customer_DynamicField_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Customer_DynamicField)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Customer_DynamicField";
            }
            #endregion
        }
        private void Customer_DynamicField_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objOrgID
            , object objDetail
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Customer_DynamicField_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                , "objDetail", objDetail
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strDetail = string.Format("{0}", objDetail).Trim();

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Customer_DynamicField = null;
            {

                ////
                if (strOrgID == null || strOrgID.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strOrgID", strOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Customer_DynamicField_Create_InvalidOrgID
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Customer_DynamicField_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // objOrgID
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Customer_DynamicField // dtDB_Customer_DynamicField
                    );


            }
            #endregion

            #region // SaveDB Customer_DynamicField:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Customer_DynamicField.NewRow();
                strFN = "OrgID"; drDB[strFN] = strOrgID;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "Detail"; drDB[strFN] = strDetail;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Customer_DynamicField.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Customer_DynamicField" // strTableName
                    , dtDB_Customer_DynamicField // dtData
                                                 //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }
        private void Customer_DynamicField_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
           //// 
            , object objOrgID
            , object objDetail
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Customer_DynamicField_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                , "objDetail", objDetail
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
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strDetail = string.Format("{0}", objDetail).Trim();
            string strFlagActive = TUtils.CUtils.StdParam(objFlagActive);
            ////
            bool bUpd_Detail = strFt_Cols_Upd.Contains("Customer_DynamicField.Detail".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Customer_DynamicField.FlagActive".ToUpper());

            ////
            DataTable dtDB_Customer_DynamicField = null;
            {
                ////
                Customer_DynamicField_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // strOrgID 
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Customer_DynamicField // dtDB_Customer_DynamicField
                    );
            }
            #endregion

            #region // Save Customer_DynamicField:
            {
                // Init:
                ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Customer_DynamicField.Rows[0];
                if (bUpd_Detail) { strFN = "Detail"; drDB[strFN] = strDetail; alColumnEffective.Add(strFN); }
                if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                // Save:
                _cf.db.SaveData(
                    "Customer_DynamicField"
                    , dtDB_Customer_DynamicField
                    , alColumnEffective.ToArray()
                    );
            }
            #endregion
        }
        private void Customer_DynamicField_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objOrgID
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Customer_DynamicField_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);

            ////
            DataTable dtDB_Customer_DynamicField = null;
            {
                ////
                Customer_DynamicField_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // strOrgID 
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Customer_DynamicField // dtDB_Customer_DynamicField
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                dtDB_Customer_DynamicField.Rows[0].Delete();

                // Save:
                _cf.db.SaveData(
                    "Customer_DynamicField"
                    , dtDB_Customer_DynamicField
                    );
            }
            #endregion
        }

        public void Customer_DynamicField_SaveX(
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
            string strFunctionName = "Customer_DynamicField_SaveX";
            //string strErrorCodeDefault = TError.ErridNTVAN.Customer_DynamicField_SaveAllX;
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

            #region // Refine and Check Input Customer_DynamicField:
            ////

            ////
            DataTable dtInput_Customer_DynamicField = null;
            {
                ////
                string strTableCheck = "Customer_DynamicField";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Customer_DynamicField_SaveX_Input_TblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Customer_DynamicField = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Customer_DynamicField.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Customer_DynamicField_SaveX_Input_TblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Customer_DynamicField // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
                    , "", "Detail" // arrstrCouple
                    , "StdParam", "FlagActive" // arrstrCouple
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
                    , "#input_Customer_DynamicField" // strTableName
                    , new object[] {
                            "OrgID", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "Detail", TConst.BizMix.MyText_DBColType
                            , "FlagActive", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Customer_DynamicField // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- Customer_DynamicField:
							    delete t
							    from Customer_DynamicField t --//[mylock]
								    inner join #input_Customer_DynamicField f --//[mylock]
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
                        string zzzzClauseInsert_Customer_DynamicField_zSave = CmUtils.StringUtils.Replace(@"
                                insert into Customer_DynamicField
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
                                from #input_Customer_DynamicField t --//[mylock]
                                ;
                            ");

                        /////

                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Customer_DynamicField_zSave
							"
                            , "zzzzClauseInsert_Customer_DynamicField_zSave", zzzzClauseInsert_Customer_DynamicField_zSave
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
						        drop table #input_Customer_DynamicField;
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
						        drop table #input_Customer_DynamicField;
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

        public DataSet Customer_DynamicField_Save(
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
            string strErrorCodeDefault = TError.ErridnInventory.Customer_DynamicField_Save;
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
                    Customer_DynamicField_SaveX(
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

        public DataSet WAS_Customer_DynamicField_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Customer_DynamicField objRQ_Customer_DynamicField
            ////
            , out RT_Customer_DynamicField objRT_Customer_DynamicField
            )
        {
            #region // Temp:
            string strTid = objRQ_Customer_DynamicField.Tid;
            objRT_Customer_DynamicField = new RT_Customer_DynamicField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Customer_DynamicField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Customer_DynamicField_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Customer_DynamicField_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "FlagIsDelete", objRQ_Customer_DynamicField.FlagIsDelete
                , "Lst_Customer_DynamicField", TJson.JsonConvert.SerializeObject(objRQ_Customer_DynamicField.Lst_Customer_DynamicField)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Customer_DynamicField> lst_Customer_DynamicField = new List<Customer_DynamicField>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Customer_DynamicField = TUtils.DataTableCmUtils.ToDataTable<Customer_DynamicField>(objRQ_Customer_DynamicField.Lst_Customer_DynamicField, "Customer_DynamicField");
                    dsData.Tables.Add(dt_Customer_DynamicField);
                    ////
                }
                #endregion

                #region // WS_Customer_DynamicField_Create: 
                // Customer_DynamicField_Save_Root_New20190704
                mdsResult = Customer_DynamicField_Save(
                    objRQ_Customer_DynamicField.Tid // strTid
                    , objRQ_Customer_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Customer_DynamicField.GwPassword // strGwPassword
                    , objRQ_Customer_DynamicField.WAUserCode // strUserCode
                    , objRQ_Customer_DynamicField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Customer_DynamicField.FlagIsDelete // objFlagIsDelete
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

        #region // Mst_CustomerType:
        private void Mst_CustomerType_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objCustomerType
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_CustomerType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_CustomerType t --//[mylock]
					where (1=1)
						and t.CustomerType = @objCustomerType
					;
				");
            dtDB_Mst_CustomerType = _cf.db.ExecQuery(
                strSqlExec
                , "@objCustomerType", objCustomerType
                ).Tables[0];
            dtDB_Mst_CustomerType.TableName = "Mst_CustomerType";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_CustomerType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerType", objCustomerType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_CheckDB_OrganNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_CustomerType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.CustomerType", objCustomerType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_CheckDB_OrganExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_CustomerType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CustomerType", objCustomerType
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_CustomerType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_CustomerType_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet WAS_Mst_CustomerType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerType objRQ_Mst_CustomerType
            ////
            , out RT_Mst_CustomerType objRT_Mst_CustomerType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerType.Tid;
            objRT_Mst_CustomerType = new RT_Mst_CustomerType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerType_Get;
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
                List<Mst_CustomerType> lst_Mst_CustomerType = new List<Mst_CustomerType>();
                List<Mst_CustomerTypeImages> lst_Mst_CustomerTypeImages = new List<Mst_CustomerTypeImages>();
                bool bGet_Mst_CustomerType = (objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerType != null && objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerType.Length > 0);
                bool bGet_Mst_CustomerTypeImages = (objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerTypeImages != null && objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerTypeImages.Length > 0);
                #endregion

                #region // WS_Mst_CustomerType_Get:
                mdsResult = Mst_CustomerType_Get(
                    objRQ_Mst_CustomerType.Tid // strTid
                    , objRQ_Mst_CustomerType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerType.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerType.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerType.WAUserPassword // strUserPassword
					, objRQ_Mst_CustomerType.AccessToken // strAccessToken
					, objRQ_Mst_CustomerType.NetworkID // strNetworkID
					, objRQ_Mst_CustomerType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_CustomerType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_CustomerType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_CustomerType.Ft_WhereClause // strFt_WhereClause
                                                            //// Return:
                    , objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerType // strRt_Cols_Mst_CustomerType
                    , objRQ_Mst_CustomerType.Rt_Cols_Mst_CustomerTypeImages // strRt_Cols_Mst_CustomerTypeImages
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_CustomerType.MySummaryTable = lst_MySummaryTable[0];
                    /////
                    if (bGet_Mst_CustomerType)
                    {
                        DataTable dt_Mst_CustomerType = mdsResult.Tables["Mst_CustomerType"].Copy();
                        lst_Mst_CustomerType = TUtils.DataTableCmUtils.ToListof<Mst_CustomerType>(dt_Mst_CustomerType);
                        objRT_Mst_CustomerType.Lst_Mst_CustomerType = lst_Mst_CustomerType;
                    }
                    if (bGet_Mst_CustomerTypeImages)
                    {
                        DataTable dt_Mst_CustomerTypeImages = mdsResult.Tables["Mst_CustomerTypeImages"].Copy();
                        lst_Mst_CustomerTypeImages = TUtils.DataTableCmUtils.ToListof<Mst_CustomerTypeImages>(dt_Mst_CustomerTypeImages);
                        objRT_Mst_CustomerType.Lst_Mst_CustomerTypeImages = lst_Mst_CustomerTypeImages;
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
        public DataSet Mst_CustomerType_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_CustomerType
            , string strRt_Cols_Mst_CustomerTypeImages
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerType_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_CustomerType", strRt_Cols_Mst_CustomerType
                    , "strRt_Cols_Mst_CustomerTypeImages", strRt_Cols_Mst_CustomerTypeImages
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
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Mst_CustomerType_GetX:
				DataSet dsGetData = null;
                {
                    Mst_CustomerType_GetX(
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
                        , strRt_Cols_Mst_CustomerType // strRt_Cols_Mst_CustomerType
                        , strRt_Cols_Mst_CustomerTypeImages // strRt_Cols_Mst_CustomerTypeImages
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

        public DataSet WAS_Mst_CustomerType_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerType objRQ_Mst_CustomerType
            ////
            , out RT_Mst_CustomerType objRT_Mst_CustomerType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerType.Tid;
            objRT_Mst_CustomerType = new RT_Mst_CustomerType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerType_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerType_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "Mst_CustomerType", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerType)
                , "Lst_Mst_CustomerTypeImages", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerType.Lst_Mst_CustomerTypeImages)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Mst_CustomerType> lst_Mst_CustomerType = new List<Mst_CustomerType>();
                ////
                DataSet dsData = new DataSet();
                {
                    DataTable dt_Mst_CustomerTypeImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerTypeImages>(objRQ_Mst_CustomerType.Lst_Mst_CustomerTypeImages, "Mst_CustomerTypeImages");
                    dsData.Tables.Add(dt_Mst_CustomerTypeImages);
                }
                #endregion

                #region // Mst_CustomerType_Create:
                mdsResult = Mst_CustomerType_Create(
                    objRQ_Mst_CustomerType.Tid // strTid
                    , objRQ_Mst_CustomerType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerType.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerType.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerType.Mst_CustomerType.CustomerType // objCustomerType
                    , objRQ_Mst_CustomerType.Mst_CustomerType.CustomerTypeName // objCustomerTypeName
                    , objRQ_Mst_CustomerType.Mst_CustomerType.CustomerTypeDesc // objCustomerTypeDesc
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
        public DataSet Mst_CustomerType_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCustomerType
            , object objCustomerTypeName
            , object objCustomerTypeDesc
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerType_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerType_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objCustomerType", objCustomerType
                    , "objCustomerTypeName", objCustomerTypeName
                    , "objCustomerTypeDesc", objCustomerTypeDesc
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

                #region // Mst_CustomerType_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerType_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objCustomerType // objCustomerType
                        , objCustomerTypeName // objCustomerTypeName
                        , objCustomerTypeDesc // objCustomerTypeDesc
                        , dsData
                        );
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
        public DataSet WAS_Mst_CustomerType_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerType objRQ_Mst_CustomerType
            ////
            , out RT_Mst_CustomerType objRT_Mst_CustomerType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerType.Tid;
            objRT_Mst_CustomerType = new RT_Mst_CustomerType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerType_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerType_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "Mst_CustomerType", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerType)
                , "Mst_CustomerTypeImages", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerType.Lst_Mst_CustomerTypeImages)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Mst_CustomerType> lst_Mst_CustomerType = new List<Mst_CustomerType>();
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Mst_CustomerTypeImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerTypeImages>(objRQ_Mst_CustomerType.Lst_Mst_CustomerTypeImages, "Mst_CustomerTypeImages");
                    dsData.Tables.Add(dt_Mst_CustomerTypeImages);
                    ////
                }
                
                #endregion

                #region // Mst_CustomerType_Update:
                mdsResult = Mst_CustomerType_Update(
                    objRQ_Mst_CustomerType.Tid // strTid
                    , objRQ_Mst_CustomerType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerType.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerType.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerType.Mst_CustomerType.CustomerType // objCustomerType
                    , objRQ_Mst_CustomerType.Mst_CustomerType.CustomerTypeName // objCustomerTypeName
                    , objRQ_Mst_CustomerType.Mst_CustomerType.CustomerTypeDesc // objCustomerTypeDesc
                    , objRQ_Mst_CustomerType.Mst_CustomerType.FlagActive // objLogLUBy
                    , dsData // dsData
                             ////
                    , objRQ_Mst_CustomerType.Ft_Cols_Upd // objFt_Cols_Upd
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
        public DataSet Mst_CustomerType_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
           ////
           , object objCustomerType
            , object objCustomerTypeName
            , object objCustomerTypeDesc
            , object objFlagActive
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerType_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerType_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objCustomerType", objCustomerType
                    , "objCustomerTypeName", objCustomerTypeName
                    , "objCustomerTypeDesc", objCustomerTypeDesc
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
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Mst_CustomerType_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerType_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objCustomerType // objCustomerType
                        , objCustomerTypeName // objCustomerTypeName
                        , objCustomerTypeDesc // objCustomerTypeDesc
                        , objFlagActive // objFlagActive
                        , dsData
                                     ////
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
        public DataSet WAS_Mst_CustomerType_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerType objRQ_Mst_CustomerType
            ////
            , out RT_Mst_CustomerType objRT_Mst_CustomerType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerType.Tid;
            objRT_Mst_CustomerType = new RT_Mst_CustomerType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerType_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerType_Delete;
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
                List<Mst_CustomerType> lst_Mst_CustomerType = new List<Mst_CustomerType>();
                #endregion

                #region // Mst_CustomerType_Delete:
                mdsResult = Mst_CustomerType_Delete(
                    objRQ_Mst_CustomerType.Tid // strTid
                    , objRQ_Mst_CustomerType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerType.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerType.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerType.Mst_CustomerType.CustomerType // objCustomerType
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
        public DataSet Mst_CustomerType_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objCustomerType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerType_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerType_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objCustomerType", objCustomerType
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

                #region // Mst_CustomerType_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerType_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objCustomerType // objCustomerType
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
        private void Mst_CustomerType_GetX(
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
            , string strRt_Cols_Mst_CustomerType
            , string strRt_Cols_Mst_CustomerTypeImages
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_CustomerType_GetX";
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
            bool bGet_Mst_CustomerType = (strRt_Cols_Mst_CustomerType != null && strRt_Cols_Mst_CustomerType.Length > 0);
            bool bGet_Mst_CustomerTypeImages = (strRt_Cols_Mst_CustomerTypeImages != null && strRt_Cols_Mst_CustomerTypeImages.Length > 0);

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
            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Mst_CustomerType_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mo.CustomerType
					into #tbl_Mst_CustomerType_Filter_Draft
					from Mst_CustomerType mo --//[mylock]
                        left join Mst_CustomerTypeImages mcti --//[mylock]
                            on mo.CustomerType = mcti.CustomerType
					where (1=1)
						zzB_Where_strFilter_zzE
					order by mo.CustomerType asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_CustomerType_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_CustomerType_Filter:
					select
						t.*
					into #tbl_Mst_CustomerType_Filter
					from #tbl_Mst_CustomerType_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_CustomerType -----:
					zzB_Select_Mst_CustomerType_zzE
					------------------------

                    -------- Mst_CustomerType -----:
					zzB_Select_Mst_CustomerTypeImages_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_CustomerType_Filter_Draft;
					--drop table #tbl_Mst_CustomerType_Filter;
					"
                );
            ////
            string zzB_Select_Mst_CustomerType_zzE = "-- Nothing.";
            if (bGet_Mst_CustomerType)
            {
                #region // bGet_Mst_CustomerType:
                zzB_Select_Mst_CustomerType_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_CustomerType:
					select
						t.MyIdxSeq
						, mo.*
					from #tbl_Mst_CustomerType_Filter t --//[mylock]
						inner join Mst_CustomerType mo --//[mylock]
							on t.CustomerType = mo.CustomerType
					order by t.MyIdxSeq asc
					;
				"
                );
                #endregion
            }
            string zzB_Select_Mst_CustomerTypeImages_zzE = "-- Nothing.";
            if (bGet_Mst_CustomerTypeImages)
            {
                #region // bGet_Mst_SpecImage:
                zzB_Select_Mst_CustomerTypeImages_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_SpecImage:
							select
								t.MyIdxSeq
								, mcti.*
							from #tbl_Mst_CustomerType_Filter t --//[mylock]
								inner join Mst_CustomerTypeImages mcti --//[mylock]
									on t.CustomerType = mcti.CustomerType
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
                        , "Mst_CustomerType" // strTableNameDB
                        , "Mst_CustomerType." // strPrefixStd
                        , "mo." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                    _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_CustomerTypeImages" // strTableNameDB
                        , "Mst_CustomerTypeImages." // strPrefixStd
                        , "mcti." // strPrefixAlias
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
                , "zzB_Select_Mst_CustomerType_zzE", zzB_Select_Mst_CustomerType_zzE
                , "zzB_Select_Mst_CustomerTypeImages_zzE", zzB_Select_Mst_CustomerTypeImages_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_CustomerType)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerType";
            }
            if (bGet_Mst_CustomerTypeImages)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerTypeImages";
            }
            #endregion
        }
        private void Mst_CustomerType_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objCustomerType
            , object objCustomerTypeName
            , object objCustomerTypeDesc
            , DataSet dsData
            ////
            )
        {
            #region // Temp:
            int nTidSeq = 0;
            bool bMyDebugSql = false;
            string strFunctionName = "Mst_CustomerType_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objCustomerType", objCustomerType
                , "objCustomerTypeName", objCustomerTypeName
                , "objCustomerTypeDesc", objCustomerTypeDesc
                });
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strCustomerType = TUtils.CUtils.StdParam(objCustomerType);
            string strCustomerTypeName = string.Format("{0}",objCustomerTypeName).Trim();
            string strCustomerTypeDesc = string.Format("{0}", objCustomerTypeDesc).Trim();

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_CustomerType = null;
            {

                ////
                if (strCustomerType == null || strCustomerType.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strCustomerType", strCustomerType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Create_InvalidCustomerType
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_CustomerType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strCustomerType // objCustomerType
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerType // dtDB_Mst_CustomerType
                    );


                ////
            }
            #endregion

            #region // SaveTemp Mst_CustomerType:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerType"
                    , new object[] {
                        "CustomerType", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerTypeName", TConst.BizMix.Default_DBColType,
                        "CustomerTypeDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[] {
                        new object[]{
                            strCustomerType, // strCustomerType
                            nNetworkID, // nNetworkID
                            strCustomerTypeName, // strCustomerTypeName
                            strCustomerTypeDesc, // strCustomerTypeDesc
                            TConst.Flag.Active, // FlagActive
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
                            }
                        }
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerTypeImages:
            ////
            DataTable dtInput_Mst_CustomerTypeImages = null;
            {
                ////
                string strTableCheck = "Mst_CustomerTypeImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Create_Input_Mst_CustomerTypeImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerTypeImages = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_KUNN_ValLaiSuatChangeHist.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrTCGQLTV.Mst_Spec_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerTypeImages // dtData
                    , "", "Idx" // arrstrCouple
                    , "", "CustomerType" // arrstrCouple
                    , "", "ImageSpec" // arrstrCouple
                    //, "", "ImagePath" // arrstrCouple
                    , "", "ImageName" // arrstrCouple
                    , "", "ImageDesc" // arrstrCouple
                    , "StdFlag", "FlagPrimaryImage" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "ImagePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_CustomerTypeImages.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_CustomerTypeImages.Rows[nScan];
                    ////
                    ////
                    string strImagePath = null;
                    string strImageSpec = drScan["ImageSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"),nTidSeq++, drScan["ImageName"].ToString());
                    ////
                    string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                    string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                    string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"]; //["TVAN_FilePath"];
                    byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                    string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                    string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                    strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                    ////
                    drScan["ImagePath"] = strImagePath;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                    #region // WriteFile:
                    ////
                    if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
                    {
                        bool exist = Directory.Exists(strFilePathBase);

                        if (!exist)
                        {
                            Directory.CreateDirectory(strFilePathBase);
                        }
                        System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
                    }
                    ////
                    #endregion
                }
            }
            #endregion

            #region //// SaveTemp Mst_SpecImage:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerTypeImages"
                    , new object[]{
                        "Idx", TConst.BizMix.Default_DBColType,
                        "CustomerType", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerTypeImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                //string strSqlDelete = CmUtils.StringUtils.Replace(@"
                //			---- Mst_SpecDtl:
                //			delete t
                //			from Mst_SpecDtl t
                //			where (1=1)
                //				and t.KUNNNo = @strKUNNNo
                //			;

                //			---- Mst_Spec:
                //			delete t
                //			from Mst_Spec t
                //			where (1=1)
                //				and t.KUNNNo = @strKUNNNo
                //			;

                //		");
                //_cf.db.ExecQuery(
                //	strSqlDelete
                //	, "@strKUNNNo", strKUNNNo
                //	);
            }

            //// Insert All:
            {
                ////
                string zzzzClauseInsert_Mst_CustomerType_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerType:
        		        insert into Mst_CustomerType
                        (
                            CustomerType
                            , NetworkID
                            , CustomerTypeName
                            , CustomerTypeDesc
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.CustomerType
                            , t.NetworkID
                            , t.CustomerTypeName
                            , t.CustomerTypeDesc
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerType t --//[mylock]
        		        ;
        	        ");
                ////
                string zzzzClauseInsert_Mst_CustomerTypeImages_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerTypeImages:
        		        insert into Mst_CustomerTypeImages
                        (
                            Idx
                            , CustomerType
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.CustomerType
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerTypeImages t --//[mylock]
        		        ;
        	        ");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
        		        ----
        		        zzzzClauseInsert_Mst_CustomerType_zSave

        		        ----
        		        zzzzClauseInsert_Mst_CustomerTypeImages_zSave


        	        "
                    , "zzzzClauseInsert_Mst_CustomerType_zSave", zzzzClauseInsert_Mst_CustomerType_zSave
                    , "zzzzClauseInsert_Mst_CustomerTypeImages_zSave", zzzzClauseInsert_Mst_CustomerTypeImages_zSave
                    );
                ////
                if (bMyDebugSql)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strSqlExec", strSqlExec
                        });
                }
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
        		        ---- Clear for Debug:
        		        drop table #input_Mst_CustomerType;
        		        drop table #input_Mst_CustomerTypeImages;
        	        ");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion
        }
        private void Mst_CustomerType_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
           //// 
           , object objCustomerType
            , object objCustomerTypeName
            , object objCustomerTypeDesc
            , object objFlagActive
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            int nTidSeq = 0;
            bool bMyDebugSql = false;
            string strFunctionName = "Mst_CustomerType_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objCustomerType", objCustomerType
                , "objCustomerTypeName", objCustomerTypeName
                , "objCustomerTypeDesc", objCustomerTypeDesc
                , "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
                });
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
            string strCustomerType = TUtils.CUtils.StdParam(objCustomerType);
            string strCustomerTypeName = string.Format("{0}", objCustomerTypeName).Trim();
            string strCustomerTypeDesc = string.Format("{0}", objCustomerTypeDesc).Trim();
            string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
            ////
            bool bUpd_CustomerType = strFt_Cols_Upd.Contains("Mst_CustomerType.CustomerType".ToUpper());
            bool bUpd_NetworkID = strFt_Cols_Upd.Contains("Mst_CustomerType.NetworkID".ToUpper());
            bool bUpd_CustomerTypeName = strFt_Cols_Upd.Contains("Mst_CustomerType.CustomerTypeName".ToUpper());
            bool bUpd_CustomerTypeDesc = strFt_Cols_Upd.Contains("Mst_CustomerType.CustomerTypeDesc".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_CustomerType.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_CustomerType = null;
            {
                ////
                Mst_CustomerType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strCustomerType // strCustomerType 
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerType // dtDB_Mst_CustomerType
                    );
                ////
            }
            #endregion

            #region // SaveTemp Mst_CustomerType:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerType"
                    , new object[] {
                        "CustomerType", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerTypeName", TConst.BizMix.Default_DBColType,
                        "CustomerTypeDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[] {
                        new object[]{
                            strCustomerType, // strCustomerType
                            nNetworkID, // nNetworkID
                            strCustomerTypeName, // strCustomerTypeName
                            strCustomerTypeDesc, // strCustomerTypeDesc
                            strFlagActive, // FlagActive
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
                            }
                        }
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerTypeImages:
            ////
            DataTable dtInput_Mst_CustomerTypeImages = null;
            {
                ////
                string strTableCheck = "Mst_CustomerTypeImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Create_Input_Mst_CustomerTypeImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerTypeImages = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_KUNN_ValLaiSuatChangeHist.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrTCGQLTV.Mst_Spec_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerTypeImages // dtData
                    , "", "Idx" // arrstrCouple
                    , "", "CustomerType" // arrstrCouple
                    , "", "ImageSpec" // arrstrCouple
                                      //, "", "ImagePath" // arrstrCouple
                    , "", "ImageName" // arrstrCouple
                    , "", "ImageDesc" // arrstrCouple
                    , "StdFlag", "FlagPrimaryImage" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "ImagePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerTypeImages, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_CustomerTypeImages.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_CustomerTypeImages.Rows[nScan];
                    ////
                    ////
                    string strImagePath = null;
                    string strImageSpec = drScan["ImageSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["ImageName"].ToString());
                    ////
                    string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                    string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                    string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"]; //["TVAN_FilePath"];
                    byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                    string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                    string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                    strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                    ////
                    drScan["ImagePath"] = strImagePath;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                    #region // WriteFile:
                    ////
                    if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
                    {
                        bool exist = Directory.Exists(strFilePathBase);

                        if (!exist)
                        {
                            Directory.CreateDirectory(strFilePathBase);
                        }
                        System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
                    }
                    ////
                    #endregion
                }
            }
            #endregion

            #region //// SaveTemp Mst_SpecImage:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerTypeImages"
                    , new object[]{
                        "Idx", TConst.BizMix.Default_DBColType,
                        "CustomerType", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerTypeImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_CustomerTypeImages:
        			    delete t
                        from Mst_CustomerTypeImages t with(nolock)
                        where (1=1)
	                        and t.CustomerType = @strCustomerType
                        ;

        		    ");
                _cf.db.ExecQuery(
                    strSqlDelete
                    , "@strCustomerType", strCustomerType
                    );
            }

            //// Insert All:
            {
                string zzB_Update_Mst_CustomerType_ClauseSet_zzE = @"
        				t.LogLUDTimeUTC = f.LogLUDTimeUTC
        				, t.LogLUBy = f.LogLUBy
        				";

                if (bUpd_CustomerTypeName) zzB_Update_Mst_CustomerType_ClauseSet_zzE += ", t.CustomerTypeName = f.CustomerTypeName";
                if (bUpd_CustomerTypeDesc) zzB_Update_Mst_CustomerType_ClauseSet_zzE += ", t.CustomerTypeDesc = f.CustomerTypeDesc";
                //if (bUpd_SpecType1) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.SpecType1 = f.SpecType1";
                //if (bUpd_SpecType2) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.SpecType2 = f.SpecType2";
                if (bUpd_FlagActive) zzB_Update_Mst_CustomerType_ClauseSet_zzE += ", t.FlagActive = f.FlagActive";
                ////
                string zzB_Update_Mst_Spec_zzE = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_Spec:
        			    update t
        			    set
        				    zzB_Update_Mst_CustomerType_ClauseSet_zzE
        			    from Mst_CustomerType t --//[mylock]
        				    inner join #input_Mst_CustomerType f --//[mylock]
        					    on t.CustomerType = f.CustomerType
        			    ;
        		    "
                    , "zzB_Update_Mst_CustomerType_ClauseSet_zzE", zzB_Update_Mst_CustomerType_ClauseSet_zzE
                    );
                ////
                string zzzzClauseInsert_Mst_CustomerTypeImages_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerTypeImages:
        		        insert into Mst_CustomerTypeImages
                        (
                            Idx
                            , CustomerType
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.CustomerType
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerTypeImages t --//[mylock]
        		        ;
        	    ");
                ////

                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
        		        ----
        		        zzB_Update_Mst_Spec_zzE

        		        ----
        		        zzzzClauseInsert_Mst_CustomerTypeImages_zSave

        	        "
                    , "zzB_Update_Mst_Spec_zzE", zzB_Update_Mst_Spec_zzE
                    , "zzzzClauseInsert_Mst_CustomerTypeImages_zSave", zzzzClauseInsert_Mst_CustomerTypeImages_zSave
                    );
                ////
                if (bMyDebugSql)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strSqlExec", strSqlExec
                        });
                }
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion
        }
        private void Mst_CustomerType_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objCustomerType
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_CustomerType_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objCustomerType", objCustomerType
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strCustomerType = TUtils.CUtils.StdParam(objCustomerType);

            ////
            DataTable dtDB_Mst_CustomerType = null;
            {
                ////
                Mst_CustomerType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strCustomerType // strCustomerType 
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerType // dtDB_Mst_CustomerType
                    );
                ////
            }
            #endregion

            #region // SaveTemp Mst_CustomerType:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerType"
                    , new object[] {
                        "CustomerType", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[] {
                        new object[]{
                            strCustomerType, // strCustomerType
                            nNetworkID, // nNetworkID
                            TConst.Flag.Active, // FlagActive
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
                            }
                        }
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_CustomerType:
        			    delete t
        			    from Mst_CustomerType t
        			    where (1=1)
        				    and t.CustomerType = @strCustomerType
        			    ;

        			    ---- Mst_CustomerTypeImages:
        			    delete t
        			    from Mst_CustomerTypeImages t
        			    where (1=1)
        				    and t.CustomerType = @strCustomerType
        			    ;
        		    ");
                _cf.db.ExecQuery(
                    strSqlDelete
                    , "@strCustomerType", strCustomerType
                    );
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
        		        ---- Clear for Debug:
        		        drop table #input_Mst_CustomerType;
        	        ");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion
        }

        public DataSet Mst_CustomerType_Save(
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
            //bool bMyDebugSql = false;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerType_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerType_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
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

                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Call Func:
                ////
                Mst_CustomerType_SaveX(
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
                //alParamsCoupleError.AddRange(alPCErrEx);
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

        private void Mst_CustomerType_SaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objFlagIsDelete
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            bool bMyDebugSql = false;
            string strFunctionName = "Mst_CustomerType_UpdateX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerType_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                });
            #endregion

            #region //// Refine and Check Mst_CustomerType:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            string strCreateDTime = null;
            string strCreateBy = null;

            ////
            //DataTable dtDB_Mst_CustomerType = null;
            string strDateStart = dtimeSys.ToString("yyyy-MM-dd");

            ////
            //string strFunctionActionType = TConst.FunctionActionType.Update;
            //string strFunctionRemark = "";
            {

                ////
                strCreateDTime = string.IsNullOrEmpty(strCreateDTime) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTime;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
            }
            ////
            #endregion

            #region //// Refine and Check Mst_CustomerType:
            ////
            DataTable dtInput_Mst_CustomerType = null;
            {
                ////
                string strTableCheck = "Mst_CustomerType";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Save_Input_Mst_CustomerTypeTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerType = dsData.Tables[strTableCheck];
            }
            #endregion

            #region //// SaveTemp Mst_CustomerType:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerType"
                    , new object[]{
                        "CustomerType", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerTypeName", TConst.BizMix.Default_DBColType,
                        "CustomerTypeDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerType
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerTypeImages:
            ////
            DataTable dtInput_Mst_CustomerTypeImages = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "Mst_CustomerTypeImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Update_Input_Mst_CustomerTypeImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerTypeImages = dsData.Tables[strTableCheck];
                ////
            }
            #endregion

            #region //// SaveTemp Mst_CustomerTypeImages:
            if (!bIsDelete)
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerTypeImages"
                    , new object[]{
                        "Idx", "int",
                        "CustomerType", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerTypeImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
							---- Mst_CustomerTypeImages:
                            delete t
                            from Mst_CustomerTypeImages t --//[mylock]
	                            inner join #input_Mst_CustomerType f --//[mylock]
		                            on t.CustomerType = f.CustomerType
                            where (1=1)
                            ;


                            ---- Mst_CustomerType:
                            delete t
                            from Mst_CustomerType t --//[mylock]
	                            inner join #input_Mst_CustomerType f --//[mylock]
		                            on t.CustomerType = f.CustomerType
                            where (1=1)
                            ;
						");
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
            }
            //// Insert All:
            if (!bIsDelete)
            {
                ////
                string zzzzClauseInsert_Mst_CustomerType_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_CustomerType:
						insert into Mst_CustomerType
                        (
                            CustomerType
                            , NetworkID
                            , CustomerTypeName
                            , CustomerTypeDesc
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.CustomerType
                            , t.NetworkID
                            , t.CustomerTypeName
                            , t.CustomerTypeDesc
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
						from #input_Mst_CustomerType t --//[mylock]
						;
					");
                ////
                string zzzzClauseInsert_Mst_CustomerTypeImages_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_CustomerTypeImages:
						insert into Mst_CustomerTypeImages
                        (
                            Idx
                            , CustomerType
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.CustomerType
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
						from #input_Mst_CustomerTypeImages t --//[mylock]
						;
					");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_Mst_CustomerType_zSave

						----
						zzzzClauseInsert_Mst_CustomerTypeImages_zSave
						
						----
					"
                    , "zzzzClauseInsert_Mst_CustomerType_zSave", zzzzClauseInsert_Mst_CustomerType_zSave
                    , "zzzzClauseInsert_Mst_CustomerTypeImages_zSave", zzzzClauseInsert_Mst_CustomerTypeImages_zSave
                    );
                ////
                if (bMyDebugSql)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strSqlExec", strSqlExec
                        });
                }
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_CustomerType;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            // Return Good:
            //return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }

        public DataSet WAS_Mst_CustomerType_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerType objRQ_Mst_CustomerType
            ////
            , out RT_Mst_CustomerType objRT_Mst_CustomerType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerType.Tid;
            objRT_Mst_CustomerType = new RT_Mst_CustomerType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerType_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerType_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Mst_CustomerType", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerType.Lst_Mst_CustomerType)
                , "Lst_Mst_CustomerTypeImages", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerType.Lst_Mst_CustomerTypeImages)
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
                    DataTable dt_Mst_CustomerType = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerType>(objRQ_Mst_CustomerType.Lst_Mst_CustomerType, "Mst_CustomerType");
                    dsData.Tables.Add(dt_Mst_CustomerType);
                    ////
                    DataTable dt_Mst_CustomerTypeImages = null;
                    if (objRQ_Mst_CustomerType.Lst_Mst_CustomerTypeImages != null)
                    {
                        dt_Mst_CustomerTypeImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerTypeImages>(objRQ_Mst_CustomerType.Lst_Mst_CustomerTypeImages, "Mst_CustomerTypeImages");
                        dsData.Tables.Add(dt_Mst_CustomerTypeImages);
                    }
                    else
                    {
                        dt_Mst_CustomerTypeImages = TDALUtils.DBUtils.GetSchema(_cf.db, "Mst_CustomerTypeImages").Tables[0];
                        dsData.Tables.Add(dt_Mst_CustomerTypeImages.Copy());
                    }
                    ////
                }
                #endregion

                #region // Mst_CustomerType_Save:
                mdsResult = Mst_CustomerType_Save(
                    objRQ_Mst_CustomerType.Tid // strTid
                    , objRQ_Mst_CustomerType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerType.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerType.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerType.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerType.FlagIsDelete // objFlagIsDelete
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

        #region // Mst_CustomerGroup:
        private void Mst_CustomerGroup_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objOrgID
            , object objCustomerGrpCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_CustomerGroup
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_CustomerGroup t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
                        and t.CustomerGrpCode = @objCustomerGrpCode
					;
				");
            dtDB_Mst_CustomerGroup = _cf.db.ExecQuery(
                strSqlExec
                , "@objOrgID", objOrgID
                , "@objCustomerGrpCode", objCustomerGrpCode
                ).Tables[0];
            dtDB_Mst_CustomerGroup.TableName = "Mst_CustomerGroup";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_CustomerGroup.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        , "Check.CustomerGrpCode", objCustomerGrpCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerGroup_CheckDB_OrganNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_CustomerGroup.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        , "Check.CustomerGrpCode", objCustomerGrpCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerGroup_CheckDB_OrganExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_CustomerGroup.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.OrgID", objOrgID
                    , "Check.CustomerGrpCode", objCustomerGrpCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_CustomerGroup.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_CustomerGroup_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet WAS_Mst_CustomerGroup_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup
            ////
            , out RT_Mst_CustomerGroup objRT_Mst_CustomerGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerGroup.Tid;
            objRT_Mst_CustomerGroup = new RT_Mst_CustomerGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerGroup_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerGroup_Get;
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
                List<Mst_CustomerGroup> lst_Mst_CustomerGroup = new List<Mst_CustomerGroup>();
                List<Mst_CustomerGroupImages> lst_Mst_CustomerGroupImages = new List<Mst_CustomerGroupImages>();
                bool bGet_Mst_CustomerGroup = (objRQ_Mst_CustomerGroup.Rt_Cols_Mst_CustomerGroup != null && objRQ_Mst_CustomerGroup.Rt_Cols_Mst_CustomerGroup.Length > 0);
                bool bGet_Mst_CustomerGroupImages = (objRQ_Mst_CustomerGroup.Rt_Cols_Mst_CustomerGroupImages != null && objRQ_Mst_CustomerGroup.Rt_Cols_Mst_CustomerGroupImages.Length > 0);
                #endregion

                #region // WS_Mst_CustomerGroup_Get:
                mdsResult = Mst_CustomerGroup_Get(
                    objRQ_Mst_CustomerGroup.Tid // strTid
                    , objRQ_Mst_CustomerGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerGroup.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerGroup.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerGroup.WAUserPassword // strUserPassword
					, objRQ_Mst_CustomerGroup.AccessToken // strAccessToken
					, objRQ_Mst_CustomerGroup.NetworkID // strNetworkID
					, objRQ_Mst_CustomerGroup.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_CustomerGroup.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_CustomerGroup.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_CustomerGroup.Ft_WhereClause // strFt_WhereClause
                                                             //// Return:
                    , objRQ_Mst_CustomerGroup.Rt_Cols_Mst_CustomerGroup // strRt_Cols_Mst_CustomerGroup
                    , objRQ_Mst_CustomerGroup.Rt_Cols_Mst_CustomerGroupImages // Rt_Cols_Mst_CustomerGroupImages
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_CustomerGroup.MySummaryTable = lst_MySummaryTable[0];
                    /////
                    if (bGet_Mst_CustomerGroup)
                    {
                        DataTable dt_Mst_CustomerGroup = mdsResult.Tables["Mst_CustomerGroup"].Copy();
                        lst_Mst_CustomerGroup = TUtils.DataTableCmUtils.ToListof<Mst_CustomerGroup>(dt_Mst_CustomerGroup);
                        objRT_Mst_CustomerGroup.Lst_Mst_CustomerGroup = lst_Mst_CustomerGroup;
                    }
                    if (bGet_Mst_CustomerGroupImages)
                    {
                        DataTable dt_Mst_CustomerGroupImages = mdsResult.Tables["Mst_CustomerGroupImages"].Copy();
                        lst_Mst_CustomerGroupImages = TUtils.DataTableCmUtils.ToListof<Mst_CustomerGroupImages>(dt_Mst_CustomerGroupImages);
                        objRT_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages = lst_Mst_CustomerGroupImages;
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
        public DataSet Mst_CustomerGroup_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_CustomerGroup
            , string strRt_Cols_Mst_CustomerGroupImages
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerGroup_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerGroup_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_CustomerGroup", strRt_Cols_Mst_CustomerGroup
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
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Mst_CustomerGroup_GetX:
				DataSet dsGetData = null;
                {
                    Mst_CustomerGroup_GetX(
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
                        , strRt_Cols_Mst_CustomerGroup // strRt_Cols_Mst_CustomerGroup
                        , strRt_Cols_Mst_CustomerGroupImages
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

        public DataSet WAS_Mst_CustomerGroup_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup
            ////
            , out RT_Mst_CustomerGroup objRT_Mst_CustomerGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerGroup.Tid;
            objRT_Mst_CustomerGroup = new RT_Mst_CustomerGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerGroup_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerGroup_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "Mst_CustomerGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerGroup)
                , "Lst_Mst_CustomerGroupImages", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Mst_CustomerGroup> lst_Mst_CustomerGroup = new List<Mst_CustomerGroup>();
                DataSet dsData = new DataSet();
                {
                    DataTable dt_Mst_CustomerGroupImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerGroupImages>(objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages, "Mst_CustomerGroupImages");
                    dsData.Tables.Add(dt_Mst_CustomerGroupImages);
                }
                #endregion

                #region // WS_Mst_CustomerGroup_Get:
                mdsResult = Mst_CustomerGroup_Create(
                    objRQ_Mst_CustomerGroup.Tid // strTid
                    , objRQ_Mst_CustomerGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerGroup.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerGroup.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.OrgID // objOrgID
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpCode // objCustomerGrpCode
                    //, objRQ_Mst_CustomerGroup.Mst_CustomerGroup.NetworkID // objNetworkID
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpCodeParent // objCustomerGrpCodeParent
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpLevel // objCustomerGrpLevel
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpName // objCustomerGrpName
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpDesc // objCustomerGrpDesc
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
        public DataSet Mst_CustomerGroup_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objCustomerGrpCode
            //, object objNetworkID
            , object objCustomerGrpCodeParent
            , object objCustomerGrpLevel
            , object objCustomerGrpName
            , object objCustomerGrpDesc
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerGroup_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerGroup_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
                    , "objCustomerGrpCode", objCustomerGrpCode
                    //, "objNetworkID", objNetworkID
                    , "objCustomerGrpCodeParent", objCustomerGrpCodeParent
                    , "objCustomerGrpLevel", objCustomerGrpLevel
                    , "objCustomerGrpName", objCustomerGrpName
                    , "objCustomerGrpDesc", objCustomerGrpDesc
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

                #region // Mst_CustomerGroup_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerGroup_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
                        , objCustomerGrpCode // objCustomerGrpCode
                        //, objNetworkID // objNetworkID
                        , objCustomerGrpCodeParent // objCustomerGrpCodeParent
                        , objCustomerGrpLevel // objCustomerGrpLevel
                        , objCustomerGrpName // objCustomerGrpName
                        , objCustomerGrpDesc // objCustomerGrpDesc
                        , dsData
                        );
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
        public DataSet WAS_Mst_CustomerGroup_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup
            ////
            , out RT_Mst_CustomerGroup objRT_Mst_CustomerGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerGroup.Tid;
            objRT_Mst_CustomerGroup = new RT_Mst_CustomerGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerGroup_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerGroup_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "Mst_CustomerGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerGroup)
                , "Lst_Mst_CustomerGroupImages", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Mst_CustomerGroup> lst_Mst_CustomerGroup = new List<Mst_CustomerGroup>();
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Mst_CustomerGroupImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerGroupImages>(objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages, "Mst_CustomerGroupImages");
                    dsData.Tables.Add(dt_Mst_CustomerGroupImages);
                    ////
                }
                #endregion

                #region // Mst_CustomerGroup_Update:
                mdsResult = Mst_CustomerGroup_Update(
                    objRQ_Mst_CustomerGroup.Tid // strTid
                    , objRQ_Mst_CustomerGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerGroup.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerGroup.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.OrgID // objOrgID
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpCode // objCustomerGrpCode
                    //, objRQ_Mst_CustomerGroup.Mst_CustomerGroup.NetworkID // objNetworkID
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpName // objCustomerGrpName
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpDesc // objCustomerGrpDesc
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.FlagActive // objFlagActive
                    , dsData // dsData
                             ////
                    , objRQ_Mst_CustomerGroup.Ft_Cols_Upd // objFt_Cols_Upd
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
        public DataSet Mst_CustomerGroup_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
           ////
           , object objOrgID
            , object objCustomerGrpCode
            //, object objNetworkID
            , object objCustomerGrpName
            , object objCustomerGrpDesc
            , object objFlagActive
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerGroup_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerGroup_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
                    , "objCustomerGrpCode", objCustomerGrpCode
                    //, "objNetworkID", objNetworkID
                    , "objCustomerGrpName", objCustomerGrpName
                    , "objCustomerGrpDesc", objCustomerGrpDesc
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
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Mst_CustomerGroup_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerGroup_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
                        , objCustomerGrpCode // objCustomerGrpCode
                        //, objNetworkID // objNetworkID
                        , objCustomerGrpName // objCustomerGrpName
                        , objCustomerGrpDesc // objCustomerGrpDesc
                        , objFlagActive
                        , dsData // dsData
                                 ////
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
        public DataSet WAS_Mst_CustomerGroup_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup
            ////
            , out RT_Mst_CustomerGroup objRT_Mst_CustomerGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerGroup.Tid;
            objRT_Mst_CustomerGroup = new RT_Mst_CustomerGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerGroup_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerGroup_Delete;
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
                List<Mst_CustomerGroup> lst_Mst_CustomerGroup = new List<Mst_CustomerGroup>();
                #endregion

                #region // Mst_CustomerGroup_Delete:
                mdsResult = Mst_CustomerGroup_Delete(
                    objRQ_Mst_CustomerGroup.Tid // strTid
                    , objRQ_Mst_CustomerGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerGroup.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerGroup.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.OrgID // objOrgID
                    , objRQ_Mst_CustomerGroup.Mst_CustomerGroup.CustomerGrpCode // objCustomerGrpCode
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
        public DataSet Mst_CustomerGroup_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objCustomerGrpCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerGroup_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerGroup_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
                    , "objCustomerGrpCode", objCustomerGrpCode
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

                #region // Mst_CustomerGroup_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerGroup_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
                        , objCustomerGrpCode // objCustomerGrpCode
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
        private void Mst_CustomerGroup_GetX(
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
            , string strRt_Cols_Mst_CustomerGroup
            , string strRt_Cols_Mst_CustomerGroupImages
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_CustomerGroup_GetX";
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
            bool bGet_Mst_CustomerGroup = (strRt_Cols_Mst_CustomerGroup != null && strRt_Cols_Mst_CustomerGroup.Length > 0);
            bool bGet_Mst_CustomerGroupImages = (strRt_Cols_Mst_CustomerGroupImages != null && strRt_Cols_Mst_CustomerGroupImages.Length > 0);
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
            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Mst_CustomerGroup_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mo.OrgID
						, mo.CustomerGrpCode
					into #tbl_Mst_CustomerGroup_Filter_Draft
					from Mst_CustomerGroup mo --//[mylock]
                        left join Mst_CustomerGroupImages mcgi --//[mylock]
							on mo.OrgID = mcgi.OrgID
								and mo.CustomerGrpCode = mcgi.CustomerGrpCode
					where (1=1)
						zzB_Where_strFilter_zzE
					order by 
						MyIdxSeq asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Mst_CustomerGroup_Filter_Draft t --//[mylock]
					;

					---- #tbl_Mst_CustomerGroup_Filter:
					select
						t.*
					into #tbl_Mst_CustomerGroup_Filter
					from #tbl_Mst_CustomerGroup_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Mst_CustomerGroup -----:
					zzB_Select_Mst_CustomerGroup_zzE
					------------------------

                    -------- Mst_CustomerGroupImages -----:
					zzB_Select_Mst_CustomerGroupImages_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_CustomerGroup_Filter_Draft;
					--drop table #tbl_Mst_CustomerGroup_Filter;
					"
                );
            ////
            string zzB_Select_Mst_CustomerGroup_zzE = "-- Nothing.";
            if (bGet_Mst_CustomerGroup)
            {
                #region // bGet_Mst_CustomerGroup:
                zzB_Select_Mst_CustomerGroup_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_CustomerGroup:
					select
						t.MyIdxSeq
						, mo.*
					from #tbl_Mst_CustomerGroup_Filter t --//[mylock]
						inner join Mst_CustomerGroup mo --//[mylock]
							on t.OrgID = mo.OrgID
								and t.CustomerGrpCode = mo.CustomerGrpCode
					order by t.MyIdxSeq asc
					;
				"
                );
                #endregion
            }
            string zzB_Select_Mst_CustomerGroupImages_zzE = "-- Nothing.";
            if (bGet_Mst_CustomerGroupImages)
            {
                #region // bGet_Mst_CustomerGroup:
                zzB_Select_Mst_CustomerGroupImages_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_CustomerGroup:
					select
						t.MyIdxSeq
						, mcgi.*
					from #tbl_Mst_CustomerGroup_Filter t --//[mylock]
						inner join Mst_CustomerGroupImages mcgi --//[mylock]
							on t.OrgID = mcgi.OrgID
								and t.CustomerGrpCode = mcgi.CustomerGrpCode
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
                        , "Mst_CustomerGroup" // strTableNameDB
                        , "Mst_CustomerGroup." // strPrefixStd
                        , "mo." // strPrefixAlias
                        );
                    ////
                    #endregion

                    #region // htSpCols:
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_CustomerGroupImages" // strTableNameDB
                        , "Mst_CustomerGroupImages." // strPrefixStd
                        , "mcgi." // strPrefixAlias
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
                , "zzB_Select_Mst_CustomerGroup_zzE", zzB_Select_Mst_CustomerGroup_zzE
                , "zzB_Select_Mst_CustomerGroupImages_zzE", zzB_Select_Mst_CustomerGroupImages_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_CustomerGroup)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerGroup";
            }
            if (bGet_Mst_CustomerGroupImages)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerGroupImages";
            }
            #endregion
        }
        private void Mst_CustomerGroup_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objOrgID
            , object objCustomerGrpCode
            //, object objNetworkID
            , object objCustomerGrpCodeParent
            , object objCustomerGrpLevel
            , object objCustomerGrpName
            , object objCustomerGrpDesc
            , DataSet dsData
            ////
            )
        {
            #region // Temp:
            int nTidSeq = 0;
            string strFunctionName = "Mst_CustomerGroup_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                , "objCustomerGrpCode", objCustomerGrpCode
                //, "objNetworkID", objNetworkID
                , "objCustomerGrpCodeParent", objCustomerGrpCodeParent
                , "objCustomerGrpLevel", objCustomerGrpLevel
                , "objCustomerGrpName", objCustomerGrpName
                , "objCustomerGrpDesc", objCustomerGrpDesc
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strCustomerGrpCode = TUtils.CUtils.StdParam(objCustomerGrpCode);
            //string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
            string strCustomerGrpCodeParent = TUtils.CUtils.StdParam(objCustomerGrpCodeParent);
            string strCustomerGrpLevel = TUtils.CUtils.StdParam(objCustomerGrpLevel);
            string strCustomerGrpName = string.Format("{0}", objCustomerGrpName).Trim();
            string strCustomerGrpDesc = string.Format("{0}", objCustomerGrpDesc).Trim();

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_CustomerGroup = null;
            {

                ////
                if (strOrgID == null || strOrgID.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strOrgID", strOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerGroup_Create_InvalidOrgID
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_CustomerGroup_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // objOrgID
                    , strCustomerGrpCode // objCustomerGrpCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerGroup // dtDB_Mst_CustomerGroup
                    );

                if (!string.IsNullOrEmpty(strCustomerGrpCodeParent))
                {
                    Mst_CustomerGroup_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strOrgID // objOrgID
                        , strCustomerGrpCodeParent // objCustomerGrpCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Mst_CustomerGroup // dtDB_Mst_CustomerGroup
                        );
                }
            }
            #endregion

            #region // SaveTemp Mst_CustomerGroup:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerGroup"
                    , new object[] {
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpCodeParent", TConst.BizMix.Default_DBColType,
                        "CustomerGrpBUCode", TConst.BizMix.Default_DBColType,
                        "CustomerGrpBUPattern", TConst.BizMix.Default_DBColType,
                        "CustomerGrpLevel", TConst.BizMix.Default_DBColType,
                        "CustomerGrpName", TConst.BizMix.Default_DBColType,
                        "CustomerGrpDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[] {
                        new object[]{
                            strOrgID, // strOrgID
                            strCustomerGrpCode, // strCustomerGrpCode
                            nNetworkID, // nNetworkID
                            strCustomerGrpCodeParent, // strCustomerGrpCodeParent
                            "X", // CustomerGrpBUCode
                            "X", // CustomerGrpBUPattern
                            1, // CustomerGrpLevel
                            strCustomerGrpName, // strCustomerGrpName
                            strCustomerGrpDesc, // strCustomerGrpDesc
                            TConst.Flag.Active, // FlagActive
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
                            }
                        }
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerGroupImages:
            ////
            DataTable dtInput_Mst_CustomerGroupImages = null;
            {
                ////
                string strTableCheck = "Mst_CustomerGroupImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Create_Input_Mst_CustomerGroupImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerGroupImages = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_KUNN_ValLaiSuatChangeHist.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrTCGQLTV.Mst_Spec_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerGroupImages // dtData
                    , "", "Idx" // arrstrCouple
                    , "", "OrgID" // arrstrCouple
                    , "", "CustomerGrpCode" // arrstrCouple
                    //, "", "NetworkID" // arrstrCouple
                    , "", "ImageSpec" // arrstrCouple
                                      //, "", "ImagePath" // arrstrCouple
                    , "", "ImageName" // arrstrCouple
                    , "", "ImageDesc" // arrstrCouple
                    , "StdFlag", "FlagPrimaryImage" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "ImagePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_CustomerGroupImages.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_CustomerGroupImages.Rows[nScan];
                    ////
                    ////
                    string strImagePath = null;
                    string strImageSpec = drScan["ImageSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["ImageName"].ToString());
                    ////
                    string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                    string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                    string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"]; //["TVAN_FilePath"];
                    byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                    string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                    string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                    strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                    ////
                    drScan["ImagePath"] = strImagePath;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                    #region // WriteFile:
                    ////
                    if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
                    {
                        bool exist = Directory.Exists(strFilePathBase);

                        if (!exist)
                        {
                            Directory.CreateDirectory(strFilePathBase);
                        }
                        System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
                    }
                    ////
                    #endregion
                }
            }
            #endregion

            #region //// SaveTemp Mst_CustomerGroupImages:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerGroupImages"
                    , new object[]{
                        "Idx", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerGroupImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                //string strSqlDelete = CmUtils.StringUtils.Replace(@"
                //			---- Mst_SpecDtl:
                //			delete t
                //			from Mst_SpecDtl t
                //			where (1=1)
                //				and t.KUNNNo = @strKUNNNo
                //			;

                //			---- Mst_Spec:
                //			delete t
                //			from Mst_Spec t
                //			where (1=1)
                //				and t.KUNNNo = @strKUNNNo
                //			;

                //		");
                //_cf.db.ExecQuery(
                //	strSqlDelete
                //	, "@strKUNNNo", strKUNNNo
                //	);
            }

            //// Insert All:
            {
                ////
                string zzzzClauseInsert_Mst_CustomerGroup_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerGroup:
        		        insert into Mst_CustomerGroup
                        (
                            OrgID
                            , CustomerGrpCode
                            , NetworkID
                            , CustomerGrpCodeParent
                            , CustomerGrpBUCode
                            , CustomerGrpBUPattern
                            , CustomerGrpLevel
                            , CustomerGrpName
                            , CustomerGrpDesc
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.OrgID
                            , t.CustomerGrpCode
                            , t.NetworkID
                            , t.CustomerGrpCodeParent
                            , t.CustomerGrpBUCode
                            , t.CustomerGrpBUPattern
                            , t.CustomerGrpLevel
                            , t.CustomerGrpName
                            , t.CustomerGrpDesc
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerGroup t --//[mylock]
        		        ;
        	        ");
                ////
                string zzzzClauseInsert_Mst_CustomerGroupImages_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerGroupImages:
        		        insert into Mst_CustomerGroupImages
                        (
                            Idx
                            , OrgID
                            , CustomerGrpCode
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.OrgID
                            , t.CustomerGrpCode
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerGroupImages t --//[mylock]
        		        ;
        	        ");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
        		        ----
        		        zzzzClauseInsert_Mst_CustomerGroup_zSave

        		        ----
        		        zzzzClauseInsert_Mst_CustomerGroupImages_zSave


        	        "
                    , "zzzzClauseInsert_Mst_CustomerGroup_zSave", zzzzClauseInsert_Mst_CustomerGroup_zSave
                    , "zzzzClauseInsert_Mst_CustomerGroupImages_zSave", zzzzClauseInsert_Mst_CustomerGroupImages_zSave
                    );
                ////
                //if (bMyDebugSql)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.strSqlExec", strSqlExec
                //        });
                //}
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
        		        ---- Clear for Debug:
        		        drop table #input_Mst_CustomerGroup;
        		        drop table #input_Mst_CustomerGroupImages;
        	        ");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            #region // Post Save:
            {
                Mst_CustomerGroup_UpdBU();
            }
            #endregion
        }

        private void Mst_CustomerGroup_UpdBU()
        {
            string strSqlPostSave = CmUtils.StringUtils.Replace(@"
                    declare @strCustomerGrpCode_Root nvarchar(100); select @strCustomerGrpCode_Root = 'ALL';

                    update t
                    set
	                    t.CustomerGrpBUCode = @strCustomerGrpCode_Root
	                    , t.CustomerGrpBUPattern = @strCustomerGrpCode_Root + '%'
	                    , t.CustomerGrpLevel = 1
                    from Mst_CustomerGroup t
	                    left join Mst_CustomerGroup t_Parent
		                    on t.CustomerGrpCodeParent = t_Parent.CustomerGrpCode
                    where (1=1)
	                    and t.CustomerGrpCode in (@strCustomerGrpCode_Root)
                    ;

                    declare @nDeepCustomerGrp int; select @nDeepCustomerGrp = 0;
                    while (@nDeepCustomerGrp <= 6)
                    begin
	                    select @nDeepCustomerGrp = @nDeepCustomerGrp + 1;
	
	                    update t
	                    set
		                    t.CustomerGrpBUCode = IsNull(t_Parent.CustomerGrpBUCode + '.', '') + t.CustomerGrpCode
		                    , t.CustomerGrpBUPattern = IsNull(t_Parent.CustomerGrpBUCode + '.', '') + t.CustomerGrpCode + '%'
		                    , t.CustomerGrpLevel = IsNull(t_Parent.CustomerGrpLevel, 0) + 1
	                    from Mst_CustomerGroup t
		                    left join Mst_CustomerGroup t_Parent
			                    on t.CustomerGrpCodeParent = t_Parent.CustomerGrpCode
	                    where (1=1)
		                    and t.CustomerGrpCode not in (@strCustomerGrpCode_Root)
	                    ;
                    end;
                ");
            DataSet dsPostSave = _cf.db.ExecQuery(strSqlPostSave);
        }

        private void Mst_CustomerGroup_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objOrgID
            , object objCustomerGrpCode
            //, object objNetworkID
            , object objCustomerGrpName
            , object objCustomerGrpDesc
            , object objFlagActive
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            int nTidSeq = 0;
            string strFunctionName = "Mst_CustomerGroup_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                , "objCustomerGrpCode", objCustomerGrpCode
                //, "objNetworkID", objNetworkID
                , "objCustomerGrpName", objCustomerGrpName
                , "objCustomerGrpDesc", objCustomerGrpDesc
                , "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
                });
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
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strCustomerGrpCode = TUtils.CUtils.StdParam(objCustomerGrpCode);
            //string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
            string strCustomerGrpName = string.Format("{0}",objCustomerGrpName).Trim();
            string strCustomerGrpDesc = string.Format("{0}", objCustomerGrpDesc).Trim();
            string strFlagActive = TUtils.CUtils.StdParam(objFlagActive);
            ////
            bool bUpd_CustomerGrpName = strFt_Cols_Upd.Contains("Mst_CustomerGroup.CustomerGrpName".ToUpper());
            bool bUpd_CustomerGrpDesc = strFt_Cols_Upd.Contains("Mst_CustomerGroup.CustomerGrpDesc".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_CustomerGroup.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_CustomerGroup = null;
            {
                ////
                Mst_CustomerGroup_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // strOrgID 
                    , strCustomerGrpCode // strCustomerGrpCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerGroup // dtDB_Mst_CustomerGroup
                    );
                ////
            }
            #endregion

            #region // SaveTemp Mst_CustomerGroup:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerGroup"
                    , new object[] {
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpName", TConst.BizMix.Default_DBColType,
                        "CustomerGrpDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[] {
                        new object[]{
                            strOrgID, // strOrgID
                            strCustomerGrpCode, // strCustomerGrpCode
                            nNetworkID, // nNetworkID
                            strCustomerGrpName, // strCustomerGrpName
                            strCustomerGrpDesc, // strCustomerGrpDesc
                            strFlagActive, // FlagActive
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
                            }
                        }
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerGroupImages:
            ////
            DataTable dtInput_Mst_CustomerGroupImages = null;
            {
                ////
                string strTableCheck = "Mst_CustomerGroupImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Create_Input_Mst_CustomerGroupImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerGroupImages = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_KUNN_ValLaiSuatChangeHist.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrTCGQLTV.Mst_Spec_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerGroupImages // dtData
                    , "", "Idx" // arrstrCouple
                    , "", "OrgID" // arrstrCouple
                    , "", "CustomerGrpCode" // arrstrCouple
                    //, "", "NetworkID" // arrstrCouple
                    , "", "ImageSpec" // arrstrCouple
                                      //, "", "ImagePath" // arrstrCouple
                    , "", "ImageName" // arrstrCouple
                    , "", "ImageDesc" // arrstrCouple
                    , "StdFlag", "FlagPrimaryImage" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "ImagePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_CustomerGroupImages.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_CustomerGroupImages.Rows[nScan];
                    ////
                    ////
                    string strImagePath = null;
                    string strImageSpec = drScan["ImageSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["ImageName"].ToString());
                    ////
                    string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                    string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                    string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"]; //["TVAN_FilePath"];
                    byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                    string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                    string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                    strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                    ////
                    drScan["ImagePath"] = strImagePath;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                    #region // WriteFile:
                    ////
                    if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
                    {
                        bool exist = Directory.Exists(strFilePathBase);

                        if (!exist)
                        {
                            Directory.CreateDirectory(strFilePathBase);
                        }
                        System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
                    }
                    ////
                    #endregion
                }
            }
            #endregion

            #region //// SaveTemp Mst_CustomerGroupImages:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerGroupImages"
                    , new object[]{
                        "Idx", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerGroupImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_CustomerGroupImages:
        			    delete t
                        from Mst_CustomerGroupImages t --//[mylock]
                        where (1=1)
	                        and t.OrgID = @strOrgID
	                        and t.CustomerGrpCode = @strCustomerGrpCode
                        ;
        		    ");
                _cf.db.ExecQuery(
                    strSqlDelete
                    , "@strCustomerGrpCode", strCustomerGrpCode
                    , "@strOrgID", strOrgID
                    );
            }

            //// Insert All:
            {
                string zzB_Update_Mst_CustomerGroup_ClauseSet_zzE = @"
        				t.LogLUDTimeUTC = f.LogLUDTimeUTC
        				, t.LogLUBy = f.LogLUBy
        				";

                if (bUpd_CustomerGrpName) zzB_Update_Mst_CustomerGroup_ClauseSet_zzE += ", t.CustomerGrpName = f.CustomerGrpName";
                if (bUpd_CustomerGrpDesc) zzB_Update_Mst_CustomerGroup_ClauseSet_zzE += ", t.CustomerGrpDesc = f.CustomerGrpDesc";
                if (bUpd_FlagActive) zzB_Update_Mst_CustomerGroup_ClauseSet_zzE += ", t.FlagActive = f.FlagActive";
                ////
                string zzB_Update_Mst_Spec_zzE = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_Spec:
        			    update t
        			    set
        				    zzB_Update_Mst_CustomerGroup_ClauseSet_zzE
        			    from Mst_CustomerGroup t --//[mylock]
        				    inner join #input_Mst_CustomerGroup f --//[mylock]
        					    on t.OrgID = f.OrgID
                                    and t.CustomerGrpCode = f.CustomerGrpCode
        			    ;
        		    "
                    , "zzB_Update_Mst_CustomerGroup_ClauseSet_zzE", zzB_Update_Mst_CustomerGroup_ClauseSet_zzE
                    );
                ////
                string zzzzClauseInsert_Mst_CustomerTypeImages_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerTypeImages:
        		        insert into Mst_CustomerGroupImages
                        (
                            Idx
                            , OrgID
                            , CustomerGrpCode
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.OrgID
                            , t.CustomerGrpCode
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerGroupImages t --//[mylock]
        		        ;
        	    ");
                ////

                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
        		        ----
        		        zzB_Update_Mst_Spec_zzE

        		        ----
        		        zzzzClauseInsert_Mst_CustomerTypeImages_zSave

        	        "
                    , "zzB_Update_Mst_Spec_zzE", zzB_Update_Mst_Spec_zzE
                    , "zzzzClauseInsert_Mst_CustomerTypeImages_zSave", zzzzClauseInsert_Mst_CustomerTypeImages_zSave
                    );
                ////
                //if (bMyDebugSql)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.strSqlExec", strSqlExec
                //        });
                //}
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion
        }
        private void Mst_CustomerGroup_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objOrgID
            , object objCustomerGrpCode
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_CustomerGroup_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                , "objCustomerGrpCode", objCustomerGrpCode
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strCustomerGrpCode = TUtils.CUtils.StdParam(objCustomerGrpCode);
            ////
            DataTable dtDB_Mst_CustomerGroup = null;
            {
                ////
                Mst_CustomerGroup_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // strOrgID 
                    , strCustomerGrpCode // strCustomerGrpCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerGroup // dtDB_Mst_CustomerGroup
                    );
                ////
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                        ---- Mst_CustomerGroup:
        			    delete t
                        from Mst_CustomerGroup t --//[mylock]
                        where (1=1)
	                        and t.OrgID = @strOrgID
	                        and t.CustomerGrpCode = @strCustomerGrpCode
                        ;

                        delete t
                        from Mst_CustomerGroupImages t --//[mylock]
                        where (1=1)
	                        and t.OrgID = @strOrgID
	                        and t.CustomerGrpCode = @strCustomerGrpCode
                        ;
        		    ");
                _cf.db.ExecQuery(
                    strSqlDelete
                    , "@strOrgID", strOrgID
                    , "@strCustomerGrpCode", strCustomerGrpCode
                    );
            }
            #endregion

            #region //// Clear For Debug:
            //{
            //    ////
            //    string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
        		  //      ---- Clear for Debug:
        		  //      drop table #input_Mst_CustomerType;
        	   //     ");

            //    _cf.db.ExecQuery(
            //        strSqlClearForDebug
            //        );
            //    ////
            //}
            #endregion
        }

        public DataSet Mst_CustomerGroup_Save(
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
            //bool bMyDebugSql = false;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerGroup_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
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

                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Call Func:
                ////
                Mst_CustomerGroup_SaveX(
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
                //alParamsCoupleError.AddRange(alPCErrEx);
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

        private void Mst_CustomerGroup_SaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objFlagIsDelete
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            bool bMyDebugSql = false;
            string strFunctionName = "Mst_CustomerGroup_UpdateX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerGroup_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                });
            #endregion

            #region //// Refine and Check Mst_CustomerGroup:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            string strCreateDTime = null;
            string strCreateBy = null;

            ////
            //DataTable dtDB_Mst_CustomerGroup = null;
            string strDateStart = dtimeSys.ToString("yyyy-MM-dd");

            ////
            //string strFunctionActionType = TConst.FunctionActionType.Update;
            //string strFunctionRemark = "";
            {

                ////
                strCreateDTime = string.IsNullOrEmpty(strCreateDTime) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTime;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
            }
            ////
            #endregion

            #region //// Refine and Check Mst_CustomerGroup:
            ////
            DataTable dtInput_Mst_CustomerGroup = null;
            {
                ////
                string strTableCheck = "Mst_CustomerGroup";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerGroup_Save_Input_Mst_CustomerGroupTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerGroup = dsData.Tables[strTableCheck];
            }
            #endregion

            #region //// SaveTemp Mst_CustomerGroup:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerGroup"
                    , new object[]{
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpName", TConst.BizMix.Default_DBColType,
                        "CustomerGrpDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerGroup
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerGroupImages:
            ////
            DataTable dtInput_Mst_CustomerGroupImages = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "Mst_CustomerGroupImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerGroup_Update_Input_Mst_CustomerGroupImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerGroupImages = dsData.Tables[strTableCheck];
                ////
            }
            #endregion

            #region //// SaveTemp Mst_CustomerGroupImages:
            if (!bIsDelete)
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerGroupImages"
                    , new object[]{
                        "Idx", "int",
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerGrpCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerGroupImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
							---- Mst_CustomerGroupImages:
                            delete t
                            from Mst_CustomerGroupImages t --//[mylock]
	                            inner join #input_Mst_CustomerGroup f --//[mylock]
		                            on t.CustomerGrpCode = f.CustomerGrpCode
                            where (1=1)
                            ;


                            ---- Mst_CustomerGroup:
                            delete t
                            from Mst_CustomerGroup t --//[mylock]
	                            inner join #input_Mst_CustomerGroup f --//[mylock]
		                            on t.CustomerGrpCode = f.CustomerGrpCode
                            where (1=1)
                            ;
						");
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
            }
            //// Insert All:
            if (!bIsDelete)
            {
                ////
                string zzzzClauseInsert_Mst_CustomerGroup_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_CustomerGroup:
						insert into Mst_CustomerGroup
                        (
                            OrgID
                            , CustomerGrpCode
                            , NetworkID
                            , CustomerGrpName
                            , CustomerGrpDesc
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.OrgID
                            , t.CustomerGrpCode
                            , t.NetworkID
                            , t.CustomerGrpName
                            , t.CustomerGrpDesc
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
						from #input_Mst_CustomerGroup t --//[mylock]
						;
					");
                ////
                string zzzzClauseInsert_Mst_CustomerGroupImages_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_CustomerGroupImages:
						insert into Mst_CustomerGroupImages
                        (
                            Idx
                            , OrgID
                            , CustomerGrpCode
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.OrgID
                            , t.CustomerGrpCode
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
						from #input_Mst_CustomerGroupImages t --//[mylock]
						;
					");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_Mst_CustomerGroup_zSave

						----
						zzzzClauseInsert_Mst_CustomerGroupImages_zSave
						
						----
					"
                    , "zzzzClauseInsert_Mst_CustomerGroup_zSave", zzzzClauseInsert_Mst_CustomerGroup_zSave
                    , "zzzzClauseInsert_Mst_CustomerGroupImages_zSave", zzzzClauseInsert_Mst_CustomerGroupImages_zSave
                    );
                ////
                if (bMyDebugSql)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strSqlExec", strSqlExec
                        });
                }
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_CustomerGroup;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            // Return Good:
            //return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }

        public DataSet WAS_Mst_CustomerGroup_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup
            ////
            , out RT_Mst_CustomerGroup objRT_Mst_CustomerGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerGroup.Tid;
            objRT_Mst_CustomerGroup = new RT_Mst_CustomerGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerGroup_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Mst_CustomerGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroup)
                , "Lst_Mst_CustomerGroupImages", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages)
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
                    DataTable dt_Mst_CustomerGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerGroup>(objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroup, "Mst_CustomerGroup");
                    dsData.Tables.Add(dt_Mst_CustomerGroup);
                    ////
                    DataTable dt_Mst_CustomerGroupImages = null;
                    if (objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages != null)
                    {
                        dt_Mst_CustomerGroupImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerGroupImages>(objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages, "Mst_CustomerGroupImages");
                        dsData.Tables.Add(dt_Mst_CustomerGroupImages);
                    }
                    else
                    {
                        dt_Mst_CustomerGroupImages = TDALUtils.DBUtils.GetSchema(_cf.db, "Mst_CustomerGroupImages").Tables[0];
                        dsData.Tables.Add(dt_Mst_CustomerGroupImages.Copy());
                    }
                    ////
                }
                #endregion

                #region // Mst_CustomerGroup_Save:
                mdsResult = Mst_CustomerGroup_Save(
                    objRQ_Mst_CustomerGroup.Tid // strTid
                    , objRQ_Mst_CustomerGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerGroup.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerGroup.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerGroup.FlagIsDelete // objFlagIsDelete
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

        #region // Mst_CustomerSource:
        private void Mst_CustomerSource_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objOrgID
            , object objCustomerSourceCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_CustomerSource
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_CustomerSource t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
                        and t.CustomerSourceCode = @objCustomerSourceCode
					;
				");
            dtDB_Mst_CustomerSource = _cf.db.ExecQuery(
                strSqlExec
                , "@objOrgID", objOrgID
                , "@objCustomerSourceCode", objCustomerSourceCode
                ).Tables[0];
            dtDB_Mst_CustomerSource.TableName = "Mst_CustomerSource";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_CustomerSource.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        , "Check.CustomerSourceCode", objCustomerSourceCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerSource_CheckDB_OrganNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_CustomerSource.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        , "Check.CustomerSourceCode", objCustomerSourceCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerSource_CheckDB_OrganExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_CustomerSource.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.OrgID", objOrgID
                    , "Check.CustomerSourceCode", objCustomerSourceCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_CustomerSource.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_CustomerSource_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet WAS_Mst_CustomerSource_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerSource objRQ_Mst_CustomerSource
            ////
            , out RT_Mst_CustomerSource objRT_Mst_CustomerSource
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerSource.Tid;
            objRT_Mst_CustomerSource = new RT_Mst_CustomerSource();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerSource.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerSource_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerSource_Get;
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
                List<Mst_CustomerSource> lst_Mst_CustomerSource = new List<Mst_CustomerSource>();
                List<Mst_CustomerSourceImages> lst_Mst_CustomerSourceImages = new List<Mst_CustomerSourceImages>();
                bool bGet_Mst_CustomerSource = (objRQ_Mst_CustomerSource.Rt_Cols_Mst_CustomerSource != null && objRQ_Mst_CustomerSource.Rt_Cols_Mst_CustomerSource.Length > 0);
                bool bGet_Mst_CustomerSourceImages = (objRQ_Mst_CustomerSource.Rt_Cols_Mst_CustomerSourceImages != null && objRQ_Mst_CustomerSource.Rt_Cols_Mst_CustomerSourceImages.Length > 0);
                #endregion

                #region // WS_Mst_CustomerSource_Get:
                mdsResult = Mst_CustomerSource_Get(
                    objRQ_Mst_CustomerSource.Tid // strTid
                    , objRQ_Mst_CustomerSource.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerSource.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerSource.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerSource.WAUserPassword // strUserPassword
					, objRQ_Mst_CustomerSource.AccessToken // strAccessToken
					, objRQ_Mst_CustomerSource.NetworkID // strNetworkID
					, objRQ_Mst_CustomerSource.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_CustomerSource.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_CustomerSource.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_CustomerSource.Ft_WhereClause // strFt_WhereClause
                                                              //// Return:
                    , objRQ_Mst_CustomerSource.Rt_Cols_Mst_CustomerSource // strRt_Cols_Mst_CustomerSource
                    , objRQ_Mst_CustomerSource.Rt_Cols_Mst_CustomerSourceImages //Rt_Cols_Mst_CustomerSourceImages
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_CustomerSource.MySummaryTable = lst_MySummaryTable[0];
                    /////
                    if (bGet_Mst_CustomerSource)
                    {
                        DataTable dt_Mst_CustomerSource = mdsResult.Tables["Mst_CustomerSource"].Copy();
                        lst_Mst_CustomerSource = TUtils.DataTableCmUtils.ToListof<Mst_CustomerSource>(dt_Mst_CustomerSource);
                        objRT_Mst_CustomerSource.Lst_Mst_CustomerSource = lst_Mst_CustomerSource;
                    }
                    if (bGet_Mst_CustomerSourceImages)
                    {
                        DataTable dt_Mst_CustomerSourceImages = mdsResult.Tables["Mst_CustomerSourceImages"].Copy();
                        lst_Mst_CustomerSourceImages = TUtils.DataTableCmUtils.ToListof<Mst_CustomerSourceImages>(dt_Mst_CustomerSourceImages);
                        objRT_Mst_CustomerSource.Lst_Mst_CustomerSourceImages = lst_Mst_CustomerSourceImages;
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
        public DataSet Mst_CustomerSource_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_CustomerSource
            , string strRt_Cols_Mst_CustomerSourceImages
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerSource_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerSource_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Mst_CustomerSource", strRt_Cols_Mst_CustomerSource
                    , "strRt_Cols_Mst_CustomerSourceImages", strRt_Cols_Mst_CustomerSourceImages
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
					, strOrgID_RQ // strOrgID
					, TConst.Flag.Active // strFlagUserCodeToCheck
					);

				// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Mst_CustomerSource_GetX:
				DataSet dsGetData = null;
                {
                    Mst_CustomerSource_GetX(
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
                        , strRt_Cols_Mst_CustomerSource // strRt_Cols_Mst_CustomerSource
                        , strRt_Cols_Mst_CustomerSourceImages // strRt_Cols_Mst_CustomerSourceImages
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

        public DataSet WAS_Mst_CustomerSource_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerSource objRQ_Mst_CustomerSource
            ////
            , out RT_Mst_CustomerSource objRT_Mst_CustomerSource
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerSource.Tid;
            objRT_Mst_CustomerSource = new RT_Mst_CustomerSource();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerSource.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerSource_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerSource_Create;
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
                List<Mst_CustomerSource> lst_Mst_CustomerSource = new List<Mst_CustomerSource>();
                DataSet dsData = new DataSet();
                {
                    DataTable dt_Mst_CustomerSourceImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerSourceImages>(objRQ_Mst_CustomerSource.Lst_Mst_CustomerSourceImages, "Mst_CustomerSourceImages");
                    dsData.Tables.Add(dt_Mst_CustomerSourceImages);
                }
                #endregion

                #region // WS_Mst_CustomerSource_Get:
                mdsResult = Mst_CustomerSource_Create(
                    objRQ_Mst_CustomerSource.Tid // strTid
                    , objRQ_Mst_CustomerSource.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerSource.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerSource.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerSource.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.OrgID // objOrgID
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.CustomerSourceCode // objCustomerSourceCode
                    //, objRQ_Mst_CustomerSource.Mst_CustomerSource.NetworkID // objNetworkID
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.CustomerSourceCodeParent // objCustomerSourceCodeParent
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.CustomerSourceName // objCustomerSourceName
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.CustomerSourceDesc // objCustomerSourceDesc
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
        public DataSet Mst_CustomerSource_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objCustomerSourceCode
            //, object objNetworkID
            , object objCustomerSourceCodeParent
            , object objCustomerSourceName
            , object objCustomerSourceDesc
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerSource_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerSource_Create;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
                    , "objCustomerSourceCode", objCustomerSourceCode
                    //, "objNetworkID", objNetworkID
                    , "objCustomerSourceCodeParent", objCustomerSourceCodeParent
                    , "objCustomerSourceName", objCustomerSourceName
                    , "objCustomerSourceDesc", objCustomerSourceDesc
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

                #region // Mst_CustomerSource_CreateX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerSource_CreateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
                        , objCustomerSourceCode // objCustomerSourceCode
                        //, objNetworkID // objNetworkID
                        , objCustomerSourceCodeParent // objCustomerSourceCodeParent
                        , objCustomerSourceName // objCustomerSourceName
                        , objCustomerSourceDesc // objCustomerSourceDesc
                        , dsData
                        );
                }
                ////
                //CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db); // Always Rollback.
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
        public DataSet WAS_Mst_CustomerSource_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerSource objRQ_Mst_CustomerSource
            ////
            , out RT_Mst_CustomerSource objRT_Mst_CustomerSource
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerSource.Tid;
            objRT_Mst_CustomerSource = new RT_Mst_CustomerSource();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerSource.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerSource_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerSource_Update;
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
                List<Mst_CustomerSource> lst_Mst_CustomerSource = new List<Mst_CustomerSource>();
                DataSet dsData = new DataSet();
                {
                    DataTable dt_Mst_CustomerSourceImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerSourceImages>(objRQ_Mst_CustomerSource.Lst_Mst_CustomerSourceImages, "Mst_CustomerSourceImages");
                    dsData.Tables.Add(dt_Mst_CustomerSourceImages);
                }
                #endregion

                #region // Mst_CustomerSource_Update:
                mdsResult = Mst_CustomerSource_Update(
                    objRQ_Mst_CustomerSource.Tid // strTid
                    , objRQ_Mst_CustomerSource.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerSource.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerSource.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerSource.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.OrgID // objOrgID
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.CustomerSourceCode // objCustomerSourceCode
                    //, objRQ_Mst_CustomerSource.Mst_CustomerSource.NetworkID // objNetworkID
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.CustomerSourceName // objCustomerSourceName
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.CustomerSourceDesc // objCustomerSourceDesc
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.FlagActive // objFlagActive
                    , dsData
                                                                          ////
                    , objRQ_Mst_CustomerSource.Ft_Cols_Upd // objFt_Cols_Upd
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
        public DataSet Mst_CustomerSource_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
           ////
           , object objOrgID
            , object objCustomerSourceCode
            //, object objNetworkID
            , object objCustomerSourceName
            , object objCustomerSourceDesc
            , object objFlagActive
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerSource_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerSource_Update;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
                    , "objCustomerSourceCode", objCustomerSourceCode
                    //, "objNetworkID", objNetworkID
                    , "objCustomerSourceName", objCustomerSourceName
                    , "objCustomerSourceDesc", objCustomerSourceDesc
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
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Mst_CustomerSource_UpdateX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerSource_UpdateX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
                        , objCustomerSourceCode // objCustomerSourceCode
                        //, objNetworkID // objNetworkID
                        , objCustomerSourceName // objCustomerSourceName
                        , objCustomerSourceDesc // objCustomerSourceDesc
                        , objFlagActive // objFlagActive
                        , dsData
                                     ////
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
        public DataSet WAS_Mst_CustomerSource_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerSource objRQ_Mst_CustomerSource
            ////
            , out RT_Mst_CustomerSource objRT_Mst_CustomerSource
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerSource.Tid;
            objRT_Mst_CustomerSource = new RT_Mst_CustomerSource();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerSource.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerSource_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerSource_Delete;
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
                List<Mst_CustomerSource> lst_Mst_CustomerSource = new List<Mst_CustomerSource>();
                #endregion

                #region // Mst_CustomerSource_Delete:
                mdsResult = Mst_CustomerSource_Delete(
                    objRQ_Mst_CustomerSource.Tid // strTid
                    , objRQ_Mst_CustomerSource.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerSource.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerSource.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerSource.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.OrgID // objOrgID
                    , objRQ_Mst_CustomerSource.Mst_CustomerSource.CustomerSourceCode // objOrgID
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
        public DataSet Mst_CustomerSource_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objOrgID
            , object objCustomerSourceCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerSource_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerSource_Delete;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objOrgID", objOrgID
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

                #region // Mst_CustomerSource_DeleteX:
                //DataSet dsGetData = null;
                {
                    Mst_CustomerSource_DeleteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objOrgID // objOrgID
                        , objCustomerSourceCode
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
        private void Mst_CustomerSource_GetX(
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
            , string strRt_Cols_Mst_CustomerSource
            , string strRt_Cols_Mst_CustomerSourceImages
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Mst_CustomerSource_GetX";
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
            bool bGet_Mst_CustomerSource = (strRt_Cols_Mst_CustomerSource != null && strRt_Cols_Mst_CustomerSource.Length > 0);
            bool bGet_Mst_CustomerSourceImages = (strRt_Cols_Mst_CustomerSourceImages != null && strRt_Cols_Mst_CustomerSourceImages.Length > 0);
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
            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Mst_CustomerSource_Filter_Draft:
					select distinct
	                    identity(bigint, 0, 1) MyIdxSeq
	                    , mo.OrgID
	                    , mo.CustomerSourceCode
                    into #tbl_Mst_CustomerSource_Filter_Draft
                    from Mst_CustomerSource mo --//[mylock]
	                    left join Mst_CustomerSourceImages mcsi--//[mylock]
		                    on mo.OrgID = mcsi.OrgID
			                    and mo.CustomerSourceCode = mcsi.CustomerSourceCode
                    where (1=1)
	                    zzB_Where_strFilter_zzE
                    order by mo.OrgID asc
                    ;

                    ---- Summary:
                    select Count(0) MyCount from #tbl_Mst_CustomerSource_Filter_Draft t --//[mylock]
                    ;

                    ---- #tbl_Mst_CustomerSource_Filter:
                    select
	                    t.*
                    into #tbl_Mst_CustomerSource_Filter
                    from #tbl_Mst_CustomerSource_Filter_Draft t --//[mylock]
                    where (1=1)
	                    and (t.MyIdxSeq >= @nFilterRecordStart)
	                    and (t.MyIdxSeq <= @nFilterRecordEnd)
                    ;


					-------- Mst_CustomerSource -----:
					zzB_Select_Mst_CustomerSource_zzE
					------------------------

                    -------- Mst_CustomerSourceImages -----:
					zzB_Select_Mst_CustomerSourceImages_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Mst_CustomerSource_Filter_Draft;
					--drop table #tbl_Mst_CustomerSource_Filter;
					"
                );
            ////
            string zzB_Select_Mst_CustomerSource_zzE = "-- Nothing.";
            if (bGet_Mst_CustomerSource)
            {
                #region // bGet_Mst_CustomerSource:
                zzB_Select_Mst_CustomerSource_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_CustomerSource:
					select
	                    t.MyIdxSeq
	                    , mo.*
                    from #tbl_Mst_CustomerSource_Filter t --//[mylock]
	                    inner join Mst_CustomerSource mo --//[mylock]
		                    on t.OrgID = mo.OrgID
			                    and t.CustomerSourceCode = mo.CustomerSourceCode
                    order by t.MyIdxSeq asc
                    ;
				"
                );
                #endregion
            }

            string zzB_Select_Mst_CustomerSourceImages_zzE = "-- Nothing.";
            if (bGet_Mst_CustomerSourceImages)
            {
                #region // bGet_Mst_CustomerSource:
                zzB_Select_Mst_CustomerSourceImages_zzE = CmUtils.StringUtils.Replace(@"
					---- Mst_CustomerSourceImages:
					select
	                    t.MyIdxSeq
	                    , mcsi.*
                    from #tbl_Mst_CustomerSource_Filter t --//[mylock]
	                    inner join Mst_CustomerSourceImages mcsi --//[mylock]
		                    on t.OrgID = mcsi.OrgID
			                    and t.CustomerSourceCode = mcsi.CustomerSourceCode
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
                        , "Mst_CustomerSource" // strTableNameDB
                        , "Mst_CustomerSource." // strPrefixStd
                        , "mo." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                    _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_CustomerSourceImages" // strTableNameDB
                        , "Mst_CustomerSourceImages." // strPrefixStd
                        , "mcsi." // strPrefixAlias
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
                , "zzB_Select_Mst_CustomerSource_zzE", zzB_Select_Mst_CustomerSource_zzE
                , "zzB_Select_Mst_CustomerSourceImages_zzE", zzB_Select_Mst_CustomerSourceImages_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_CustomerSource)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerSource";
            }
            if (bGet_Mst_CustomerSourceImages)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_CustomerSourceImages";
            }
            #endregion
        }
        private void Mst_CustomerSource_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objOrgID
            , object objCustomerSourceCode
            //, object objNetworkID
            , object objCustomerSourceCodeParent
            , object objCustomerSourceName
            , object objCustomerSourceDesc
            , DataSet dsData
            ////
            )
        {
            #region // Temp:
            int nTidSeq = 0;
            string strFunctionName = "Mst_CustomerSource_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                , "objCustomerSourceCode", objCustomerSourceCode
                //, "objNetworkID", objNetworkID
                , "objCustomerSourceName", objCustomerSourceName
                , "objCustomerSourceDesc", objCustomerSourceDesc
                });
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strCustomerSourceCode = TUtils.CUtils.StdParam(objCustomerSourceCode);
            string strCustomerSourceCodeParent = TUtils.CUtils.StdParam(objCustomerSourceCodeParent);
            //string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
            string strCustomerSourceName = string.Format("{0}", objCustomerSourceName).Trim();
            string strCustomerSourceDesc = string.Format("{0}", objCustomerSourceDesc).Trim();

            // drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);
            ////
            DataTable dtDB_Mst_CustomerSource = null;
            {

                ////
                if (strOrgID == null || strOrgID.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strOrgID", strOrgID
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerSource_Create_InvalidOrgID
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                Mst_CustomerSource_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // objOrgID
                    , strCustomerSourceCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerSource // dtDB_Mst_CustomerSource
                    );

                if (!string.IsNullOrEmpty(strCustomerSourceCodeParent))
                {
                    Mst_CustomerSource_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strOrgID // objOrgID
                        , strCustomerSourceCodeParent
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Mst_CustomerSource // dtDB_Mst_CustomerSource
                        );
                }
            }
            #endregion

            #region // SaveTemp Mst_CustomerGroup:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerSource"
                    , new object[] {
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceCodeParent", TConst.BizMix.Default_DBColType,
                        "CustomerSourceBUCode", TConst.BizMix.Default_DBColType,
                        "CustomerSourceBUPattern", TConst.BizMix.Default_DBColType,
                        "CustomerSourceLevel", TConst.BizMix.Default_DBColType,
                        "CustomerSourceName", TConst.BizMix.Default_DBColType,
                        "CustomerSourceDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[] {
                        new object[]{
                            strOrgID, // strOrgID
                            strCustomerSourceCode, // strCustomerSourceCode
                            nNetworkID, // nNetworkID
                            strCustomerSourceCodeParent, // strCustomerSourceCodeParent
                            "X", // CustomerGrpBUCode
                            "X", // CustomerGrpBUPattern
                            1, // CustomerGrpLevel
                            strCustomerSourceName, // strCustomerSourceName
                            strCustomerSourceDesc, // strCustomerSourceDesc
                            TConst.Flag.Active, // FlagActive
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
                            }
                        }
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerGroupImages:
            ////
            DataTable dtInput_Mst_CustomerGroupImages = null;
            {
                ////
                string strTableCheck = "Mst_CustomerSourceImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Create_Input_Mst_CustomerSourceImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerGroupImages = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_KUNN_ValLaiSuatChangeHist.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrTCGQLTV.Mst_Spec_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerGroupImages // dtData
                    , "", "Idx" // arrstrCouple
                    , "", "OrgID" // arrstrCouple
                    , "", "CustomerSourceCode" // arrstrCouple
                    //, "", "NetworkID" // arrstrCouple
                    , "", "ImageSpec" // arrstrCouple
                                      //, "", "ImagePath" // arrstrCouple
                    , "", "ImageName" // arrstrCouple
                    , "", "ImageDesc" // arrstrCouple
                    , "StdFlag", "FlagPrimaryImage" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "ImagePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerGroupImages, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_CustomerGroupImages.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_CustomerGroupImages.Rows[nScan];
                    ////
                    ////
                    string strImagePath = null;
                    string strImageSpec = drScan["ImageSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["ImageName"].ToString());
                    ////
                    string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                    string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                    string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"]; //["TVAN_FilePath"];
                    byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                    string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                    string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                    strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                    ////
                    drScan["ImagePath"] = strImagePath;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                    #region // WriteFile:
                    ////
                    if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
                    {
                        bool exist = Directory.Exists(strFilePathBase);

                        if (!exist)
                        {
                            Directory.CreateDirectory(strFilePathBase);
                        }
                        System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
                    }
                    ////
                    #endregion
                }
            }
            #endregion

            #region //// SaveTemp Mst_CustomerGroupImages:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerSourceImages"
                    , new object[]{
                        "Idx", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerGroupImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                //string strSqlDelete = CmUtils.StringUtils.Replace(@"
                //			---- Mst_SpecDtl:
                //			delete t
                //			from Mst_SpecDtl t
                //			where (1=1)
                //				and t.KUNNNo = @strKUNNNo
                //			;

                //			---- Mst_Spec:
                //			delete t
                //			from Mst_Spec t
                //			where (1=1)
                //				and t.KUNNNo = @strKUNNNo
                //			;

                //		");
                //_cf.db.ExecQuery(
                //	strSqlDelete
                //	, "@strKUNNNo", strKUNNNo
                //	);
            }

            //// Insert All:
            {
                ////
                string zzzzClauseInsert_Mst_CustomerSource_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerGroup:
        		        insert into Mst_CustomerSource
                        (
                            OrgID
                            , CustomerSourceCode
                            , NetworkID
                            , CustomerSourceCodeParent
                            , CustomerSourceBUCode
                            , CustomerSourceBUPattern
                            , CustomerSourceLevel
                            , CustomerSourceName
                            , CustomerSourceDesc
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.OrgID
                            , t.CustomerSourceCode
                            , t.NetworkID
                            , t.CustomerSourceCodeParent
                            , t.CustomerSourceBUCode
                            , t.CustomerSourceBUPattern
                            , t.CustomerSourceLevel
                            , t.CustomerSourceName
                            , t.CustomerSourceDesc
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerSource t --//[mylock]
        		        ;
        	        ");
                ////
                string zzzzClauseInsert_Mst_CustomerSourceImages_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerSourceImages:
        		        insert into Mst_CustomerSourceImages
                        (
                            Idx
                            , OrgID
                            , CustomerSourceCode
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.OrgID
                            , t.CustomerSourceCode
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerSourceImages t --//[mylock]
        		        ;
        	        ");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
        		        ----
        		        zzzzClauseInsert_Mst_CustomerSource_zSave

        		        ----
        		        zzzzClauseInsert_Mst_CustomerSourceImages_zSave


        	        "
                    , "zzzzClauseInsert_Mst_CustomerSource_zSave", zzzzClauseInsert_Mst_CustomerSource_zSave
                    , "zzzzClauseInsert_Mst_CustomerSourceImages_zSave", zzzzClauseInsert_Mst_CustomerSourceImages_zSave
                    );
                ////
                //if (bMyDebugSql)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.strSqlExec", strSqlExec
                //        });
                //}
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
        		        ---- Clear for Debug:
        		        drop table #input_Mst_CustomerSource;
        		        drop table #input_Mst_CustomerSourceImages;
        	        ");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            #region // Post Save:
            {
                Mst_CustomerSource_UpdBU();
            }
            #endregion
        }

        private void Mst_CustomerSource_UpdBU()
        {
            string strSqlPostSave = CmUtils.StringUtils.Replace(@"
                    declare @strCustomerSourceCode_Root nvarchar(100); select @strCustomerSourceCode_Root = 'ALL';

                    update t
                    set
	                    t.CustomerSourceBUCode = @strCustomerSourceCode_Root
	                    , t.CustomerSourceBUPattern = @strCustomerSourceCode_Root + '%'
	                    , t.CustomerSourceLevel = 1
                    from Mst_CustomerSource t
	                    left join Mst_CustomerSource t_Parent
		                    on t.CustomerSourceCodeParent = t_Parent.CustomerSourceCode
                    where (1=1)
	                    and t.CustomerSourceCode in (@strCustomerSourceCode_Root)
                    ;

                    declare @nDeepCustomerSource int; select @nDeepCustomerSource = 0;
                    while (@nDeepCustomerSource <= 6)
                    begin
	                    select @nDeepCustomerSource = @nDeepCustomerSource + 1;
	
	                    update t
	                    set
		                    t.CustomerSourceBUCode = IsNull(t_Parent.CustomerSourceBUCode + '.', '') + t.CustomerSourceCode
		                    , t.CustomerSourceBUPattern = IsNull(t_Parent.CustomerSourceBUCode + '.', '') + t.CustomerSourceCode + '%'
		                    , t.CustomerSourceLevel = IsNull(t_Parent.CustomerSourceLevel, 0) + 1
	                    from Mst_CustomerSource t
		                    left join Mst_CustomerSource t_Parent
			                    on t.CustomerSourceCodeParent = t_Parent.CustomerSourceCode
	                    where (1=1)
		                    and t.CustomerSourceCode not in (@strCustomerSourceCode_Root)
	                    ;
                    end;
                ");
            DataSet dsPostSave = _cf.db.ExecQuery(strSqlPostSave);
        }
        private void Mst_CustomerSource_UpdateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objOrgID
            , object objCustomerSourceCode
            //, object objNetworkID
            , object objCustomerSourceName
            , object objCustomerSourceDesc
            , object objFlagActive
            , DataSet dsData
            ////
            , object objFt_Cols_Upd
            ////
            )
        {
            #region // Temp:
            int nTidSeq = 0;
            string strFunctionName = "Mst_CustomerSource_UpdateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                , "objCustomerSourceCode", objCustomerSourceCode
                //, "objNetworkID", objNetworkID
                , "objCustomerSourceName", objCustomerSourceName
                , "objCustomerSourceDesc", objCustomerSourceDesc
                , "objFlagActive", objFlagActive
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
                });
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
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strCustomerSourceCode = TUtils.CUtils.StdParam(objCustomerSourceCode);
            //string nNetworkID = TUtils.CUtils.StdParam(objNetworkID);
            string strCustomerSourceName = string.Format("{0}", objCustomerSourceName).Trim();
            string strCustomerSourceDesc = string.Format("{0}", objCustomerSourceDesc).Trim();
            string strFlagActive = TUtils.CUtils.StdParam(objFlagActive);
            ////
            bool bUpd_CustomerSourceName = strFt_Cols_Upd.Contains("Mst_CustomerSource.CustomerSourceName".ToUpper());
            bool bUpd_CustomerSourceDesc = strFt_Cols_Upd.Contains("Mst_CustomerSource.CustomerSourceDesc".ToUpper());
            bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_CustomerSource.FlagActive".ToUpper());

            ////
            DataTable dtDB_Mst_CustomerSource = null;
            {
                ////
                Mst_CustomerSource_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // strOrgID 
                    , strCustomerSourceCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerSource // dtDB_Mst_CustomerSource
                    );
            }
            #endregion

            #region // SaveTemp Mst_CustomerSource:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerSource"
                    , new object[] {
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceName", TConst.BizMix.Default_DBColType,
                        "CustomerSourceDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[] {
                        new object[]{
                            strOrgID, // strOrgID
                            strCustomerSourceCode, // strCustomerSourceCode
                            nNetworkID, // nNetworkID
                            strCustomerSourceName, // strCustomerSourceName
                            strCustomerSourceDesc, // strCustomerSourceDesc
                            TConst.Flag.Active, // FlagActive
                            dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
                            }
                        }
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerSourceImages:
            ////
            DataTable dtInput_Mst_CustomerSourceImages = null;
            {
                ////
                string strTableCheck = "Mst_CustomerSourceImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerType_Create_Input_Mst_CustomerSourceImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerSourceImages = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_KUNN_ValLaiSuatChangeHist.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrTCGQLTV.Mst_CustomerSource_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerSourceImages // dtData
                    , "", "Idx" // arrstrCouple
                    , "", "OrgID" // arrstrCouple
                    , "", "CustomerSourceCode" // arrstrCouple
                    //, "", "NetworkID" // arrstrCouple
                    , "", "ImageSpec" // arrstrCouple
                                      //, "", "ImagePath" // arrstrCouple
                    , "", "ImageName" // arrstrCouple
                    , "", "ImageDesc" // arrstrCouple
                    , "StdFlag", "FlagPrimaryImage" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerSourceImages, "ImagePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerSourceImages, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerSourceImages, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerSourceImages, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_CustomerSourceImages, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_CustomerSourceImages.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_CustomerSourceImages.Rows[nScan];
                    ////
                    ////
                    string strImagePath = null;
                    string strImageSpec = drScan["ImageSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["ImageName"].ToString());
                    ////
                    string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                    string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                    string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"]; //["TVAN_FilePath"];
                    byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                    string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                    string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                    strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                    ////
                    drScan["ImagePath"] = strImagePath;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                    #region // WriteFile:
                    ////
                    if (!string.IsNullOrEmpty(strImageSpec) && !string.IsNullOrEmpty(strImageName))
                    {
                        bool exist = Directory.Exists(strFilePathBase);

                        if (!exist)
                        {
                            Directory.CreateDirectory(strFilePathBase);
                        }
                        System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
                    }
                    ////
                    #endregion
                }
            }
            #endregion

            #region //// SaveTemp Mst_CustomerSourceImages:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerSourceImages"
                    , new object[]{
                        "Idx", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerSourceImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_CustomerSourceImages:
        			    delete t
                        from Mst_CustomerSourceImages t --//[mylock]
                        where (1=1)
	                        and t.OrgID = @strOrgID
	                        and t.CustomerSourceCode = @strCustomerSourceCode
                        ;
        		    ");
                _cf.db.ExecQuery(
                    strSqlDelete
                    , "@strCustomerSourceCode", strCustomerSourceCode
                    , "@strOrgID", strOrgID
                    );
            }

            //// Insert All:
            {
                string zzB_Update_Mst_CustomerSource_ClauseSet_zzE = @"
        				t.LogLUDTimeUTC = f.LogLUDTimeUTC
        				, t.LogLUBy = f.LogLUBy
        				";

                if (bUpd_CustomerSourceName) zzB_Update_Mst_CustomerSource_ClauseSet_zzE += ", t.CustomerSourceName = f.CustomerSourceName";
                if (bUpd_CustomerSourceDesc) zzB_Update_Mst_CustomerSource_ClauseSet_zzE += ", t.CustomerSourceDesc = f.CustomerSourceDesc";
                if (bUpd_FlagActive) zzB_Update_Mst_CustomerSource_ClauseSet_zzE += ", t.FlagActive = f.FlagActive";
                ////
                string zzB_Update_Mst_CustomerSource_zzE = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_CustomerSource:
        			    update t
        			    set
        				    zzB_Update_Mst_CustomerSource_ClauseSet_zzE
        			    from Mst_CustomerSource t --//[mylock]
        				    inner join #input_Mst_CustomerSource f --//[mylock]
        					    on t.OrgID = f.OrgID
                                    and t.CustomerSourceCode = f.CustomerSourceCode
        			    ;
        		    "
                    , "zzB_Update_Mst_CustomerSource_ClauseSet_zzE", zzB_Update_Mst_CustomerSource_ClauseSet_zzE
                    );
                ////
                string zzzzClauseInsert_Mst_CustomerSourceImages_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_CustomerSourceImages:
        		        insert into Mst_CustomerSourceImages
                        (
                            Idx
                            , OrgID
                            , CustomerSourceCode
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.OrgID
                            , t.CustomerSourceCode
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
        		        from #input_Mst_CustomerSourceImages t --//[mylock]
        		        ;
        	    ");
                ////

                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
        		        ----
        		        zzB_Update_Mst_CustomerSource_zzE

        		        ----
        		        zzzzClauseInsert_Mst_CustomerSourceImages_zSave

        	        "
                    , "zzB_Update_Mst_CustomerSource_zzE", zzB_Update_Mst_CustomerSource_zzE
                    , "zzzzClauseInsert_Mst_CustomerSourceImages_zSave", zzzzClauseInsert_Mst_CustomerSourceImages_zSave
                    );
                ////
                //if (bMyDebugSql)
                //{
                //    alParamsCoupleError.AddRange(new object[]{
                //        "Check.strSqlExec", strSqlExec
                //        });
                //}
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
        		        ---- Clear for Debug:
        		        drop table #input_Mst_CustomerSource;
        		        drop table #input_Mst_CustomerSourceImages;
        	        ");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion
        }
        private void Mst_CustomerSource_DeleteX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objOrgID
            , object objCustomerSourceCode
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_CustomerSource_DeleteX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "objOrgID", objOrgID
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strCustomerSourceCode = TUtils.CUtils.StdParam(objCustomerSourceCode);
            ////
            DataTable dtDB_Mst_CustomerSource = null;
            {
                ////
                Mst_CustomerSource_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strOrgID // strOrgID 
                    , strCustomerSourceCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Mst_CustomerSource // dtDB_Mst_CustomerSource
                    );
                ////
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                        ---- Mst_CustomerGroup:
        			    delete t
                        from Mst_CustomerSource t --//[mylock]
                        where (1=1)
	                        and t.OrgID = @strOrgID
	                        and t.CustomerSourceCode = @strCustomerSourceCode
                        ;

                        delete t
                        from Mst_CustomerSourceImages t --//[mylock]
                        where (1=1)
	                        and t.OrgID = @strOrgID
	                        and t.CustomerSourceCode = @strCustomerSourceCode
                        ;
        		    ");
                _cf.db.ExecQuery(
                    strSqlDelete
                    , "@strOrgID", strOrgID
                    , "@strCustomerSourceCode", strCustomerSourceCode
                    );
            }
            #endregion
        }

        public DataSet Mst_CustomerSource_Save(
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
            //bool bMyDebugSql = false;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Mst_CustomerSource_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerSource_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
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

                //// Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                //// Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Call Func:
                ////
                Mst_CustomerSource_SaveX(
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
                //alParamsCoupleError.AddRange(alPCErrEx);
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

        private void Mst_CustomerSource_SaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objFlagIsDelete
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            //int nTidSeq = 0;
            bool bMyDebugSql = false;
            string strFunctionName = "Mst_CustomerSource_UpdateX";
            //string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerSource_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                });
            #endregion

            #region //// Refine and Check Mst_CustomerSource:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            string strCreateDTime = null;
            string strCreateBy = null;

            ////
            //DataTable dtDB_Mst_CustomerSource = null;
            string strDateStart = dtimeSys.ToString("yyyy-MM-dd");

            ////
            //string strFunctionActionType = TConst.FunctionActionType.Update;
            //string strFunctionRemark = "";
            {

                ////
                strCreateDTime = string.IsNullOrEmpty(strCreateDTime) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTime;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
            }
            ////
            #endregion

            #region //// Refine and Check Mst_CustomerSource:
            ////
            DataTable dtInput_Mst_CustomerSource = null;
            {
                ////
                string strTableCheck = "Mst_CustomerSource";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerSource_Save_Input_Mst_CustomerSourceTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerSource = dsData.Tables[strTableCheck];
            }
            #endregion

            #region //// SaveTemp Mst_CustomerSource:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerSource"
                    , new object[]{
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceName", TConst.BizMix.Default_DBColType,
                        "CustomerSourceDesc", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerSource
                    );
            }
            #endregion

            #region //// Refine and Check Mst_CustomerSourceImages:
            ////
            DataTable dtInput_Mst_CustomerSourceImages = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "Mst_CustomerSourceImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerSource_Update_Input_Mst_CustomerSourceImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerSourceImages = dsData.Tables[strTableCheck];
                ////
            }
            #endregion

            #region //// SaveTemp Mst_CustomerSourceImages:
            if (!bIsDelete)
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_CustomerSourceImages"
                    , new object[]{
                        "Idx", "int",
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "CustomerSourceCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ImagePath", TConst.BizMix.Default_DBColType,
                        "ImageName", TConst.BizMix.Default_DBColType,
                        "ImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_CustomerSourceImages
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
							---- Mst_CustomerSourceImages:
                            delete t
                            from Mst_CustomerSourceImages t --//[mylock]
	                            inner join #input_Mst_CustomerSource f --//[mylock]
		                            on t.CustomerSourceCode = f.CustomerSourceCode
                            where (1=1)
                            ;


                            ---- Mst_CustomerSource:
                            delete t
                            from Mst_CustomerSource t --//[mylock]
	                            inner join #input_Mst_CustomerSource f --//[mylock]
		                            on t.CustomerSourceCode = f.CustomerSourceCode
                            where (1=1)
                            ;
						");
                DataSet dtDB = _cf.db.ExecQuery(
                    strSqlDelete
                    );
            }
            //// Insert All:
            if (!bIsDelete)
            {
                ////
                string zzzzClauseInsert_Mst_CustomerSource_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_CustomerSource:
						insert into Mst_CustomerSource
                        (
                            OrgID
                            , CustomerSourceCode
                            , NetworkID
                            , CustomerSourceName
                            , CustomerSourceDesc
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.OrgID
                            , t.CustomerSourceCode
                            , t.NetworkID
                            , t.CustomerSourceName
                            , t.CustomerSourceDesc
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
						from #input_Mst_CustomerSource t --//[mylock]
						;
					");
                ////
                string zzzzClauseInsert_Mst_CustomerSourceImages_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_CustomerSourceImages:
						insert into Mst_CustomerSourceImages
                        (
                            Idx
                            , OrgID
                            , CustomerSourceCode
                            , NetworkID
                            , ImagePath
                            , ImageName
                            , ImageDesc
                            , FlagPrimaryImage
                            , FlagActive
                            , LogLUDTimeUTC
                            , LogLUBy
                        )
                        select
                            t.Idx
                            , t.OrgID
                            , t.CustomerSourceCode
                            , t.NetworkID
                            , t.ImagePath
                            , t.ImageName
                            , t.ImageDesc
                            , t.FlagPrimaryImage
                            , t.FlagActive
                            , t.LogLUDTimeUTC
                            , t.LogLUBy
						from #input_Mst_CustomerSourceImages t --//[mylock]
						;
					");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_Mst_CustomerSource_zSave

						----
						zzzzClauseInsert_Mst_CustomerSourceImages_zSave
						
						----
					"
                    , "zzzzClauseInsert_Mst_CustomerSource_zSave", zzzzClauseInsert_Mst_CustomerSource_zSave
                    , "zzzzClauseInsert_Mst_CustomerSourceImages_zSave", zzzzClauseInsert_Mst_CustomerSourceImages_zSave
                    );
                ////
                if (bMyDebugSql)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strSqlExec", strSqlExec
                        });
                }
                DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_Mst_CustomerSource;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
            #endregion

            // Return Good:
            //return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }

        public DataSet WAS_Mst_CustomerSource_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerSource objRQ_Mst_CustomerSource
            ////
            , out RT_Mst_CustomerSource objRT_Mst_CustomerSource
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerSource.Tid;
            objRT_Mst_CustomerSource = new RT_Mst_CustomerSource();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerSource.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerSource_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerSource_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Mst_CustomerSource", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerSource.Lst_Mst_CustomerSource)
                , "Lst_Mst_CustomerSourceImages", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerSource.Lst_Mst_CustomerSourceImages)
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
                    DataTable dt_Mst_CustomerSource = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerSource>(objRQ_Mst_CustomerSource.Lst_Mst_CustomerSource, "Mst_CustomerSource");
                    dsData.Tables.Add(dt_Mst_CustomerSource);
                    ////
                    DataTable dt_Mst_CustomerSourceImages = null;
                    if (objRQ_Mst_CustomerSource.Lst_Mst_CustomerSourceImages != null)
                    {
                        dt_Mst_CustomerSourceImages = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerSourceImages>(objRQ_Mst_CustomerSource.Lst_Mst_CustomerSourceImages, "Mst_CustomerSourceImages");
                        dsData.Tables.Add(dt_Mst_CustomerSourceImages);
                    }
                    else
                    {
                        dt_Mst_CustomerSourceImages = TDALUtils.DBUtils.GetSchema(_cf.db, "Mst_CustomerSourceImages").Tables[0];
                        dsData.Tables.Add(dt_Mst_CustomerSourceImages.Copy());
                    }
                    ////
                }
                #endregion

                #region // Mst_CustomerSource_Save:
                mdsResult = Mst_CustomerSource_Save(
                    objRQ_Mst_CustomerSource.Tid // strTid
                    , objRQ_Mst_CustomerSource.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerSource.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerSource.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerSource.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerSource.FlagIsDelete // objFlagIsDelete
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

        #region // Mst_CustomerInCustomerGroup:
        public void Mst_CustomerInCustomerGroup_SaveX(
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
            string strFunctionName = "Mst_CustomerInCustomerGroup_SaveX";
            //string strErrorCodeDefault = TError.ErridNTVAN.Mst_CustomerInCustomerGroup_SaveAllX;
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

            #region // Refine and Check Input Mst_CustomerInCustomerGroup:
            ////

            ////
            DataTable dtInput_Mst_CustomerInCustomerGroup = null;
            {
                ////
                string strTableCheck = "Mst_CustomerInCustomerGroup";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerInCustomerGroup_SaveX_Input_BrandTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_CustomerInCustomerGroup = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_CustomerInCustomerGroup.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_CustomerInCustomerGroup_SaveX_Input_BrandTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_CustomerInCustomerGroup // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "StdParam", "CustomerGrpCode" // arrstrCouple
                    , "StdParam", "CustomerCodeSys" // arrstrCouple
                    , "StdParam", "NetworkID" // arrstrCouple
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
                    , "#input_Mst_CustomerInCustomerGroup" // strTableName
                    , new object[] {
                            "OrgID", TConst.BizMix.Default_DBColType
                            , "CustomerGrpCode", TConst.BizMix.Default_DBColType
                            , "CustomerCodeSys", TConst.BizMix.Default_DBColType
                            , "NetworkID", TConst.BizMix.Default_DBColType
                            , "LogLUDTimeUTC", TConst.BizMix.Default_DBColType
                            , "LogLUBy", TConst.BizMix.Default_DBColType
                        } // arrSingleStructure
                    , dtInput_Mst_CustomerInCustomerGroup // dtData
                );
            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
                                ---- Mst_CustomerInCustomerGroup:
							    delete t
							    from Mst_CustomerInCustomerGroup t --//[mylock]
								    inner join #input_Mst_CustomerInCustomerGroup f --//[mylock]
									    on t.OrgID = f.OrgID
                                            and t.CustomerGrpCode = f.CustomerGrpCode
                                            --and t.CustomerCodeSys = f.CustomerCodeSys
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
                        string zzzzClauseInsert_Mst_CustomerInCustomerGroup_zSave = CmUtils.StringUtils.Replace(@"
                                insert into Mst_CustomerInCustomerGroup
                                (
                                    OrgID
                                    , CustomerGrpCode
                                    , CustomerCodeSys
                                    , NetworkID
                                    , LogLUDTimeUTC
                                    , LogLUBy
                                )
                                select
                                    t.OrgID
                                    , t.CustomerGrpCode
                                    , t.CustomerCodeSys
                                    , t.NetworkID
                                    , t.LogLUDTimeUTC
                                    , t.LogLUBy
                                from #input_Mst_CustomerInCustomerGroup t --//[mylock]
                                ;
                            ");

                        /////

                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_Mst_CustomerInCustomerGroup_zSave
							"
                            , "zzzzClauseInsert_Mst_CustomerInCustomerGroup_zSave", zzzzClauseInsert_Mst_CustomerInCustomerGroup_zSave
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
						        drop table #input_Mst_CustomerInCustomerGroup;
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
						        drop table #input_Mst_CustomerInCustomerGroup;
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

        public DataSet Mst_CustomerInCustomerGroup_Save(
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
            string strErrorCodeDefault = TError.ErridnInventory.Mst_CustomerInCustomerGroup_Save;
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
                    Mst_CustomerInCustomerGroup_SaveX(
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

        public DataSet WAS_Mst_CustomerInCustomerGroup_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_CustomerInCustomerGroup objRQ_Mst_CustomerInCustomerGroup
            ////
            , out RT_Mst_CustomerInCustomerGroup objRT_Mst_CustomerInCustomerGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_CustomerInCustomerGroup.Tid;
            objRT_Mst_CustomerInCustomerGroup = new RT_Mst_CustomerInCustomerGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerInCustomerGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_CustomerInCustomerGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_CustomerInCustomerGroup_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                , "FlagIsDelete", objRQ_Mst_CustomerInCustomerGroup.FlagIsDelete
                , "Lst_Mst_CustomerInCustomerGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerInCustomerGroup.Lst_Mst_CustomerInCustomerGroup)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Mst_CustomerInCustomerGroup> lst_Mst_CustomerInCustomerGroup = new List<Mst_CustomerInCustomerGroup>();
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Mst_CustomerInCustomerGroup = TUtils.DataTableCmUtils.ToDataTable<Mst_CustomerInCustomerGroup>(objRQ_Mst_CustomerInCustomerGroup.Lst_Mst_CustomerInCustomerGroup, "Mst_CustomerInCustomerGroup");
                    dsData.Tables.Add(dt_Mst_CustomerInCustomerGroup);
                    ////
                }
                #endregion

                #region // WS_Mst_CustomerInCustomerGroup_Create: 
                // Mst_CustomerInCustomerGroup_Save_Root_New20190704
                mdsResult = Mst_CustomerInCustomerGroup_Save(
                    objRQ_Mst_CustomerInCustomerGroup.Tid // strTid
                    , objRQ_Mst_CustomerInCustomerGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerInCustomerGroup.GwPassword // strGwPassword
                    , objRQ_Mst_CustomerInCustomerGroup.WAUserCode // strUserCode
                    , objRQ_Mst_CustomerInCustomerGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Mst_CustomerInCustomerGroup.FlagIsDelete // objFlagIsDelete
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
