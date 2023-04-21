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



namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
		#region // Email_BatchSendEmail:
		private void Email_BatchSendEmail_CheckDB(
			ref ArrayList alParamsCoupleError
			, object objBatchNo
			, string strFlagExistToCheck
			, string strStatusListToCheck
			, out DataTable dtDB_Email_BatchSendEmail
			)
		{
			// GetInfo:
			string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Email_BatchSendEmail t --//[mylock]
					where (1=1)
						and t.BatchNo = @objBatchNo
					;
				");
			dtDB_Email_BatchSendEmail = _cf.db.ExecQuery(
				strSqlExec
				, "@objBatchNo", objBatchNo
				).Tables[0];
			dtDB_Email_BatchSendEmail.TableName = "Email_BatchSendEmail";

			// strFlagExistToCheck:
			if (strFlagExistToCheck.Length > 0)
			{
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Email_BatchSendEmail.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.BatchNo", objBatchNo
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Email_BatchSendEmail_CheckDB_BatchSendEmailNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Email_BatchSendEmail.Rows.Count > 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.BatchNo", objBatchNo
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Email_BatchSendEmail_CheckDB_BatchSendEmailExist
						, null
						, alParamsCoupleError.ToArray()
						);
				}
			}
			// strStatusListToCheck:
			if (strStatusListToCheck.Length > 0 && !strStatusListToCheck.Contains(Convert.ToString(dtDB_Email_BatchSendEmail.Rows[0]["BatchStatus"])))
			{
				alParamsCoupleError.AddRange(new object[]{
					"Check.BatchNo", objBatchNo
					, "Check.strStatusListToCheck", strStatusListToCheck
					, "DB.Status", dtDB_Email_BatchSendEmail.Rows[0]["BatchStatus"]
					});
				throw CmUtils.CMyException.Raise(
					TError.ErridnInventory.Email_BatchSendEmail_CheckDB_StatusNotMatched
					, null
					, alParamsCoupleError.ToArray()
					);
			}
		}

		private void Email_BatchSendEmail_SaveX(
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
			, object objBatchNo
			, object objConfigCode
			, object objTEmailCode
			, object objSubject
			, object objBodyText
			, object objBodyHTML
			, object objWSPath
			, DataSet dsData
			)
		{
			#region // Temp:
			//mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			bool bMyDebugSql = false;
			//DateTime dtimeSys = DateTime.Now;
			//bool bNeedTransaction = true;
			string strFunctionName = "Email_BatchSendEmail_SaveX";
			//string strErrorCodeDefault = TError.ErridnInventory.Email_BatchSendEmail_SaveX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objBatchNo", objBatchNo
				, "objConfigCode", objConfigCode
				, "objTEmailCode", objTEmailCode
				, "objSubject", objSubject
				, "objBodyText", objBodyText
				, "objBodyHTML", objBodyHTML
				, "objWSPath", objWSPath
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Convert Input:
			alParamsCoupleError.AddRange(new object[]{
				"Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
				});
			#endregion

			#region // Init:
			#endregion

			#region //// Refine and Check Email_BatchSendEmail:
			////
			bool bIsDelete = CmUtils.StringUtils.StringEqual(objFlagIsDelete, TConst.Flag.Yes);
			////
			string strBatchNo = TUtils.CUtils.StdParam(objBatchNo);
			string strConfigCode = TUtils.CUtils.StdParam(objConfigCode);
			string strTEmailCode = TUtils.CUtils.StdParam(objTEmailCode);
			string strSubject = string.Format("{0}", objSubject).Trim();
			string strBodyText = string.Format("{0}", objBodyText).Trim();
			string strBodyHTML = string.Format("{0}", objBodyHTML).Trim();
			string strWSPath = string.Format("{0}", objWSPath).Trim();
			//string strCreateDTime = null;
			//string strCreateBy = null;

			// drAbilityOfUser:
			//DataRow drAbilityOfUser = myCommon_GetAbilityOfUser(strWAUserCode);
			//myCommon_CheckHTCDirect(
			//	ref alParamsCoupleError // alParamsCoupleError
			//	, drAbilityOfUser // drAbilityOfUser
			//	, TConst.Flag.Active // strFlagDirectListToCheck
			//	);
			////
			DataTable dtDB_Email_BatchSendEmail = null;
			{
				////
				Email_BatchSendEmail_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError					
					, strBatchNo // objBatchNo
					, "" // strFlagExistToCheck
					, "" // strStatusListToCheck
					, out dtDB_Email_BatchSendEmail // dtDB_Email_BatchSendEmail
					);
				if (dtDB_Email_BatchSendEmail.Rows.Count < 1) // Chưa Tồn tại.
				{
					if (bIsDelete)
					{
						goto MyCodeLabel_Done; // Thành công.
					}
					else
					{
						////	
					}
				}
				else // Đã Tồn tại.
				{
					
				}
				////
			}
			////
			#endregion

			#region //// SaveTemp Email_BatchSendEmail:
			if (!bIsDelete)
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Email_BatchSendEmail"
					, new object[]{
						"BatchNo", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"ConfigCode", TConst.BizMix.Default_DBColType,
						"TEmailCode", TConst.BizMix.Default_DBColType,
						"Subject", TConst.BizMix.Default_DBColType,
						"BodyText", TConst.BizMix.MyText_DBColType,
						"BodyHTML", TConst.BizMix.MyText_DBColType,
						"WSPath", TConst.BizMix.Default_DBColType,
						"CreatedDate", TConst.BizMix.Default_DBColType,
						"EffectDate", TConst.BizMix.Default_DBColType,
						"SendBy", TConst.BizMix.Default_DBColType,
						"DealerCode", TConst.BizMix.Default_DBColType,
						"BatchStatus", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, new object[]{
						new object[]{
							strBatchNo, // BatchNo
							nNetworkID, // NetworkID
							strConfigCode, // ConfigCode
							strTEmailCode, // TEmailCode
							strSubject, // Subject
							strBodyText, // BodyText
							strBodyHTML, // BodyHTML
							strWSPath, // WSPath
							strTEmailCode, // TEmailCode
							dtimeSys.ToString("yyyy-MM-dd"), // CreatedDate
							dtimeSys.ToString("yyyy-MM-dd"), // SendBy
							TConst.BizMix.DealerCodeRoot, // DealerCode
							TConst.BatchStatus.Approve, // BatchStatus
							dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
							strWAUserCode, // LogLUBy
							}
						}
					);
			}
			#endregion

			#region // Check:
			{
				////
				string strSqlCheck = CmUtils.StringUtils.Replace(@"
						select * from #input_Email_BatchSendEmail
					");

				//DataSet dsExec = _cf.db.ExecQuery(strSqlCheck);
			}
			#endregion

			#region //// Refine and Check Email_BatchSendEmailTo:
			////
			DataTable dtInput_Email_BatchSendEmailTo = null;
			if (!bIsDelete)
			{
				////
				string strTableCheck = "Email_BatchSendEmailTo";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailToTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Email_BatchSendEmailTo = dsData.Tables[strTableCheck];
				////
				if (dtInput_Email_BatchSendEmailTo.Rows.Count < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailToTblInvalid
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Email_BatchSendEmailTo // dtData
					, "", "EmailCode" // arrstrCouple					
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailTo, "BatchNo", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailTo, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailTo, "BatchStatusTo", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailTo, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailTo, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Email_BatchSendEmailTo.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Email_BatchSendEmailTo.Rows[nScan];

					////					
					drScan["BatchNo"] = strBatchNo;
					drScan["NetworkID"] = nNetworkID;
					drScan["BatchStatusTo"] = TConst.BatchStatus.Approve;
					//drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp Email_BatchSendEmailTo:
			if (!bIsDelete)
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Email_BatchSendEmailTo"
					, new object[]{
						"BatchNo", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"EmailCode", TConst.BizMix.Default_DBColType,
						"BatchStatusTo", TConst.BizMix.Default_DBColType,
						//"Remark", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Email_BatchSendEmailTo
					);
			}
			#endregion

			#region //// Refine and Check Email_BatchSendEmailCC:
			////
			DataTable dtInput_Email_BatchSendEmailCC = null;
			if (!bIsDelete)
			{
				////
				string strTableCheck = "Email_BatchSendEmailCC";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailCCTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Email_BatchSendEmailCC = dsData.Tables[strTableCheck];
				////
				//if (dtInput_Email_BatchSendEmailCC.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailCCTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Email_BatchSendEmailCC // dtData
					, "", "EmailCode" // arrstrCouple					
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailCC, "BatchNo", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailCC, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailCC, "BatchStatusCC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailCC, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailCC, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Email_BatchSendEmailCC.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Email_BatchSendEmailCC.Rows[nScan];

					////					
					drScan["BatchNo"] = strBatchNo;
					drScan["NetworkID"] = nNetworkID;
					drScan["BatchStatusCC"] = TConst.BatchStatus.Approve;
					//drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp Email_BatchSendEmailCC:
			if (!bIsDelete)
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Email_BatchSendEmailCC"
					, new object[]{
						"BatchNo", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"EmailCode", TConst.BizMix.Default_DBColType,
						"BatchStatusCC", TConst.BizMix.Default_DBColType,
						//"Remark", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Email_BatchSendEmailCC
					);
			}
			#endregion

			#region //// Refine and Check Email_BatchSendEmailBCC:
			////
			DataTable dtInput_Email_BatchSendEmailBCC = null;
			if (!bIsDelete)
			{
				////
				string strTableCheck = "Email_BatchSendEmailBCC";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailBCCTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Email_BatchSendEmailBCC = dsData.Tables[strTableCheck];
				////
				//if (dtInput_Email_BatchSendEmailBCC.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailBCCTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Email_BatchSendEmailBCC // dtData
					, "", "EmailCode" // arrstrCouple					
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailBCC, "BatchNo", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailBCC, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailBCC, "BatchStatusBCC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailBCC, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailBCC, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Email_BatchSendEmailBCC.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Email_BatchSendEmailBCC.Rows[nScan];

					////					
					drScan["BatchNo"] = strBatchNo;
					drScan["NetworkID"] = nNetworkID;
					drScan["BatchStatusBCC"] = TConst.BatchStatus.Approve;
					//drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp Email_BatchSendEmailBCC:
			if (!bIsDelete)
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Email_BatchSendEmailBCC"
					, new object[]{
						"BatchNo", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"EmailCode", TConst.BizMix.Default_DBColType,
						"BatchStatusBCC", TConst.BizMix.Default_DBColType,
						//"Remark", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Email_BatchSendEmailBCC
					);
			}
			#endregion

			#region //// Refine and Check Email_BatchSendEmailFileAttach:
			////
			DataTable dtInput_Email_BatchSendEmailFileAttach = null;
			if (!bIsDelete)
			{
				////
				string strTableCheck = "Email_BatchSendEmailFileAttach";
				////
				if (!dsData.Tables.Contains(strTableCheck))
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.TableName", strTableCheck
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailFileAttachTblNotFound
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				dtInput_Email_BatchSendEmailFileAttach = dsData.Tables[strTableCheck];
				////
				//if (dtInput_Email_BatchSendEmailFileAttach.Rows.Count < 1)
				//{
				//	alParamsCoupleError.AddRange(new object[]{
				//		"Check.TableName", strTableCheck
				//		});
				//	throw CmUtils.CMyException.Raise(
				//		TError.ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailFileAttachTblInvalid
				//		, null
				//		, alParamsCoupleError.ToArray()
				//		);
				//}
				////
				TUtils.CUtils.StdDataInTable(
					dtInput_Email_BatchSendEmailFileAttach // dtData
					, "", "FilePath" // arrstrCouple					
					);
				////
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailFileAttach, "BatchNo", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailFileAttach, "NetworkID", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailFileAttach, "BatchStatusFA", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailFileAttach, "LogLUDTimeUTC", typeof(object));
				TUtils.CUtils.MyForceNewColumn(ref dtInput_Email_BatchSendEmailFileAttach, "LogLUBy", typeof(object));
				////
				for (int nScan = 0; nScan < dtInput_Email_BatchSendEmailFileAttach.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dtInput_Email_BatchSendEmailFileAttach.Rows[nScan];

					////					
					drScan["BatchNo"] = strBatchNo;
                    /// ThomPTT.20190718.
                    drScan["FilePath"] = string.Format("{0}{1}", _cf.nvcParams["TVAN_FilePath"], drScan["FilePath"].ToString().Replace(@"/", @"\"));
                    //alParamsCoupleError.AddRange(new object[]{
                    //    "Check.FilePath", drScan["FilePath"]
                    //    });
                    drScan["NetworkID"] = nNetworkID;
					drScan["BatchStatusFA"] = TConst.BatchStatus.Approve;
					//drScan["Remark"] = string.Format("{0}", drScan["Remark"]).Trim();
					drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					drScan["LogLUBy"] = strWAUserCode;
					////
				}
			}
			#endregion

			#region //// SaveTemp Email_BatchSendEmailFileAttach:
			if (!bIsDelete)
			{
				TUtils.CUtils.MyBuildDBDT_Common(
					_cf.db
					, "#input_Email_BatchSendEmailFileAttach"
					, new object[]{
						"BatchNo", TConst.BizMix.Default_DBColType,
						"NetworkID", TConst.BizMix.Default_DBColType,
						"FilePath", TConst.BizMix.Default_DBColType,
						"BatchStatusFA", TConst.BizMix.Default_DBColType,
						//"Remark", TConst.BizMix.Default_DBColType,
						"LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
						"LogLUBy", TConst.BizMix.Default_DBColType,
						}
					, dtInput_Email_BatchSendEmailFileAttach
					);
			}
			#endregion

			#region // Check:
			{
				string strSqlCheck = CmUtils.StringUtils.Replace(@"
						select * from #input_Email_BatchSendEmail

						select * from #input_Email_BatchSendEmailTo

						select * from #input_Email_BatchSendEmailCC

						select * from #input_Email_BatchSendEmailBCC

						select * from #input_Email_BatchSendEmailFileAttach
					");

				DataSet dsExec = _cf.db.ExecQuery(strSqlCheck);
			}
			#endregion

			#region //// Save:
			//// Clear All:
			{
				string strSqlDelete = CmUtils.StringUtils.Replace(@"
								---- Email_BatchSendEmailFileAttach:
								delete t
								from Email_BatchSendEmailFileAttach t
								where (1=1)
									and t.BatchNo = @strBatchNo
								;

								---- Email_BatchSendEmailBCC:
								delete t
								from Email_BatchSendEmailBCC t
								where (1=1)
									and t.BatchNo = @strBatchNo
								;

								---- Email_BatchSendEmailCC:
								delete t
								from Email_BatchSendEmailCC t
								where (1=1)
									and t.BatchNo = @strBatchNo
								;
				
								---- Email_BatchSendEmailTo:
								delete t
								from Email_BatchSendEmailTo t
								where (1=1)
									and t.BatchNo = @strBatchNo
								;

								---- Email_BatchSendEmail:
								delete t
								from Email_BatchSendEmail t
								where (1=1)
									and t.BatchNo = @strBatchNo
								;

							");
				_cf.db.ExecQuery(
					strSqlDelete
					, "@strBatchNo", strBatchNo
					);
			}

			//// Insert All:
			if (!bIsDelete)
			{
				////
				string zzzzClauseInsert_Email_BatchSendEmail_zSave = CmUtils.StringUtils.Replace(@"
							---- Email_BatchSendEmail:
							insert into Email_BatchSendEmail
							(
								BatchNo
								, NetworkID
								, ConfigCode
								, TEmailCode
								, Subject
								, BodyText
								, BodyHTML
								, WSPath
								, CreatedDate
								, EffectDate
								, SendBy
								, DealerCode
								, BatchStatus
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.BatchNo
								, t.NetworkID
								, t.ConfigCode
								, t.TEmailCode
								, t.Subject
								, t.BodyText
								, t.BodyHTML
								, t.WSPath
								, t.CreatedDate
								, t.EffectDate
								, t.SendBy
								, t.DealerCode
								, t.BatchStatus
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Email_BatchSendEmail t --//[mylock]
							;
						");
				////
				string zzzzClauseInsert_Email_BatchSendEmailTo_zSave = "-- Nothing.";
				//if (CmUtils.StringUtils.StringEqual(strPaymentType, TConst.PaymentType.LC))
				{
					zzzzClauseInsert_Email_BatchSendEmailTo_zSave = CmUtils.StringUtils.Replace(@"
							---- Email_BatchSendEmailTo:
							insert into Email_BatchSendEmailTo
							(
								BatchNo
								, NetworkID
								, EmailCode
								, BatchStatusTo
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.BatchNo
								, t.NetworkID
 								, t.EmailCode
								, t.BatchStatusTo
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Email_BatchSendEmailTo t --//[mylock]
							;
						");
				}
				////
				string zzzzClauseInsertEmail_BatchSendEmailCC_zSave = "-- Nothing.";
				//if (CmUtils.StringUtils.StringEqual(strPaymentType, TConst.PaymentType.LC))
				{
					zzzzClauseInsertEmail_BatchSendEmailCC_zSave = CmUtils.StringUtils.Replace(@"
							---- Email_BatchSendEmailTo:
							insert into Email_BatchSendEmailTo
							(
								BatchNo
								, NetworkID
								, EmailCode
								, BatchStatusCC
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.BatchNo
								, t.NetworkID
 								, t.EmailCode
								, t.BatchStatusCC
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #inputEmail_BatchSendEmailCC t --//[mylock]
							;
						");
				}
				////
				string zzzzClauseInsert_Email_BatchSendEmailCC_zSave = "-- Nothing.";
				//if (CmUtils.StringUtils.StringEqual(strPaymentType, TConst.PaymentType.LC))
				{
					zzzzClauseInsert_Email_BatchSendEmailCC_zSave = CmUtils.StringUtils.Replace(@"
							---- Email_BatchSendEmailCC:
							insert into Email_BatchSendEmailCC
							(
								BatchNo
								, NetworkID
								, EmailCode
								, BatchStatusCC
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.BatchNo
								, t.NetworkID
 								, t.EmailCode
								, t.BatchStatusCC
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Email_BatchSendEmailCC t --//[mylock]
							;
						");
				}
				////
				string zzzzClauseInsert_Email_BatchSendEmailBCC_zSave = "-- Nothing.";
				//if (CmUtils.StringUtils.StringEqual(strPaymentType, TConst.PaymentType.LC))
				{
					zzzzClauseInsert_Email_BatchSendEmailBCC_zSave = CmUtils.StringUtils.Replace(@"
							---- Email_BatchSendEmailBCC:
							insert into Email_BatchSendEmailBCC
							(
								BatchNo
								, NetworkID
								, EmailCode
								, BatchStatusBCC
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.BatchNo
								, t.NetworkID
 								, t.EmailCode
								, t.BatchStatusBCC
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Email_BatchSendEmailBCC t --//[mylock]
							;
						");
				}
				////
				string zzzzClauseInsert_Email_BatchSendEmailFileAttach_zSave = "-- Nothing.";
				//if (CmUtils.StringUtils.StringEqual(strPaymentType, TConst.PaymentType.LC))
				{
					zzzzClauseInsert_Email_BatchSendEmailFileAttach_zSave = CmUtils.StringUtils.Replace(@"
							---- Email_BatchSendEmailFileAttach:
							insert into Email_BatchSendEmailFileAttach
							(
								BatchNo
								, NetworkID
								, FilePath
								, BatchStatusFA
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.BatchNo
								, t.NetworkID
 								, t.FilePath
								, t.BatchStatusFA
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Email_BatchSendEmailFileAttach t --//[mylock]
							;
						");
				}
				////
				string strSqlExec = CmUtils.StringUtils.Replace(@"
							----
							zzzzClauseInsert_Email_BatchSendEmail_zSave
							----
							zzzzClauseInsert_Email_BatchSendEmailTo_zSave
							----
							zzzzClauseInsert_Email_BatchSendEmailCC_zSave
							----
							zzzzClauseInsert_Email_BatchSendEmailBCC_zSave
							----
							zzzzClauseInsert_Email_BatchSendEmailFileAttach_zSave
							----
						"
					, "zzzzClauseInsert_Email_BatchSendEmail_zSave", zzzzClauseInsert_Email_BatchSendEmail_zSave
					, "zzzzClauseInsert_Email_BatchSendEmailTo_zSave", zzzzClauseInsert_Email_BatchSendEmailTo_zSave
					, "zzzzClauseInsert_Email_BatchSendEmailCC_zSave", zzzzClauseInsert_Email_BatchSendEmailCC_zSave
					, "zzzzClauseInsert_Email_BatchSendEmailBCC_zSave", zzzzClauseInsert_Email_BatchSendEmailBCC_zSave
					, "zzzzClauseInsert_Email_BatchSendEmailFileAttach_zSave", zzzzClauseInsert_Email_BatchSendEmailFileAttach_zSave
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

			#region // Clear For Debug:
			{
				string strSqlClearForDebug = CmUtils.StringUtils.Replace(@"
						drop table #input_Email_BatchSendEmail

						drop table #input_Email_BatchSendEmailTo

						drop table #input_Email_BatchSendEmailCC

						drop table #input_Email_BatchSendEmailBCC

						drop table #input_Email_BatchSendEmailFileAttach
					");

				DataSet dsExec = _cf.db.ExecQuery(strSqlClearForDebug);
			}
		#endregion

		// Return Good:
		MyCodeLabel_Done:
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			//mdsFinal.AcceptChanges();
			return;
		}

		private void Email_BatchSendEmail_SaveAndSendX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objSubject
			, object objBodyText
			, object objBodyHTML
			, DataSet dsData
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Email_BatchSendEmail_SaveAndSendX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				});
			#endregion

			#region // Check:
			#endregion

			#region // Refine and Check:
			string strBatchNo = null;
			DataSet mdsDMS40EmailBatchDtl = null;
			//DataTable dt_Email_Config = null;
			//DataTable dt_Email_TempEmail = null;
			//DataTable dt_DMS40_Email_BatchSendEmailTo = null;
			//DataTable dt_DMS40_Email_BatchSendEmailCC = null;
			//DataTable dt_DMS40_Email_BatchSendEmailBCC = null;
			//DataTable dt_DMS40_Email_BatchSendEmailFileAttach = null;
			#endregion

			#region // Get BatchNo:
			DataSet dsSeq = null;

			dsSeq = Seq_Common_Get(
				strTid // strTid
				, strGwUserCode // strGwUserCode
				, strGwPassword // strGwPassword
				, strWAUserCode // strWAUserCode
				, strWAUserPassword // strWAUserPassword
				, ref alParamsCoupleError // alParamsCoupleError
				////
				, TConst.SeqType.BatchNo // strSequenceType
				, "" // strParam_Prefix
				, "" // strParam_Postfix
				);

			if (CmUtils.CMyDataSet.HasError(dsSeq))
			{
				throw CmUtils.CMyException.Raise(
					"SequenceGetForDMS40"
					, null
					, alParamsCoupleError.ToArray()
					);
			}

			strBatchNo = Convert.ToString(CmUtils.CMyDataSet.GetRemark(dsSeq));
			#endregion

			#region // Email_BatchSendEmail_SaveX:
			////
			string strWSPath = null;
			string strConfigCode = TConst.ConfigCode.Common;
			string strTEmailCode = null;
			//object[] arrobjDSData = CmUtils.ConvertUtils.DataSet2Array(mdsDMS40EmailBatchDtl);
			{
				Email_BatchSendEmail_SaveX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					////
					, TConst.Flag.No // objFlagIsDelete
					////
					, strBatchNo // objBatchNo
					, strConfigCode // objConfigCode
					, strTEmailCode // objTEmailCode
					, objSubject // objSubject
					, objBodyText // objBodyText
					, objBodyHTML // objBodyHTML
					, strWSPath // objWSPath
					, dsData // arrobjDSData
					);
			}
			#endregion

			#region // Email_BatchSendEmail_BuildX:
			{
				////
				Email_BatchSendEmail_BuildX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
					////
					, strBatchNo // objBatchNo
					, out mdsDMS40EmailBatchDtl // mdsDMS40EmailBatchDtl
					);
			}
			#endregion

			#region // Email_BatchSendEmail_SaveX:
			{
				////
				Email_BatchSendEmail_SendX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   ////
					, strBatchNo // objBatchNo
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["MG_Domain"] // objMG_Domain
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["MG_From"] // objMG_From
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["MG_HttpBasicAuthUserPwd"] // objMG_HttpBasicAuthUserPwd
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["MG_HttpBasicAuthUserName"] // objMG_HttpBasicAuthUserName
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["MG_HttpBasicAuthUserName"] // objWSPath
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["EmailServerAddress"] // objFromMail
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["EmailServerPassword"] // objPassword
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["Host"]  // objHost
					, mdsDMS40EmailBatchDtl.Tables["Email_Config"].Rows[0]["Port"]  // objPort
					, mdsDMS40EmailBatchDtl.Tables["Email_BatchSendEmail"].Rows[0]["Subject"] // objSubject
					, mdsDMS40EmailBatchDtl.Tables["Email_BatchSendEmail"].Rows[0]["BodyHTML"] // objBody
					, mdsDMS40EmailBatchDtl.Tables["Email_BatchSendEmailTo"] // dt_Email_BatchSendEmailTo
					, mdsDMS40EmailBatchDtl.Tables["Email_BatchSendEmailCC"] // dt_Email_BatchSendEmailCC
					, mdsDMS40EmailBatchDtl.Tables["Email_BatchSendEmailBCC"] // dt_Email_BatchSendEmailBCC
					, mdsDMS40EmailBatchDtl.Tables["Email_BatchSendEmailFileAttach"] // dt_Email_BatchSendEmailFileAttach
					);
			}
			#endregion
		}

		public DataSet Email_BatchSendEmail_SaveAndSend(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objSubject
			, object objBodyText
			, object objBodyHTML
			, DataSet dsData
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Email_BatchSendEmail_SaveAndSend";
			string strErrorCodeDefault = TError.ErridnInventory.Email_BatchSendEmail_SaveAndSend;
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
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // Call Func:
				////
				Email_BatchSendEmail_SaveAndSendX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   ////
					, objSubject // objSubject
					, objBodyText // objBodyText
					, objBodyHTML // objBodyHTML
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

		public DataSet Email_BatchSendEmail_MstSv_Inos_User_Send(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Email_BatchSendEmail_MstSv_Inos_User_Send";
			string strErrorCodeDefault = TError.ErridnInventory.Email_BatchSendEmail_MstSv_Inos_User_Send;
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
				//Sys_Access_CheckDenyV30(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strFunctionName
				//	);
				#endregion

				#region // MstSv_Inos_User:
				////
				DataTable dt_MstSv_Inos_User = null;
				{
					string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_User:
							select distinct 
								t.MST
							from MstSv_Inos_User t
							where (1=1)
								and t.Id = '@strId'
								and t.FlagEmailSend = '1'
							;
						"
						, "@strId", TConst.InosMix.Default_Anonymous
						);

					dt_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];
				}
				#endregion

				#region // Call Func:
				////
				for (int nScan = 0; nScan < dt_MstSv_Inos_User.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dt_MstSv_Inos_User.Rows[nScan];
					////
					DataTable dt_Email_Config = null;
					DataTable dt_Email_TempEmail = null;
					DataTable dt_Email_BatchSendEmail = null;
					DataSet mdsDMS40EmailBatchDtl = null;

					try
					{
						////
						Email_Build_Inos_User_ActiveX(
							strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, drScan["MST"] // objMST
											////
							, out dt_Email_Config // dt_Email_Config
							, out dt_Email_TempEmail // dt_Email_TempEmail
							, out dt_Email_BatchSendEmail // dt_Email_BatchSendEmail
							, out mdsDMS40EmailBatchDtl // mdsDMS40EmailBatchDtl
							);

						////
						Email_BatchSendEmail_SaveAndSendX(
							strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, dt_Email_TempEmail.Rows[0]["TEmailSubject"] // objSubject
							, dt_Email_TempEmail.Rows[0]["TEmailBody"] // objBodyText
							, dt_Email_TempEmail.Rows[0]["TEmailBody"] // objBodyHTML
							, mdsDMS40EmailBatchDtl // dsData
							);

						////
						string strSqlUpdDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
								---- MstSv_Inos_User:
								update t
								set
									t.FlagEmailSend = '0'
								from MstSv_Inos_User t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
									--and t.FlagAdmin = '1'
									and t.FlagActive = '1'
								;
							"
							, "@strMST", drScan["MST"]
							);

						_cf.db.ExecQuery(
							strSqlUpdDB_MstSv_Inos_User
							);
					}
					catch (Exception exc)
					{
						////
						TUtils.CProcessExc.Process(
							ref mdsFinal
							, exc
							, strErrorCodeDefault
							, alParamsCoupleError.ToArray()
							);

						// Write ReturnLog:
						_cf.ProcessBizReturn_OutSide(
							ref mdsFinal // mdsFinal
							, strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // objUserCode
							, strFunctionName // strFunctionName
							);
						continue;
					}
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

		private void Email_BatchSendEmail_Sys_User_SendX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objUUID
			, object objEmail
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Email_BatchSendEmail_Sys_User_SendX";
			//string strErrorCodeDefault = TError.ErridnInventory.Email_BatchSendEmail_MstSv_Inos_User_Send;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUUID", objUUID
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Refine and Check Input:
			DataTable dt_Email_Config = null;
			DataTable dt_Email_TempEmail = null;
			DataTable dt_Email_BatchSendEmail = null;
			DataSet mdsDMS40EmailBatchDtl = null;
			#endregion

			////
			Email_Build_Sys_User_ActiveX(
				strTid // strTid
				, strGwUserCode // strGwUserCode
				, strGwPassword // strGwPassword
				, strWAUserCode // strWAUserCode
				, strWAUserPassword // strWAUserPassword
				, ref alParamsCoupleError // alParamsCoupleError
				, dtimeSys // dtimeSys
						   ////
				, objUUID // objUUID
				, objEmail // objEmail
						   ////
				, out dt_Email_Config // dt_Email_Config
				, out dt_Email_TempEmail // dt_Email_TempEmail
				, out dt_Email_BatchSendEmail // dt_Email_BatchSendEmail
				, out mdsDMS40EmailBatchDtl // mdsDMS40EmailBatchDtl
				);

			////
			Email_BatchSendEmail_SaveAndSendX(
				strTid // strTid
				, strGwUserCode // strGwUserCode
				, strGwPassword // strGwPassword
				, strWAUserCode // strWAUserCode
				, strWAUserPassword // strWAUserPassword
				, ref alParamsCoupleError // alParamsCoupleError
				, dtimeSys // dtimeSys
						   ////
				, dt_Email_TempEmail.Rows[0]["TEmailSubject"] // objSubject
				, dt_Email_TempEmail.Rows[0]["TEmailBody"] // objBodyText
				, dt_Email_TempEmail.Rows[0]["TEmailBody"] // objBodyHTML
				, mdsDMS40EmailBatchDtl // dsData
				);
		}

		private void Email_BatchSendEmail_BuildX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objBatchNo
			, out DataSet mdsDMS40EmailBatchDtl
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "Email_BatchSendEmail_BuildX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				});
			#endregion

			#region // Check:
			////
			string strBatchNo = TUtils.CUtils.StdParam(objBatchNo);
			string strConfigCode = TConst.ConfigCode.Common;
			//string strTEmailCode = null;
			////
			mdsDMS40EmailBatchDtl = new DataSet();
			DataTable dt_Email_Config = null;
			DataTable dt_Email_TempEmail = null;
			DataTable dt_Email_BatchSendEmail = null;
			DataTable dt_Email_BatchSendEmailTo = null;
			DataTable dt_Email_BatchSendEmailCC = null;
			DataTable dt_Email_BatchSendEmailBCC = null;
			DataTable dt_Email_BatchSendEmailFileAttach = null;

			////
			DataTable dtDB_Email_BatchSendEmail = null;
			{
				////
				Email_BatchSendEmail_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError					
					, strBatchNo // objBatchNo
					, TConst.Flag.Yes // strFlagExistToCheck
					, "" // strStatusListToCheck
					, out dtDB_Email_BatchSendEmail // dtDB_Email_BatchSendEmail
					);

				strConfigCode = TUtils.CUtils.StdParam(dtDB_Email_BatchSendEmail.Rows[0]["ConfigCode"]);
			}
			#endregion

			#region // Email_Config:
			{
				////
				string strSqlGetDB_Email_Config = CmUtils.StringUtils.Replace(@"
						---- Email_Config:
						select 
							t.*
						from Email_Config t --//[mylock]
						where (1=1)
							and t.ConfigCode = '@strConfigCode'
						;
					"
					, "@strConfigCode", strConfigCode
					);

				dt_Email_Config = _cf.db.ExecQuery(strSqlGetDB_Email_Config).Tables[0];
				dt_Email_Config.TableName = "Email_Config";
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_Config.Copy());
			}
			#endregion

			#region // Email_TempEmail:
			{
				////
				string strSqlGetDB_Email_TempEmail = CmUtils.StringUtils.Replace(@"
						---- Email_TempEmail:
						select 
							t.*
						from Email_TempEmail t --//[mylock]
						where (0=1)
							--and t.TEmailCode = '@strTEmailCode'
						;
					"
					//, "@strTEmailCode", strTEmailCode
					);
				

				dt_Email_TempEmail = _cf.db.ExecQuery(strSqlGetDB_Email_TempEmail).Tables[0];
				dt_Email_TempEmail.TableName = "Email_TempEmail";
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_TempEmail.Copy());
			}
			#endregion

			#region // Email_BatchSendEmail:
			{
				////
				string strSqlGetDB_Email_BatchSendEmail = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmail:
						select 
							t.*
						from Email_BatchSendEmail t --//[mylock]
						where (1=1)
							and t.BatchNo = '@strBatchNo'
						;
					"
					, "@strBatchNo", strBatchNo
					);

				dt_Email_BatchSendEmail = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmail).Tables[0];
				dt_Email_BatchSendEmail.TableName = "Email_BatchSendEmail";
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmail.Copy());
			}
			#endregion

			#region // Email_BatchSendEmailTo:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailTo = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailTo:
						select 
							t.EmailCode
						from Email_BatchSendEmailTo t --//[mylock]
						where (1=1)
							and t.BatchNo = '@strBatchNo'
						;
					"
					, "@strBatchNo", strBatchNo
					);

				dt_Email_BatchSendEmailTo = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailTo).Tables[0];
				dt_Email_BatchSendEmailTo.TableName = "Email_BatchSendEmailTo";
				
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailTo.Copy());
			}
			#endregion

			#region // Email_BatchSendEmailCC:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailCC = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailCC:
						select 
							t.EmailCode
						from Email_BatchSendEmailCC t --//[mylock]
						where (1=1)
							and t.BatchNo = '@strBatchNo'
						;
					"
					, "@strBatchNo", strBatchNo
					);

				dt_Email_BatchSendEmailCC = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailCC).Tables[0];
				dt_Email_BatchSendEmailCC.TableName = "Email_BatchSendEmailCC";
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailCC.Copy());
			}
			#endregion

			#region // Email_BatchSendEmailBCC:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailBCC = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailBCC:
						select 
							t.EmailCode
						from Email_BatchSendEmailBCC t --//[mylock]
						where (1=1)
							and t.BatchNo = '@strBatchNo'
						;
					"
					, "@strBatchNo", strBatchNo
					);

				dt_Email_BatchSendEmailBCC = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailBCC).Tables[0];
				dt_Email_BatchSendEmailBCC.TableName = "Email_BatchSendEmailBCC";
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailBCC.Copy());
				/////
			}
			#endregion

			#region // Email_BatchSendEmailFileAttach:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailFileAttach = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailFileAttach:
						select 
							t.FilePath
						from Email_BatchSendEmailFileAttach t --//[mylock]
						where (1=1)
							and t.BatchNo = '@strBatchNo'
						;
					"
					, "@strBatchNo", strBatchNo
					);

				dt_Email_BatchSendEmailFileAttach = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailFileAttach).Tables[0];
				dt_Email_BatchSendEmailFileAttach.TableName = "Email_BatchSendEmailFileAttach";
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailFileAttach.Copy());
			}
			#endregion
		}

		private void Email_BatchSendEmail_SendX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objBatchNo
			, object objMG_Domain
			, object objMG_From
			, object objMG_HttpBasicAuthUserPwd
			, object objMG_HttpBasicAuthUserName
			, object objWSPath
			, object objFromMail
			, object objPassword
			, object objHost
			, object objPort
			, object objSubject
			, object objBody
			, DataTable dt_Email_BatchSendEmailTo
			, DataTable dt_Email_BatchSendEmailCC
			, DataTable dt_Email_BatchSendEmailBCC
			, DataTable dt_Email_BatchSendEmailFileAttach
			////
			//, out DataSet mdsFinal
			)
		{
			#region // Temp:
			//mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			//bool bMyDebugSql = false;
			//DateTime dtimeSys = DateTime.Now;
			//bool bNeedTransaction = true;
			string strFunctionName = "Email_BatchSendEmail_SendX";
			//string strErrorCodeDefault = TError.ErrHTC.Email_BatchSendEmail_SaveX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objBatchNo", objBatchNo
				});
			//ArrayList alPCErrEx = new ArrayList();
			////
			Hashtable htParamsSql = new Hashtable();
			#endregion

			#region // Convert Input:
			#endregion

			#region // Init:
			#endregion

			#region //// Refine and Check Email_BatchSendEmail:
			////
			string strBatchNo = TUtils.CUtils.StdParam(objBatchNo);
			string strWSPath = string.Format("{0}", objWSPath).Trim();
			////
			SendMail objSendMail = null;
			string strMG_Domain = null;
			string strMG_From = null;
			string strMG_HttpBasicAuthUserPwd = null;
			string strMG_HttpBasicAuthUserName = null;
			string strFromMail = null;
			List<string> lstToMail = null;
			List<string> lstCcMail = null;
			List<string> lstBccMail = null;
			string strPassword = null;
			string strHost = null;
			Int32 nPort = 0;
			string strSubject = null;
			string strBody = null;
			List<string> lstAttachmentFile = null;

			////
			DataTable dtDB_Email_BatchSendEmail = null;
			{
				////
				Email_BatchSendEmail_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strBatchNo // objBatchNo
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.BatchStatus.Approve // strStatusListToCheck
					, out dtDB_Email_BatchSendEmail // dtDB_Email_BatchSendEmail
					);
				////
			}
			////
			#endregion

			#region // Build Param:
			{
				////
				strMG_Domain = string.Format("{0}", objMG_Domain).Trim();
				strMG_From = string.Format("{0}", objMG_From).Trim();
				strMG_HttpBasicAuthUserPwd = string.Format("{0}", objMG_HttpBasicAuthUserPwd).Trim();
				strMG_HttpBasicAuthUserName = string.Format("{0}", objMG_HttpBasicAuthUserName).Trim();
				strFromMail = string.Format("{0}", objFromMail).Trim();
				strPassword = string.Format("{0}", objPassword).Trim();
				strHost = string.Format("{0}", objHost).Trim();
				nPort = Convert.ToInt32(objPort);
				strSubject = string.Format("{0}", objSubject).Trim();
				strBody = string.Format("{0}", objBody).Trim();

				////
				lstToMail = new List<string>();

				for (int nScan = 0; nScan < dt_Email_BatchSendEmailTo.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dt_Email_BatchSendEmailTo.Rows[nScan];
					string strEmailCode = string.Format("{0}", drScan["EmailCode"]).Trim();

					////
					lstToMail.Add(strEmailCode);
				}

				////
				lstCcMail = new List<string>();

				for (int nScan = 0; nScan < dt_Email_BatchSendEmailCC.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dt_Email_BatchSendEmailCC.Rows[nScan];
					string strEmailCode = string.Format("{0}", drScan["EmailCode"]).Trim();

					////
					lstCcMail.Add(strEmailCode);
				}

				////
				lstBccMail = new List<string>();

				for (int nScan = 0; nScan < dt_Email_BatchSendEmailBCC.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dt_Email_BatchSendEmailBCC.Rows[nScan];
					string strEmailCode = string.Format("{0}", drScan["EmailCode"]).Trim();

					////
					lstBccMail.Add(strEmailCode);
				}

				////
				lstAttachmentFile = new List<string>();

				//lstAttachmentFile.Add(@"d:\AllWebData\WebSites\Test.DMS.HTC.Sales.WS\UploadedFiles\Contracts\VN040\2018-08-22\180822-005_PLHD_VN040.xls");
				for (int nScan = 0; nScan < dt_Email_BatchSendEmailFileAttach.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dt_Email_BatchSendEmailFileAttach.Rows[nScan];
					string strFilePath = string.Format("{0}", drScan["FilePath"]).Trim();
					string strFilePathFull = strFilePath;
					////
					lstAttachmentFile.Add(strFilePathFull);
				}

				////
				objSendMail = new SendMail();
				objSendMail.MG_Domain = strMG_Domain;
				objSendMail.MG_From = strMG_From;
				objSendMail.MG_HttpBasicAuthUserName = strMG_HttpBasicAuthUserName;
				objSendMail.MG_HttpBasicAuthUserPwd = strMG_HttpBasicAuthUserPwd;
				objSendMail.FromMail = strFromMail;
				objSendMail.ToMail = lstToMail;
				objSendMail.CcMail = lstCcMail;
				objSendMail.BccMail = lstBccMail;
				objSendMail.AttachmentFile = lstAttachmentFile;
				objSendMail.Password = strPassword;
				objSendMail.Host = strHost;
				objSendMail.Port = nPort;
				objSendMail.Subject = strSubject;
				objSendMail.Body = strBody;
			}
			#endregion

			#region // SendMail:
			{
				////
				TUtils.MailGunCommon.SendMail(objSendMail);
			}
			#endregion

			// Return Good:
			//MyCodeLabel_Done:
			//TDALUtils.DBUtils.CommitSafety(_db);
			//mdsFinal.AcceptChanges();
			return;
		}

		private void Email_Build_Inos_User_ActiveX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objMST
			////
			, out DataTable dt_Email_Config
			, out DataTable dt_Email_TempEmail
			, out DataTable dt_Email_BatchSendEmail
			, out DataSet mdsDMS40EmailBatchDtl
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "TCT_Email_Send_Transaction_DSThongBaoX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "objMST", objMST
				});
			#endregion

			#region // Refine and Check:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strContactEmail = null;

			////
			string strBatchNo = null;
			string strConfigCode = TConst.ConfigCode.Common;
			string strTEmailCode = TConst.TEmailCode.Inos_User_Active;
			////
			mdsDMS40EmailBatchDtl = new DataSet();
			DataTable dt_Email_BatchSendEmailTo = null;
			DataTable dt_Email_BatchSendEmailCC = null;
			DataTable dt_Email_BatchSendEmailBCC = null;
			DataTable dt_Email_BatchSendEmailFileAttach = null;
			////
			{
				#region // Get BatchNo:
				DataSet dsSeq = null;

				dsSeq = Seq_Common_Get(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, TConst.SeqType.BatchNo // strSequenceType
					, "" // strParam_Prefix
					, "" // strParam_Postfix
					);

				if (CmUtils.CMyDataSet.HasError(dsSeq))
				{
					throw CmUtils.CMyException.Raise(
						"SequenceGetForDMS40"
						, null
						, alParamsCoupleError.ToArray()
						);
				}

				strBatchNo = Convert.ToString(CmUtils.CMyDataSet.GetRemark(dsSeq));
				#endregion

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

				strContactEmail = string.Format("{0}", dtDB_Mst_NNT.Rows[0]["ContactEmail"]).Trim();
				////
			}
			#endregion
				
			#region // Email_Config:
			{
				////
				string strSqlGetDB_Email_Config = CmUtils.StringUtils.Replace(@"
						---- Email_Config:
						select 
							t.*
						from Email_Config t --//[mylock]
						where (1=1)
							and t.ConfigCode = '@strConfigCode'
						;
					"
					, "@strConfigCode", strConfigCode
					);

				dt_Email_Config = _cf.db.ExecQuery(strSqlGetDB_Email_Config).Tables[0];
				dt_Email_Config.TableName = "Email_Config";
			}
			#endregion

			#region // Email_TempEmail:
			{
				////
				string strSqlGetDB_Email_TempEmail = CmUtils.StringUtils.Replace(@"
						---- Email_TempEmail:
						select 
							t.TEmailCode
							, t.TEmailSubject TEmailSubject
							, t.TEmailBody TEmailBody
						from Email_TempEmail t
						where (1=1)
							and t.TEmailCode = '@strTEmailCode'
						;
					"
					, "@strTEmailCode", strTEmailCode
					);

				dt_Email_TempEmail = _cf.db.ExecQuery(strSqlGetDB_Email_TempEmail).Tables[0];
				string strTEmailCode_DB = TUtils.CUtils.StdParam(dt_Email_TempEmail.Rows[0]["TEmailCode"]);
				string strTEmailSubject_DB = string.Format("{0}", dt_Email_TempEmail.Rows[0]["TEmailSubject"]).Trim();
				string strTEmailBody_DB = string.Format("{0}", dt_Email_TempEmail.Rows[0]["TEmailBody"]).Trim();

				string strTEmailSubject_Final = CmUtils.StringUtils.Replace(
					strTEmailSubject_DB
					);

				string strTEmailBody_Final = CmUtils.StringUtils.Replace(
					strTEmailBody_DB
					, "@strMST", strMST
					);


				////
				string strSqlGetDB_Email_TempEmail_Final = CmUtils.StringUtils.Replace(@"
						---- Email_TempEmail:
						select 
							t.TEmailCode
							, N'@strTEmailSubject_Final' TEmailSubject
							, N'@strTEmailBody_Final' TEmailBody
						from Email_TempEmail t
						where (1=1)
							and t.TEmailCode = '@strTEmailCode'
						;
					"
					, "@strTEmailCode", strTEmailCode
					, "@strTEmailSubject_Final", strTEmailSubject_Final
					, "@strTEmailBody_Final", strTEmailBody_Final
					);

				dt_Email_TempEmail = _cf.db.ExecQuery(strSqlGetDB_Email_TempEmail_Final).Tables[0];
				dt_Email_TempEmail.TableName = "Email_TempEmail";
			}
			#endregion

			#region // Email_BatchSendEmail:
			{
				////
				string strSqlGetDB_Email_BatchSendEmail = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmail:
						select 
							t.*
						from Email_BatchSendEmail t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmail = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmail).Tables[0];
				dt_Email_BatchSendEmail.TableName = "Email_BatchSendEmail";
				////
				string strFN = "";
				DataRow drNew = dt_Email_BatchSendEmail.NewRow();
				strFN = "BatchNo"; drNew[strFN] = strBatchNo;
				strFN = "ConfigCode"; drNew[strFN] = strConfigCode;
				strFN = "TEmailCode"; drNew[strFN] = strTEmailCode;
				strFN = "WSPath"; drNew[strFN] = null;
				strFN = "CreatedDate"; drNew[strFN] = dtimeSys.ToString("yyyy-MM-dd");
				strFN = "EffectDate"; drNew[strFN] = dtimeSys.ToString("yyyy-MM-dd");
				strFN = "SendBy"; drNew[strFN] = strWAUserCode;
				strFN = "DealerCode"; drNew[strFN] = "idocNet";
				strFN = "BatchStatus"; drNew[strFN] = TConst.BatchStatus.Pending;
				strFN = "LogLUDTimeUTC"; drNew[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drNew[strFN] = strWAUserCode;
				dt_Email_BatchSendEmail.Rows.Add(drNew);
			}
			#endregion

			#region // Email_BatchSendEmailTo:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailTo = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailTo:
						select 
							t.EmailCode
						from Email_BatchSendEmailTo t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmailTo = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailTo).Tables[0];
				dt_Email_BatchSendEmailTo.TableName = "Email_BatchSendEmailTo";
				dt_Email_BatchSendEmailTo.Rows.Add(strContactEmail);
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailTo.Copy());
			}
			#endregion

			#region // Email_BatchSendEmailCC:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailCC = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailCC:
						select 
							t.EmailCode
						from Email_BatchSendEmailCC t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmailCC = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailCC).Tables[0];
				dt_Email_BatchSendEmailCC.TableName = "Email_BatchSendEmailCC";

				//dt_Email_BatchSendEmailCC.Rows.Add(TConst.DMS40EmailBatchSendEmailCC.TVANEmail);
				//dt_Email_BatchSendEmailCC.Rows.Add("dungnd@idocnet.com");
				dt_Email_BatchSendEmailCC.Rows.Add(_cf.nvcParams["Biz_Email_BatchSendEmailCC"]);
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailCC.Copy());
			}
			#endregion

			#region // Email_BatchSendEmailBCC:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailBCC = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailBCC:
						select 
							t.EmailCode
						from Email_BatchSendEmailBCC t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmailBCC = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailBCC).Tables[0];
				dt_Email_BatchSendEmailBCC.TableName = "Email_BatchSendEmailBCC";
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailBCC.Copy());
				/////
			}
			#endregion

			#region // Email_BatchSendEmailFileAttach:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailFileAttach = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailFileAttach:
						select 
							t.FilePath
						from Email_BatchSendEmailFileAttach t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmailFileAttach = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailFileAttach).Tables[0];
				dt_Email_BatchSendEmailFileAttach.TableName = "Email_BatchSendEmailFileAttach";

				////
				//if (!string.IsNullOrEmpty(strFilePath)) dt_Email_BatchSendEmailFileAttach.Rows.Add(strFilePath);
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailFileAttach.Copy());
			}
			#endregion
		}

		private void Email_Build_Sys_User_ActiveX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			////
			, object objUUID
			, object objEmail
			////
			, out DataTable dt_Email_Config
			, out DataTable dt_Email_TempEmail
			, out DataTable dt_Email_BatchSendEmail
			, out DataSet mdsDMS40EmailBatchDtl
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			//bool bNeedTransaction = true;
			string strFunctionName = "TCT_Email_Send_Transaction_DSThongBaoX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "objUUID", objUUID
				});
			#endregion

			#region // Refine and Check:
			////
			string strUUID = TUtils.CUtils.StdParam(objUUID);
			//string strContactEmail = null;

			////
			string strBatchNo = null;
			string strConfigCode = TConst.ConfigCode.Common;
			string strTEmailCode = TConst.TEmailCode.Sys_User_Active;
			////
			mdsDMS40EmailBatchDtl = new DataSet();
			DataTable dt_Email_BatchSendEmailTo = null;
			DataTable dt_Email_BatchSendEmailCC = null;
			DataTable dt_Email_BatchSendEmailBCC = null;
			DataTable dt_Email_BatchSendEmailFileAttach = null;
			////
			{
				#region // Get BatchNo:
				DataSet dsSeq = null;

				dsSeq = Seq_Common_Get(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, TConst.SeqType.BatchNo // strSequenceType
					, "" // strParam_Prefix
					, "" // strParam_Postfix
					);

				if (CmUtils.CMyDataSet.HasError(dsSeq))
				{
					throw CmUtils.CMyException.Raise(
						"SequenceGetForDMS40"
						, null
						, alParamsCoupleError.ToArray()
						);
				}

				strBatchNo = Convert.ToString(CmUtils.CMyDataSet.GetRemark(dsSeq));
				#endregion
				////
			}
			#endregion

			#region // Email_Config:
			{
				////
				string strSqlGetDB_Email_Config = CmUtils.StringUtils.Replace(@"
						---- Email_Config:
						select 
							t.*
						from Email_Config t --//[mylock]
						where (1=1)
							and t.ConfigCode = '@strConfigCode'
						;
					"
					, "@strConfigCode", strConfigCode
					);

				dt_Email_Config = _cf.db.ExecQuery(strSqlGetDB_Email_Config).Tables[0];
				dt_Email_Config.TableName = "Email_Config";
			}
			#endregion

			#region // Email_TempEmail:
			{
				////
				string strSqlGetDB_Email_TempEmail = CmUtils.StringUtils.Replace(@"
						---- Email_TempEmail:
						select 
							t.TEmailCode
							, t.TEmailSubject TEmailSubject
							, t.TEmailBody TEmailBody
						from Email_TempEmail t
						where (1=1)
							and t.TEmailCode = '@strTEmailCode'
						;
					"
					, "@strTEmailCode", strTEmailCode
					);

				dt_Email_TempEmail = _cf.db.ExecQuery(strSqlGetDB_Email_TempEmail).Tables[0];
				string strTEmailCode_DB = TUtils.CUtils.StdParam(dt_Email_TempEmail.Rows[0]["TEmailCode"]);
				string strTEmailSubject_DB = string.Format("{0}", dt_Email_TempEmail.Rows[0]["TEmailSubject"]).Trim();
				string strTEmailBody_DB = string.Format("{0}", dt_Email_TempEmail.Rows[0]["TEmailBody"]).Trim();

				string strTEmailSubject_Final = CmUtils.StringUtils.Replace(
					strTEmailSubject_DB
					);

				string strTEmailBody_Final = CmUtils.StringUtils.Replace(
					strTEmailBody_DB
					, "@strUUID", strUUID
					, "@strNetworkID", nNetworkID
					);


				////
				string strSqlGetDB_Email_TempEmail_Final = CmUtils.StringUtils.Replace(@"
						---- Email_TempEmail:
						select 
							t.TEmailCode
							, N'@strTEmailSubject_Final' TEmailSubject
							, N'@strTEmailBody_Final' TEmailBody
						from Email_TempEmail t
						where (1=1)
							and t.TEmailCode = '@strTEmailCode'
						;
					"
					, "@strTEmailCode", strTEmailCode
					, "@strTEmailSubject_Final", strTEmailSubject_Final
					, "@strTEmailBody_Final", strTEmailBody_Final
					);

				dt_Email_TempEmail = _cf.db.ExecQuery(strSqlGetDB_Email_TempEmail_Final).Tables[0];
				dt_Email_TempEmail.TableName = "Email_TempEmail";
			}
			#endregion

			#region // Email_BatchSendEmail:
			{
				////
				string strSqlGetDB_Email_BatchSendEmail = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmail:
						select 
							t.*
						from Email_BatchSendEmail t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmail = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmail).Tables[0];
				dt_Email_BatchSendEmail.TableName = "Email_BatchSendEmail";
				////
				string strFN = "";
				DataRow drNew = dt_Email_BatchSendEmail.NewRow();
				strFN = "BatchNo"; drNew[strFN] = strBatchNo;
				strFN = "ConfigCode"; drNew[strFN] = strConfigCode;
				strFN = "TEmailCode"; drNew[strFN] = strTEmailCode;
				strFN = "WSPath"; drNew[strFN] = null;
				strFN = "CreatedDate"; drNew[strFN] = dtimeSys.ToString("yyyy-MM-dd");
				strFN = "EffectDate"; drNew[strFN] = dtimeSys.ToString("yyyy-MM-dd");
				strFN = "SendBy"; drNew[strFN] = strWAUserCode;
				strFN = "DealerCode"; drNew[strFN] = "idocNet";
				strFN = "BatchStatus"; drNew[strFN] = TConst.BatchStatus.Pending;
				strFN = "LogLUDTimeUTC"; drNew[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drNew[strFN] = strWAUserCode;
				dt_Email_BatchSendEmail.Rows.Add(drNew);
			}
			#endregion

			#region // Email_BatchSendEmailTo:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailTo = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailTo:
						select 
							t.EmailCode
						from Email_BatchSendEmailTo t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmailTo = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailTo).Tables[0];
				dt_Email_BatchSendEmailTo.TableName = "Email_BatchSendEmailTo";
				dt_Email_BatchSendEmailTo.Rows.Add(objEmail);
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailTo.Copy());
			}
			#endregion

			#region // Email_BatchSendEmailCC:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailCC = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailCC:
						select 
							t.EmailCode
						from Email_BatchSendEmailCC t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmailCC = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailCC).Tables[0];
				dt_Email_BatchSendEmailCC.TableName = "Email_BatchSendEmailCC";

				//dt_Email_BatchSendEmailCC.Rows.Add(TConst.DMS40EmailBatchSendEmailCC.TVANEmail);
				//dt_Email_BatchSendEmailCC.Rows.Add("dungnd@idocnet.com");
				dt_Email_BatchSendEmailCC.Rows.Add(_cf.nvcParams["Biz_Email_BatchSendEmailCC"]);
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailCC.Copy());
			}
			#endregion

			#region // Email_BatchSendEmailBCC:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailBCC = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailBCC:
						select 
							t.EmailCode
						from Email_BatchSendEmailBCC t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmailBCC = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailBCC).Tables[0];
				dt_Email_BatchSendEmailBCC.TableName = "Email_BatchSendEmailBCC";
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailBCC.Copy());
				/////
			}
			#endregion

			#region // Email_BatchSendEmailFileAttach:
			{
				////
				string strSqlGetDB_Email_BatchSendEmailFileAttach = CmUtils.StringUtils.Replace(@"
						---- Email_BatchSendEmailFileAttach:
						select 
							t.FilePath
						from Email_BatchSendEmailFileAttach t --//[mylock]
						where (0=1)
						;
					");

				dt_Email_BatchSendEmailFileAttach = _cf.db.ExecQuery(strSqlGetDB_Email_BatchSendEmailFileAttach).Tables[0];
				dt_Email_BatchSendEmailFileAttach.TableName = "Email_BatchSendEmailFileAttach";

				////
				//if (!string.IsNullOrEmpty(strFilePath)) dt_Email_BatchSendEmailFileAttach.Rows.Add(strFilePath);
				////
				mdsDMS40EmailBatchDtl.Tables.Add(dt_Email_BatchSendEmailFileAttach.Copy());
			}
			#endregion
		}

		public DataSet MstSv_Inos_Org_BuildAndCreateAuto(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Inos_Org_BuildAndCreate";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_Org_BuildAndCreate;
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

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // MstSv_Inos_Org:
				DataTable dt_MstSv_Inos_Org = null;
				{
					// //
					string strSqlGet_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_Org:
							select distinct
								t.MST
							from MstSv_Inos_Org t --//[mylock]
							where (1=1)
								and t.Id = @objId
							;
						"
						, "@objId", TConst.InosMix.Default_Anonymous
						);

					dt_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGet_MstSv_Inos_Org).Tables[0];
				}
				#endregion

				#region // MstSv_Inos_Org_CreateX:
				//DataSet dsGetData = null;
				{
					for (int nScan = 0; nScan < dt_MstSv_Inos_Org.Rows.Count; nScan++)
					{
						// //
						DataRow drScan = dt_MstSv_Inos_Org.Rows[nScan];

						////
						string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
								---- MstSv_Inos_User:
								select top 1
									t.*
								from MstSv_Inos_User t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
								;
							"
							, "@strMST", drScan["MST"]
							);

					 	DataTable dtDB_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];

						////
						object objAccessToken = null;

						Inos_AccountService_GetAccessTokenX(
							strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, ref mdsFinal // mdsFinal
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, dtDB_MstSv_Inos_User.Rows[0]["Email"] // objEmail
							, dtDB_MstSv_Inos_User.Rows[0]["Password"] // objPassword
							////
							, out objAccessToken // objAccessToken
							);

						////
						MstSv_Inos_Org_CreateX(
							strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, (string)objAccessToken // strAccessToken
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, drScan["MST"] // objMST
							);
					}

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

		public DataSet MstSv_Inos_Org_BuildAndCreate(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Inos_Org_BuildAndCreate";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_Org_BuildAndCreate;
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

				//// rem lại do khong can dung user/pass để đăng ký NNT
				//// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strWAUserPassword
				//    );

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // MstSv_Inos_Org_BuildAndCreateX:
				{
					MstSv_Inos_Org_BuildAndCreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref alParamsCoupleError // alParamsCoupleError
						////
						, objMST // objMST
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


		public void MstSv_Inos_Org_BuildAndCreateX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Inos_Org_BuildAndCreate";
			//string strErrorCodeDefault = TError.ErridnInventory.MstSv_Inos_Org_BuildAndCreate;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strMST = TUtils.CUtils.StdParam(objMST);
			#endregion

			#region // MstSv_Inos_Org:
			DataTable dt_MstSv_Inos_Org = null;
			{
				// //
				string strSqlGet_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_Org:
							select distinct
								t.MST
							from MstSv_Inos_Org t --//[mylock]
							where (1=1)
								and t.MST = '@objMST'
							;
						"
					//, "@objId", TConst.InosMix.Default_Anonymous
					, "@objMST", strMST
					);

				dt_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGet_MstSv_Inos_Org).Tables[0];
			}
			#endregion

			#region // MstSv_Inos_Org_CreateX:
			//DataSet dsGetData = null;
			{
				for (int nScan = 0; nScan < dt_MstSv_Inos_Org.Rows.Count; nScan++)
				{
					// //
					DataRow drScan = dt_MstSv_Inos_Org.Rows[nScan];

					////
					string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
								---- MstSv_Inos_User:
								select top 1
									t.*
								from MstSv_Inos_User t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
									and t.FlagAdmin = '1'
								;
							"
						, "@strMST", drScan["MST"]
						);

					DataTable dtDB_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];

					////
					object objAccessToken = null;

					Inos_AccountService_GetAccessTokenX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, dtDB_MstSv_Inos_User.Rows[0]["Email"] // objEmail
						, dtDB_MstSv_Inos_User.Rows[0]["Password"] // objPassword
																   ////
						, out objAccessToken // objAccessToken
						);

					////
					MstSv_Inos_Org_CreateX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, (string)objAccessToken // strAccessToken
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						////
						, drScan["MST"] // objMST
						);
				}

			}
			////
			//CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
			#endregion

			// Return Good:
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			mdsFinal.AcceptChanges();
			//return mdsFinal;
		}

		public DataSet WAS_Email_BatchSendEmail_SaveAndSend(
			ref ArrayList alParamsCoupleError
			, RQ_Email_BatchSendEmail objRQ_Email_BatchSendEmail
			////
			, out RT_Email_BatchSendEmail objRT_Email_BatchSendEmail
			)
		{
			#region // Temp:
			string strTid = objRQ_Email_BatchSendEmail.Tid;
			objRT_Email_BatchSendEmail = new RT_Email_BatchSendEmail();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Email_BatchSendEmail.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Email_BatchSendEmail_SaveAndSend";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Email_BatchSendEmail_SaveAndSend;
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
					DataTable dt_Email_BatchSendEmailTo = TUtils.DataTableCmUtils.ToDataTable<Email_BatchSendEmailTo>(objRQ_Email_BatchSendEmail.Lst_Email_BatchSendEmailTo, "Email_BatchSendEmailTo");
					dsData.Tables.Add(dt_Email_BatchSendEmailTo);
					////
					DataTable dt_Email_BatchSendEmailCC = TUtils.DataTableCmUtils.ToDataTable<Email_BatchSendEmailCC>(objRQ_Email_BatchSendEmail.Lst_Email_BatchSendEmailCC, "Email_BatchSendEmailCC");
					dsData.Tables.Add(dt_Email_BatchSendEmailCC);
					////
					DataTable dt_Email_BatchSendEmailBCC = TUtils.DataTableCmUtils.ToDataTable<Email_BatchSendEmailBCC>(objRQ_Email_BatchSendEmail.Lst_Email_BatchSendEmailBCC, "Email_BatchSendEmailBCC");
					dsData.Tables.Add(dt_Email_BatchSendEmailBCC);
					////
					DataTable dt_Email_BatchSendEmailFileAttach = TUtils.DataTableCmUtils.ToDataTable<Email_BatchSendEmailFileAttach>(objRQ_Email_BatchSendEmail.Lst_Email_BatchSendEmailFileAttach, "Email_BatchSendEmailFileAttach");
					dsData.Tables.Add(dt_Email_BatchSendEmailFileAttach);
				}
				#endregion

				#region // Email_BatchSendEmail_BaseHTTPReq:
				mdsResult = Email_BatchSendEmail_SaveAndSend(
					objRQ_Email_BatchSendEmail.Tid // strTid
					, objRQ_Email_BatchSendEmail.GwUserCode // strGwUserCode
					, objRQ_Email_BatchSendEmail.GwPassword // strGwPassword
					, objRQ_Email_BatchSendEmail.WAUserCode // strUserCode
					, objRQ_Email_BatchSendEmail.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  // //
					, objRQ_Email_BatchSendEmail.Email_BatchSendEmail.Subject // objSubject						 
					, objRQ_Email_BatchSendEmail.Email_BatchSendEmail.BodyText // objBodyText
					, objRQ_Email_BatchSendEmail.Email_BatchSendEmail.BodyHTML // objBodyHTML
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

		public DataSet WAS_Email_BatchSendEmail_MstSv_Inos_User_Send(
			ref ArrayList alParamsCoupleError
			, RQ_Email_BatchSendEmail objRQ_Email_BatchSendEmail
			////
			, out RT_Email_BatchSendEmail objRT_Email_BatchSendEmail
			)
		{
			#region // Temp:
			string strTid = objRQ_Email_BatchSendEmail.Tid;
			objRT_Email_BatchSendEmail = new RT_Email_BatchSendEmail();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Email_BatchSendEmail.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_Email_BatchSendEmail_MstSv_Inos_User_Send";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_Email_BatchSendEmail_MstSv_Inos_User_Send;
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
				}
				#endregion

				#region // Email_BatchSendEmail_BaseHTTPReq:
				mdsResult = Email_BatchSendEmail_MstSv_Inos_User_Send(
					objRQ_Email_BatchSendEmail.Tid // strTid
					, objRQ_Email_BatchSendEmail.GwUserCode // strGwUserCode
					, objRQ_Email_BatchSendEmail.GwPassword // strGwPassword
					, objRQ_Email_BatchSendEmail.WAUserCode // strUserCode
					, objRQ_Email_BatchSendEmail.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
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
