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
//using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
		#region // Mst_Spec:
		private void Mst_Spec_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objSpecCode
			, string strFlagExistToCheck
			, string strFlagHasSerialListToCheck
			, string strFlagHasLOTListToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Spec
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Spec t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.SpecCode = @objSpecCode
					;
				");
			dtDB_Mst_Spec = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objSpecCode", objSpecCode
				, "@dateSys", DateTime.UtcNow.ToString("yyyy-MM-dd")
				).Tables[0];
			dtDB_Mst_Spec.TableName = "Mst_Spec";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Spec.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.SpecCode", objSpecCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_CheckDB_SpecNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Spec.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.SpecCode", objSpecCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_CheckDB_SpecNotExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			// strFlagHasSerialListToCheck:
			if (strFlagHasSerialListToCheck.Length > 0 && !strFlagHasSerialListToCheck.Contains(Convert.ToString(dtDB_Mst_Spec.Rows[0]["FlagHasSerial"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.SpecCode", objSpecCode
					, "Check.strFlagHasSerialListToCheck", strFlagHasSerialListToCheck
					, "DB.FlagHasSerial", dtDB_Mst_Spec.Rows[0]["FlagHasSerial"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Spec_CheckDB_FlagHasSerialNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
			// strFlagHasLOTListToCheck:
			if (strFlagHasLOTListToCheck.Length > 0 && !strFlagHasLOTListToCheck.Contains(Convert.ToString(dtDB_Mst_Spec.Rows[0]["FlagHasLOT"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.SpecCode", objSpecCode
					, "Check.strFlagHasLOTListToCheck", strFlagHasLOTListToCheck
					, "DB.FlagActive", dtDB_Mst_Spec.Rows[0]["FlagHasLOT"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Spec_CheckDB_FlagHasLOTNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Spec.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.SpecCode", objSpecCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Spec.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Spec_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		private void Mst_Spec_CheckListDB(
			ref ArrayList alParamsCoupleError
			, DataTable dtInput_Mst_Spec
			, out DataSet dsDB_Mst_Spec
			)
		{
			#region //// SaveTemp dtInput_Mst_Spec:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_Spec"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"SpecCode", TConst.BizMix.Default_DBColType,
						"FlagExist", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
					}
					, dtInput_Mst_Spec
					);
			}
			#endregion

			#region //// Check Exist.
			{
				//// 
				string strSqlCheckDB = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Spec_Filter:
                        select
                            t.OrgID input_OrgID
                            , t.SpecCode input_SpecCode
                            , t.FlagExist input_FlagExist
                            , t.FlagActive input_FlagActive
                            -----
                            , f.OrgID DB_OrgID
                            , f.SpecCode DB_SpecCode
                            , (case
							        when (f.OrgID is null or f.SpecCode is null) then 0
							        else 1
							    end
                            ) DB_FlagExist
                            , f.FlagActive DB_FlagActive
                        into #tbl_Mst_Spec_Filter
                        from #input_Mst_Spec t --//[mylock]
						    left join Mst_Spec f --//[mylock]
							    on t.OrgID = f.OrgID 
								    and t.SpecCode = f.SpecCode
                        where (1=1)
                        ;

                        --select null tbl_Mst_Spec_Filter, * from #tbl_Mst_Spec_Filter --//[mylock];


                        ------ Return Check:
                        ---- #tbl_Mst_Spec_Exist:
                        select
						    t.*
                        --into #tbl_Mst_Spec_Exist
					    from #tbl_Mst_Spec_Filter t--//[mylock]
					    where (1=1)
                            and t.input_FlagExist != t.DB_FlagExist
                        ;

                        --select null tbl_Mst_Spec_Exist, * from #tbl_Mst_Spec_Exist --//[mylock];

                        ---- #tbl_Mst_Spec_Active:
                        select
						    t.*
                        --into #tbl_Mst_Spec_Active
					    from #tbl_Mst_Spec_Filter t--//[mylock]
					    where (1=1)
                            and t.input_FlagActive != t.DB_FlagActive
                        ;

                        --select null tbl_Mst_Spec_Active, * from #tbl_Mst_Spec_Active --//[mylock];
					"
					);

				dsDB_Mst_Spec = _cf.db.ExecQuery(strSqlCheckDB);

				//DataSet dsCheckDB = _cf.db.ExecQuery(strSqlCheckDB);

				DataTable dt_Mst_Spec_Exist = dsDB_Mst_Spec.Tables[0];
				DataTable dt_Mst_Spec_Active = dsDB_Mst_Spec.Tables[1];
				////
				if (dt_Mst_Spec_Exist.Rows.Count > 0)
				{
					//// 
					alParamsCoupleError.AddRange(new object[]{
						"Check.input_OrgID",  dt_Mst_Spec_Exist.Rows[0]["input_OrgID"]
						, "Check.input_SpecCode", dt_Mst_Spec_Exist.Rows[0]["input_SpecCode"]
						, "Check.DB_OrgID",  dt_Mst_Spec_Exist.Rows[0]["DB_OrgID"]
						, "Check.DB_SpecCode", dt_Mst_Spec_Exist.Rows[0]["DB_SpecCode"]
						, "Check.input_FlagExist", dt_Mst_Spec_Exist.Rows[0]["input_FlagExist"]
						, "Check.DB_FlagExist", dt_Mst_Spec_Exist.Rows[0]["DB_FlagExist"]
						, "Check.CondtionRaiseError", "t.input_FlagExist != t.DB_FlagExist"
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_CheckDB_SpecNotExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}

				////
				if (dt_Mst_Spec_Active.Rows.Count > 0)
				{
					//// 
					alParamsCoupleError.AddRange(new object[]{
						"Check.input_OrgID",  dt_Mst_Spec_Exist.Rows[0]["input_OrgID"]
						, "Check.input_SpecCode", dt_Mst_Spec_Exist.Rows[0]["input_SpecCode"]
						, "Check.DB_OrgID",  dt_Mst_Spec_Exist.Rows[0]["DB_OrgID"]
						, "Check.DB_SpecCode", dt_Mst_Spec_Exist.Rows[0]["DB_SpecCode"]
						, "Check.input_FlagActive", dt_Mst_Spec_Exist.Rows[0]["input_FlagActive"]
						, "Check.DB_FlagActive", dt_Mst_Spec_Exist.Rows[0]["DB_FlagActive"]
						, "Check.CondtionRaiseError", "t.input_FlagActive != t.DB_FlagActive"
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_CheckDB_FlagActiveNotMatched
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			#endregion

		}

		private void Mst_Spec_CheckDB_NetworkSpecCodeOfOrgParent(
			ref ArrayList alParamsCoupleError
			, object objNetWorkID
			, object objOrgID
			, object objSpecCode
			, object objNetworkSpecCode
			)
		{
			if (CmUtils.StringUtils.StringEqualIgnoreCase(objNetWorkID, objOrgID))
			{
				if (!CmUtils.StringUtils.StringEqualIgnoreCase(objSpecCode, objNetworkSpecCode))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.objNetWorkID", objNetWorkID
						, "Check.objOrgID", objOrgID
						, "Check.objSpecCode", objSpecCode
						, "Check.objNetworkSpecCode", objNetworkSpecCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_CheckDB_NetworkSpecCodeNoEqualSpecCode
						, null
						, alParamsCoupleError.ToArray()
						);

				}
			}
			else
			{
				Mst_Spec_CheckDB_ExistNetworkSpecCode(
					ref alParamsCoupleError
					, objNetWorkID // nNetworkID
					, objNetworkSpecCode // strNetworkSpecCode
					);
			}
		}

		private void Mst_Spec_CheckDB_ExistNetworkSpecCode(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objSpecCode
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Spec t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.SpecCode = @objSpecCode
					;
				");
			DataTable dtDB_Mst_Spec = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objSpecCode", objSpecCode
				).Tables[0];
			dtDB_Mst_Spec.TableName = "Mst_Spec";

			if (dtDB_Mst_Spec.Rows.Count < 1)
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.SpecCode", objSpecCode
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Spec_CheckDB_NotExistNetworkSpecCode
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		public DataSet WAS_Mst_Spec_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Spec objRQ_Mst_Spec
			////
			, out RT_Mst_Spec objRT_Mst_Spec
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Spec.Tid;
			objRT_Mst_Spec = new RT_Mst_Spec();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Spec_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Spec_Get;
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
				List<Mst_Spec> lst_Mst_Spec = new List<Mst_Spec>();
				List<Mst_SpecImage> lst_Mst_SpecImage = new List<Mst_SpecImage>();
				List<Mst_SpecFiles> lst_Mst_SpecFiles = new List<Mst_SpecFiles>();
				bool bGet_Mst_Spec = (objRQ_Mst_Spec.Rt_Cols_Mst_Spec != null && objRQ_Mst_Spec.Rt_Cols_Mst_Spec.Length > 0);
				bool bGet_Mst_SpecImage = (objRQ_Mst_Spec.Rt_Cols_Mst_SpecImage != null && objRQ_Mst_Spec.Rt_Cols_Mst_SpecImage.Length > 0);
				bool bGet_Mst_SpecFiles = (objRQ_Mst_Spec.Rt_Cols_Mst_SpecFiles != null && objRQ_Mst_Spec.Rt_Cols_Mst_SpecFiles.Length > 0);
				//bool bGet_Mst_SpecDtl = (objRQ_Mst_Spec.Rt_Cols_Mst_SpecDtl != null && objRQ_Mst_Spec.Rt_Cols_Mst_SpecDtl.Length > 0);
				strOrgID_Login = objRQ_Mst_Spec.OrgID;
				#endregion

				#region // WS_Mst_Spec_Get:
				mdsResult = Mst_Spec_Get(
					objRQ_Mst_Spec.Tid // strTid
					, objRQ_Mst_Spec.GwUserCode // strGwUserCode
					, objRQ_Mst_Spec.GwPassword // strGwPassword
					, objRQ_Mst_Spec.WAUserCode // strUserCode
					, objRQ_Mst_Spec.WAUserPassword // strUserPassword
					, objRQ_Mst_Spec.OrgID // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Spec.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Spec.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Spec.Ft_WhereClause // strFt_WhereClause
													//// Return:
					, objRQ_Mst_Spec.Rt_Cols_Mst_Spec // strRt_Cols_Mst_Spec
					, objRQ_Mst_Spec.Rt_Cols_Mst_SpecImage // strRt_Cols_Mst_SpecImage
					, objRQ_Mst_Spec.Rt_Cols_Mst_SpecFiles // strRt_Cols_Mst_SpecFiles
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Spec.MySummaryTable = lst_MySummaryTable[0];

					////
					if (bGet_Mst_Spec)
					{
						////
						DataTable dt_Mst_Spec = mdsResult.Tables["Mst_Spec"].Copy();
						lst_Mst_Spec = TUtils.DataTableCmUtils.ToListof<Mst_Spec>(dt_Mst_Spec);
						objRT_Mst_Spec.Lst_Mst_Spec = lst_Mst_Spec;
					}
					////
					if (bGet_Mst_SpecImage)
					{
						////
						DataTable dt_Mst_SpecImage = mdsResult.Tables["Mst_SpecImage"].Copy();
						lst_Mst_SpecImage = TUtils.DataTableCmUtils.ToListof<Mst_SpecImage>(dt_Mst_SpecImage);
						objRT_Mst_Spec.Lst_Mst_SpecImage = lst_Mst_SpecImage;
					}
					////
					if (bGet_Mst_SpecFiles)
					{
						////
						DataTable dt_Mst_SpecFiles = mdsResult.Tables["Mst_SpecFiles"].Copy();
						lst_Mst_SpecFiles = TUtils.DataTableCmUtils.ToListof<Mst_SpecFiles>(dt_Mst_SpecFiles);
						objRT_Mst_Spec.Lst_Mst_SpecFiles = lst_Mst_SpecFiles;
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


		public DataSet Mst_Spec_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strOrgID
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
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Spec_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Spec_Get;
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

				#region // Check:
				//// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_Mst_Spec = (strRt_Cols_Mst_Spec != null && strRt_Cols_Mst_Spec.Length > 0);
				bool bGet_Mst_SpecImage = (strRt_Cols_Mst_SpecImage != null && strRt_Cols_Mst_SpecImage.Length > 0);
				bool bGet_Mst_SpecFiles = (strRt_Cols_Mst_SpecFiles != null && strRt_Cols_Mst_SpecFiles.Length > 0);

				#endregion

				#region // Build Sql:
				////
				ArrayList alParamsCoupleSql = new ArrayList();
				alParamsCoupleSql.AddRange(new object[] {
					"@nFilterRecordStart", nFilterRecordStart
					, "@nFilterRecordEnd", nFilterRecordEnd
					, "@dateSys", DateTime.UtcNow.ToString("yyyy-MM-dd")
					});
				////
				////
				zzzzClauseSelect_Mst_Org_ViewAbility_Get(
					strOrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					);
				////
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Spec_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, ms.OrgID
							, ms.SpecCode
						into #tbl_Mst_Spec_Filter_Draft
						from Mst_Spec ms --//[mylock]
                            inner join #tbl_Mst_Org_ViewAbility t_mo --//[mylock]
	                            on ms.OrgID = t_mo.OrgID
							left join Mst_SpecImage msi --//[mylock]
								on ms.OrgID = msi.OrgID
								    and ms.SpecCode = msi.SpecCode
							left join Mst_SpecFiles msf --//[mylock]
								on ms.SpecCode = msf.SpecCode
							left join Mst_Model mm --//[mylock]
								on ms.OrgID = mm.OrgID
								    and ms.ModelCode = mm.ModelCode
							left join Mst_Brand mb --//[mylock]
								on mm.OrgID = mb.OrgID
								    and mm.BrandCode = mb.BrandCode
						where (1=1)
							zzB_Where_strFilter_zzE
						order by ms.SpecCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Spec_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Spec_Filter:
						select
							t.*
						into #tbl_Mst_Spec_Filter
						from #tbl_Mst_Spec_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Spec ------:
						zzB_Select_Mst_Spec_zzE
						-------------------------

						-------- Mst_SpecImage -----:
						zzB_Select_Mst_SpecImage_zzE
						-----------------------------

						-------- Mst_SpecFiles ------:
						zzB_Select_Mst_SpecFiles_zzE
						------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Spec_Filter_Draft;
						--drop table #tbl_Mst_Spec_Filter;
					"
					);
				////
				string zzB_Select_Mst_Spec_zzE = "-- Nothing.";
				if (bGet_Mst_Spec)
				{
					#region // bGet_Mst_Spec:
					zzB_Select_Mst_Spec_zzE = CmUtils.StringUtils.Replace(@"
                            ---- Mst_Spec:
							select
								t.MyIdxSeq
								, ms.*
                                , mst1.SpecType1 mst1_SpecType1
                                , mst1.SpecType1Name mst1_SpecType1Name
                                , mst2.SpecType2 mst2_SpecType2
                                , mst2.SpecType2Name mst2_SpecType2Name
								----
                                , msc.SpecCustomFieldCode msc_SpecCustomFieldCode
                                , msc.SpecCustomFieldName msc_SpecCustomFieldName
								----
                                , mm.ModelCode mm_ModelCode
								----
                                , mb.BrandCode mb_BrandCode
                                , mb.BrandName mb_BrandName
								----
                                , msp.UnitCode msp_UnitCode
								, msp.BuyPrice msp_BuyPrice
								, msp.SellPrice msp_SellPrice
								----
								, mu.UnitName msp_UnitName -- mượn
							from #tbl_Mst_Spec_Filter t --//[mylock]
								inner join Mst_Spec ms --//[mylock]
									on t.OrgID = ms.OrgID
									    and t.SpecCode = ms.SpecCode
								left join Mst_SpecType1 mst1 --//[mylock]
									on ms.OrgID = mst1.OrgID
									    and ms.SpecType1 = mst1.SpecType1
								left join Mst_SpecType2 mst2 --//[mylock]
									on ms.OrgID = mst2.OrgID
									    and ms.SpecType2 = mst2.SpecType2
								left join Mst_SpecCustomField msc --//[mylock]
									on msc.SpecCustomFieldCode = 'CUSTOMFIELD1'
									    and t.OrgID = msc.OrgID
								left join Mst_Model mm --//[mylock]
									on ms.OrgID = mm.OrgID
										and ms.ModelCode = mm.ModelCode
								left join Mst_Brand mb --//[mylock]
									on mm.OrgID = mb.OrgID
										and mm.BrandCode = mb.BrandCode
								left join Mst_SpecPrice msp --//[mylock]
									on ms.OrgID = msp.OrgID
										and ms.SpecCode = msp.SpecCode
										and ms.StandardUnitCode = msp.UnitCode
								left join Mst_Unit mu --//[mylock]
									on ms.StandardUnitCode = mu.UnitCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzB_Select_Mst_SpecImage_zzE = "-- Nothing.";
				if (bGet_Mst_SpecImage)
				{
					#region // bGet_Mst_SpecImage:
					zzB_Select_Mst_SpecImage_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_SpecImage:
							select
								t.MyIdxSeq
								, msi.*
							from #tbl_Mst_Spec_Filter t --//[mylock]
								inner join Mst_SpecImage msi --//[mylock]
									on t.SpecCode = msi.SpecCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzB_Select_Mst_SpecFiles_zzE = "-- Nothing.";
				if (bGet_Mst_SpecFiles)
				{
					#region // bGet_Mst_SpecFiles:
					zzB_Select_Mst_SpecFiles_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_SpecFiles:
							select
								t.MyIdxSeq
								, msf.*
							from #tbl_Mst_Spec_Filter t --//[mylock]
								inner join Mst_SpecFiles msf --//[mylock]
									on t.SpecCode = msf.SpecCode
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
							, "Mst_Spec" // strTableNameDB
							, "Mst_Spec." // strPrefixStd
							, "ms." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Mst_SpecImage" // strTableNameDB
							, "Mst_SpecImage." // strPrefixStd
							, "msi." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Mst_SpecFiles" // strTableNameDB
							, "Mst_SpecFiles." // strPrefixStd
							, "msf." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Mst_Model" // strTableNameDB
							, "Mst_Model." // strPrefixStd
							, "mm." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Mst_Brand" // strTableNameDB
							, "Mst_Brand." // strPrefixStd
							, "mb." // strPrefixAlias
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
					, "zzB_Select_Mst_Spec_zzE", zzB_Select_Mst_Spec_zzE
					, "zzB_Select_Mst_SpecImage_zzE", zzB_Select_Mst_SpecImage_zzE
					, "zzB_Select_Mst_SpecFiles_zzE", zzB_Select_Mst_SpecFiles_zzE
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_Mst_Spec)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Mst_Spec";
				}
				if (bGet_Mst_SpecImage)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Mst_SpecImage";
				}
				if (bGet_Mst_SpecFiles)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Mst_SpecFiles";
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

		public DataSet WAS_Mst_Spec_Add(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Spec objRQ_Mst_Spec
			////
			, out RT_Mst_Spec objRT_Mst_Spec
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Spec.Tid;
			objRT_Mst_Spec = new RT_Mst_Spec();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Spec_Add";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Spec_Add;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					, "Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Mst_Spec)
					, "Lst_Mst_SpecImage", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Lst_Mst_SpecImage)
					, "Lst_Mst_SpecFiles", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Lst_Mst_SpecFiles)
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
					DataTable dt_Mst_SpecImage = TUtils.DataTableCmUtils.ToDataTable<Mst_SpecImage>(objRQ_Mst_Spec.Lst_Mst_SpecImage, "Mst_SpecImage");
					dsData.Tables.Add(dt_Mst_SpecImage);
					////
					DataTable dt_Mst_SpecFiles = TUtils.DataTableCmUtils.ToDataTable<Mst_SpecFiles>(objRQ_Mst_Spec.Lst_Mst_SpecFiles, "Mst_SpecFiles");
					dsData.Tables.Add(dt_Mst_SpecFiles);
					strOrgID_Login = objRQ_Mst_Spec.OrgID;

				}
				#endregion

				#region // Mst_Spec_Add:
				mdsResult = Mst_Spec_Add(
					objRQ_Mst_Spec.Tid // strTid
					, objRQ_Mst_Spec.GwUserCode // strGwUserCode
					, objRQ_Mst_Spec.GwPassword // strGwPassword
					, objRQ_Mst_Spec.WAUserCode // strUserCode
					, objRQ_Mst_Spec.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Spec.Mst_Spec.OrgID // objOrgID
					, objRQ_Mst_Spec.Mst_Spec.SpecCode // objSpecCode
					, objRQ_Mst_Spec.Mst_Spec.SpecName // objSpecName
					, objRQ_Mst_Spec.Mst_Spec.SpecDesc // objSpecDesc
					, objRQ_Mst_Spec.Mst_Spec.ModelCode // objModelCode
					, objRQ_Mst_Spec.Mst_Spec.SpecType1 // objSpecType1
					, objRQ_Mst_Spec.Mst_Spec.SpecType2 // objSpecType2
					, objRQ_Mst_Spec.Mst_Spec.Color // objColor
					, objRQ_Mst_Spec.Mst_Spec.FlagHasSerial // objFlagHasSerial
					, objRQ_Mst_Spec.Mst_Spec.FlagHasLOT // objFlagHasLOT
					, objRQ_Mst_Spec.Mst_Spec.DefaultUnitCode // objDefaultUnitCode
					, objRQ_Mst_Spec.Mst_Spec.StandardUnitCode // objStandardUnitCode
					, objRQ_Mst_Spec.Mst_Spec.NetworkSpecCode // objNetworkSpecCode
					, objRQ_Mst_Spec.Mst_Spec.Remark // objRemark
					, objRQ_Mst_Spec.Mst_Spec.CustomField1 // objCustomField1
					, objRQ_Mst_Spec.Mst_Spec.CustomField2 // objCustomField2
					, objRQ_Mst_Spec.Mst_Spec.CustomField3 // objCustomField3
					, objRQ_Mst_Spec.Mst_Spec.CustomField4 // objCustomField4	
					, objRQ_Mst_Spec.Mst_Spec.CustomField5 // objCustomField5
					, objRQ_Mst_Spec.Mst_Spec.CustomField6 // objCustomField6	
					, objRQ_Mst_Spec.Mst_Spec.CustomField7 // objCustomField7
					, objRQ_Mst_Spec.Mst_Spec.CustomField8 // objCustomField8	
					, objRQ_Mst_Spec.Mst_Spec.CustomField9 // objCustomField9
					, objRQ_Mst_Spec.Mst_Spec.CustomField10 // objCustomField10		
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

		public DataSet Mst_Spec_Add(
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
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Spec_Add";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Spec_Add;
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

				#region // Call Func:
				////
				// Mst_Spec_AddX
				Mst_Spec_AddX_New20200116(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   ////
					, objOrgID // objOrgID
					, objSpecCode // objSpecCode
					, objSpecName // objSpecName
					, objSpecDesc // objSpecDesc
					, objModelCode // objModelCode
					, objSpecType1 // objSpecType1
					, objSpecType2 // objSpecType2
					, objColor // objColor
					, objFlagHasSerial // objFlagHasSerial
					, objFlagHasLOT // objFlagHasLOT
					, objDefaultUnitCode // objDefaultUnitCode
					, objStandardUnitCode // objStandardUnitCode
					, objNetworkSpecCode // objNetworkSpecCode
					, objRemark // objRemark
					, objCustomField1 // objCustomField1
					, objCustomField2 // objCustomField2
					, objCustomField3 // objCustomField3
					, objCustomField4 // objCustomField4
					, objCustomField5 // objCustomField5
					, objCustomField6 // objCustomField6
					, objCustomField7 // objCustomField7
					, objCustomField8 // objCustomField8
					, objCustomField9 // objCustomField9
					, objCustomField10 // objCustomField10
					, dsData // dsData			
							 ////
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

		private void Mst_Spec_AddX_New20200116(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
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
			//int nTidSeq = 0;
			bool bMyDebugSql = false;
			string strFunctionName = "Mst_Spec_AddX";
			//string strErrorCodeDefault = TError.ErrTCGQLTV.Mst_Spec_Add;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    ////
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
					, "objNetworkSpecCode",objNetworkSpecCode
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
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Convert Input:
			alParamsCoupleError.AddRange(new object[]{
				"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
				});
			#endregion

			#region //// Refine and Check Mst_Spec:
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strSpecCode = TUtils.CUtils.StdParam(objSpecCode);
			string strSpecName = string.Format("{0}", objSpecName).Trim();
			string strSpecDesc = string.Format("{0}", objSpecDesc).Trim();
			string strModelCode = TUtils.CUtils.StdParam(objModelCode);
			string strSpecType1 = TUtils.CUtils.StdParam(objSpecType1);
			string strSpecType2 = TUtils.CUtils.StdParam(objSpecType2);
			string strColor = string.Format("{0}", objColor).Trim();
			string strFlagHasSerial = TUtils.CUtils.StdFlag(objFlagHasSerial);
			string strFlagHasLOT = TUtils.CUtils.StdFlag(objFlagHasLOT);
			string strDefaultUnitCode = TUtils.CUtils.StdParam(objDefaultUnitCode);
			string strStandardUnitCode = TUtils.CUtils.StdParam(objStandardUnitCode);
			string strNetworkSpecCode = TUtils.CUtils.StdParam(objNetworkSpecCode);
			string strRemark = string.Format("{0}", objRemark).Trim();
			string strCustomField1 = string.Format("{0}", objCustomField1).Trim();
			string strCustomField2 = string.Format("{0}", objCustomField2).Trim();
			string strCustomField3 = string.Format("{0}", objCustomField3).Trim();
			string strCustomField4 = string.Format("{0}", objCustomField4).Trim();
			string strCustomField5 = string.Format("{0}", objCustomField5).Trim();
			string strCustomField6 = string.Format("{0}", objCustomField6).Trim();
			string strCustomField7 = string.Format("{0}", objCustomField7).Trim();
			string strCustomField8 = string.Format("{0}", objCustomField8).Trim();
			string strCustomField9 = string.Format("{0}", objCustomField9).Trim();
			string strCustomField10 = string.Format("{0}", objCustomField10).Trim();

			DataTable dtDB_Mst_Spec = null;
			{
				////
				Mst_Spec_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strSpecCode // objSpecCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagHasSerialListToCheck 
					, "" // strFlagHasLOTListToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Spec // dtDB_Mst_Spec
					);
				////
				if (strSpecCode == null || strSpecCode.Length <= 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSpecCode", strSpecCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_Add_InvalidSpecCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strSpecName == null || strSpecName.Length <= 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSpecName", strSpecName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_Add_InvalidModelName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (!string.IsNullOrEmpty(strModelCode))
				{
					////
					DataTable dtDB_Mst_Model = null;

					Mst_Model_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // objOrgID
						, strModelCode // objModelCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_Model // dtDB_Mst_Model
						);
				}
				////
				if (!string.IsNullOrEmpty(strSpecType1))
				{
					////
					DataTable dtDB_Mst_SpecType1 = null;

					Mst_SpecType1_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // objOrgID
						, strSpecType1 // objSpecType1
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecType1 // dtDB_Mst_SpecType1
						);
				}
				////
				if (!string.IsNullOrEmpty(strSpecType2))
				{
					////
					DataTable dtDB_Mst_SpecType2 = null;

					Mst_SpecType2_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // objOrgID
						, strSpecType2 // objSpecType2
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecType2 // dtDB_Mst_SpecType2
						);
				}
				////
				if (!string.IsNullOrEmpty(strDefaultUnitCode))
				{
					////
					DataTable dtDB_Mst_Unit = null;

					Mst_Unit_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strDefaultUnitCode // objDefaultUnitCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_Unit // dtDB_Mst_Unit
						);
				}
				////
				if (!string.IsNullOrEmpty(strStandardUnitCode))
				{
					////
					DataTable dtDB_Mst_Unit = null;

					Mst_Unit_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strStandardUnitCode // objStandardUnitCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_Unit // dtDB_Mst_Unit
						);
				}
				////
				if (!string.IsNullOrEmpty(strCustomField1))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField1 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (!string.IsNullOrEmpty(strCustomField2))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField2 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				/////
				if (!string.IsNullOrEmpty(strCustomField3))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField3 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				/////
				if (!string.IsNullOrEmpty(strCustomField4))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField4 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				/////
				if (!string.IsNullOrEmpty(strCustomField5))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField5 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (!string.IsNullOrEmpty(strCustomField6))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField6 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (!string.IsNullOrEmpty(strCustomField7))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField7 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				/////
				if (!string.IsNullOrEmpty(strCustomField8))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField8 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				/////
				if (!string.IsNullOrEmpty(strCustomField9))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField9 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				/////
				if (!string.IsNullOrEmpty(strCustomField10))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField10 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				/////
				if (CmUtils.StringUtils.StringEqualIgnoreCase(nNetworkID, objOrgID)) strNetworkSpecCode = strSpecCode;
				// Check : Check tồn tại trong list Mã Brand của Org cha
				if (!string.IsNullOrEmpty(strNetworkSpecCode))
				{
					////
					myCommon_CheckOrgParent(
						ref alParamsCoupleError // alParamsCoupleError
						, nNetworkID // nNetworkID
						, strOrgID_Login // strOrgID
						);
					////
					Mst_Spec_CheckDB_NetworkSpecCodeOfOrgParent(
						ref alParamsCoupleError // alParamsCoupleError
						, nNetworkID // nNetworkID
						, strOrgID // strOrgID
						, strSpecCode // strSpecCode
						, strNetworkSpecCode // strNetworkSpecCode
						);
					//              ////
					//              Mst_Spec_CheckDB_ExistNetworkSpecCode(
					//ref alParamsCoupleError
					//, nNetworkID // nNetworkID
					//, strNetworkSpecCode // strNetworkSpecCode
					//);
				}
				////


			}
			////
			#endregion

			#region //// SaveTemp Mst_Spec:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_Spec"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"SpecCode", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"SpecName", TConst.BizMix.Default_DBColType,
						"SpecDesc", TConst.BizMix.Default_DBColType,
						"ModelCode", TConst.BizMix.Default_DBColType,
						"SpecType1", TConst.BizMix.Default_DBColType,
						"SpecType2", TConst.BizMix.Default_DBColType,
						"Color", TConst.BizMix.Default_DBColType,
						"FlagHasSerial", TConst.BizMix.Default_DBColType,
						"FlagHasLOT", TConst.BizMix.Default_DBColType,
						"DefaultUnitCode", TConst.BizMix.Default_DBColType,
						"StandardUnitCode", TConst.BizMix.Default_DBColType,
						"NetworkSpecCode", TConst.BizMix.Default_DBColType,
						"Remark", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"CustomField1", TConst.BizMix.Default_DBColType,
						"CustomField2", TConst.BizMix.Default_DBColType,
						"CustomField3", TConst.BizMix.Default_DBColType,
						"CustomField4", TConst.BizMix.Default_DBColType,
						"CustomField5", TConst.BizMix.Default_DBColType,
						"CustomField6", TConst.BizMix.Default_DBColType,
						"CustomField7", TConst.BizMix.Default_DBColType,
						"CustomField8", TConst.BizMix.Default_DBColType,
						"CustomField9", TConst.BizMix.Default_DBColType,
						"CustomField10", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
						new object[]{
							strOrgID, // OrgID
                            strSpecCode, // SpecCode
        			        nNetworkID, // NetworkID
        			        strSpecName, // SpecName								
        			        strSpecDesc , // SpecDesc
                            strModelCode , // ModelCode
        			        strSpecType1, // SpecType1
        			        strSpecType2, // SpecType2
        			        strColor, // Color
        			        strFlagHasSerial, // FlagHasSerial
        			        strFlagHasLOT , // FlagHasLOT
        			        strDefaultUnitCode, // DefaultUnitCode
        			        strStandardUnitCode, // StandardUnitCode
                            strNetworkSpecCode, // NetworkSpecCode
        			        strRemark, // Remark
        			        TConst.Flag.Active, // FlagActive
        			        strCustomField1 , // CustomField1
        			        strCustomField2 , // CustomField2
        			        strCustomField3 , // CustomField3
        			        strCustomField4 , // CustomField4
        			        strCustomField5 , // CustomField5
        			        strCustomField6 , // CustomField6
        			        strCustomField7 , // CustomField7
        			        strCustomField8 , // CustomField8
        			        strCustomField9 , // CustomField9
        			        strCustomField10 , // CustomField10
        			        dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
        			        }
						}
					);
			}
			#endregion

			#region //// Refine and Check Mst_SpecImage:
			////
			DataTable dtInput_Mst_SpecImage = null;
			{
				////
				string strTableCheck = "Mst_SpecImage";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_Add_Input_Mst_SpecImageTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_SpecImage = dsData.Tables[strTableCheck];
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
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_SpecImage // dtData
					, "", "SpecImageSpec" // arrstrCouple
					, "", "SpecImageName" // arrstrCouple
					, "", "SpecImageDesc" // arrstrCouple
					, "StdFlag", "FlagPrimaryImage" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "OrgID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "SpecCode", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "SpecImagePath", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Mst_SpecImage.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_SpecImage.Rows[nScan];
					////
					string strSpecImagePath = null;
					string strSpecImageSpec = drScan["SpecImageSpec"].ToString();
					string strSpecImageName = drScan["SpecImageName"].ToString();
					////
					DataTable dtDB_Mst_Param = null;
					Mst_Param_CheckDB(
						ref alParamsCoupleError
						, TConst.Mst_Param.PARAM_UPLOADFILE
						, TConst.Flag.Yes
						, out dtDB_Mst_Param
						);

					string folderUpload = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
					////
					string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
					string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
					byte[] strDeCodeBase64 = Convert.FromBase64String(strSpecImageSpec);
					string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strSpecImageName);
					string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
					strSpecImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strSpecImageName);
					////
					drScan["OrgID"] = strOrgID;
					drScan["SpecCode"] = strSpecCode;
					drScan["SpecImagePath"] = strSpecImagePath;
					drScan["NetworkID"] = nNetworkID;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
					#region // WriteFile:
					////
					if (!string.IsNullOrEmpty(strSpecImageSpec) && !string.IsNullOrEmpty(strSpecImageName))
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
					, "#input_Mst_SpecImage"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"SpecCode", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"SpecImagePath", TConst.BizMix.Default_DBColType,
						"SpecImageName", TConst.BizMix.Default_DBColType,
						"SpecImageDesc", TConst.BizMix.Default_DBColType,
						"FlagPrimaryImage", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Mst_SpecImage
					);
			}
			#endregion

			#region //// Refine and Check Mst_SpecFiles:
			////
			DataTable dtInput_Mst_SpecFiles = null;
			{
				////
				string strTableCheck = "Mst_SpecFiles";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_Add_Input_Mst_SpecFilesTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_SpecFiles = dsData.Tables[strTableCheck];
				////
				//if (dtInput_KUNN_FileUpload.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErrTCGQLTV.Mst_Spec_Add_Input_KUNN_FileUploadTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_SpecFiles // dtData
					, "", "SpecFileSpec" // arrstrCouple
					, "", "SpecFileName" // arrstrCouple
					, "", "SpecFileDesc" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "OrgID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "SpecCode", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "SpecFilePath", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Mst_SpecFiles.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_SpecFiles.Rows[nScan];
					////
					string strSpecFilePath = null;
					string strSpecFileSpec = drScan["SpecFileSpec"].ToString();
					string strSpecFileName = drScan["SpecFileName"].ToString();
					////
					DataTable dtDB_Mst_Param = null;
					Mst_Param_CheckDB(
						ref alParamsCoupleError
						, TConst.Mst_Param.PARAM_UPLOADFILE
						, TConst.Flag.Yes
						, out dtDB_Mst_Param
						);

					string folderUpload = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
					////
					string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
					string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
					byte[] strDeCodeBase64 = Convert.FromBase64String(strSpecFileSpec);
					string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strSpecFileName);
					string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
					strSpecFilePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strSpecFileName);
					////
					drScan["OrgID"] = strOrgID;
					drScan["SpecCode"] = strSpecCode;
					drScan["SpecFilePath"] = strSpecFilePath;
					drScan["NetworkID"] = nNetworkID;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
					#region // WriteFile:
					////
					if (!string.IsNullOrEmpty(strSpecFileSpec) && !string.IsNullOrEmpty(strSpecFileName))
					{
						bool exist = Directory.Exists(strFilePathBase);

						if (!exist)
						{
							Directory.CreateDirectory(strFilePathBase);
						}
						System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
					}
					#endregion
				}
			}
			#endregion

			#region //// SaveTemp Mst_SpecFiles:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_SpecFiles"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"SpecCode", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"SpecFilePath", TConst.BizMix.Default_DBColType,
						"SpecFileName", TConst.BizMix.Default_DBColType,
						"SpecFileDesc", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Mst_SpecFiles
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
				string zzzzClauseInsert_Mst_Spec_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_Spec:
        		        insert into Mst_Spec
        		        (
                            OrgID
                            , SpecCode
                            , NetworkID
                            , SpecName
                            , SpecDesc
                            , ModelCode
                            , SpecType1
                            , SpecType2
                            , Color
                            , FlagHasSerial
                            , FlagHasLOT
                            , DefaultUnitCode
                            , StandardUnitCode
                            , NetworkSpecCode
                            , Remark
                            , FlagActive
                            , CustomField1
                            , CustomField2
                            , CustomField3
                            , CustomField4
                            , CustomField5
                            , CustomField6
                            , CustomField7
                            , CustomField8
                            , CustomField9
                            , CustomField10
        			        , LogLUDTimeUTC
        			        , LogLUBy
        		        )
        		        select 
        			        t.OrgID
        			        , t.SpecCode
        			        , t.NetworkID
        			        , t.SpecName
        			        , t.SpecDesc
                            , t.ModelCode
        			        , t.SpecType1
        			        , t.SpecType2
        			        , t.Color
        			        , t.FlagHasSerial
        			        , t.FlagHasLOT
        			        , t.DefaultUnitCode
        			        , t.StandardUnitCode
                            , t.NetworkSpecCode
        			        , t.Remark
        			        , t.FlagActive
        			        , t.CustomField1
        			        , t.CustomField2
        			        , t.CustomField3
        			        , t.CustomField4
        			        , t.CustomField5
        			        , t.CustomField6
        			        , t.CustomField7
        			        , t.CustomField8
        			        , t.CustomField9
        			        , t.CustomField10
        			        , t.LogLUDTimeUTC
        			        , t.LogLUBy
        		        from #input_Mst_Spec t --//[mylock]
        		        ;
        	        ");
				////
				string zzzzClauseInsert_Mst_SpecImage_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_SpecImage:
        		        insert into Mst_SpecImage
        		        (
        			        OrgID
        			        , SpecCode
        			        , NetworkID
        			        , SpecImagePath
        			        , SpecImageName
        			        , SpecImageDesc
        			        , FlagPrimaryImage
        			        , LogLUDTimeUTC
        			        , LogLUBy
        		        )
        		        select 
        			        t.OrgID
        			        , t.SpecCode
        			        , t.NetworkID
        			        , t.SpecImagePath
        			        , t.SpecImageName
        			        , t.SpecImageDesc
        			        , t.FlagPrimaryImage
        			        , t.LogLUDTimeUTC
        			        , t.LogLUBy
        		        from #input_Mst_SpecImage t --//[mylock]
        		        ;
        	        ");
				////
				string zzzzClauseInsert_Mst_SpecFiles_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_SpecFiles:
        		        insert into Mst_SpecFiles
        		        (
        			        OrgID
        			        , SpecCode
        			        , NetworkID
        			        , SpecFilePath
        			        , SpecFileName
        			        , SpecFileDesc
        			        , LogLUDTimeUTC
        			        , LogLUBy
        		        )
        		        select 
        			        t.OrgID
        			        , t.SpecCode
        			        , t.NetworkID
        			        , t.SpecFilePath
        			        , t.SpecFileName
        			        , t.SpecFileDesc
        			        , t.LogLUDTimeUTC
        			        , t.LogLUBy
        		        from #input_Mst_SpecFiles t --//[mylock]
        		        ;
                    ");
				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
        		        ----
        		        zzzzClauseInsert_Mst_Spec_zSave

        		        ----
        		        zzzzClauseInsert_Mst_SpecImage_zSave

        		        ----
        		        zzzzClauseInsert_Mst_SpecFiles_zSave
        	        "
					, "zzzzClauseInsert_Mst_Spec_zSave", zzzzClauseInsert_Mst_Spec_zSave
					, "zzzzClauseInsert_Mst_SpecImage_zSave", zzzzClauseInsert_Mst_SpecImage_zSave
					, "zzzzClauseInsert_Mst_SpecFiles_zSave", zzzzClauseInsert_Mst_SpecFiles_zSave
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
        		        drop table #input_Mst_Spec;
        		        drop table #input_Mst_SpecImage;
        		        drop table #input_Mst_SpecFiles;
        	        ");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//MyCodeLabel_Done:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

		public DataSet WAS_Mst_Spec_Upd(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Spec objRQ_Mst_Spec
			////
			, out RT_Mst_Spec objRT_Mst_Spec
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Spec.Tid;
			objRT_Mst_Spec = new RT_Mst_Spec();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Spec_Upd";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Spec_Upd;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					, "Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Mst_Spec)
					, "Lst_Mst_SpecImage", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Lst_Mst_SpecImage)
					, "Lst_Mst_SpecFiles", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Lst_Mst_SpecFiles)
            ////
            });
			#endregion

			try
			{
				#region // Init:
				strOrgID_Login = objRQ_Mst_Spec.OrgID;
				#endregion

				#region // Refine and Check Input:
				////
				DataSet dsData = new DataSet();
				{
					////
					DataTable dt_Mst_SpecImage = TUtils.DataTableCmUtils.ToDataTable<Mst_SpecImage>(objRQ_Mst_Spec.Lst_Mst_SpecImage, "Mst_SpecImage");
					dsData.Tables.Add(dt_Mst_SpecImage);
					////
					DataTable dt_Mst_SpecFiles = TUtils.DataTableCmUtils.ToDataTable<Mst_SpecFiles>(objRQ_Mst_Spec.Lst_Mst_SpecFiles, "Mst_SpecFiles");
					dsData.Tables.Add(dt_Mst_SpecFiles);
					strOrgID_Login = objRQ_Mst_Spec.OrgID;
				}
				#endregion

				#region // Mst_Spec_Upd:
				mdsResult = Mst_Spec_Upd(
					objRQ_Mst_Spec.Tid // strTid
					, objRQ_Mst_Spec.GwUserCode // strGwUserCode
					, objRQ_Mst_Spec.GwPassword // strGwPassword
					, objRQ_Mst_Spec.WAUserCode // strUserCode
					, objRQ_Mst_Spec.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Spec.Mst_Spec.OrgID // objOrgID
					, objRQ_Mst_Spec.Mst_Spec.SpecCode // objSpecCode
					, objRQ_Mst_Spec.Mst_Spec.SpecName // objSpecName
					, objRQ_Mst_Spec.Mst_Spec.SpecDesc // objSpecDesc
													   //, objRQ_Mst_Spec.Mst_Spec.ModelCode // objModelCode
													   //, objRQ_Mst_Spec.Mst_Spec.SpecType1 // objSpecType1
													   //, objRQ_Mst_Spec.Mst_Spec.SpecType2 // objSpecType2
					, objRQ_Mst_Spec.Mst_Spec.Color // objColor
					, objRQ_Mst_Spec.Mst_Spec.FlagHasSerial // objFlagHasSerial
					, objRQ_Mst_Spec.Mst_Spec.FlagHasLOT // objFlagHasLOT
					, objRQ_Mst_Spec.Mst_Spec.DefaultUnitCode // objDefaultUnitCode
					, objRQ_Mst_Spec.Mst_Spec.StandardUnitCode // objStandardUnitCode
					, objRQ_Mst_Spec.Mst_Spec.NetworkSpecCode // objNetworkSpecCode
					, objRQ_Mst_Spec.Mst_Spec.Remark // objRemark
					, objRQ_Mst_Spec.Mst_Spec.FlagActive // objFlagActive
					, objRQ_Mst_Spec.Mst_Spec.CustomField1 // objCustomField1
					, objRQ_Mst_Spec.Mst_Spec.CustomField2 // objCustomField2
					, objRQ_Mst_Spec.Mst_Spec.CustomField3 // objCustomField3
					, objRQ_Mst_Spec.Mst_Spec.CustomField4 // objCustomField4	
					, objRQ_Mst_Spec.Mst_Spec.CustomField5 // objCustomField5
					, objRQ_Mst_Spec.Mst_Spec.CustomField6 // objCustomField6	
					, objRQ_Mst_Spec.Mst_Spec.CustomField7 // objCustomField7
					, objRQ_Mst_Spec.Mst_Spec.CustomField8 // objCustomField8	
					, objRQ_Mst_Spec.Mst_Spec.CustomField9 // objCustomField9
					, objRQ_Mst_Spec.Mst_Spec.CustomField10 // objCustomField10			
					, dsData // dsData
							 ////
					, objRQ_Mst_Spec.Ft_Cols_Upd // objFt_Cols_Upd
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

		public DataSet Mst_Spec_Upd(
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
			//, object objModelCode
			//, object objSpecType1
			//, object objSpecType2
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
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Spec_Upd";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Spec_Upd;
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

				#region // Call Func:
				////
				Mst_Spec_UpdX_New20200116(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   ////
					, objOrgID // objOrgID
					, objSpecCode // objSpecCode
					, objSpecName // objSpecName
					, objSpecDesc // objSpecDesc
								  //, objModelCode // objModelCode
								  //, objSpecType1 // objSpecType1
								  //, objSpecType2 // objSpecType2
					, objColor // objColor
					, objFlagHasSerial // objFlagHasSerial
					, objFlagHasLOT // objFlagHasLOT
					, objDefaultUnitCode // objDefaultUnitCode
					, objStandardUnitCode // objStandardUnitCode
					, objNetworkSpecCode // objNetworkSpecCode
					, objRemark // objRemark
					, objFlagActive // objFlagActive
					, objCustomField1 // objCustomField1
					, objCustomField2 // objCustomField2
					, objCustomField3 // objCustomField3
					, objCustomField4 // objCustomField4
					, objCustomField5 // objCustomField5
					, objCustomField6 // objCustomField6
					, objCustomField7 // objCustomField7
					, objCustomField8 // objCustomField8
					, objCustomField9 // objCustomField9
					, objCustomField10 // objCustomField10
					, dsData // dsData			
							 ////
					, objFt_Cols_Upd // objFt_Cols_Upd
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

		private void Mst_Spec_UpdX_New20200116(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objOrgID
			, object objSpecCode
			, object objSpecName
			, object objSpecDesc
			//, object objModelCode
			//, object objSpecType1
			//, object objSpecType2
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
			//int nTidSeq = 0;
			bool bMyDebugSql = false;
			string strFunctionName = "Mst_Spec_UpdX";
			//string strErrorCodeDefault = TError.ErrTCGQLTV.Mst_Spec_Upd;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
                , "objOrgID", objOrgID
				, "objSpecCode", objSpecCode
				, "objSpecName", objSpecName
				, "objSpecDesc", objSpecDesc
                //, "objModelCode", objModelCode
                //, "objSpecType1", objSpecType1
                //, "objSpecType2", objSpecType2
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
                ////
                , "objFt_Cols_Upd", objFt_Cols_Upd
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

			#region //// Refine and Check Mst_Spec:
			////
			string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
			strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
			////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strSpecCode = TUtils.CUtils.StdParam(objSpecCode);

			string strSpecName = string.Format("{0}", objSpecName).Trim();
			string strSpecDesc = string.Format("{0}", objSpecDesc).Trim();
			//string strModelCode = TUtils.CUtils.StdParam(objModelCode);
			//string strSpecType1 = string.Format("{0}", objSpecType1).Trim();
			//string strSpecType2 = string.Format("{0}", objSpecType2).Trim();
			string strColor = string.Format("{0}", objColor).Trim();
			string strFlagHasSerial = TUtils.CUtils.StdFlag(objFlagHasSerial);
			string strFlagHasLOT = TUtils.CUtils.StdFlag(objFlagHasLOT);
			string strDefaultUnitCode = TUtils.CUtils.StdParam(objDefaultUnitCode);
			string strStandardUnitCode = TUtils.CUtils.StdParam(objStandardUnitCode);
			string strNetworkSpecCode = TUtils.CUtils.StdParam(objNetworkSpecCode);
			string strRemark = string.Format("{0}", objRemark).Trim();
			string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
			string strCustomField1 = string.Format("{0}", objCustomField1).Trim();
			string strCustomField2 = string.Format("{0}", objCustomField2).Trim();
			string strCustomField3 = string.Format("{0}", objCustomField3).Trim();
			string strCustomField4 = string.Format("{0}", objCustomField4).Trim();
			string strCustomField5 = string.Format("{0}", objCustomField5).Trim();
			string strCustomField6 = string.Format("{0}", objCustomField6).Trim();
			string strCustomField7 = string.Format("{0}", objCustomField7).Trim();
			string strCustomField8 = string.Format("{0}", objCustomField8).Trim();
			string strCustomField9 = string.Format("{0}", objCustomField9).Trim();
			string strCustomField10 = string.Format("{0}", objCustomField10).Trim();

			////
			bool bUpd_SpecName = strFt_Cols_Upd.Contains("Mst_Spec.SpecName".ToUpper());
			bool bUpd_SpecDesc = strFt_Cols_Upd.Contains("Mst_Spec.SpecDesc".ToUpper());
			//bool bUpd_SpecType1 = strFt_Cols_Upd.Contains("Mst_Spec.SpecType1".ToUpper());
			//bool bUpd_SpecType2 = strFt_Cols_Upd.Contains("Mst_Spec.SpecType2".ToUpper());
			bool bUpd_Color = strFt_Cols_Upd.Contains("Mst_Spec.Color".ToUpper());
			bool bUpd_FlagHasSerial = strFt_Cols_Upd.Contains("Mst_Spec.FlagHasSerial".ToUpper());
			bool bUpd_FlagHasLOT = strFt_Cols_Upd.Contains("Mst_Spec.FlagHasLOT".ToUpper());
			bool bUpd_NetworkSpecCode = strFt_Cols_Upd.Contains("Mst_Spec.NetworkSpecCode".ToUpper());
			bool bUpd_Remark = strFt_Cols_Upd.Contains("Mst_Spec.Remark".ToUpper());
			bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Mst_Spec.FlagActive".ToUpper());
			bool bUpd_CustomField1 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField1".ToUpper());
			bool bUpd_CustomField2 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField2".ToUpper());
			bool bUpd_CustomField3 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField3".ToUpper());
			bool bUpd_CustomField4 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField4".ToUpper());
			bool bUpd_CustomField5 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField5".ToUpper());
			bool bUpd_CustomField6 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField6".ToUpper());
			bool bUpd_CustomField7 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField7".ToUpper());
			bool bUpd_CustomField8 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField8".ToUpper());
			bool bUpd_CustomField9 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField9".ToUpper());
			bool bUpd_CustomField10 = strFt_Cols_Upd.Contains("Mst_Spec.CustomField10".ToUpper());

			////
			DataTable dtDB_Mst_Spec = null;
			{
				////
				Mst_Spec_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strSpecCode // objSpecCode
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagHasSerialListToCheck
					, "" // strFlagHasLOTListToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Spec // dtDB_Mst_Spec
					);
				////
				if (bUpd_SpecName && string.IsNullOrEmpty(strSpecName))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strSpecName", strSpecName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_Upd_InvalidSpecName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				//if (!string.IsNullOrEmpty(strModelCode))
				//{
				//    ////
				//    DataTable dtDB_Mst_Model = null;

				//    Mst_Model_CheckDB(
				//        ref alParamsCoupleError // alParamsCoupleError
				//        , strModelCode // objModelCode
				//        , TConst.Flag.Yes // strFlagExistToCheck
				//        , TConst.Flag.Active // strFlagActiveListToCheck
				//        , out dtDB_Mst_Model // dtDB_Mst_Model
				//        );
				//}
				////
				//if (!string.IsNullOrEmpty(strDefaultUnitCode))
				//{
				//    ////
				//    DataTable dtDB_Mst_Unit = null;

				//    Mst_Unit_CheckDB(
				//        ref alParamsCoupleError // alParamsCoupleError
				//        , strDefaultUnitCode // objDefaultUnitCode
				//        , TConst.Flag.Yes // strFlagExistToCheck
				//        , TConst.Flag.Active // strFlagActiveListToCheck
				//        , out dtDB_Mst_Unit // dtDB_Mst_Unit
				//        );
				//}
				////////
				//if (!string.IsNullOrEmpty(strStandardUnitCode))
				//{
				//    ////
				//    DataTable dtDB_Mst_Unit = null;

				//    Mst_Unit_CheckDB(
				//        ref alParamsCoupleError // alParamsCoupleError
				//        , strStandardUnitCode // objStandardUnitCode
				//        , TConst.Flag.Yes // strFlagExistToCheck
				//        , TConst.Flag.Active // strFlagActiveListToCheck
				//        , out dtDB_Mst_Unit // dtDB_Mst_Unit
				//        );
				//}
				////
				if (bUpd_CustomField1 && !string.IsNullOrEmpty(strCustomField1))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField1 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField2 && !string.IsNullOrEmpty(strCustomField2))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField2 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField3 && !string.IsNullOrEmpty(strCustomField3))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField3 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField4 && !string.IsNullOrEmpty(strCustomField4))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField4 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField5 && !string.IsNullOrEmpty(strCustomField5))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField5 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField6 && !string.IsNullOrEmpty(strCustomField6))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField6 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField7 && !string.IsNullOrEmpty(strCustomField7))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField7 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField8 && !string.IsNullOrEmpty(strCustomField8))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField8 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField9 && !string.IsNullOrEmpty(strCustomField9))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField9 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				////
				if (bUpd_CustomField10 && !string.IsNullOrEmpty(strCustomField10))
				{
					////
					DataTable dtDB_Mst_SpecCustomField = null;

					Mst_SpecCustomField_CheckDB_New20190629(
						ref alParamsCoupleError // alParamsCoupleError
						, strOrgID // strOrgID
						, TConst.SpecCustomFieldCode.CustomField10 // objSpecCustomFieldCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dtDB_Mst_SpecCustomField // dtDB_Mst_SpecCustomField
						);
				}
				/////
				if (CmUtils.StringUtils.StringEqualIgnoreCase(nNetworkID, objOrgID)) strNetworkSpecCode = strSpecCode;
				if (bUpd_NetworkSpecCode && !string.IsNullOrEmpty(strNetworkSpecCode))
				{
					////
					myCommon_CheckOrgParent(
						ref alParamsCoupleError // alParamsCoupleError
						, nNetworkID // nNetworkID
						, strOrgID_Login // strOrgID
						);
					////
					Mst_Spec_CheckDB_NetworkSpecCodeOfOrgParent(
						ref alParamsCoupleError // alParamsCoupleError
						, nNetworkID // nNetworkID
						, strOrgID // strOrgID
						, strSpecCode // strSpecCode
						, strNetworkSpecCode // strNetworkSpecCode
						);
					////
					//              Mst_Spec_CheckDB_ExistNetworkSpecCode(
					//ref alParamsCoupleError
					//, nNetworkID // nNetworkID
					//, strNetworkSpecCode // strNetworkSpecCode
					//);
				}
				////
			}
			////
			#endregion

			#region //// SaveTemp Mst_Spec:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_Spec"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"SpecCode", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"SpecName", TConst.BizMix.Default_DBColType,
						"SpecDesc", TConst.BizMix.Default_DBColType,
						"ModelCode", TConst.BizMix.Default_DBColType,
						"SpecType1", TConst.BizMix.Default_DBColType,
						"SpecType2", TConst.BizMix.Default_DBColType,
						"Color", TConst.BizMix.Default_DBColType,
						"FlagHasSerial", TConst.BizMix.Default_DBColType,
						"FlagHasLOT", TConst.BizMix.Default_DBColType,
						"DefaultUnitCode", TConst.BizMix.Default_DBColType,
						"StandardUnitCode", TConst.BizMix.Default_DBColType,
						"NetworkSpecCode", TConst.BizMix.Default_DBColType,
						"Remark", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						"CustomField1", TConst.BizMix.Default_DBColType,
						"CustomField2", TConst.BizMix.Default_DBColType,
						"CustomField3", TConst.BizMix.Default_DBColType,
						"CustomField4", TConst.BizMix.Default_DBColType,
						"CustomField5", TConst.BizMix.Default_DBColType,
						"CustomField6", TConst.BizMix.Default_DBColType,
						"CustomField7", TConst.BizMix.Default_DBColType,
						"CustomField8", TConst.BizMix.Default_DBColType,
						"CustomField9", TConst.BizMix.Default_DBColType,
						"CustomField10", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
						new object[]{
							strOrgID, // OrgID
                            strSpecCode, // SpecCode
        			        nNetworkID, // NetworkID
        			        strSpecName, // SpecName								
        			        strSpecDesc , // SpecDesc
                            dtDB_Mst_Spec.Rows[0]["ModelCode"] , // ModelCode
        			        dtDB_Mst_Spec.Rows[0]["SpecType1"], // SpecType1
        			        dtDB_Mst_Spec.Rows[0]["SpecType2"], // SpecType2
        			        strColor, // Color
        			        strFlagHasSerial, // FlagHasSerial
        			        strFlagHasLOT , // FlagHasLOT
        			        strDefaultUnitCode, // DefaultUnitCode
        			        strStandardUnitCode, // StandardUnitCode
                            strNetworkSpecCode, // NetworkSpecCode
                            strRemark, // Remark
        			        strFlagActive, // FlagActive
        			        strCustomField1 , // CustomField1
        			        strCustomField2 , // CustomField2
        			        strCustomField3 , // CustomField3
        			        strCustomField4 , // CustomField4
        			        strCustomField5 , // CustomField5
        			        strCustomField6 , // CustomField6
        			        strCustomField7 , // CustomField7
        			        strCustomField8 , // CustomField8
        			        strCustomField9 , // CustomField9
        			        strCustomField10 , // CustomField10
        			        dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
        			        strWAUserCode, // LogLUBy
        			        }
						}
					);
			}
			#endregion

			#region //// Refine and Check Mst_SpecImage:
			////
			DataTable dtInput_Mst_SpecImage = null;
			{
				////
				string strTableCheck = "Mst_SpecImage";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_Upd_Input_Mst_SpecImageTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_SpecImage = dsData.Tables[strTableCheck];
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
					dtInput_Mst_SpecImage // dtData
					, "", "SpecImageSpec" // arrstrCouple
					, "", "SpecImageName" // arrstrCouple
					, "", "SpecImageDesc" // arrstrCouple
					, "", "FlagPrimaryImage" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "OrgID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "SpecCode", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "SpecImagePath", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "NetworkID", typeof(object));
				//TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "FlagPrimaryImage", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecImage, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Mst_SpecImage.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_SpecImage.Rows[nScan];
					////
					string strSpecImagePath = null;
					string strSpecImageSpec = drScan["SpecImageSpec"].ToString();
					string strSpecImageName = drScan["SpecImageName"].ToString();
					////
					DataTable dtDB_Mst_Param = null;
					Mst_Param_CheckDB(
						ref alParamsCoupleError
						, TConst.Mst_Param.PARAM_UPLOADFILE
						, TConst.Flag.Yes
						, out dtDB_Mst_Param
						);

					string folderUpload = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
					////
					string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
					string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
					byte[] strDeCodeBase64 = Convert.FromBase64String(strSpecImageSpec);
					string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strSpecImageName);
					string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
					strSpecImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strSpecImageName);
					////
					drScan["OrgID"] = strOrgID;
					drScan["SpecCode"] = strSpecCode;
					drScan["SpecImagePath"] = strSpecImagePath;
					drScan["NetworkID"] = nNetworkID;
					//drScan["FlagPrimaryImage"] = TConst.Flag.Active;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
					#region // WriteFile:
					////
					if (!string.IsNullOrEmpty(strSpecImageSpec) && !string.IsNullOrEmpty(strSpecImageName))
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
					, "#input_Mst_SpecImage"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"SpecCode", TConst.BizMix.Default_DBColType,
						"SpecImagePath", TConst.BizMix.Default_DBColType,
						"SpecImageName", TConst.BizMix.Default_DBColType,
						"SpecImageDesc", TConst.BizMix.Default_DBColType,
						"FlagPrimaryImage", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Mst_SpecImage
					);
			}
			#endregion

			#region //// Refine and Check Mst_SpecFiles:
			////
			DataTable dtInput_Mst_SpecFiles = null;
			{
				////
				string strTableCheck = "Mst_SpecFiles";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Spec_Upd_Input_Mst_SpecFilesTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Mst_SpecFiles = dsData.Tables[strTableCheck];
				////
				//if (dtInput_KUNN_FileUpload.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErrTCGQLTV.Mst_Spec_Add_Input_KUNN_FileUploadTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Mst_SpecFiles // dtData
					, "", "SpecFileSpec" // arrstrCouple
					, "", "SpecFileName" // arrstrCouple
					, "", "SpecFileDesc" // arrstrCouple
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "OrgID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "SpecCode", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "SpecImagePath", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_SpecFiles, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Mst_SpecFiles.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Mst_SpecFiles.Rows[nScan];
					////
					string strSpecFilePath = null;
					string strSpecFileSpec = drScan["SpecFileSpec"].ToString();
					string strSpecFileName = drScan["SpecFileName"].ToString();
					////
					DataTable dtDB_Mst_Param = null;
					Mst_Param_CheckDB(
						ref alParamsCoupleError
						, TConst.Mst_Param.PARAM_UPLOADFILE
						, TConst.Flag.Yes
						, out dtDB_Mst_Param
						);

					string folderUpload = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
					////
					string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
					string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
					byte[] strDeCodeBase64 = Convert.FromBase64String(strSpecFileSpec);
					string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strSpecFileName);
					string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
					strSpecFilePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strSpecFileName);
					////
					drScan["OrgID"] = strOrgID;
					drScan["SpecCode"] = strSpecCode;
					drScan["SpecFilePath"] = strSpecFilePath;
					drScan["NetworkID"] = nNetworkID;
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
					#region // WriteFile:
					////
					if (!string.IsNullOrEmpty(strSpecFileSpec) && !string.IsNullOrEmpty(strSpecFileName))
					{
						bool exist = Directory.Exists(strFilePathBase);

						if (!exist)
						{
							Directory.CreateDirectory(strFilePathBase);
						}
						System.IO.File.WriteAllBytes(strFilePathSave, strDeCodeBase64);
					}
					#endregion
				}
			}
			#endregion

			#region //// SaveTemp Mst_SpecFiles:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_SpecFiles"
					, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"SpecCode", TConst.BizMix.Default_DBColType,
						"SpecFilePath", TConst.BizMix.Default_DBColType,
						"SpecFileName", TConst.BizMix.Default_DBColType,
						"SpecFileDesc", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Mst_SpecFiles
					);
			}
			#endregion

			#region //// Save:
			//// Clear All:
			{
				string strSqlDelete = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_SpecFiles:
        			    delete t
        			    from Mst_SpecFiles t
        			    where (1=1)
        				    and t.OrgID = @strOrgID
        				    and t.SpecCode = @strSpecCode
        			    ;

        			    ---- Mst_SpecImage:
        			    delete t
        			    from Mst_SpecImage t
        			    where (1=1)
        				    and t.OrgID = @strOrgID
        				    and t.SpecCode = @strSpecCode
        			    ;

        		    ");
				_cf.db.ExecQuery(
					strSqlDelete
					, "@strOrgID", strOrgID
					, "@strSpecCode", strSpecCode
					);
			}

			//// Insert All:
			{
				string zzB_Update_Mst_Spec_ClauseSet_zzE = @"
        				t.LogLUDTimeUTC = f.LogLUDTimeUTC
        				, t.LogLUBy = f.LogLUBy
        				";

				if (bUpd_SpecName) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.SpecName = f.SpecName";
				if (bUpd_SpecDesc) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.SpecDesc = f.SpecDesc";
				//if (bUpd_SpecType1) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.SpecType1 = f.SpecType1";
				//if (bUpd_SpecType2) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.SpecType2 = f.SpecType2";
				if (bUpd_Color) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.Color = f.Color";
				if (bUpd_FlagHasSerial) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.FlagHasSerial = f.FlagHasSerial";
				if (bUpd_FlagHasLOT) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.FlagHasLOT = f.FlagHasLOT";
				if (bUpd_NetworkSpecCode) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.NetworkSpecCode = f.NetworkSpecCode";
				if (bUpd_Remark) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.Remark = f.Remark";
				if (bUpd_FlagActive) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.FlagActive = f.FlagActive";
				if (bUpd_CustomField1) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField1 = f.CustomField1";
				if (bUpd_CustomField2) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField2 = f.CustomField2";
				if (bUpd_CustomField3) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField3 = f.CustomField3";
				if (bUpd_CustomField4) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField4 = f.CustomField4";
				if (bUpd_CustomField5) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField5 = f.CustomField5";
				if (bUpd_CustomField6) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField6 = f.CustomField6";
				if (bUpd_CustomField7) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField7 = f.CustomField7";
				if (bUpd_CustomField8) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField8 = f.CustomField8";
				if (bUpd_CustomField9) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField9 = f.CustomField9";
				if (bUpd_CustomField10) zzB_Update_Mst_Spec_ClauseSet_zzE += ", t.CustomField10 = f.CustomField10";
				////
				string zzB_Update_Mst_Spec_zzE = CmUtils.StringUtils.Replace(@"
        			    ---- Mst_Spec:
        			    update t
        			    set
        				    zzB_Update_Mst_Spec_ClauseSet_zzE
        			    from Mst_Spec t --//[mylock]
        				    inner join #input_Mst_Spec f --//[mylock]
        					    on t.OrgID = f.OrgID
        					        and t.SpecCode = f.SpecCode
        			    ;
        		    "
					, "zzB_Update_Mst_Spec_ClauseSet_zzE", zzB_Update_Mst_Spec_ClauseSet_zzE
					);
				////
				string zzzzClauseInsert_Mst_SpecImage_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_SpecImage:
        		        insert into Mst_SpecImage
        		        (
        			        OrgID
        			        , SpecCode
        			        , NetworkID
        			        , SpecImagePath
        			        , SpecImageName
        			        , SpecImageDesc
        			        , FlagPrimaryImage
        			        , LogLUDTimeUTC
        			        , LogLUBy
        		        )
        		        select 
        			        t.OrgID
        			        , t.SpecCode
        			        , t.NetworkID
        			        , t.SpecImagePath
        			        , t.SpecImageName
        			        , t.SpecImageDesc
        			        , t.FlagPrimaryImage
        			        , t.LogLUDTimeUTC
        			        , t.LogLUBy
        		        from #input_Mst_SpecImage t --//[mylock]
        		        ;
        	    ");
				////
				string zzzzClauseInsert_Mst_SpecFiles_zSave = CmUtils.StringUtils.Replace(@"
        		        ---- Mst_SpecFiles:
        		        insert into Mst_SpecFiles
        		        (
        			        OrgID
        			        , SpecCode
        			        , NetworkID
        			        , SpecFilePath
        			        , SpecFileName
        			        , SpecFileDesc
        			        , LogLUDTimeUTC
        			        , LogLUBy
        		        )
        		        select 
        			        t.OrgID
        			        , t.SpecCode
        			        , t.NetworkID
        			        , t.SpecFilePath
        			        , t.SpecFileName
        			        , t.SpecFileDesc
        			        , t.LogLUDTimeUTC
        			        , t.LogLUBy
        		        from #input_Mst_SpecFiles t --//[mylock]
        		        ;
        	        ");

				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
        		        ----
        		        zzB_Update_Mst_Spec_zzE

        		        ----
        		        zzzzClauseInsert_Mst_SpecImage_zSave

        		        ----
        		        zzzzClauseInsert_Mst_SpecFiles_zSave
        	        "
					, "zzB_Update_Mst_Spec_zzE", zzB_Update_Mst_Spec_zzE
					, "zzzzClauseInsert_Mst_SpecImage_zSave", zzzzClauseInsert_Mst_SpecImage_zSave
					, "zzzzClauseInsert_Mst_SpecFiles_zSave", zzzzClauseInsert_Mst_SpecFiles_zSave
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

			// Return Good:
			//MyCodeLabel_Done:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

		public DataSet WAS_Mst_Spec_Del(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Spec objRQ_Mst_Spec
			////
			, out RT_Mst_Spec objRT_Mst_Spec
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Spec.Tid;
			objRT_Mst_Spec = new RT_Mst_Spec();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Spec_Del";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Spec_Del;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					, "Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Mst_Spec)
                    //, "Lst_KUNN_ValLaiSuatChangeHist", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Lst_KUNN_ValLaiSuatChangeHist)
                    //, "Lst_KUNN_FileUpload", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec.Lst_KUNN_FileUpload)
            ////
            });
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
				//	DataTable dt_KUNN_ValLaiSuatChangeHist = TUtils.DataTableCmUtils.ToDataTable<KUNN_ValLaiSuatChangeHist>(objRQ_Mst_Spec.Lst_KUNN_ValLaiSuatChangeHist, "KUNN_ValLaiSuatChangeHist");
				//	dsData.Tables.Add(dt_KUNN_ValLaiSuatChangeHist);
				//	////
				//	DataTable dt_KUNN_FileUpload = TUtils.DataTableCmUtils.ToDataTable<KUNN_FileUpload>(objRQ_Mst_Spec.Lst_KUNN_FileUpload, "KUNN_FileUpload");
				//	dsData.Tables.Add(dt_KUNN_FileUpload);
				//	////
				//	//DataTable dt_Mst_SpecDtl = TUtils.DataTableCmUtils.ToDataTable<Mst_SpecDtl>(objRQ_Mst_Spec.Lst_Mst_SpecDtl, "Mst_SpecDtl");
				//	//dsData.Tables.Add(dt_Mst_SpecDtl);
				//}
				strOrgID_Login = objRQ_Mst_Spec.OrgID;
				#endregion

				#region // Mst_Spec_Del:
				mdsResult = Mst_Spec_Del(
					objRQ_Mst_Spec.Tid // strTid
					, objRQ_Mst_Spec.GwUserCode // strGwUserCode
					, objRQ_Mst_Spec.GwPassword // strGwPassword
					, objRQ_Mst_Spec.WAUserCode // strUserCode
					, objRQ_Mst_Spec.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Spec.Mst_Spec.OrgID // objOrgID
					, objRQ_Mst_Spec.Mst_Spec.SpecCode // objSpecCode
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

		public DataSet Mst_Spec_Del(
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
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Spec_Del";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Spec_Del;
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

				#region // Call Func:
				////
				Mst_Spec_DelX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   ////
					, objOrgID // OrgID
					, objSpecCode //  objSpecCode
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

		private void Mst_Spec_DelX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objOrgID
			, object objSpecCode
			)
		{
			#region // Temp:
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			string strFunctionName = "Mst_Spec_DelX";
			//string strErrorCodeDefault = TError.ErrTCGQLTV.Mst_Spec_Del;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    ////
                    , "objOrgID", objOrgID
					, "objSpecCode", objSpecCode
					});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Convert Input:
			#endregion

			#region //// Refine and Check Mst_Spec:
			/////
			string strOrgID = TUtils.CUtils.StdParam(objOrgID);
			string strSpecCode = TUtils.CUtils.StdParam(objSpecCode);

			DataTable dtDB_Mst_Spec = null;
			{
				////
				Mst_Spec_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strOrgID // objOrgID
					, strSpecCode // objSpecCode
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strFlagHasSerialListToCheck
					, "" // strFlagHasLOTListToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Mst_Spec // dtDB_Mst_Spec
					);
			}
			////
			#endregion

			#region //// SaveTemp Mst_Spec:
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Mst_Spec"
					, new object[]{
							"OrgID", TConst.BizMix.Default_DBColType,
							"SpecCode", TConst.BizMix.Default_DBColType,
							"NetworkID", TConst.BizMix.Default_DBColType,
							"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
							"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
							new object[]{
								strOrgID, // OrgID
                                strSpecCode, // SpecCode
        			            nNetworkID, // NetworkID
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
        			    ---- Mst_SpecFiles:
        			    delete t
        			    from Mst_SpecFiles t
        			    where (1=1)
        				    and t.OrgID = @strOrgID
        				    and t.SpecCode = @strSpecCode
        			    ;

        			    ---- Mst_SpecImage:
        			    delete t
        			    from Mst_SpecImage t
        			    where (1=1)
        				    and t.OrgID = @strOrgID
        				    and t.SpecCode = @strSpecCode
        			    ;

        			    ---- Mst_Spec:
        			    delete t
        			    from Mst_Spec t
        			    where (1=1)
        				    and t.OrgID = @strOrgID
        				    and t.SpecCode = @strSpecCode
        			    ;
        		    ");
				_cf.db.ExecQuery(
					strSqlDelete
					, "@strOrgID", strOrgID
					, "@strSpecCode", strSpecCode
					);
			}
			#endregion

			#region //// Clear For Debug:
			{
				////
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
        		        ---- Clear for Debug:
        		        drop table #input_Mst_Spec;
        	        ");

				_cf.db.ExecQuery(
					strSqlClearForDebug
					);
				////
			}
			#endregion

			// Return Good:
			//MyCodeLabel_Done:
			//return;
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			//return mdsFinal;		
		}

		public DataSet WAS_Mst_Spec_Exist_Active_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Spec objRQ_Mst_Spec
			////
			, out RT_Mst_Spec objRT_Mst_Spec
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Spec.Tid;
			objRT_Mst_Spec = new RT_Mst_Spec();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Spec_Exist_Active_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Spec_Exist_Active_Get;
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
				DataTable dtInput_Mst_Spec = new DataTable();
				{
					////
					dtInput_Mst_Spec = TUtils.DataTableCmUtils.ToDataTable<Mst_Spec>(objRQ_Mst_Spec.Lst_Mst_Spec, "dtInput_Mst_Spec");

				}
				////
				List<dt_Mst_Spec_Exist> lst_dt_Mst_Spec_Exist = new List<dt_Mst_Spec_Exist>();
				List<dt_Mst_Spec_Active> lst_dt_Mst_Spec_Active = new List<dt_Mst_Spec_Active>();
				#endregion

				#region // WS_Mst_Spec_Get:
				DataSet DSData = new DataSet();
				mdsResult = Mst_Spec_Exist_Active_Get(
					objRQ_Mst_Spec.Tid // strTid
					, objRQ_Mst_Spec.GwUserCode // strGwUserCode
					, objRQ_Mst_Spec.GwPassword // strGwPassword
					, objRQ_Mst_Spec.WAUserCode // strUserCode
					, objRQ_Mst_Spec.WAUserPassword // strUserPassword
					, objRQ_Mst_Spec.OrgID // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, dtInput_Mst_Spec // dtInput_Mst_Spec
					, out DSData
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_Mst_Spec_Exist = mdsResult.Tables["dt_Mst_Spec_Exist"].Copy();
					lst_dt_Mst_Spec_Exist = TUtils.DataTableCmUtils.ToListof<dt_Mst_Spec_Exist>(dt_Mst_Spec_Exist);
					objRT_Mst_Spec.Lst_dt_Mst_Spec_Exist = lst_dt_Mst_Spec_Exist;

					////
					DataTable dt_Mst_Spec_Active = mdsResult.Tables["dt_Mst_Spec_Active"].Copy();
					lst_dt_Mst_Spec_Active = TUtils.DataTableCmUtils.ToListof<dt_Mst_Spec_Active>(dt_Mst_Spec_Active);
					objRT_Mst_Spec.Lst_dt_Mst_Spec_Active = lst_dt_Mst_Spec_Active;

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

		public DataSet Mst_Spec_Exist_Active_Get(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strOrgID
			, ref ArrayList alParamsCoupleError
			//// 
			, DataTable dtInput_Mst_Spec
			, out DataSet mdsFinal
			)
		{
			#region // Temp:
			mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Spec_Exist_Active_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Spec_Exist_Active_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
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

				#region //// SaveTemp dtInput_Mst_Spec:
				{
					TUtils.CUtils.MyBuildDBDT_Common(
						_cf.db
						, "#input_Mst_Spec"
						, new object[]{
						"OrgID", TConst.BizMix.Default_DBColType,
						"SpecCode", TConst.BizMix.Default_DBColType,
						"FlagExist", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
						}
						, dtInput_Mst_Spec
						);
				}
				#endregion

				#region // Check:
				//// Refine:

				#endregion

				#region // Build Sql:

				////
				zzzzClauseSelect_Mst_Org_ViewAbility_Get(
					strOrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
					);

				////
				string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Spec_Filter:
                        select
                            t.OrgID input_OrgID
                            , t.SpecCode input_SpecCode
                            , t.FlagExist input_FlagExist
                            , t.FlagActive input_FlagActive
                            -----
                            , f.OrgID DB_OrgID
                            , f.SpecCode DB_SpecCode
                            , (case
							        when (f.OrgID is null or f.SpecCode is null) then 0
							        else 1
							    end
                            ) DB_FlagExist
                            , f.FlagActive DB_FlagActive
                        into #tbl_Mst_Spec_Filter
                        from #input_Mst_Spec t --//[mylock]
						    left join Mst_Spec f --//[mylock]
							    on t.OrgID = f.OrgID 
								    and t.SpecCode = f.SpecCode
                        where (1=1)
                        ;

                        --select null tbl_Mst_Spec_Filter, * from #tbl_Mst_Spec_Filter --//[mylock];


                        ------ Return Check:
                        ---- #tbl_Mst_Spec_Exist:
                        select
						    t.*
                        --into #tbl_Mst_Spec_Exist
					    from #tbl_Mst_Spec_Filter t--//[mylock]
					    where (1=1)
                            and t.input_FlagExist != t.DB_FlagExist
                        ;

                        --select null tbl_Mst_Spec_Exist, * from #tbl_Mst_Spec_Exist --//[mylock];

                        ---- #tbl_Mst_Spec_Active:
                        select
						    t.*
                        --into #tbl_Mst_Spec_Active
					    from #tbl_Mst_Spec_Filter t--//[mylock]
					    where (1=1)
                            and t.input_FlagActive != t.DB_FlagActive
                        ;

                        --select null tbl_Mst_Spec_Active, * from #tbl_Mst_Spec_Active --//[mylock];
					"
					);
				////
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					);
				int nIdxTable = 0;
				{
					dsGetData.Tables[nIdxTable++].TableName = "dt_Mst_Spec_Exist";
					dsGetData.Tables[nIdxTable++].TableName = "dt_Mst_Spec_Active";
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

		public DataSet WAS_Mst_Spec_CheckListDB(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Spec objRQ_Mst_Spec
			////
			, out RT_Mst_Spec objRT_Mst_Spec
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Spec.Tid;
			objRT_Mst_Spec = new RT_Mst_Spec();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Spec.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Spec_CheckListDB";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Spec_CheckListDB;
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
					DataTable dt_Mst_Spec = new DataTable();
					dt_Mst_Spec = TUtils.DataTableCmUtils.ToDataTable<Mst_Spec>(objRQ_Mst_Spec.Lst_Mst_Spec, "Mst_Spec");
					dsData.Tables.Add(dt_Mst_Spec);

				}
				#endregion

				#region // Mst_Spec_CheckListDB:
				DataSet DSData = new DataSet();
				mdsResult = Mst_Spec_CheckListDB(
					objRQ_Mst_Spec.Tid // strTid
					, objRQ_Mst_Spec.GwUserCode // strGwUserCode
					, objRQ_Mst_Spec.GwPassword // strGwPassword
					, objRQ_Mst_Spec.WAUserCode // strUserCode
					, objRQ_Mst_Spec.WAUserPassword // strUserPassword
					, objRQ_Mst_Spec.OrgID // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
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

		public DataSet Mst_Spec_CheckListDB(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strOrgID
			, ref ArrayList alParamsCoupleError
			//// 
			, DataSet dsData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Spec_CheckListDB";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Spec_CheckListDB;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
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
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Convert Input:
				//DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
				//if (dsData == null) dsData = new DataSet("dsData");
				//dsData.AcceptChanges();
				alParamsCoupleError.AddRange(new object[]{
					"Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
					});
				#endregion

				#region // Refine and Check Input Mst_Spec:
				////
				DataTable dtInput_Mst_Spec = null;
				{
					////
					string strTableCheck = "Mst_Spec";
					////
					if (!dsData.Tables.Contains(strTableCheck))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.TableName", strTableCheck
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Spec_CheckListDB_Input_MstSpecNotFound
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					dtInput_Mst_Spec = dsData.Tables[strTableCheck];
					////
					if (dtInput_Mst_Spec.Rows.Count < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.TableName", strTableCheck
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Mst_Spec_CheckListDB_Input_MstSpecTblInvalid
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					DataSet dsDB_Mst_Spec = null;

					Mst_Spec_CheckListDB(
						ref alParamsCoupleError//alParamsCoupleError
						, dtInput_Mst_Spec // dtInput_Mst_Spec
						, out dsDB_Mst_Spec // dsDB_Mst_Spec
						);
					////
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

		#region // Mst_Model:
		private void Mst_Model_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objModelCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Model
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Model t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.ModelCode = @objModelCode
					;
				");
			dtDB_Mst_Model = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objModelCode", objModelCode
				).Tables[0];
			dtDB_Mst_Model.TableName = "Mst_Model";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Model.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.ModelCode", objModelCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Model_CheckDB_ModelCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Model.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.ModelCode", objModelCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Model_CheckDB_ModelCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Model.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.ModelCode", objModelCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Model.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Model_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		#endregion

		#region // Mst_Unit:
		private void Mst_Unit_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objUnitCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_Unit
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Unit t --//[mylock]
					where (1=1)
						and t.UnitCode = @objUnitCode
					;
				");
			dtDB_Mst_Unit = _cf.db.ExecQuery(
				strSqlExec
				, "@objUnitCode", objUnitCode
				).Tables[0];
			dtDB_Mst_Unit.TableName = "Mst_Unit";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Unit.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UnitCode", objUnitCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Unit_CheckDB_UnitCodeNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Unit.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.UnitCode", objUnitCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_Unit_CheckDB_UnitCodeExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Unit.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.UnitCode", objUnitCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_Unit.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_Unit_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		#endregion

		#region // Mst_SpecType1:
		private void Mst_SpecType1_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objSpecType1
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_SpecType1
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_SpecType1 t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.SpecType1 = @objSpecType1
					;
				");
			dtDB_Mst_SpecType1 = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objSpecType1", objSpecType1
				).Tables[0];
			dtDB_Mst_SpecType1.TableName = "Mst_SpecType1";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_SpecType1.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.SpecType1", objSpecType1
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_SpecType1_CheckDB_SpecType1NotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_SpecType1.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.SpecType1", objSpecType1
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_SpecType1_CheckDB_SpecType1Exist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_SpecType1.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.SpecType1", objSpecType1
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_SpecType1.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_SpecType1_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		#endregion

		#region // Mst_SpecType2:
		private void Mst_SpecType2_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objSpecType2
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_SpecType2
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_SpecType2 t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.SpecType2 = @objSpecType2
					;
				");
			dtDB_Mst_SpecType2 = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objSpecType2", objSpecType2
				).Tables[0];
			dtDB_Mst_SpecType2.TableName = "Mst_SpecType2";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_SpecType2.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.SpecType2", objSpecType2
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_SpecType2_CheckDB_SpecType2NotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_SpecType2.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.SpecType2", objSpecType2
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_SpecType2_CheckDB_SpecType2Exist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_SpecType2.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.SpecType2", objSpecType2
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_SpecType2.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_SpecType2_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		#endregion

		#region // Mst_SpecCustomField:
		private void Mst_SpecCustomField_CheckDB_New20190629(
			ref ArrayList alParamsCoupleError
			, object objOrgID
			, object objSpecCustomFieldCode
			, string strFlagExistToCheck
			, string strFlagActiveListToCheck
			, out DataTable dtDB_Mst_SpecCustomField
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_SpecCustomField t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.SpecCustomFieldCode = @objSpecCustomFieldCode
					;
				");
			dtDB_Mst_SpecCustomField = _cf.db.ExecQuery(
				strSqlExec
				, "@objOrgID", objOrgID
				, "@objSpecCustomFieldCode", objSpecCustomFieldCode
				, "@dateSys", DateTime.UtcNow.ToString("yyyy-MM-dd")
				).Tables[0];
			dtDB_Mst_SpecCustomField.TableName = "Mst_SpecCustomField";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_SpecCustomField.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.SpecCustomFieldCode", objSpecCustomFieldCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_SpecCustomField_CheckDB_CustomFieldNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_SpecCustomField.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.OrgID", objOrgID
						, "Check.SpecCustomFieldCode", objSpecCustomFieldCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Mst_SpecCustomField_CheckDB_CustomFieldExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}

			// strFlagActiveListToCheck:
			if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_SpecCustomField.Rows[0]["FlagActive"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.OrgID", objOrgID
					, "Check.SpecCustomFieldCode", objSpecCustomFieldCode
					, "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
					, "DB.FlagActive", dtDB_Mst_SpecCustomField.Rows[0]["FlagActive"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Mst_SpecCustomField_CheckDB_FlagActiveNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
		#endregion

		#region // Prd_ProductID:
		private void myCommon_CheckOrgParent(
			ref ArrayList alParamsCoupleError
			, object objNetWorkID
			, object objOrgID
			)
		{
			if (!CmUtils.StringUtils.StringEqualIgnoreCase(objNetWorkID, objOrgID))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.objNetWorkID", objNetWorkID
					, "Check.objOrgID", objOrgID
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.myCommon_CheckNoOrgParent
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}
        #endregion

        #region // Mst_Product:
        private void Mst_Product_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objOrgID
            , object objProductCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Mst_Product
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Mst_Product t --//[mylock]
					where (1=1)
						and t.OrgID = @objOrgID
						and t.ProductCode = @objProductCode
					;
				");
            dtDB_Mst_Product = _cf.db.ExecQuery(
                strSqlExec
                , "@objOrgID", objOrgID
                , "@objProductCode", objProductCode
                ).Tables[0];
            dtDB_Mst_Product.TableName = "Mst_Product";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Mst_Product.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        , "Check.ProductCode", objProductCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Product_CheckDB_ProductNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Mst_Product.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.OrgID", objOrgID
                        , "Check.ProductCode", objProductCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Product_CheckDB_ProductExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Mst_Product.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.OrgID", objOrgID
                    , "Check.ProductCode", objProductCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Mst_Product.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_Product_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet Mst_Product_Get(
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
			, string strRt_Cols_Mst_Product
			, string strRt_Cols_Mst_ProductImages
			, string strRt_Cols_Mst_ProductFiles
			, string strRt_Cols_Prd_BOM
			, string strRt_Cols_Prd_Attribute
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Mst_Product_Get";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Product_Get;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
				, "strFt_RecordCount", strFt_RecordCount
				, "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Mst_Product", strRt_Cols_Mst_Product
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

				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				////// Tạm thời bỏ do nhúng với iNos
				//Sys_User_CheckAuthorize(
				//	strTid // strTid
				//	, strGwUserCode // strGwUserCode
				//	, strGwPassword // strGwPassword
				//	, strWAUserCode // strWAUserCode
				//					//, strWAUserPassword // strWAUserPassword
				//	, ref mdsFinal // mdsFinal
				//	, ref alParamsCoupleError // alParamsCoupleError
				//	, dtimeSys // dtimeSys
				//	, strAccessToken // strAccessToken
				//	, strNetworkID // strNetworkID
				//	, strOrgID // strOrgID
				//	, TConst.Flag.Active // strFlagUserCodeToCheck
				//	);

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Check:
				//// Refine:
				long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
				long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
				bool bGet_Mst_Product = (strRt_Cols_Mst_Product != null && strRt_Cols_Mst_Product.Length > 0);
				bool bGet_Mst_ProductImages = (strRt_Cols_Mst_ProductImages != null && strRt_Cols_Mst_ProductImages.Length > 0);
				bool bGet_Mst_ProductFiles = (strRt_Cols_Mst_ProductFiles != null && strRt_Cols_Mst_ProductFiles.Length > 0);
				bool bGet_Prd_BOM = (strRt_Cols_Prd_BOM != null && strRt_Cols_Prd_BOM.Length > 0);
				bool bGet_Prd_Attribute = (strRt_Cols_Prd_Attribute != null && strRt_Cols_Prd_Attribute.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                //myCache_Mst_Organ_RW_ViewAbility_Get(drAbilityOfUser);

                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                strOrgID = TUtils.CUtils.StdParam(drAbilityOfUser["MNNTOrgID"]);
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
						---- #tbl_Mst_Product_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, mp.OrgID
							, mp.ProductCode
						into #tbl_Mst_Product_Filter_Draft
						from Mst_Product mp --//[mylock]
							inner join #tbl_Mst_Org_ViewAbility t_mo --//[mylock]
	                            on mp.OrgID = t_mo.OrgID
							left join Prd_Attribute pattb --//[mylock]
								on mp.OrgID = pattb.OrgID
									and mp.ProductCode = pattb.ProductCode
							left join Prd_BOM pbom --//[mylock]
								on mp.OrgID = pbom.OrgIDParent
									and mp.ProductCode = pbom.ProductCodeParent
						where (1=1)
							zzB_Where_strFilter_zzE
                            and mp.FlagFG = '0'
						order by 
							mp.OrgID 
							, mp.ProductCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Mst_Product_Filter_Draft t --//[mylock]
						;

						---- #tbl_Mst_Product_Filter:
						select
							t.*
						into #tbl_Mst_Product_Filter
						from #tbl_Mst_Product_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Mst_Product --------:
						zzB_Select_Mst_Product_zzE
						----------------------------------------

						-------- Mst_ProductImages --------:
						zzB_Select_Mst_ProductImages_zzE
						----------------------------------------

						-------- Mst_ProductFiles --------:
						zzB_Select_Mst_ProductFiles_zzE
						----------------------------------------

						-------- Prd_BOM --------:
						zzB_Select_Prd_BOM_zzE
						----------------------------------------

						-------- Prd_Attribute --------:
						zzB_Select_Prd_Attribute_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Mst_Product_Filter_Draft;
						--drop table #tbl_Mst_Product_Filter;
					"
                    );
				////
				string zzB_Select_Mst_Product_zzE = "-- Nothing.";
				if (bGet_Mst_Product)
				{
					#region // bGet_Mst_Product:
					zzB_Select_Mst_Product_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_Product:
							select
	                            t.MyIdxSeq
	                            , mp.*
	                            , mb.BrandName mb_BrandName
	                            , mpg.ProductGrpName mpg_ProductGrpName
	                            , mpt.ProductType mpt_ProductType
                                , mpt.ProductTypeName mpt_ProductTypeName
                                , mvat.VATRate mvat_VATRate
                            from #tbl_Mst_Product_Filter t --//[mylock]
	                            inner join Mst_Product mp --//[mylock]
		                            on t.OrgID = mp.OrgID
			                            and t.ProductCode = mp.ProductCode
	                            left join Mst_Brand mb --//[mylock]
		                            on mp.BrandCode = mb.BrandCode
                                        and mp.OrgID = mb.OrgID
	                            left join Mst_ProductGroup mpg --//[mylock]
		                            on mp.ProductGrpCode = mpg.ProductGrpCode
                                        and mp.OrgID = mpg.OrgID
	                            left join Mst_ProductType mpt --//[mylock]
		                            on mp.ProductType = mpt.ProductType
                                left join Mst_VATRate mvat --//[mylock]
                                    on mp.VATRateCode = mvat.VATRateCode
                            order by t.MyIdxSeq asc
                            ;
						"
                        );
					#endregion
				}
				////
				string zzB_Select_Mst_ProductImages_zzE = "-- Nothing.";
				if (bGet_Mst_ProductImages)
				{
					#region // bGet_Mst_ProductImages:
					zzB_Select_Mst_ProductImages_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_ProductImages:
							select
								t.MyIdxSeq
								, mpi.*
							from #tbl_Mst_Product_Filter t --//[mylock]
								inner join Mst_ProductImages mpi --//[mylock]
									on t.OrgID = mpi.OrgID
										and t.ProductCode = mpi.ProductCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzB_Select_Mst_ProductFiles_zzE = "-- Nothing.";
				if (bGet_Mst_ProductFiles)
				{
					#region // bGet_Mst_ProductFiles:
					zzB_Select_Mst_ProductFiles_zzE = CmUtils.StringUtils.Replace(@"
							---- Mst_ProductFiles:
							select
								t.MyIdxSeq
								, mpf.*
							from #tbl_Mst_Product_Filter t --//[mylock]
								inner join Mst_ProductFiles mpf --//[mylock]
									on t.OrgID = mpf.OrgID
										and t.ProductCode = mpf.ProductCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzB_Select_Prd_BOM_zzE = "-- Nothing.";
				if (bGet_Prd_BOM)
				{
					#region // bGet_Prd_BOM:
					zzB_Select_Prd_BOM_zzE = CmUtils.StringUtils.Replace(@"
							---- Prd_BOM:
							select
								t.MyIdxSeq
								, pbom.*
								, mp.ProductCode mp_ProductCode
								, mp.ProductName mp_ProductName
								, mp.UPBuy mp_UPBuy
								, mp.UPSell mp_UPSell
                                , mp.ProductCodeUser mp_ProductCodeUser
                                , mp.UnitCode mp_UnitCode
                                , mp.ProductCodeBase mp_ProductCodeBase
                                , mp.ValConvert mp_ValConvert
							from #tbl_Mst_Product_Filter t --//[mylock]
								inner join Prd_BOM pbom --//[mylock]
									on t.OrgID = pbom.OrgIDParent
										and t.ProductCode = pbom.ProductCodeParent
								left join Mst_Product mp --//[mylock]
									on pbom.OrgID = mp.OrgID
										and pbom.ProductCode = mp.ProductCode
							order by t.MyIdxSeq asc
							;
						"
						);
					#endregion
				}
				////
				string zzB_Select_Prd_Attribute_zzE = "-- Nothing.";
				if (bGet_Prd_Attribute)
				{
					#region // bGet_Prd_Attribute:
					zzB_Select_Prd_Attribute_zzE = CmUtils.StringUtils.Replace(@"
							---- Prd_Attribute:
							select
								t.MyIdxSeq
								, pattb.*
							from #tbl_Mst_Product_Filter t --//[mylock]
								inner join Prd_Attribute pattb --//[mylock]
									on t.OrgID = pattb.OrgID
										and t.ProductCode = pattb.ProductCode
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
							, "Mst_Product" // strTableNameDB
							, "Mst_Product." // strPrefixStd
							, "mp." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Prd_Attribute" // strTableNameDB
							, "Prd_Attribute." // strPrefixStd
							, "pattb." // strPrefixAlias
							);
						////
						TUtils.CUtils.MyBuildHTSupportedColumns(
							_cf.db // db
							, ref htSpCols // htSupportedColumns
							, "Prd_BOM" // strTableNameDB
							, "Prd_BOM." // strPrefixStd
							, "pbom." // strPrefixAlias
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
					, "zzB_Select_Mst_Product_zzE", zzB_Select_Mst_Product_zzE
					, "zzB_Select_Mst_ProductImages_zzE", zzB_Select_Mst_ProductImages_zzE
					, "zzB_Select_Mst_ProductFiles_zzE", zzB_Select_Mst_ProductFiles_zzE
					, "zzB_Select_Prd_BOM_zzE", zzB_Select_Prd_BOM_zzE
					, "zzB_Select_Prd_Attribute_zzE", zzB_Select_Prd_Attribute_zzE
					);
				#endregion

				#region // Get Data:
				DataSet dsGetData = _cf.db.ExecQuery(
					strSqlGetData
					, alParamsCoupleSql.ToArray()
					);
				int nIdxTable = 0;
				dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
				if (bGet_Mst_Product)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Mst_Product";
				}
				if (bGet_Mst_ProductImages)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Mst_ProductImages";
				}
				if (bGet_Mst_ProductFiles)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Mst_ProductFiles";
				}
				if (bGet_Prd_BOM)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Prd_BOM";
				}
				if (bGet_Prd_Attribute)
				{
					dsGetData.Tables[nIdxTable++].TableName = "Prd_Attribute";
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
		public DataSet WAS_Mst_Product_Get(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Product objRQ_Mst_Product
			////
			, out RT_Mst_Product objRT_Mst_Product
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Product.Tid;
			objRT_Mst_Product = new RT_Mst_Product();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Product.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Product_Get";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Product_Get;
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
				List<Mst_Product> lst_Mst_Product = new List<Mst_Product>();
				List<Mst_ProductImages> lst_Mst_ProductImages = new List<Mst_ProductImages>();
				List<Mst_ProductFiles> lst_Mst_ProductFiles = new List<Mst_ProductFiles>();
				List<Prd_BOM> lst_Prd_BOM = new List<Prd_BOM>();
				List<Prd_Attribute> lst_Prd_Attribute = new List<Prd_Attribute>();
				bool bGet_Mst_Product = (objRQ_Mst_Product.Rt_Cols_Mst_Product != null && objRQ_Mst_Product.Rt_Cols_Mst_Product.Length > 0);
				bool bGet_Mst_ProductImages = (objRQ_Mst_Product.Rt_Cols_Mst_ProductImages != null && objRQ_Mst_Product.Rt_Cols_Mst_ProductImages.Length > 0);
				bool bGet_Mst_ProductFiles = (objRQ_Mst_Product.Rt_Cols_Mst_ProductFiles != null && objRQ_Mst_Product.Rt_Cols_Mst_ProductFiles.Length > 0);
				bool bGet_Prd_BOM = (objRQ_Mst_Product.Rt_Cols_Prd_BOM != null && objRQ_Mst_Product.Rt_Cols_Prd_BOM.Length > 0);
				bool bGet_Prd_Attribute = (objRQ_Mst_Product.Rt_Cols_Prd_Attribute != null && objRQ_Mst_Product.Rt_Cols_Prd_Attribute.Length > 0);
				#endregion

				#region // MasterServerLogin:
				string strWebAPIUrl = null;
				{
					////
					strWebAPIUrl = "http://14.232.244.217:12088/idocNet.Test.ProductCenter.V10.Local.WA/";

					////
				}
				#endregion

				#region // WS_Mst_Product_Get:
				mdsResult = Mst_Product_Get(
					objRQ_Mst_Product.Tid // strTid
					, objRQ_Mst_Product.GwUserCode // strGwUserCode
					, objRQ_Mst_Product.GwPassword // strGwPassword
					, objRQ_Mst_Product.WAUserCode // strUserCode
					, objRQ_Mst_Product.WAUserPassword // strUserPassword
					, objRQ_Mst_Product.AccessToken // strAccessToken
					, objRQ_Mst_Product.NetworkID // strNetworkID
					, objRQ_Mst_Product.OrgID // strOrgID
					, ref alParamsCoupleError // alParamsCoupleError
											  //// Filter:
					, objRQ_Mst_Product.Ft_RecordStart // strFt_RecordStart
					, objRQ_Mst_Product.Ft_RecordCount // strFt_RecordCount
					, objRQ_Mst_Product.Ft_WhereClause // strFt_WhereClause
													   //// Return:
					, objRQ_Mst_Product.Rt_Cols_Mst_Product // strRt_Cols_Mst_Product
					, objRQ_Mst_Product.Rt_Cols_Mst_ProductImages // strRt_Cols_Mst_ProductImages
					, objRQ_Mst_Product.Rt_Cols_Mst_ProductFiles // strRt_Cols_Mst_ProductFiles
					, objRQ_Mst_Product.Rt_Cols_Prd_BOM // strRt_Cols_Prd_BOM
					, objRQ_Mst_Product.Rt_Cols_Prd_Attribute // strRt_Cols_Prd_Attribute
					);
				#endregion

				#region // GetData:
				if (!CmUtils.CMyDataSet.HasError(mdsResult))
				{
					////
					DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
					lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
					objRT_Mst_Product.MySummaryTable = lst_MySummaryTable[0];

					////
					if (bGet_Mst_Product)
					{
						////
						DataTable dt_Mst_Product = mdsResult.Tables["Mst_Product"].Copy();
						lst_Mst_Product = TUtils.DataTableCmUtils.ToListof<Mst_Product>(dt_Mst_Product);
						objRT_Mst_Product.Lst_Mst_Product = lst_Mst_Product;
					}
					////
					if (bGet_Mst_ProductImages)
					{
						////
						DataTable dt_Mst_ProductImages = mdsResult.Tables["Mst_ProductImages"].Copy();
						lst_Mst_ProductImages = TUtils.DataTableCmUtils.ToListof<Mst_ProductImages>(dt_Mst_ProductImages);
						objRT_Mst_Product.Lst_Mst_ProductImages = lst_Mst_ProductImages;
					}
					////
					if (bGet_Mst_ProductFiles)
					{
						////
						DataTable dt_Mst_ProductFiles = mdsResult.Tables["Mst_ProductFiles"].Copy();
						lst_Mst_ProductFiles = TUtils.DataTableCmUtils.ToListof<Mst_ProductFiles>(dt_Mst_ProductFiles);
						objRT_Mst_Product.Lst_Mst_ProductFiles = lst_Mst_ProductFiles;
					}
					////
					if (bGet_Prd_BOM)
					{
						////
						DataTable dt_Prd_BOM = mdsResult.Tables["Prd_BOM"].Copy();
						lst_Prd_BOM = TUtils.DataTableCmUtils.ToListof<Prd_BOM>(dt_Prd_BOM);
						objRT_Mst_Product.Lst_Prd_BOM = lst_Prd_BOM;
					}
					////
					if (bGet_Prd_Attribute)
					{
						////
						DataTable dt_Prd_Attribute = mdsResult.Tables["Prd_Attribute"].Copy();
						lst_Prd_Attribute = TUtils.DataTableCmUtils.ToListof<Prd_Attribute>(dt_Prd_Attribute);
						objRT_Mst_Product.Lst_Prd_Attribute = lst_Prd_Attribute;
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


		public DataSet Mst_Product_Save(
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
			string strFunctionName = "Mst_Product_Save";
			string strErrorCodeDefault = TError.ErridnInventory.Mst_Product_Save;
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
				Mst_Product_SaveX(
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

        private void Mst_Product_SaveX(
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
            int nTidSeq = 0;
            bool bMyDebugSql = false;
            string strFunctionName = "Mst_Product_UpdateX";
            //string strErrorCodeDefault = TError.ErridnDMS.Mst_Product_Update;
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

            #region //// Refine and Check Mst_Product:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            string strCreateDTime = null;
            string strCreateBy = null;

            ////
            DataTable dtDB_Mst_Product = null;
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

            #region //// Refine and Check Mst_Product:
            ////
            DataTable dtInput_Mst_Product = null;
            {
                ////
                string strTableCheck = "Mst_Product";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Product_Save_Input_Mst_ProductTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_Product = dsData.Tables[strTableCheck];
                ////
                if (dtInput_Mst_Product.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Product_Create_Input_Mst_ProductTblInvalid
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_Product // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "", "ProductCodeUser" // arrstrCouple
                    , "StdParam", "ProductCode" // arrstrCouple
                    , "StdParam", "ProductLevelSys" // arrstrCouple
                    , "", "BrandCode" // arrstrCouple
                    , "StdParam", "ProductType" // arrstrCouple
                    , "", "ProductGrpCode" // arrstrCouple
                    , "", "ProductName" // arrstrCouple
                    , "", "ProductNameEN" // arrstrCouple
                    , "", "ProductBarCode" // arrstrCouple
                    , "", "ProductCodeNetwork" // arrstrCouple
                    , "StdParam", "ProductCodeBase" // arrstrCouple
                    , "StdParam", "ProductCodeRoot" // arrstrCouple
                    , "", "ProductImagePathList" // arrstrCouple
                    , "", "ProductFilePathList" // arrstrCouple
                    , "", "FlagSerial" // arrstrCouple
                    , "", "FlagLot" // arrstrCouple
                    , "", "ValConvert" // arrstrCouple
                    , "", "UnitCode" // arrstrCouple
                    , "StdFlag", "FlagSell" // arrstrCouple
                    , "StdFlag", "FlagBuy" // arrstrCouple
                    , "", "UPBuy" // arrstrCouple
                    , "", "UPSell" // arrstrCouple
                    , "", "QtyMaxSt" // arrstrCouple
                    , "", "QtyMinSt" // arrstrCouple
                    , "", "QtyEffSt" // arrstrCouple
                    , "", "ListOfPrdDynamicFieldValue" // arrstrCouple
                    , "", "ProductStd" // arrstrCouple
                    , "", "ProductExpiry" // arrstrCouple
                    , "", "ProductQuyCach" // arrstrCouple
                    , "", "ProductMnfUrl" // arrstrCouple
                    , "", "ProductIntro" // arrstrCouple
                    , "", "ProductUserGuide" // arrstrCouple
                    , "", "ProductDrawing" // arrstrCouple
                    , "", "ProductOrigin" // arrstrCouple
                    , "StdFlag", "FlagFG" // arrstrCouple
                    , "", "VATRateCode" // arrstrCouple
                    , "", "Remark" // arrstrCouple
                    , "", "CustomField1" //
                    , "", "CustomField2" //
                    , "", "CustomField3" //
                    , "", "CustomField4" //
                    , "", "CustomField5" //
                                         //, "", "GLN" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "DTimeUsed", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "CreateDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "CreateBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "LUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "LUBy", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_Product, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_Product.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_Product.Rows[nScan];

                    ////
                    Mst_Product_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , drScan["OrgID"] // objOrgID
                        , drScan["ProductCode"] // objProductCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strStatusListToCheck
                        , out dtDB_Mst_Product // dtDB_Mst_Product
                        );
                    ////
                    //Mst_Product_CheckProductCodeUser(
                    //    ref alParamsCoupleError // alParamsCoupleError
                    //    , drScan["OrgID"] // objOrgID
                    //    , TUtils.CUtils.StdParam(drScan["ProductCodeUser"]) // objProductCode
                    //    , TConst.Flag.No // strFlagExistToCheck
                    //    , "" // strStatusListToCheck
                    //    , out dtDB_Mst_Product // dtDB_Mst_Product
                    //    );
                    ////
                    //DataTable dtDB_Mst_ProductType = null;

                    //Mst_ProductType_CheckDB(
                    //    ref alParamsCoupleError // alParamsCoupleError
                    //    , drScan["ProductType"] // objProductType
                    //    , TConst.Flag.Yes // strFlagExistToCheck
                    //    , TConst.Flag.Active // strFlagActiveListToCheck
                    //    , out dtDB_Mst_ProductType // dtDB_Mst_ProductType
                    //    );

                    //DataTable dtDB_Mst_VATRate = null;
                    //if (!string.IsNullOrEmpty(drScan["VATRateCode"].ToString()))
                    //{
                    //    Mst_VATRate_CheckDB(
                    //        ref alParamsCoupleError // alParamsCoupleError
                    //        , drScan["VATRateCode"] // objVATRateCode
                    //        , TConst.Flag.Yes // strFlagExistToCheck
                    //        , TConst.Flag.Active // strFlagActiveListToCheck
                    //        , out dtDB_Mst_VATRate // dtDB_Mst_VATRate
                    //        );
                    //}
                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["ProductCodeUser"] = TUtils.CUtils.StdParam(drScan["ProductCodeUser"]);
                    drScan["CreateDTimeUTC"] = strCreateDTime;
                    drScan["CreateBy"] = strWAUserCode;
                    drScan["LUDTimeUTC"] = strCreateDTime;
                    drScan["LUBy"] = strWAUserCode;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
            }
            #endregion

            #region //// SaveTemp Mst_Product:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_Product"
                    , new object[]{
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ProductCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ProductLevelSys", TConst.BizMix.Default_DBColType,
                        "ProductCodeUser", TConst.BizMix.Default_DBColType,
                        "BrandCode", TConst.BizMix.Default_DBColType,
                        "ProductType", TConst.BizMix.Default_DBColType,
                        "ProductGrpCode", TConst.BizMix.Default_DBColType,
                        "ProductName", TConst.BizMix.Default_DBColType,
                        "ProductNameEN", TConst.BizMix.Default_DBColType,
                        "ProductBarCode", TConst.BizMix.Default_DBColType,
                        "ProductCodeNetwork", TConst.BizMix.Default_DBColType,
                        "ProductCodeBase", TConst.BizMix.Default_DBColType,
                        "ProductCodeRoot", TConst.BizMix.Default_DBColType,
                        "ProductImagePathList", TConst.BizMix.MyText_DBColType,
                        "ProductFilePathList", TConst.BizMix.MyText_DBColType,
                        "FlagSerial", TConst.BizMix.Default_DBColType,
                        "FlagLot", TConst.BizMix.Default_DBColType,
                        "ValConvert", "float",
                        "UnitCode", TConst.BizMix.Default_DBColType,
                        "FlagSell", TConst.BizMix.Default_DBColType,
                        "FlagBuy", TConst.BizMix.Default_DBColType,
                        "UPBuy", "float",
                        "UPSell", "float",
                        "QtyMaxSt", "float",
                        "QtyMinSt", "float",
                        "QtyEffSt", "float",
                        "VATRateCode", TConst.BizMix.Default_DBColType,
                        "ListOfPrdDynamicFieldValue", TConst.BizMix.Default_DBColType,
                        "ProductStd", TConst.BizMix.MyText_DBColType,
                        "ProductExpiry", TConst.BizMix.MyText_DBColType,
                        "ProductQuyCach", TConst.BizMix.MyText_DBColType,
                        "ProductMnfUrl", TConst.BizMix.MyText_DBColType,
                        "ProductIntro", TConst.BizMix.MyText_DBColType,
                        "ProductUserGuide", TConst.BizMix.MyText_DBColType,
                        "ProductDrawing", TConst.BizMix.MyText_DBColType,
                        "ProductOrigin", TConst.BizMix.MyText_DBColType,
                        "FlagFG", TConst.BizMix.MyText_DBColType,
                        "CreateDTimeUTC", TConst.BizMix.Default_DBColType,
                        "CreateBy", TConst.BizMix.Default_DBColType,
                        "LUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LUBy", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "Remark", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        "DTimeUsed", TConst.BizMix.Default_DBColType,
                        "CustomField1", TConst.BizMix.Default_DBColType,
                        "CustomField2", TConst.BizMix.Default_DBColType,
                        "CustomField3", TConst.BizMix.Default_DBColType,
                        "CustomField4", TConst.BizMix.Default_DBColType,
                        "CustomField5", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_Product
                    );
            }
            #endregion

            #region //// Refine and Check Prd_Attribute:
            ////
            DataTable dtInput_Prd_Attribute = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "Prd_Attribute";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Product_Update_Input_Prd_AttributeTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Prd_Attribute = dsData.Tables[strTableCheck];
                ////
                ////
                //if (dtInput_Prd_Attribute.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrProductCenter.Mst_Product_Create_Input_Prd_AttributeTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Prd_Attribute // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "", "ProductCode" // arrstrCouple
                    , "", "AttributeCode" // arrstrCouple
                    , "", "AttributeValue" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Prd_Attribute, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Prd_Attribute, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Prd_Attribute, "LogLUDTime", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Prd_Attribute, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Prd_Attribute.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Prd_Attribute.Rows[nScan];

                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
                ////
            }
            #endregion

            #region //// SaveTemp Prd_Attribute:
            if (!bIsDelete)
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Prd_Attribute"
                    , new object[]{
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ProductCode", TConst.BizMix.Default_DBColType,
                        "AttributeCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "AttributeValue", TConst.BizMix.MyText_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Prd_Attribute
                    );
            }
            #endregion

            #region //// Refine and Check Prd_BOM:
            ////
            DataTable dtInput_Prd_BOM = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "Prd_BOM";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Product_Update_Input_Prd_BOMTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Prd_BOM = dsData.Tables[strTableCheck];
                ////
                ////
                //if (dtInput_Prd_BOM.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrProductCenter.Mst_Product_Create_Input_Prd_BOMTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Prd_BOM // dtData
                    , "StdParam", "OrgID" // arrstrCouple
                    , "", "ProductCode" // arrstrCouple
                    , "", "OrgIDParent" // arrstrCouple
                    , "", "ProductCodeParent" // arrstrCouple
                    , "", "Qty" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Prd_BOM, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Prd_BOM, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Prd_BOM, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Prd_BOM, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Prd_BOM.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Prd_BOM.Rows[nScan];
                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
                ////
            }
            #endregion

            #region //// SaveTemp Prd_BOM:
            if (!bIsDelete)
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Prd_BOM"
                    , new object[]{
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ProductCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "OrgIDParent", TConst.BizMix.Default_DBColType,
                        "ProductCodeParent", TConst.BizMix.Default_DBColType,
                        "Qty", "float",
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Prd_BOM
                    );
            }
            #endregion

            #region //// Refine and Check Mst_ProductImages:
            ////
            DataTable dtInput_Mst_ProductImages = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "Mst_ProductImages";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Product_Update_Input_Mst_ProductImagesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductImages = dsData.Tables[strTableCheck];
                ////
                ////
                //if (dtInput_Mst_ProductImages.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrProductCenter.Mst_Product_Create_Input_Mst_ProductImagesTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductImages // dtData
                    , "", "Idx" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    , "", "ProductCode" // arrstrCouple
                    , "", "ProductImageSpec" // arrstrCouple
                    , "", "ProductImagePath" // arrstrCouple
                    , "", "FlagIsImagePath" // arrstrCouple
                    , "", "ProductImageName" // arrstrCouple
                    , "", "ProductImageDesc" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductImages, "NetworkID", typeof(object));
                //TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductImages, "ProductImagePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductImages, "FlagPrimaryImage", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductImages, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductImages, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductImages, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_ProductImages.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductImages.Rows[nScan];
                    string strFlagIsImagePath = TUtils.CUtils.StdFlag(drScan["FlagIsImagePath"]);
                    string strFileName = drScan["ProductImageName"].ToString();

                    string[] typeFile = strFileName.Split('.');
                    string strtypeFile = typeFile.Last(); // Lấy phần mở rộng
                    string strGuid = Guid.NewGuid().ToString().Replace("-", ""); // Lấy GUID bỏ -
                    string strSub = strGuid.Substring(0, 10) + "." + strtypeFile;
                    ////
                    string strImagePath = null;
                    string strImageSpec = drScan["ProductImageSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, strSub);

                    ////
                    if (CmUtils.StringUtils.StringEqual(strFlagIsImagePath, TConst.Flag.Inactive))
                    {
                        string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                        string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                        string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
                        byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                        string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                        string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                        strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                        drScan["ProductImagePath"] = strImagePath;

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
                        #endregion
                    }

                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["FlagPrimaryImage"] = TConst.Flag.Active;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
                ////
            }
            #endregion

            #region //// SaveTemp Mst_ProductImages:
            if (!bIsDelete)
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_ProductImages"
                    , new object[]{
                        "Idx", "int",
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ProductCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ProductImagePath", TConst.BizMix.Default_DBColType,
                        "ProductImageName", TConst.BizMix.Default_DBColType,
                        "ProductImageDesc", TConst.BizMix.Default_DBColType,
                        "FlagPrimaryImage", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_ProductImages
                    );
            }
            #endregion

            #region //// Refine and Check Mst_ProductFiles:
            ////
            DataTable dtInput_Mst_ProductFiles = null;
            if (!bIsDelete)
            {
                ////
                string strTableCheck = "Mst_ProductFiles";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Mst_Product_Update_Input_Mst_ProductFilesTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Mst_ProductFiles = dsData.Tables[strTableCheck];
                ////
                //if (dtInput_Mst_ProductFiles.Rows.Count < 1)
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.TableName", strTableCheck
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErrProductCenter.Mst_Product_Create_Input_Mst_ProductFilesTblInvalid
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                TUtils.CUtils.StdDataInTable(
                    dtInput_Mst_ProductFiles // dtData
                    , "", "Idx" // arrstrCouple
                    , "StdParam", "OrgID" // arrstrCouple
                    , "", "ProductCode" // arrstrCouple
                    , "", "ProductFileSpec" // arrstrCouple
                    , "", "ProductFilePath" // arrstrCouple
                    , "", "FlagIsFilePath" // arrstrCouple
                    , "", "ProductFileName" // arrstrCouple
                                            //, "", "ProductFileDesc" // arrstrCouple
                    );
                ////
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductFiles, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductFiles, "ProductFileDesc", typeof(object));
                //TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductFiles, "ProductFilePath", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductFiles, "FlagPrimaryImage", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductFiles, "FlagActive", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductFiles, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dtInput_Mst_ProductFiles, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dtInput_Mst_ProductFiles.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dtInput_Mst_ProductFiles.Rows[nScan];
                    string strFlagIsFilePath = TUtils.CUtils.StdFlag(drScan["FlagIsFilePath"]);

                    ////
                    string strImagePath = null;
                    string strImageSpec = drScan["ProductFileSpec"].ToString();
                    string strImageName = string.Format("{0}.{1}.{2}", DateTime.Now.ToString("yyyyMMdd.HHmmss"), nTidSeq++, drScan["ProductFileName"].ToString());

                    ////
                    if (CmUtils.StringUtils.StringEqual(strFlagIsFilePath, TConst.Flag.Inactive))
                    {
                        string folderUpload = htCacheMstParam[TConst.Mst_Param.PARAM_UPLOADFILE].ToString();
                        string subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                        string strTVAN_FilePath = _cf.nvcParams["TVAN_FilePath"];
                        byte[] strDeCodeBase64 = Convert.FromBase64String(strImageSpec);
                        string strFilePathSave = string.Format("{0}\\{1}\\{2}\\{3}", strTVAN_FilePath, folderUpload, subFolder, strImageName);
                        string strFilePathBase = string.Format("{0}\\{1}\\{2}", strTVAN_FilePath, folderUpload, subFolder);
                        strImagePath = string.Format("{0}\\{1}\\{2}", folderUpload, subFolder, strImageName);

                        drScan["ProductFilePath"] = strImagePath;

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
                        #endregion
                    }

                    ////
                    drScan["NetworkID"] = nNetworkID;
                    drScan["FlagPrimaryImage"] = TConst.Flag.Active;
                    drScan["FlagActive"] = TConst.Flag.Active;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                    ////
                }
                ////
            }
            #endregion

            #region //// SaveTemp Mst_ProductFiles:
            if (!bIsDelete)
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Mst_ProductFiles"
                    , new object[]{
                        "Idx", "int",
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ProductCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ProductFilePath", TConst.BizMix.Default_DBColType,
                        "ProductFileName", TConst.BizMix.Default_DBColType,
                        "ProductFileDesc", TConst.BizMix.Default_DBColType,
						//"FlagPrimaryImage", TConst.BizMix.Default_DBColType,
						"FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Mst_ProductFiles
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
							---- Prd_Attribute:
                            delete t
                            from Prd_Attribute t --//[mylock]
	                            inner join #input_Mst_Product f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.ProductCode = f.ProductCode
                            where (1=1)
                            ;

							---- Prd_BOM:
							delete t
							from Prd_BOM t --//[mylock]
								inner join #input_Mst_Product f --//[mylock]
									on t.OrgIDParent = f.OrgID
										and t.ProductCodeParent = f.ProductCode
							where (1=1)
							;

							---- Mst_ProductFiles:
                            delete t
                            from Mst_ProductFiles t --//[mylock]
	                            inner join #input_Mst_Product f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.ProductCode = f.ProductCode
                            where (1=1)
                            ;

							---- Mst_ProductImages:
                            delete t
                            from Mst_ProductImages t --//[mylock]
	                            inner join #input_Mst_Product f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.ProductCode = f.ProductCode
                            where (1=1)
                            ;


                            ---- Mst_Product:
                            delete t
                            from Mst_Product t --//[mylock]
	                            inner join #input_Mst_Product f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.ProductCode = f.ProductCode
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
                string zzzzClauseInsert_Mst_Product_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_Product:
						insert into Mst_Product
						(
							OrgID
							, ProductCode
							, NetworkID
							, ProductLevelSys
							, ProductCodeUser
							, BrandCode
							, ProductType
							, ProductGrpCode
							, ProductName
							, ProductNameEN
							, ProductBarCode
							, ProductCodeNetwork
							, ProductCodeBase
							, ProductCodeRoot
							, ProductImagePathList
							, ProductFilePathList
							, FlagSerial
							, FlagLot
							, ValConvert
							, UnitCode
							, FlagSell
							, FlagBuy
							, UPBuy
							, UPSell
							, QtyMaxSt
							, QtyMinSt
							, QtyEffSt
                            , VATRateCode
							, ListOfPrdDynamicFieldValue
							, ProductStd
							, ProductExpiry
							, ProductQuyCach
							, ProductMnfUrl
							, ProductIntro
							, ProductUserGuide
							, ProductDrawing
							, ProductOrigin
                            , FlagFG
							, CreateDTimeUTC
							, CreateBy
							, LUDTimeUTC
							, LUBy
							, FlagActive
							, Remark
							, LogLUDTimeUTC
							, LogLUBy
                            , DTimeUsed
                            , CustomField1
                            , CustomField2
                            , CustomField3
                            , CustomField4
                            , CustomField5
						)
						select 
							t.OrgID
							, t.ProductCode
							, t.NetworkID
							, t.ProductLevelSys
							, t.ProductCodeUser
							, t.BrandCode
							, t.ProductType
							, t.ProductGrpCode
							, t.ProductName
							, t.ProductNameEN
							, t.ProductBarCode
							, t.ProductCodeNetwork
							, t.ProductCodeBase
							, t.ProductCodeRoot
							, t.ProductImagePathList
							, t.ProductFilePathList
							, t.FlagSerial
							, t.FlagLot
							, t.ValConvert
							, t.UnitCode
							, t.FlagSell
							, t.FlagBuy
							, t.UPBuy
							, t.UPSell
							, t.QtyMaxSt
							, t.QtyMinSt
							, t.QtyEffSt
                            , t.VATRateCode
							, t.ListOfPrdDynamicFieldValue
							, t.ProductStd
							, t.ProductExpiry
							, t.ProductQuyCach
							, t.ProductMnfUrl
							, t.ProductIntro
							, t.ProductUserGuide
							, t.ProductDrawing
							, t.ProductOrigin
                            , t.FlagFG
							, t.CreateDTimeUTC
							, t.CreateBy
							, t.LUDTimeUTC
							, t.LUBy
							, t.FlagActive
							, t.Remark
							, t.LogLUDTimeUTC
							, t.LogLUBy
                            , t.DTimeUsed
                            , t.CustomField1
                            , t.CustomField2
                            , t.CustomField3
                            , t.CustomField4
                            , t.CustomField5
						from #input_Mst_Product t --//[mylock]
						order by
							t.OrgID
							, t.ProductCode asc
						;
					");
                ////
                string zzzzClauseInsert_Mst_ProductImages_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_ProductImages:
						insert into Mst_ProductImages
						(
							Idx
							, OrgID
							, ProductCode
							, NetworkID
							, ProductImagePath
							, ProductImageName
							, ProductImageDesc
							, FlagPrimaryImage
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.Idx
							, t.OrgID
							, t.ProductCode
							, t.NetworkID
							, t.ProductImagePath
							, t.ProductImageName
							, t.ProductImageDesc
							, t.FlagPrimaryImage
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_Mst_ProductImages t --//[mylock]
						;
					");
                ////
                string zzzzClauseInsert_Mst_ProductFiles_zSave = CmUtils.StringUtils.Replace(@"
						---- Mst_ProductFiles:
						insert into Mst_ProductFiles
						(
							Idx
							, OrgID
							, ProductCode
							, NetworkID
							, ProductFilePath
							, ProductFileName
							, ProductFileDesc
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.Idx
							, t.OrgID
							, t.ProductCode
							, t.NetworkID
							, t.ProductFilePath
							, t.ProductFileName
							, t.ProductFileDesc
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_Mst_ProductFiles t --//[mylock]
						;
					");
                ////
                string zzzzClauseInsert_Prd_BOM_zSave = CmUtils.StringUtils.Replace(@"
						---- Prd_BOM:
						insert into Prd_BOM
						(
							OrgID
							, ProductCode
							, NetworkID
							, OrgIDParent
							, ProductCodeParent
							, Qty
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.OrgID
							, t.ProductCode
							, t.NetworkID
							, t.OrgIDParent
							, t.ProductCodeParent
							, t.Qty
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_Prd_BOM t --//[mylock]
						;
					");
                ////
                string zzzzClauseInsert_Prd_Attribute_zSave = CmUtils.StringUtils.Replace(@"
						---- Prd_Attribute:
						insert into Prd_Attribute
						(
							OrgID
							, ProductCode
							, AttributeCode
							, NetworkID
							, AttributeValue
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.OrgID
							, t.ProductCode
							, t.AttributeCode
							, t.NetworkID
							, t.AttributeValue
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_Prd_Attribute t --//[mylock]
						;
					");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_Mst_Product_zSave

						----
						zzzzClauseInsert_Mst_ProductImages_zSave
						
						----
						zzzzClauseInsert_Mst_ProductFiles_zSave

						----
						zzzzClauseInsert_Prd_BOM_zSave

						----
						zzzzClauseInsert_Prd_Attribute_zSave
					"
                    , "zzzzClauseInsert_Mst_Product_zSave", zzzzClauseInsert_Mst_Product_zSave
                    , "zzzzClauseInsert_Mst_ProductImages_zSave", zzzzClauseInsert_Mst_ProductImages_zSave
                    , "zzzzClauseInsert_Mst_ProductFiles_zSave", zzzzClauseInsert_Mst_ProductFiles_zSave
                    , "zzzzClauseInsert_Prd_BOM_zSave", zzzzClauseInsert_Prd_BOM_zSave
                    , "zzzzClauseInsert_Prd_Attribute_zSave", zzzzClauseInsert_Prd_Attribute_zSave
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
						drop table #input_Mst_Product;
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

        public DataSet WAS_Mst_Product_Save(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_Product objRQ_Mst_Product
			////
			, out RT_Mst_Product objRT_Mst_Product
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_Product.Tid;
			objRT_Mst_Product = new RT_Mst_Product();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Product.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Mst_Product_Save";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Mst_Product_Save;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Mst_Product", TJson.JsonConvert.SerializeObject(objRQ_Mst_Product.Lst_Mst_Product)
				, "Lst_Mst_ProductImages", TJson.JsonConvert.SerializeObject(objRQ_Mst_Product.Lst_Mst_ProductImages)
				, "Lst_Mst_ProductFiles", TJson.JsonConvert.SerializeObject(objRQ_Mst_Product.Lst_Mst_ProductFiles)
				, "Lst_Prd_BOM", TJson.JsonConvert.SerializeObject(objRQ_Mst_Product.Lst_Prd_BOM)
				, "Lst_Prd_Attribute", TJson.JsonConvert.SerializeObject(objRQ_Mst_Product.Lst_Prd_Attribute)
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
					DataTable dt_Mst_Product = TUtils.DataTableCmUtils.ToDataTable<Mst_Product>(objRQ_Mst_Product.Lst_Mst_Product, "Mst_Product");
					dsData.Tables.Add(dt_Mst_Product);
					////
					DataTable dt_Mst_ProductImages = null;
					if (objRQ_Mst_Product.Lst_Mst_ProductImages != null)
					{
						dt_Mst_ProductImages = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductImages>(objRQ_Mst_Product.Lst_Mst_ProductImages, "Mst_ProductImages");
						dsData.Tables.Add(dt_Mst_ProductImages);
					}
					else
					{
						dt_Mst_ProductImages = TDALUtils.DBUtils.GetSchema(_cf.db, "Mst_ProductImages").Tables[0];
						dsData.Tables.Add(dt_Mst_ProductImages.Copy());
					}
					////
					DataTable dt_Mst_ProductFiles = null;
					if (objRQ_Mst_Product.Lst_Mst_ProductFiles != null)
					{
						dt_Mst_ProductFiles = TUtils.DataTableCmUtils.ToDataTable<Mst_ProductFiles>(objRQ_Mst_Product.Lst_Mst_ProductFiles, "Mst_ProductFiles");
						dsData.Tables.Add(dt_Mst_ProductFiles);
					}
					else
					{
						dt_Mst_ProductFiles = TDALUtils.DBUtils.GetSchema(_cf.db, "Mst_ProductFiles").Tables[0];
						dsData.Tables.Add(dt_Mst_ProductFiles.Copy());
					}
					////
					DataTable dt_Prd_BOM = null;
					if (objRQ_Mst_Product.Lst_Prd_BOM != null)
					{
						dt_Prd_BOM = TUtils.DataTableCmUtils.ToDataTable<Prd_BOM>(objRQ_Mst_Product.Lst_Prd_BOM, "Prd_BOM");
						dsData.Tables.Add(dt_Prd_BOM);
					}
					else
					{
						dt_Prd_BOM = TDALUtils.DBUtils.GetSchema(_cf.db, "Prd_BOM").Tables[0];
						dsData.Tables.Add(dt_Prd_BOM.Copy());
					}
					////
					DataTable dt_Prd_Attribute = null;
					if (objRQ_Mst_Product.Lst_Prd_Attribute != null)
					{
						dt_Prd_Attribute = TUtils.DataTableCmUtils.ToDataTable<Prd_Attribute>(objRQ_Mst_Product.Lst_Prd_Attribute, "Prd_Attribute");
						dsData.Tables.Add(dt_Prd_Attribute);
					}
					else
					{
						dt_Prd_Attribute = TDALUtils.DBUtils.GetSchema(_cf.db, "Prd_Attribute").Tables[0];
						dsData.Tables.Add(dt_Prd_Attribute.Copy());
					}
				}
				#endregion

				#region // Mst_Product_Save:
				mdsResult = Mst_Product_Save(
					objRQ_Mst_Product.Tid // strTid
					, objRQ_Mst_Product.GwUserCode // strGwUserCode
					, objRQ_Mst_Product.GwPassword // strGwPassword
					, objRQ_Mst_Product.WAUserCode // strUserCode
					, objRQ_Mst_Product.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_Mst_Product.FlagIsDelete // objFlagIsDelete
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

        #region // Product_CustomField:
        public DataSet WAS_Product_CustomField_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Product_CustomField objRQ_Product_CustomField
            ////
            , out RT_Product_CustomField objRT_Product_CustomField
            )
        {
            #region // Temp:
            string strTid = objRQ_Product_CustomField.Tid;
            objRT_Product_CustomField = new RT_Product_CustomField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Product_CustomField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Product_CustomField_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Product_CustomField_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Lst_Product_CustomField", TJson.JsonConvert.SerializeObject(objRQ_Product_CustomField.Lst_Product_CustomField)
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
                    DataTable dt_Product_CustomField = TUtils.DataTableCmUtils.ToDataTable<Product_CustomField>(objRQ_Product_CustomField.Lst_Product_CustomField, "Product_CustomField");
                    dsData.Tables.Add(dt_Product_CustomField);
                    ////
                }
                #endregion

                #region // Product_CustomField_Save:
                mdsResult = Product_CustomField_Save(
                    objRQ_Product_CustomField.Tid // strTid
                    , objRQ_Product_CustomField.GwUserCode // strGwUserCode
                    , objRQ_Product_CustomField.GwPassword // strGwPassword
                    , objRQ_Product_CustomField.WAUserCode // strUserCode
                    , objRQ_Product_CustomField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Product_CustomField.FlagIsDelete // objFlagIsDelete
                    , objRQ_Product_CustomField.FuncType // FuncType
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
        public DataSet Product_CustomField_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objFlagIsDelete
            , object objFuncType
            ////
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //bool bMyDebugSql = false;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Product_CustomField_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Product_CustomField_Save;
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
                Product_CustomField_SaveX(
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

        private void Product_CustomField_SaveX(
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
            string strFunctionName = "Product_CustomField_UpdateX";
            //string strErrorCodeDefault = TError.ErridnInventory.Product_CustomField_Update;
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

            #region //// Refine and Check Product_CustomField:
            ////
            bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
            string strCreateDTime = null;
            string strCreateBy = null;

            ////
            //DataTable dtDB_Product_CustomField = null;
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

            #region //// Refine and Check Product_CustomField:
            ////
            DataTable dtInput_Product_CustomField = null;
            {
                ////
                string strTableCheck = "Product_CustomField";
                ////
                if (!dsData.Tables.Contains(strTableCheck))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.TableName", strTableCheck
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Product_CustomField_Save_Input_Product_CustomFieldTblNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                dtInput_Product_CustomField = dsData.Tables[strTableCheck];
            }
            #endregion

            #region //// SaveTemp Product_CustomField:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    _cf.db
                    , "#input_Product_CustomField"
                    , new object[]{
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ProductCustomFieldCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "ProductCustomFieldName", TConst.BizMix.Default_DBColType,
                        "DBPhysicalType", TConst.BizMix.Default_DBColType,
                        "FlagActive", TConst.BizMix.Default_DBColType,
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dtInput_Product_CustomField
                    );
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
                            ---- Product_CustomField:
                            delete t
                            from Product_CustomField t --//[mylock]
	                            inner join #input_Product_CustomField f --//[mylock]
		                            on t.OrgID = f.OrgID
										and t.ProductCustomFieldCode = f.ProductCustomFieldCode
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
                string zzzzClauseInsert_Product_CustomField_zSave = CmUtils.StringUtils.Replace(@"
						---- Product_CustomField:
						insert into Product_CustomField
						(
							OrgID
							, ProductCustomFieldCode
							, NetworkID
							, ProductCustomFieldName
							, DBPhysicalType
							, FlagActive
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.OrgID
							, t.ProductCustomFieldCode
							, t.NetworkID
							, t.ProductCustomFieldName
							, t.DBPhysicalType
							, t.FlagActive
							, t.LogLUDTimeUTC
							, t.LogLUBy
						from #input_Product_CustomField t --//[mylock]
						order by
							t.OrgID
							, t.ProductCustomFieldCode asc
						;
					");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
						----
						zzzzClauseInsert_Product_CustomField_zSave
					"
                    , "zzzzClauseInsert_Product_CustomField_zSave", zzzzClauseInsert_Product_CustomField_zSave
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
						drop table #input_Product_CustomField;
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

        public DataSet WAS_Product_CustomField_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Product_CustomField objRQ_Product_CustomField
            ////
            , out RT_Product_CustomField objRT_Product_CustomField
            )
        {
            #region // Temp:
            string strTid = objRQ_Product_CustomField.Tid;
            objRT_Product_CustomField = new RT_Product_CustomField();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Product_CustomField.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Product_CustomField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Product_CustomField_Get;
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
                List<Product_CustomField> lst_Product_CustomField = new List<Product_CustomField>();
                #endregion

                #region // WS_Product_CustomField_Get:
                mdsResult = Product_CustomField_Get(
                    objRQ_Product_CustomField.Tid // strTid
                    , objRQ_Product_CustomField.GwUserCode // strGwUserCode
                    , objRQ_Product_CustomField.GwPassword // strGwPassword
                    , objRQ_Product_CustomField.WAUserCode // strUserCode
                    , objRQ_Product_CustomField.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Product_CustomField.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Product_CustomField.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Product_CustomField.Ft_WhereClause // strFt_WhereClause
                                                               //// Return:
                    , objRQ_Product_CustomField.Rt_Cols_Product_CustomField // strRt_Cols_Product_CustomField
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Product_CustomField = mdsResult.Tables["Product_CustomField"].Copy();
                    lst_Product_CustomField = TUtils.DataTableCmUtils.ToListof<Product_CustomField>(dt_Product_CustomField);
                    objRT_Product_CustomField.Lst_Product_CustomField = lst_Product_CustomField;
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
        public DataSet Product_CustomField_Get(
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
            , string strRt_Cols_Product_CustomField
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Product_CustomField_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Product_CustomField_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        //// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			        //// Return
					, "strRt_Cols_Product_CustomField", strRt_Cols_Product_CustomField
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

                #region // Product_CustomField_GetX:
                DataSet dsGetData = null;
                {
                    Product_CustomField_GetX(
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
                        , strRt_Cols_Product_CustomField // strRt_Cols_Product_CustomField
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

        private void Product_CustomField_GetX(
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
            , string strRt_Cols_Product_CustomField
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Product_CustomField_GetX";
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
            bool bGet_Product_CustomField = (strRt_Cols_Product_CustomField != null && strRt_Cols_Product_CustomField.Length > 0);

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
					---- #tbl_Product_CustomField_Filter_Draft:
					select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, mo.OrgID
                        , mo.ProductCustomFieldCode
					into #tbl_Product_CustomField_Filter_Draft
					from Product_CustomField mo --//[mylock]
					where (1=1)
						zzB_Where_strFilter_zzE
					order by mo.OrgID asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Product_CustomField_Filter_Draft t --//[mylock]
					;

					---- #tbl_Product_CustomField_Filter:
					select
						t.*
					into #tbl_Product_CustomField_Filter
					from #tbl_Product_CustomField_Filter_Draft t --//[mylock]
					where (1=1)
						and (t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Product_CustomField -----:
					zzB_Select_Product_CustomField_zzE
					------------------------

					---- Clear for debug:
					--drop table #tbl_Product_CustomField_Filter_Draft;
					--drop table #tbl_Product_CustomField_Filter;
					"
                );
            ////
            string zzB_Select_Product_CustomField_zzE = "-- Nothing.";
            if (bGet_Product_CustomField)
            {
                #region // bGet_Product_CustomField:
                zzB_Select_Product_CustomField_zzE = CmUtils.StringUtils.Replace(@"
					---- Product_CustomField:
					select
						t.MyIdxSeq
						, mo.*
					from #tbl_Product_CustomField_Filter t --//[mylock]
						inner join Product_CustomField mo --//[mylock]
							on t.OrgID = mo.OrgID
                                and t.ProductCustomFieldCode = mo.ProductCustomFieldCode
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
                        , "Product_CustomField" // strTableNameDB
                        , "Product_CustomField." // strPrefixStd
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
                , "zzB_Select_Product_CustomField_zzE", zzB_Select_Product_CustomField_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Product_CustomField)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Product_CustomField";
            }
            #endregion
        }
        #endregion
    }
}
