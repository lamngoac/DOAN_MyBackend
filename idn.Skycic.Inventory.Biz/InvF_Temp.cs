using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
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
using System.IO;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // UploadFile:
        public void UploadFileX(
            ref ArrayList alParamsCoupleError
            , string strPartnerUserCode
            ////
            //, TDAL.IEzDAL _dbAction
            ////
            , string folderUpload
            , string fileName
            , string uploadFileAsBase64String
            ////
            , out string strFilePath
            )
        {
            #region // Temp:
            string strFunctionName = "UploadFileX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "folderUpload", folderUpload
                , "fileName", fileName
				//, "destFileName", destFileName
                ////
				});
            #endregion

            #region // Check FileType:
            string strFileTypeCode = "";
            if (!string.IsNullOrEmpty(Path.GetExtension(fileName)))
            {
                strFileTypeCode = Path.GetExtension(fileName).ToUpper();

            }
            #endregion

            #region // UploadFileNew:
            strFilePath = "";
            GC.Collect(); // Mục đích: tránh đầy bộ nhớ.
            var subFolder = DateTime.Now.ToString("yyyy-MM-dd");
            var strFolder = folderUpload + @"\" + subFolder;
            //strFolder = @"\UploadedFiles\Temp_PrintTemp\LogoFilePath\3296932000\2020-03-17";
            var strSubFolder = CheckFolderExists(strFolder);
            byte[] fileContent = Convert.FromBase64String(uploadFileAsBase64String);
            var appPath = strSubFolder + "\\" + fileName;
            strFilePath = string.Format(@"{0}\{1}", strSubFolder, fileName);
            //appPath = string.Format(@"{0}\{1}", _cf.nvcParams["InBrand_FilePath"], strFilePath); //HttpContext.Current.Server.MapPath(appPath);
            ////// 
            ///
            string strFilePathBase = string.Format(@"{0}\{1}", _cf.nvcParams["Inventory_FilePath"], strSubFolder);
            string strFilePathSave = string.Format(@"{0}\{1}\{2}", _cf.nvcParams["Inventory_FilePath"], strSubFolder, fileName);

            #region // WriteFile:
            ////
            if (!string.IsNullOrEmpty(uploadFileAsBase64String) && !string.IsNullOrEmpty(fileName))
            {
                bool exist = Directory.Exists(strFilePathBase);

                if (!exist)
                {
                    Directory.CreateDirectory(strFilePathBase);
                }
                System.IO.File.WriteAllBytes(strFilePathSave, fileContent);
            }
            #endregion

            if (string.IsNullOrEmpty(strFilePath))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.folderUpload", folderUpload
                    , "Check.fileName", fileName
                    , "Check.strFilePath", strFilePath
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.UploadFileNewX_InvalidFilePath
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion
        }
        #endregion 

        #region // Mst_TempPrintType:
        public void Mst_TempPrintType_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objTempPrintType
            , string strFlagExistToCheck
            , string strStatusListToCheck
            , out DataTable dtDB_Mst_TempPrintType
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_TempPrintType t --//[mylock]
					where (1=1)
						and t.TempPrintType = @objTempPrintType
					;
				");
            dtDB_Mst_TempPrintType = _cf.db.ExecQuery(
                strSqlExec
                , "@objTempPrintType", objTempPrintType
                ).Tables[0];
            dtDB_Mst_TempPrintType.TableName = "Mst_TempPrintType";

            // strFlagExistToCheck
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_TempPrintType.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TempPrintType", objTempPrintType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_TempPrintType_CheckDB_TempPrintTypeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_TempPrintType.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TempPrintType", objTempPrintType
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_TempPrintType_CheckDB_TempPrintTypeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strStatusListToCheck.Length > 0 && !strStatusListToCheck.Contains(Convert.ToString(dtDB_Mst_TempPrintType.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.TempPrintType", objTempPrintType
                    , "Check.strFlagActiveListToCheck", strStatusListToCheck
                    , "DB.FlagActive", dtDB_Mst_TempPrintType.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_TempPrintType_CheckDB_StatusNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public void Mst_TempPrintType_GetX(
            ref ArrayList alParamsCoupleError
            , DateTime dtimeTDateTime
            , string strTid
            , string strWAUserCode
            ////
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Mst_TempPrintType
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            string strFunctionName = "Mst_TempPrintType_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_Mst_TempPrintType", strRt_Cols_Mst_TempPrintType
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Mst_TempPrintType = (strRt_Cols_Mst_TempPrintType != null && strRt_Cols_Mst_TempPrintType.Length > 0);
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
            ////		
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_TempPrintType_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, ifmo.TempPrintType
						into #tbl_Mst_TempPrintType_Filter_Draft
						from Mst_TempPrintType ifmo --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by ifmo.TempPrintType desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_TempPrintType_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_TempPrintType_Filter:
						select
							t.*
						into #tbl_Mst_TempPrintType_Filter
						from #tbl_Mst_TempPrintType_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_TempPrintType ------:
						zzB_Select_Mst_TempPrintType_zzE
						--------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_TempPrintType_Filter_Draft;
						--drop table #tbl_Mst_TempPrintType_Filter;
					"
                );
            ////
            string zzB_Select_Mst_TempPrintType_zzE = "-- Nothing.";
            if (bGet_Mst_TempPrintType)
            {
                #region // bGet_Mst_TempPrintType:
                zzB_Select_Mst_TempPrintType_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_TempPrintType:
							select
								t.MyIdxSeq
								, ifmo.*
							from #tbl_Mst_TempPrintType_Filter t --//[mylock]
								inner join Mst_TempPrintType ifmo --//[mylock]
									on t.TempPrintType = ifmo.TempPrintType
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
                        , "Mst_TempPrintType" // strTableNameDB
                        , "Mst_TempPrintType." // strPrefixStd
                        , "ifmo." // strPrefixAlias
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
                , "zzB_Select_Mst_TempPrintType_zzE", zzB_Select_Mst_TempPrintType_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Mst_TempPrintType)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Mst_TempPrintType";
            }
            #endregion
        }


        public DataSet Mst_TempPrintType_Get(
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
            , string strRt_Cols_Mst_TempPrintType
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Mst_TempPrintType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Mst_TempPrintType_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_TempPrintType", strRt_Cols_Mst_TempPrintType
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
				Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Mst_TempPrintType_GetX:
                DataSet dsGetData = new DataSet();
                {
                    Mst_TempPrintType_GetX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strTid // strTid
                        , strWAUserCode // strWAUserCode
                                        ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Mst_TempPrintType // strRt_Cols_Mst_TempPrintType
                                                       /////
                        , out dsGetData // dsGetData
                        );
                }
                #endregion

                #region // Get Data:
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

        public DataSet WAS_Mst_TempPrintType_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Mst_TempPrintType objRQ_Mst_TempPrintType
            ////
            , out RT_Mst_TempPrintType objRT_Mst_TempPrintType
            )
        {
            #region // Temp:
            string strTid = objRQ_Mst_TempPrintType.Tid;
            objRT_Mst_TempPrintType = new RT_Mst_TempPrintType();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TempPrintType.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Mst_TempPrintType_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_TempPrintType_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                //, "objRQ_InvF_InventoryIn", TJson.JsonConvert.SerializeObject(objRQ_Mst_TempPrintType)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Mst_TempPrintType> lst_Mst_TempPrintType = new List<Mst_TempPrintType>();
                /////
                bool bGet_Mst_TempPrintType = (objRQ_Mst_TempPrintType.Rt_Cols_Mst_TempPrintType != null && objRQ_Mst_TempPrintType.Rt_Cols_Mst_TempPrintType.Length > 0);
                #endregion

                #region // WS_Mst_TempPrintType_Get:
                mdsResult = Mst_TempPrintType_Get(
                    objRQ_Mst_TempPrintType.Tid // strTid
                    , objRQ_Mst_TempPrintType.GwUserCode // strGwUserCode
                    , objRQ_Mst_TempPrintType.GwPassword // strGwPassword
                    , objRQ_Mst_TempPrintType.WAUserCode // strUserCode
                    , objRQ_Mst_TempPrintType.WAUserPassword // strUserPassword
					, objRQ_Mst_TempPrintType.AccessToken // strAccessToken
					, objRQ_Mst_TempPrintType.NetworkID // strNetworkID
					, objRQ_Mst_TempPrintType.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Mst_TempPrintType.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Mst_TempPrintType.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Mst_TempPrintType.Ft_WhereClause // strFt_WhereClause
                                                             //// Return:
                    , objRQ_Mst_TempPrintType.Rt_Cols_Mst_TempPrintType // strRt_Cols_Mst_TempPrintType
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Mst_TempPrintType.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    ////
                    if (bGet_Mst_TempPrintType)
                    {
                        ////
                        DataTable dt_Mst_TempPrintType = mdsResult.Tables["Mst_TempPrintType"].Copy();
                        lst_Mst_TempPrintType = TUtils.DataTableCmUtils.ToListof<Mst_TempPrintType>(dt_Mst_TempPrintType);
                        objRT_Mst_TempPrintType.Lst_Mst_TempPrintType = lst_Mst_TempPrintType;
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
        #endregion

        #region // InvF_TempPrint:
        public void InvF_TempPrint_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objIF_TempPrintNo
            , string strFlagExistToCheck
            , string strStatusListToCheck
            , out DataTable dtDB_InvF_TempPrint
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from InvF_TempPrint t --//[mylock]
					where (1=1)
						and t.IF_TempPrintNo = @objIF_TempPrintNo
					;
				");
            dtDB_InvF_TempPrint = _cf.db.ExecQuery(
                strSqlExec
                , "@objIF_TempPrintNo", objIF_TempPrintNo
                ).Tables[0];
            dtDB_InvF_TempPrint.TableName = "InvF_TempPrint";

            // strFlagExistToCheck
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_InvF_TempPrint.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.IF_TempPrintNo", objIF_TempPrintNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_TempPrint_CheckDB_IF_TempPrintNoNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_InvF_TempPrint.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.IF_TempPrintNo", objIF_TempPrintNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_TempPrint_CheckDB_IF_TempPrintNoExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strStatusListToCheck.Length > 0 && !strStatusListToCheck.Contains(Convert.ToString(dtDB_InvF_TempPrint.Rows[0]["IF_MOStatus"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.IF_TempPrintNo", objIF_TempPrintNo
                    , "Check.strStatusListToCheck", strStatusListToCheck
                    , "DB.FlagActive", dtDB_InvF_TempPrint.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.InvF_TempPrint_CheckDB_StatusNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public void InvF_TempPrint_GetX(
            ref ArrayList alParamsCoupleError
            , DateTime dtimeTDateTime
            , string strTid
            , string strWAUserCode
            ////
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_InvF_TempPrint
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            string strFunctionName = "InvF_TempPrint_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName // FunctionName
				, "dtimeTDateTime", dtimeTDateTime // dtimeTDateTime
                ////
				, "strFt_RecordStart", strFt_RecordStart // strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount // strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause // strFt_WhereClause
                ////
                , "strRt_Cols_InvF_TempPrint", strRt_Cols_InvF_TempPrint
                });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_InvF_TempPrint = (strRt_Cols_InvF_TempPrint != null && strRt_Cols_InvF_TempPrint.Length > 0);
            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
            ////		
            // drAbilityOfUser:
            DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
            zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                drAbilityOfUser // drAbilityOfUser
                , ref alParamsCoupleError // alParamsCoupleError
                );
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_InvF_TempPrint_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, ifmo.IF_TempPrintNo
						into #tbl_InvF_TempPrint_Filter_Draft
						from InvF_TempPrint ifmo --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by ifmo.IF_TempPrintNo desc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_InvF_TempPrint_Filter_Draft t --//[mylock]
						;

						---- #tbl_InvF_TempPrint_Filter:
						select
							t.*
						into #tbl_InvF_TempPrint_Filter
						from #tbl_InvF_TempPrint_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- InvF_TempPrint ------:
						zzB_Select_InvF_TempPrint_zzE
						--------------------------------

						---- Clear for debug:
						--drop table #tbl_InvF_TempPrint_Filter_Draft;
						--drop table #tbl_InvF_TempPrint_Filter;
					"
                );
            ////
            string zzB_Select_InvF_TempPrint_zzE = "-- Nothing.";
            if (bGet_InvF_TempPrint)
            {
                #region // bGet_InvF_TempPrint:
                zzB_Select_InvF_TempPrint_zzE = CmUtils.StringUtils.Replace(@"
							---- InvF_TempPrint:
							select
								t.MyIdxSeq
								, ifmo.*
							from #tbl_InvF_TempPrint_Filter t --//[mylock]
								inner join InvF_TempPrint ifmo --//[mylock]
									on t.IF_TempPrintNo = ifmo.IF_TempPrintNo
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
                        , "InvF_TempPrint" // strTableNameDB
                        , "InvF_TempPrint." // strPrefixStd
                        , "ifmo." // strPrefixAlias
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
                , "zzB_Select_InvF_TempPrint_zzE", zzB_Select_InvF_TempPrint_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_InvF_TempPrint)
            {
                dsGetData.Tables[nIdxTable++].TableName = "InvF_TempPrint";
            }
            #endregion
        }


        public DataSet InvF_TempPrint_Get(
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
            , string strRt_Cols_InvF_TempPrint
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_TempPrint_Get";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_TempPrint_Get;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_InvF_TempPrint", strRt_Cols_InvF_TempPrint
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
				Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // InvF_TempPrint_GetX:
                DataSet dsGetData = new DataSet();
                {
                    InvF_TempPrint_GetX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strTid // strTid
                        , strWAUserCode // strWAUserCode
                                        ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_InvF_TempPrint // strRt_Cols_InvF_TempPrint
                                                    /////
                        , out dsGetData // dsGetData
                        );
                }
                #endregion

                #region // Get Data:
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

        public DataSet WAS_InvF_TempPrint_Get(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_TempPrint objRQ_InvF_TempPrint
            ////
            , out RT_InvF_TempPrint objRT_InvF_TempPrint
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_TempPrint.Tid;
            objRT_InvF_TempPrint = new RT_InvF_TempPrint();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_TempPrint.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_TempPrint_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_TempPrint_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                //, "objRQ_InvF_InventoryIn", TJson.JsonConvert.SerializeObject(objRQ_InvF_TempPrint)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<InvF_TempPrint> lst_InvF_TempPrint = new List<InvF_TempPrint>();
                /////
                bool bGet_InvF_TempPrint = (objRQ_InvF_TempPrint.Rt_Cols_InvF_TempPrint != null && objRQ_InvF_TempPrint.Rt_Cols_InvF_TempPrint.Length > 0);
                #endregion

                #region // WS_InvF_TempPrint_Get:
                mdsResult = InvF_TempPrint_Get(
                    objRQ_InvF_TempPrint.Tid // strTid
                    , objRQ_InvF_TempPrint.GwUserCode // strGwUserCode
                    , objRQ_InvF_TempPrint.GwPassword // strGwPassword
                    , objRQ_InvF_TempPrint.WAUserCode // strUserCode
                    , objRQ_InvF_TempPrint.WAUserPassword // strUserPassword
					, objRQ_InvF_TempPrint.AccessToken // strAccessToken
					, objRQ_InvF_TempPrint.NetworkID // strNetworkID
					, objRQ_InvF_TempPrint.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_TempPrint.Ft_RecordStart // strFt_RecordStart
                    , objRQ_InvF_TempPrint.Ft_RecordCount // strFt_RecordCount
                    , objRQ_InvF_TempPrint.Ft_WhereClause // strFt_WhereClause
                                                          //// Return:
                    , objRQ_InvF_TempPrint.Rt_Cols_InvF_TempPrint // strRt_Cols_InvF_TempPrint
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_InvF_TempPrint.MySummaryTable = lst_MySummaryTable[0];
                    ////
                    ////
                    if (bGet_InvF_TempPrint)
                    {
                        ////
                        DataTable dt_InvF_TempPrint = mdsResult.Tables["InvF_TempPrint"].Copy();
                        lst_InvF_TempPrint = TUtils.DataTableCmUtils.ToListof<InvF_TempPrint>(dt_InvF_TempPrint);
                        objRT_InvF_TempPrint.Lst_InvF_TempPrint = lst_InvF_TempPrint;
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

        private void InvF_TempPrint_SaveX(
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
            , object objIF_TempPrintNo
            , object objNetworkID
            , object objOrgID
            , object objTempPrintType
            , object objIF_TempPrintName
            , object objFlagUpdloadLogoFilePathBase64
            , object objLogoFilePathName
            , object objLogoFilePathBase64
            , object objFlagUpdloadBackgroundFilePathBase64
            , object objBackgroundFilePathName
            , object objBackgroundFilePathBase64
            , object objTempPrintBody
            , object objNNTName
            , object objNNTAddress
            , object objNNTPhone
            , object objNNTFax
            , object objNNTEmail
            , object objNNTBankName
            , object objNNTAccNo
            , object objRemark
            /////
            )
        {
            #region // Temp:
            int nTidSeq = 0;
            //bool bMyDebugSql = false;
            string strFunctionName = "Temp_PrintTemp_SaveX";
            //string strErrorCodeDefault = TError.ErrTCGQLTV.Form_Receipt_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objFlagIsDelete",objFlagIsDelete
				////
                , "objIF_TempPrintNo", objIF_TempPrintNo
                , "objNetworkID", objNetworkID
                , "objOrgID", objOrgID
                , "objTempPrintType", objTempPrintType
                , "objIF_TempPrintName", objIF_TempPrintName
                , "objFlagUpdloadLogoFilePathBase64", objFlagUpdloadLogoFilePathBase64
                , "objLogoFilePathName", objLogoFilePathName
                , "objLogoFilePathBase64", objLogoFilePathBase64
                , "objFlagUpdloadBackgroundFilePathBase64", objFlagUpdloadBackgroundFilePathBase64
                , "objBackgroundFilePathName", objBackgroundFilePathName
                , "objBackgroundFilePathBase64", objBackgroundFilePathBase64
                , "objTempPrintBody", objTempPrintBody
                , "objNNTName", objNNTName
                , "objNNTAddress", objNNTAddress
                , "objNNTName", objNNTName
                , "objNNTPhone", objNNTPhone
                , "objNNTFax", objNNTFax
                , "objNNTEmail", objNNTEmail
                , "objNNTBankName", objNNTBankName
                , "objNNTAccNo", objNNTAccNo
                ////
                , "objRemark", objRemark
                /////
                });
            //ArrayList alPCErrEx = new ArrayList();
            ////
            Hashtable htParamsSql = new Hashtable();
            #endregion

            #region // Convert Input:
            //alParamsCoupleError.AddRange(new object[]{
            //    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
            //    });
            #endregion

            #region // Refine and Check Input:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            bool bIsFlagUpdloadLogoFilePathBase64 = CmUtils.StringUtils.StringEqual(objFlagUpdloadLogoFilePathBase64, TConst.Flag.Yes);
            bool bIsFlagUpdloadBackgroundFilePathBase64 = CmUtils.StringUtils.StringEqual(objFlagUpdloadBackgroundFilePathBase64, TConst.Flag.Yes);
            ////
            string strIF_TempPrintNo = TUtils.CUtils.StdParam(objIF_TempPrintNo);
            string strNetworkID = TUtils.CUtils.StdParam(objNetworkID);
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strTempPrintType = TUtils.CUtils.StdParam(objTempPrintType);
            string strIF_TempPrintName = string.Format("{0}", objIF_TempPrintName);
            string strLogoFilePathBase64 = string.Format("{0}", objLogoFilePathBase64);
            string strLogoFilePathName = string.Format("{0}", objLogoFilePathName);
            string strBackgroundFilePathName = string.Format("{0}", objBackgroundFilePathName);
            string strBackgroundFilePathBase64 = string.Format("{0}", objBackgroundFilePathBase64);
            string strTempPrintBody = string.Format("{0}", objTempPrintBody);
            string strNNTName = string.Format("{0}", objNNTName);
            string strNNTAddress = string.Format("{0}", objNNTAddress);
            string strNNTPhone = string.Format("{0}", objNNTPhone);
            string strNNTFax = string.Format("{0}", objNNTFax);
            string strNNTEmail = string.Format("{0}", objNNTEmail);
            string strNNTBankName = string.Format("{0}", objNNTBankName);
            string strNNTAccNo = string.Format("{0}", objNNTAccNo);
            string strRemark = string.Format("{0}", objRemark);
            ////
            ////
            string strCreateDTimeUTC = null;
            string strCreateBy = null;
            string strLogoFilePath = null;
            string strBackgroundFilePath = null;
            /////
            ////
            DataTable dtDB_InvF_TempPrint = null;
            {
                ////
                if (strIF_TempPrintNo == null || strIF_TempPrintNo.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strIF_TempPrintNo", strIF_TempPrintNo
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.InvF_TempPrint_SaveX_InvalidIF_TempPrintNo
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                InvF_TempPrint_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strIF_TempPrintNo // strIF_TempPrintNo
                    , "" // strFlagExistToCheck
                    , "" // strTInvoiceStatusListToCheck
                    , out dtDB_InvF_TempPrint // dtDB_InvF_TempPrint
                    );
                ////
                if (dtDB_InvF_TempPrint.Rows.Count < 1) // Chưa tồn tại
                {
                    if (bIsDelete)
                    {
                        goto MyCodeLabel_Done; // Thành công
                    }
                    else
                    {
                        // Nothing
                    }
                }
                else // Đã tồn tại Temp_PrintTemp:
                {
                    strCreateDTimeUTC = TUtils.CUtils.StdDTime(dtDB_InvF_TempPrint.Rows[0]["CreateDTimeUTC"]);
                    strCreateBy = TUtils.CUtils.StdParam(dtDB_InvF_TempPrint.Rows[0]["CreateBy"]);
                }
                strCreateDTimeUTC = string.IsNullOrEmpty(strCreateDTimeUTC) ? dtimeSys.ToString("yyyy-MM-dd HH:mm:ss") : strCreateDTimeUTC;
                strCreateBy = string.IsNullOrEmpty(strCreateBy) ? strWAUserCode : strCreateBy;
                ////
                DataTable dtDB_Mst_TempType = null;

                Mst_TempPrintType_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strTempPrintType // objTempType
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Mst_TempType // dtDB_Mst_TempType
                    );
                ////
            }
            #endregion

            #region // Upload:
            {
                if (bIsFlagUpdloadLogoFilePathBase64)
                {
                    string strfolderUpload = string.Format(@"{0}\{1}\{2}", TConst.FolderUpload.InvF_TempPrint, "LogoFilePath", strOrgID).Trim();

                    strLogoFilePathName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, strLogoFilePathName);
                    /////
                    UploadFileX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strWAUserCode // strWAUserCode
                        , strfolderUpload // strfolderUpload
                        , strLogoFilePathName // strLogoFilePathName
                        , strLogoFilePathBase64 // strLogoFilePath
                                          /////
                        , out strLogoFilePath
                        );
                    /////
                }

                if (bIsFlagUpdloadBackgroundFilePathBase64)
                {
                    string strfolderUpload = string.Format(@"{0}\{1}\{2}", TConst.FolderUpload.InvF_TempPrint, "BackgroundFilePath", strOrgID).Trim();

                    strBackgroundFilePathName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, strBackgroundFilePathName);
                    /////
                    UploadFileX(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strWAUserCode // strWAUserCode
                        , strfolderUpload // strfolderUpload
                        , strBackgroundFilePathName // strBackgroundFilePathName
                        , strBackgroundFilePathBase64 // strBackgroundFilePath
                                                /////
                        , out strBackgroundFilePath
                        );
                    /////

                }

            }
            #endregion 

            #region // SaveTemp Temp_PrintTemp:
            {
                ////
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_InvF_TempPrint"
                    , new object[]{
                        "IF_TempPrintNo", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "TempPrintType", TConst.BizMix.Default_DBColType,
                        "IF_TempPrintName", TConst.BizMix.Default_DBColType,
                        "LogoFilePath", TConst.BizMix.Default_DBColType,
                        "BackgroundFilePath", TConst.BizMix.Default_DBColType,
                        "TempPrintBody", TConst.BizMix.MyText_DBColType,
                        "NNTName", TConst.BizMix.Default_DBColType,
                        "NNTAddress", TConst.BizMix.Default_DBColType,
                        "NNTPhone", TConst.BizMix.Default_DBColType,
                        "NNTFax", TConst.BizMix.Default_DBColType,
                        "NNTEmail", TConst.BizMix.Default_DBColType,
                        "NNTBankName", TConst.BizMix.Default_DBColType,
                        "NNTAccNo", TConst.BizMix.MyText_DBColType,
                        "CreateDTimeUTC", TConst.BizMix.Default_DBColType,
                        "CreateBy", TConst.BizMix.Default_DBColType,
                        "Remark", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[]{
                            new object[]{
                                strIF_TempPrintNo, // IF_TempPrintNo
                                strNetworkID, // NetworkID
                                strOrgID, // OrgID
                                strTempPrintType, // TempPrintType
                                strIF_TempPrintName, // IF_TempPrintName
								strLogoFilePath, // LogoFilePath
								strBackgroundFilePath, // BackgroundFilePath
								strTempPrintBody, // TempPrintBody
								strNNTName, // NNTName
								strNNTAddress, // NNTAddress
								strNNTPhone, // NNTPhone
								strNNTFax, // NNTFax
								strNNTEmail, // NNTEmail
								strNNTBankName, // NNTBankName
								strNNTAccNo, // NNTAccNo
                                strCreateDTimeUTC, // CreateDTimeUTC
                                strCreateBy, // CreateBy
                                strRemark, // Remark
								TConst.Flag.Active, // FlagActive
                                dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
                                strWAUserCode, // LogLUBy
								}
                        }
                    );

            }
            #endregion

            #region // SaveDB:
            {
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
							    ---- InvF_TempPrint:
							    delete t
							    from InvF_TempPrint t --//[mylock]
								    inner join #input_InvF_TempPrint f --//[mylock]
									    on t.IF_TempPrintNo = f.IF_TempPrintNo
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
                        string zzzzClauseInsert_InvF_TempPrint_zSave = CmUtils.StringUtils.Replace(@"
                                ---- InvF_TempPrint:
                                insert into InvF_TempPrint
                                (	
	                                IF_TempPrintNo
	                                , NetworkID
	                                , OrgID
	                                , TempPrintType
	                                , IF_TempPrintName
	                                , LogoFilePath
	                                , BackgroundFilePath
	                                , TempPrintBody
	                                , NNTName
	                                , NNTAddress
	                                , NNTPhone
	                                , NNTFax
	                                , NNTEmail
	                                , NNTBankName
	                                , NNTAccNo
	                                , CreateDTimeUTC
	                                , CreateBy
	                                , Remark
	                                , FlagActive
	                                , LogLUDTimeUTC
	                                , LogLUBy
                                )
                                select
	                                t.IF_TempPrintNo
	                                , t.NetworkID
	                                , t.OrgID
	                                , t.TempPrintType
	                                , t.IF_TempPrintName
	                                , t.LogoFilePath
	                                , t.BackgroundFilePath
	                                , t.TempPrintBody
	                                , t.NNTName
	                                , t.NNTAddress
	                                , t.NNTPhone
	                                , t.NNTFax
	                                , t.NNTEmail
	                                , t.NNTBankName
	                                , t.NNTAccNo
	                                , t.CreateDTimeUTC
	                                , t.CreateBy
	                                , t.Remark
	                                , t.FlagActive
	                                , t.LogLUDTimeUTC
	                                , t.LogLUBy
                                from #input_InvF_TempPrint t --//[mylock]
                                ;
                            ");
                        string strSqlExec = CmUtils.StringUtils.Replace(@"
								----
								zzzzClauseInsert_InvF_TempPrint_zSave
			
								----
							"
                            , "zzzzClauseInsert_InvF_TempPrint_zSave", zzzzClauseInsert_InvF_TempPrint_zSave
                            );
                        ////
                        DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                        ////
                    }
                    #endregion
                }
            }
            #endregion

            #region //// Clear For Debug:
            {
                ////
                string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						---- Clear for Debug:
						drop table #input_InvF_TempPrint;
					");

                _cf.db.ExecQuery(
                    strSqlClearForDebug
                    );
                ////
            }
        #endregion

        // Return Good:
        MyCodeLabel_Done:
            return;
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            //mdsFinal.AcceptChanges();
            //return mdsFinal;		
        }
        public DataSet InvF_TempPrint_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, ref ArrayList alParamsCoupleError
            //// 
            , object objFlagIsDelete
            ////
            , object objIF_TempPrintNo
            , object objNetworkID
            , object objOrgID
            , object objTempPrintType
            , object objIF_TempPrintName
            , object objFlagUpdloadLogoFilePathBase64
            , object objLogoFilePathName
            , object objLogoFilePathBase64
            , object objFlagUpdloadBackgroundFilePathBase64
            , object objBackgroundFilePathName
            , object objBackgroundFilePathBase64
            , object objTempPrintBody
            , object objNNTName
            , object objNNTAddress
            , object objNNTPhone
            , object objNNTFax
            , object objNNTEmail
            , object objNNTBankName
            , object objNNTAccNo
            , object objRemark
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "InvF_TempPrint_Save";
            string strErrorCodeDefault = TError.ErridnInventory.InvF_TempPrint_Save;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
                , "objFlagIsDelete",objFlagIsDelete
				////
                , "objIF_TempPrintNo", objIF_TempPrintNo
                , "objNetworkID", objNetworkID
                , "objOrgID", objOrgID
                , "objTempPrintType", objTempPrintType
                , "objIF_TempPrintName", objIF_TempPrintName
                , "objFlagUpdloadLogoFilePathBase64", objFlagUpdloadLogoFilePathBase64
                , "objLogoFilePathName", objLogoFilePathName
                , "objLogoFilePathBase64", objLogoFilePathBase64
                , "objFlagUpdloadBackgroundFilePathBase64", objFlagUpdloadBackgroundFilePathBase64
                , "objBackgroundFilePathName", objBackgroundFilePathName
                , "objBackgroundFilePathBase64", objBackgroundFilePathBase64
                , "objTempPrintBody", objTempPrintBody
                , "objNNTName", objNNTName
                , "objNNTAddress", objNNTAddress
                , "objNNTName", objNNTName
                , "objNNTPhone", objNNTPhone
                , "objNNTFax", objNNTFax
                , "objNNTEmail", objNNTEmail
                , "objNNTBankName", objNNTBankName
                , "objNNTAccNo", objNNTAccNo
                ////
                , "objRemark", objRemark
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
				Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // InvF_TempPrint_SaveX:
                DataSet dsGetData = new DataSet();
                {
                    InvF_TempPrint_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objFlagIsDelete // objFlagIsDelete
                        , objIF_TempPrintNo // objIF_TempPrintNo
                        , objNetworkID // objNetworkID
                        , objOrgID // objOrgID
                        , objTempPrintType // objTempPrintType
                        , objIF_TempPrintName // objIF_TempPrintName
                        , objFlagUpdloadLogoFilePathBase64 // objFlagUpdloadLogoFilePathBase64
                        , objLogoFilePathName // objLogoFilePathName
                        , objLogoFilePathBase64 // objLogoFilePathBase64
                        , objFlagUpdloadBackgroundFilePathBase64 // objFlagUpdloadBackgroundFilePathBase64
                        , objBackgroundFilePathName // objBackgroundFilePathName
                        , objBackgroundFilePathBase64 // objBackgroundFilePathBase64
                        , objTempPrintBody // objTempPrintBody
                        , objNNTName // objNNTName
                        , objNNTAddress // objNNTAddress
                        , objNNTPhone // objNNTPhone
                        , objNNTFax // objNNTFax
                        , objNNTEmail // objNNTEmail
                        , objNNTBankName // objNNTBankName
                        , objNNTAccNo // objNNTAccNo
                        , objRemark // objRemark
                        );
                }
                #endregion

                #region // Get Data:
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


        public DataSet WAS_InvF_TempPrint_Save(
            ref ArrayList alParamsCoupleError
            , RQ_InvF_TempPrint objRQ_InvF_TempPrint
            ////
            , out RT_InvF_TempPrint objRT_InvF_TempPrint
            )
        {
            #region // Temp:
            string strTid = objRQ_InvF_TempPrint.Tid;
            objRT_InvF_TempPrint = new RT_InvF_TempPrint();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_TempPrint.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_InvF_TempPrint_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_InvF_TempPrint_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
                //, "objRQ_InvF_InventoryIn", TJson.JsonConvert.SerializeObject(objRQ_InvF_TempPrint)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // InvF_TempPrint_Save:
                mdsResult = InvF_TempPrint_Save(
                    objRQ_InvF_TempPrint.Tid // strTid
                    , objRQ_InvF_TempPrint.GwUserCode // strGwUserCode
                    , objRQ_InvF_TempPrint.GwPassword // strGwPassword
                    , objRQ_InvF_TempPrint.WAUserCode // strUserCode
                    , objRQ_InvF_TempPrint.WAUserPassword // strUserPassword
					, objRQ_InvF_TempPrint.AccessToken // strAccessToken
					, objRQ_InvF_TempPrint.NetworkID // strNetworkID
					, objRQ_InvF_TempPrint.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_InvF_TempPrint.FlagIsDelete // strFlagIsDelete
                                                        /////
                    , objRQ_InvF_TempPrint.InvF_TempPrint.IF_TempPrintNo // objIF_TempPrintNo
                    , objRQ_InvF_TempPrint.InvF_TempPrint.NetworkID // objNetworkID
                    , objRQ_InvF_TempPrint.InvF_TempPrint.OrgID // objOrgID
                    , objRQ_InvF_TempPrint.InvF_TempPrint.TempPrintType // objTempPrintType
                    , objRQ_InvF_TempPrint.InvF_TempPrint.IF_TempPrintName // objIF_TempPrintName
                    , objRQ_InvF_TempPrint.InvF_TempPrint.FlagUpdloadLogoFilePathBase64 // objFlagUpdloadLogoFilePathBase64
                    , objRQ_InvF_TempPrint.InvF_TempPrint.LogoFileName // objLogoFilePathName
                    , objRQ_InvF_TempPrint.InvF_TempPrint.LogoFilePathBase64 // objLogoFilePathBase64
                    , objRQ_InvF_TempPrint.InvF_TempPrint.FlagUpdloadBackgroundFilePathBase64 // objFlagUpdloadBackgroundFilePathBase64
                    , objRQ_InvF_TempPrint.InvF_TempPrint.BackgroundFileName // objBackgroundFilePathName
                    , objRQ_InvF_TempPrint.InvF_TempPrint.BackgroundFilePathBase64 // objBackgroundFilePathBase64
                    , objRQ_InvF_TempPrint.InvF_TempPrint.TempPrintBody // objTempPrintBody
                    , objRQ_InvF_TempPrint.InvF_TempPrint.NNTName // objNNTName
                    , objRQ_InvF_TempPrint.InvF_TempPrint.NNTAddress // objNNTAddress
                    , objRQ_InvF_TempPrint.InvF_TempPrint.NNTPhone // objNNTPhone
                    , objRQ_InvF_TempPrint.InvF_TempPrint.NNTFax // objNNTFax
                    , objRQ_InvF_TempPrint.InvF_TempPrint.NNTEmail // objNNTEmail
                    , objRQ_InvF_TempPrint.InvF_TempPrint.NNTBankName // objNNTBankName
                    , objRQ_InvF_TempPrint.InvF_TempPrint.NNTAccNo // objNNTAccNo
                    , objRQ_InvF_TempPrint.InvF_TempPrint.Remark // objRemark
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
