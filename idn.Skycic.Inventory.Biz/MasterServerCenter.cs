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
using OSiNOSSv = inos.common.Service;
using inos.common.Model;
using inos.common.Service;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
	{
		#region // Mst_NNT:

		private void RptCenter_Mst_NNT_AddByUserExistX(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			//// InosCreateOrg
			, object objId_InosCreateOrg
			, object objParentId_InosCreateOrg
			, object objName_InosCreateOrg
			, object objBizTypeId_InosCreateOrg
			, object objBizTypeName_InosCreateOrg
			, object objBizFieldId_InosCreateOrg
			, object objBizFieldName_InosCreateOrg
			, object objContactName_InosCreateOrg
			, object objEmail_InosCreateOrg
			, object objPhoneNo_InosCreateOrg
			, object objDescription_InosCreateOrg
			, object objEnable_InosCreateOrg
			//// RptSv
			, object objMST
			, object objNNTFullName
			, object objMSTParent
			, object objProvinceCode
			, object objDistrictCode
			//, object objNNTType
			, object objDLCode
			, object objNNTAddress
			, object objNNTMobile
			, object objNTTPhone
			, object objNNTFax
			, object objPresentBy
			, object objBusinessRegNo
			, object objNNTPosition
			, object objPresentIDNo
			, object objPresentIDType
			, object objGovTaxID
			, object objContactName
			, object objContactPhone
			, object objContactEmail
			, object objWebsite
			, object objCANumber
			, object objCAOrg
			, object objCAEffDTimeUTCStart
			, object objCAEffDTimeUTCEnd
			, object objPackageCode
			, object objCreatedDate
			, object objAccNo
			, object objAccHolder
			, object objBankName
			, object objBizType
			, object objBizFieldCode
			, object objBizSizeCode
			, object objDealerType
			////
			, object objDepartmentCode
			////
			, object objUserPassword
			, object objUserPasswordRepeat
			//////
			//, object objQtyLicense
			////
			//, DataSet dsData
			//// InosCreateOrder
			, Inos_LicOrder objInos_LicOrder
			////
			, ref RQ_Mst_NNT objRQ_Mst_NNT
			, ref RT_Mst_NNT objRT_Mst_NNT
			////
			, object objFlagIsCreateOrder
			)
		{
			#region // Temp:
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Mst_NNT_AddX";
			//string strErrorCodeDefault = TError.ErridNTVAN.WA_Mst_NNT_Registry;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objMST", objMST
					, "objNNTFullName", objNNTFullName
					, "objMSTParent", objMSTParent
					, "objProvinceCode", objProvinceCode
					, "objDistrictCode", objDistrictCode
                    //, "objNNTType", objNNTType
                    , "objDLCode", objDLCode
					, "objNNTAddress", objNNTAddress
					, "objNNTMobile", objNNTMobile
					, "objNTTPhone", objNTTPhone
					, "objNNTFax", objNNTFax
					, "objPresentBy", objPresentBy
					, "objBusinessRegNo", objBusinessRegNo
					, "objNNTPosition", objNNTPosition
					, "objPresentIDNo", objPresentIDNo
					, "objPresentIDType", objPresentIDType
					, "objGovTaxID", objGovTaxID
					, "objContactName", objContactName
					, "objContactPhone", objContactPhone
					, "objContactEmail", objContactEmail
					, "objWebsite", objWebsite
					, "objCANumber", objCANumber
					, "objCAOrg", objCAOrg
					, "objCAEffDTimeUTCStart", objCAEffDTimeUTCStart
					, "objCAEffDTimeUTCEnd", objCAEffDTimeUTCEnd
					, "objPackageCode", objPackageCode
					, "objAccNo", objAccNo
					, "objAccHolder", objAccHolder
					, "objBankName", objBankName
					, "objBankName", objBizType
					, "objBizFieldCode", objBizFieldCode
					, "objBizSizeCode", objBizSizeCode
                    /////
                    , "objDepartmentCode", objDepartmentCode
                    ////
                    , "objUserPassword", objUserPassword
					, "objUserPasswordRepeat", objUserPasswordRepeat
					////
					//, "objQtyLicense", objQtyLicense
					});
			#endregion

			#region // Refine and Check Input:
			////
			bool bFlagIsCreateOrder = CmUtils.StringUtils.StringEqual(objFlagIsCreateOrder, TConst.Flag.Yes);
			//bool bFlagInosCreateUser = false;
			//bool bFlagInosCreateOrg = false;
			//bool bFlagRptSvMstNNtAdd = false;
			//bool bFlagInosCreateOrder = false;
			#endregion

			#region // Save InosCreateUser:
			////
			DataTable dt_Lst_MstSv_Inos_User = TDALUtils.DBUtils.GetSchema(_cf.db, "OS_Inos_User").Tables[0];
			dt_Lst_MstSv_Inos_User.TableName = "MstSv_Inos_User";
			TUtils.CUtils.MyForceNewColumn(ref dt_Lst_MstSv_Inos_User, "MST", typeof(object));
			TUtils.CUtils.MyForceNewColumn(ref dt_Lst_MstSv_Inos_User, "UUID", typeof(object));
			TUtils.CUtils.MyForceNewColumn(ref dt_Lst_MstSv_Inos_User, "FlagEmailActivate", typeof(object));
			TUtils.CUtils.MyForceNewColumn(ref dt_Lst_MstSv_Inos_User, "FlagAdmin", typeof(object));
			TUtils.CUtils.MyForceNewColumn(ref dt_Lst_MstSv_Inos_User, "FlagEmailSend", typeof(object));

			////
			OSiNOSSv.AccountService objAccountService = new OSiNOSSv.AccountService(null);
			InosUser dummy = new InosUser();
            dummy.Email = (string)objRQ_Mst_NNT.OS_Inos_User.Email;

            InosUser inosUser = objAccountService.GetUser(dummy);

			string strFN = "";
			DataRow drDB = dt_Lst_MstSv_Inos_User.NewRow();
			strFN = "Id"; drDB[strFN] = inosUser.Id;
			strFN = "Name"; drDB[strFN] = inosUser.Name;
			strFN = "Email"; drDB[strFN] = inosUser.Email;
			strFN = "Language"; drDB[strFN] = inosUser.Language;
			strFN = "TimeZone"; drDB[strFN] = inosUser.TimeZone;
			strFN = "Avatar"; drDB[strFN] = inosUser.Avatar;
			strFN = "MST"; drDB[strFN] = objMST;
			strFN = "UUID"; drDB[strFN] = "0";
			strFN = "FlagEmailActivate"; drDB[strFN] = TConst.Flag.Inactive;
			strFN = "FlagAdmin"; drDB[strFN] = TConst.Flag.Active;
			strFN = "FlagEmailSend"; drDB[strFN] = TConst.Flag.Inactive;
			//strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
			//strFN = "LogLUDTime"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
			//strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode;
			dt_Lst_MstSv_Inos_User.Rows.Add(drDB);
			////

			#endregion

			#region // Save InosCreateOrg:
			object objAccessToken = strAccessToken;
			DataTable dt_Lst_MstSv_Inos_Org = null;
			{
				////
				DataSet dsGetData = null;

				////
				if (objId_InosCreateOrg != null)
				{
					////
					Inos_OrgService_GetOrgX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, (string)objAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objId_InosCreateOrg // objId
											  ////
						, out dsGetData // dsData
						);
				}
				else
				{
					////
					RptSv_Inos_OrgService_CreateOrgX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, (string)objAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objParentId_InosCreateOrg
						, objName_InosCreateOrg
						, objBizTypeId_InosCreateOrg
						, objBizTypeName_InosCreateOrg
						, objBizFieldId_InosCreateOrg
						, objBizFieldName_InosCreateOrg
						, objContactName_InosCreateOrg
						, objEmail_InosCreateOrg
						, objPhoneNo_InosCreateOrg
						, objDescription_InosCreateOrg
						, objEnable_InosCreateOrg
						////
						, out dsGetData // dsData
						);
				}


				dt_Lst_MstSv_Inos_Org = dsGetData.Tables[0].Copy();
				dt_Lst_MstSv_Inos_Org.TableName = "MstSv_Inos_Org";

				////
				TUtils.CUtils.MyForceNewColumn(ref dt_Lst_MstSv_Inos_Org, "MST", typeof(object));

				////
				for (int nScan = 0; nScan < dt_Lst_MstSv_Inos_Org.Rows.Count; nScan++)
				{
					////
					DataRow drScan = dt_Lst_MstSv_Inos_Org.Rows[nScan];

					////
					drScan["MST"] = objMST;

				}

				if (objId_InosCreateOrg == null) objRQ_Mst_NNT.OS_Inos_Org.Id = dt_Lst_MstSv_Inos_Org.Rows[0]["Id"];
			}
			#endregion

			#region // Save RptSvMstNNtAdd:
			{
				DataSet dsData = new DataSet();
				dsData.Tables.Add(dt_Lst_MstSv_Inos_User.Copy());
				dsData.Tables.Add(dt_Lst_MstSv_Inos_Org);

				RptSv_Mst_NNT_RegisterX_New20200208(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, (string)objAccessToken // strAccessToken
					, ref mdsFinal // mdsFinal
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objMST // objMST
					, objNNTFullName // objNNTFullName
					, objMSTParent // objMSTParent
					, objProvinceCode // objProvinceCode
					, objDistrictCode // objDistrictCode
									  //, objNNTType
					, objDLCode // objDLCode
					, objNNTAddress // objNNTAddress
					, objNNTMobile // objNNTMobile
					, objNTTPhone // objNTTPhone
					, objNNTFax // objNNTFax
					, objPresentBy // objPresentBy
					, objBusinessRegNo // objBusinessRegNo
					, objNNTPosition // objNNTPosition
					, objPresentIDNo // objPresentIDNo
					, objPresentIDType // objPresentIDType
					, objGovTaxID // objGovTaxID
					, objContactName // objContactName
					, objContactPhone // objContactPhone
					, objRQ_Mst_NNT.OS_Inos_User.Email // objContactEmail
                    , objWebsite // objWebsite
					, objCANumber // objCANumber
					, objCAOrg // objCAOrg
					, objCAEffDTimeUTCStart // objCAEffDTimeUTCStart
					, objCAEffDTimeUTCEnd // objCAEffDTimeUTCEnd
					, objPackageCode // objPackageCode
					, objCreatedDate // objCreatedDate
					, objAccNo // objAccNo
					, objAccHolder // objAccHolder
					, objBankName // objBankName
					, objBizType // objBizType
					, objBizFieldCode // objBizFieldCode
					, objBizSizeCode // objBizSizeCode
					, objDealerType // objDealerType
									////
					, objDepartmentCode // objDepartmentCode
										////
					, objUserPassword // objUserPassword
					, objUserPasswordRepeat // objUserPasswordRepeat
											//////
											//, objQtyLicense
											////
					, dsData // dsData
					);

				//bFlagRptSvMstNNtAdd = true;
			}
			#endregion

			#region // Save Order:
			if (bFlagIsCreateOrder)
			{
				#region // Save InosCreateOrder:
				Inos_LicOrder objInos_LicOrderResult = null;
				{
					////
					objInos_LicOrder.OrgId = Convert.ToString(dt_Lst_MstSv_Inos_Org.Rows[0]["Id"]);

					objInos_LicOrderResult = Inos_OrderService_CreateOrderX_New20190913(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, (string)objAccessToken // strAccessToken
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						, objInos_LicOrder // strRt_Cols_OS_Inos_LicOrder
						);

					objRT_Mst_NNT.Inos_LicOrder = objInos_LicOrderResult;
				}
				#endregion

				#region // Upd MstSv_Inos_Org:
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
						, "@strId", dt_Lst_MstSv_Inos_Org.Rows[0]["Id"]
						, "@strOrderId", objInos_LicOrderResult.Id
						);

					_cf.db.ExecQuery(
						strUpdDB_MstSv_Inos_Org
						);
				}
				#endregion

				#region // Save Lic:
				{
					////
					DataSet dsGetData = null;

					List<OrgLicense> lstOrgLicense = Inos_LicService_GetOrgLicenseX(
						 strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, (string)objAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, dt_Lst_MstSv_Inos_Org.Rows[0]["Id"] // objOrgID
															  ////
						, out dsGetData // dsData
						);
					////

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

					////
					List<OrgSolutionUser> lstOrgSolutionUser = new List<OrgSolutionUser>();
					long lgOrgID = Convert.ToInt64(dt_Lst_MstSv_Inos_Org.Rows[0]["Id"]);
					//foreach (var item in lstOrgLicense)
					//{
					//	OrgSolutionUser obj = new OrgSolutionUser();
					//	obj.LicId = item.Id;
					//	obj.UserId = Convert.ToInt64(dt_Lst_MstSv_Inos_User.Rows[0]["Id"]);
					//	lstOrgSolutionUser.Add(obj);
					//}
					////
					OrderService objOrderService = new OrderService(null);
					objOrderService.AccessToken = strAccessToken;
					objAccountService.AccessToken = strAccessToken;
					var user = objAccountService.GetCurrentUser();
					LicOrder ret = new LicOrder();
					ret.Id = Convert.ToInt64(objInos_LicOrderResult.Id);
					LicOrder objLicOrderResult = objOrderService.GetOrderDetail(ret);
					List<LicOrderDetail> objDetailList = objLicOrderResult.DetailList;

					foreach (var obj in objDetailList)
					{
						OrgLicense objOrgLicense = obj.Lic;

						List<OrgSolution> lstOrgSolutions = objOrgLicense.OrgSolutionList;

						foreach (var item in lstOrgSolutions)
						{
							OrgSolutionUser objOrgSolutionUser = new OrgSolutionUser();

							objOrgSolutionUser.LicId = item.LicId;
							objOrgSolutionUser.SolutionCode = item.SolutionCode;
							objOrgSolutionUser.UserId = user.Id;

							lstOrgSolutionUser.Add(objOrgSolutionUser);
						}

					}

					////
					OSiNOSSv.LicService objLicService = new OSiNOSSv.LicService(null);
					objLicService.AccessToken = strAccessToken;

					List<OrgSolutionUser> lstOrgSolutionUserFinal = new List<OrgSolutionUser>();

					foreach (var obj in lstOrgSolutionUser)
					{
						////
						List<OrgSolutionUser> ret1 = objLicService.GetOrgSolutionUsers(
							Convert.ToString(dtDB_Sys_Solution.Rows[0]["SolutionCode"])
							, obj.LicId
							, lgOrgID
							);

						var objOrgSolutionUser = ret1.FirstOrDefault(x => x.LicId == obj.LicId && x.UserId == obj.UserId);

						if (objOrgSolutionUser == null)
						{
							////
							lstOrgSolutionUserFinal.Add(obj);
						}
					}
					////

					var result = objLicService.AddOrgSolutionUsers(null, lgOrgID, lstOrgSolutionUserFinal);
					////
					//DataSet dsData = null;

					//Int32 result = Inos_LicService_AddOrgSolutionUsersX(
					//	 strTid // strTid
					//	, strGwUserCode // strGwUserCode
					//	, strGwPassword // strGwPassword
					//	, strWAUserCode // strWAUserCode
					//	, strWAUserPassword // strWAUserPassword
					//	, (string)objAccessToken // strAccessToken
					//	, ref mdsFinal // mdsFinal
					//	, ref alParamsCoupleError // alParamsCoupleError
					//	, dtimeSys // dtimeSys
					//			   ////
					//	, dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
					//	, dt_Lst_MstSv_Inos_Org.Rows[0]["Id"] // objOrgID
					//	, lstOrgSolutionUserFinal // lstOrgSolutionUser
					//	////
					//	, out dsData
					//	);
				}
				#endregion
			}
			else
			{
				#region // Upd MstSv_Inos_Org:
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
                        , "@strId", objId_InosCreateOrg
                        , "@strOrderId", objInos_LicOrder.Id
                        );

					_cf.db.ExecQuery(
						strUpdDB_MstSv_Inos_Org
						);
				}
				#endregion
			}
			#endregion

			// Return Good:
			//TDALUtils.DBUtils.CommitSafety(_cf.db);
			mdsFinal.AcceptChanges();
			//return mdsFinal;
		}
		public DataSet RptSvCenter_Mst_NNT_AddByUserExist(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			//// InosCreateOrg
			, object objId_InosCreateOrg
			, object objParentId_InosCreateOrg
			, object objName_InosCreateOrg
			, object objBizTypeId_InosCreateOrg
			, object objBizTypeName_InosCreateOrg
			, object objBizFieldId_InosCreateOrg
			, object objBizFieldName_InosCreateOrg
			, object objContactName_InosCreateOrg
			, object objEmail_InosCreateOrg
			, object objPhoneNo_InosCreateOrg
			, object objDescription_InosCreateOrg
			, object objEnable_InosCreateOrg
			//// RptSv
			, object objMST
			, object objNNTFullName
			, object objMSTParent
			, object objProvinceCode
			, object objDistrictCode
			//, object objNNTType
			, object objDLCode
			, object objNNTAddress
			, object objNNTMobile
			, object objNTTPhone
			, object objNNTFax
			, object objPresentBy
			, object objBusinessRegNo
			, object objNNTPosition
			, object objPresentIDNo
			, object objPresentIDType
			, object objGovTaxID
			, object objContactName
			, object objContactPhone
			, object objContactEmail
			, object objWebsite
			, object objCANumber
			, object objCAOrg
			, object objCAEffDTimeUTCStart
			, object objCAEffDTimeUTCEnd
			, object objPackageCode
			, object objCreatedDate
			, object objAccNo
			, object objAccHolder
			, object objBankName
			, object objBizType
			, object objBizFieldCode
			, object objBizSizeCode
			, object objDealerType
			////
			, object objDepartmentCode
			////
			, object objUserPassword
			, object objUserPasswordRepeat
			//////
			//, object objQtyLicense
			////
			//, DataSet dsData
			////
			, Inos_LicOrder objInos_LicOrder
			////
			, RQ_Mst_NNT objRQ_Mst_NNT
			, ref RT_Mst_NNT objRT_Mst_NNT
			////
			, object objFlagIsCreateOrder
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Mst_NNT_AddByUserExist";
			string strErrorCodeDefault = TError.ErridnInventory.WA_Mst_NNT_Registry;
			alParamsCoupleError.AddRange(new object[]{
					"strFunctionName", strFunctionName
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objMST", objMST
					, "objNNTFullName", objNNTFullName
					, "objMSTParent", objMSTParent
					, "objProvinceCode", objProvinceCode
					, "objDistrictCode", objDistrictCode
                    //, "objNNTType", objNNTType
                    , "objDLCode", objDLCode
					, "objNNTAddress", objNNTAddress
					, "objNNTMobile", objNNTMobile
					, "objNTTPhone", objNTTPhone
					, "objNNTFax", objNNTFax
					, "objPresentBy", objPresentBy
					, "objBusinessRegNo", objBusinessRegNo
					, "objNNTPosition", objNNTPosition
					, "objPresentIDNo", objPresentIDNo
					, "objPresentIDType", objPresentIDType
					, "objGovTaxID", objGovTaxID
					, "objContactName", objContactName
					, "objContactPhone", objContactPhone
					, "objContactEmail", objContactEmail
					, "objWebsite", objWebsite
					, "objCANumber", objCANumber
					, "objCAOrg", objCAOrg
					, "objCAEffDTimeUTCStart", objCAEffDTimeUTCStart
					, "objCAEffDTimeUTCEnd", objCAEffDTimeUTCEnd
					, "objPackageCode", objPackageCode
					, "objAccNo", objAccNo
					, "objAccHolder", objAccHolder
					, "objBankName", objBankName
					, "objBankName", objBizType
					, "objBizFieldCode", objBizFieldCode
					, "objBizSizeCode", objBizSizeCode
                    /////
                    , "objDepartmentCode", objDepartmentCode
                    ////
                    , "objUserPassword", objUserPassword
					, "objUserPasswordRepeat", objUserPasswordRepeat
					////
					//, "objQtyLicense", objQtyLicense
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
				RptSv_Sys_User_CheckAuthentication(
					ref alParamsCoupleError
					, strWAUserCode
					, strWAUserPassword
					);

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region  // RptSv_Mst_NNT_AddX:
				RptCenter_Mst_NNT_AddByUserExistX(
					strTid
					, strGwUserCode
					, strGwPassword
					, strWAUserCode
					, strWAUserPassword
					, strAccessToken
					, ref mdsFinal
					, ref alParamsCoupleError
					//// InosCreateOrg
					, objId_InosCreateOrg
					, objParentId_InosCreateOrg
					, objName_InosCreateOrg
					, objBizTypeId_InosCreateOrg
					, objBizTypeName_InosCreateOrg
					, objBizFieldId_InosCreateOrg
					, objBizFieldName_InosCreateOrg
					, objContactName_InosCreateOrg
					, objEmail_InosCreateOrg
					, objPhoneNo_InosCreateOrg
					, objDescription_InosCreateOrg
					, objEnable_InosCreateOrg
					//// RptSv
					, objMST
					, objNNTFullName
					, objMSTParent
					, objProvinceCode
					, objDistrictCode
					//, objNNTType
					, objDLCode
					, objNNTAddress
					, objNNTMobile
					, objNTTPhone
					, objNNTFax
					, objPresentBy
					, objBusinessRegNo
					, objNNTPosition
					, objPresentIDNo
					, objPresentIDType
					, objGovTaxID
					, objContactName
					, objContactPhone
					, objContactEmail
					, objWebsite
					, objCANumber
					, objCAOrg
					, objCAEffDTimeUTCStart
					, objCAEffDTimeUTCEnd
					, objPackageCode
					, objCreatedDate
					, objAccNo
					, objAccHolder
					, objBankName
					, objBizType
					, objBizFieldCode
					, objBizSizeCode
					, objDealerType
					////
					, objDepartmentCode
					////
					, objUserPassword
					, objUserPasswordRepeat
					//////
					//, objQtyLicense
					////
					//, DataSet dsData
					////
					, objInos_LicOrder
					////
					, ref objRQ_Mst_NNT // objRQ_Mst_NNT
					, ref objRT_Mst_NNT
					////
					, objFlagIsCreateOrder
					);
				////
				#endregion

				#region // RptSvLocal:
				{
					#region // Call Func:
					////
					string strINVENTORY_RptSvLocal_URL = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_URL]);
					string strINVENTORY_RptSvLocal_GwUserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_GwPassword]);
					string strINVENTORY_RptSvLocal_GwPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_GwUserCode]);
					string strINVENTORY_RptSvLocal_WAUserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_WAUserCode]);
					string strINVENTORY_RptSvLocal_WAUserPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_WAUserPassword]);

					//string strNetWorkUrl = null;
					////
					RT_Mst_NNT objRT_Mst_NNT_Local = null;
					{
						#region // WA_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist:
						Mst_NNT objMst_NNT = new Mst_NNT();

						/////
						//RQ_Mst_NNT objRQ_Mst_NNT_Local = new RQ_Mst_NNT();
						//objRQ_Mst_NNT_Local = objRQ_Mst_NNT;
						//{

						//	Tid = strTid,
						//	TokenID = strHDDT_RptSvLocal_URL,
						//	NetworkID = "0",
						//	OrgID = "0",
						//	GwUserCode = strHDDT_RptSvLocal_GwUserCode,
						//	GwPassword = strHDDT_RptSvLocal_GwPassword,
						//	WAUserCode = strHDDT_RptSvLocal_WAUserCode,
						//	WAUserPassword = strHDDT_RptSvLocal_WAUserPassword
						//};
						////
						try
						{
							objRT_Mst_NNT_Local = OS_MstSvInventory_RptSvLocal_Mst_NNTService.Instance.WA_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist(strINVENTORY_RptSvLocal_URL, objRQ_Mst_NNT);
							//strNetWorkUrl = objRT_Mst_NNT.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
								TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                                , null
								, alParamsCoupleError.ToArray()
								);
						}
						////
						#endregion
					}
					#endregion

					//return;

					//System.Threading.Thread.Sleep(10000);

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

		public DataSet WAS_RptSvCenter_Mst_NNT_AddByUserExist(
			ref ArrayList alParamsCoupleError
			, RQ_Mst_NNT objRQ_Mst_NNT
			////
			, out RT_Mst_NNT objRT_Mst_NNT
			)
		{
			#region // Temp:
			string strTid = objRQ_Mst_NNT.Tid;
			objRT_Mst_NNT = new RT_Mst_NNT();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSv_Mst_NNT_AddByUserExist";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Mst_NNT_AddByUserExist;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "ReportServer", "ReportServer"
				, "OS_Inos_User", TJson.JsonConvert.SerializeObject(objRQ_Mst_NNT.OS_Inos_User)
				, "OS_Inos_Org", TJson.JsonConvert.SerializeObject(objRQ_Mst_NNT.OS_Inos_Org)
				, "iNOS_Mst_BizType", TJson.JsonConvert.SerializeObject(objRQ_Mst_NNT.iNOS_Mst_BizType)
				, "iNOS_Mst_BizField", TJson.JsonConvert.SerializeObject(objRQ_Mst_NNT.iNOS_Mst_BizField)
				, "Inos_LicOrder", TJson.JsonConvert.SerializeObject(objRQ_Mst_NNT.Inos_LicOrder)
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				////
				List<Mst_NNT> lst_Mst_NNT = new List<Mst_NNT>();
				DataSet dsData = new DataSet();
				{
					////
					//DataTable dt_MstSv_Inos_User = TUtils.DataTableCmUtils.ToDataTable<MstSv_Inos_User>(objRQ_Mst_NNT.Lst_MstSv_Inos_User, "MstSv_Inos_User");
					//dsData.Tables.Add(dt_MstSv_Inos_User);

					//DataTable dt_MstSv_Inos_Org = TUtils.DataTableCmUtils.ToDataTable<MstSv_Inos_Org>(objRQ_Mst_NNT.Lst_MstSv_Inos_Org, "MstSv_Inos_Org");
					//dsData.Tables.Add(dt_MstSv_Inos_Org);

					//DataTable dt_MstSv_Inos_OrgUser = TUtils.DataTableCmUtils.ToDataTable<MstSv_Inos_OrgUser>(objRQ_Mst_NNT.Lst_MstSv_Inos_OrgUser, "MstSv_Inos_OrgUser");
					//dsData.Tables.Add(dt_MstSv_Inos_OrgUser);

					//DataTable dt_MstSv_Inos_OrgInvite = TUtils.DataTableCmUtils.ToDataTable<MstSv_Inos_OrgInvite>(objRQ_Mst_NNT.Lst_MstSv_Inos_OrgInvite, "MstSv_Inos_OrgInvite");
					//dsData.Tables.Add(dt_MstSv_Inos_OrgInvite);


					//DataTable dt_MstSv_Inos_Package = TUtils.DataTableCmUtils.ToDataTable<MstSv_Inos_Package>(objRQ_Mst_NNT.Lst_MstSv_Inos_Package, "MstSv_Inos_Package");
					//dsData.Tables.Add(dt_MstSv_Inos_Package);
				}
				#endregion

				#region // RptSv_Mst_NNT_Add:
				mdsResult = RptSvCenter_Mst_NNT_AddByUserExist(
					objRQ_Mst_NNT.Tid // strTid
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					, objRQ_Mst_NNT.WAUserCode // strUserCode
					, objRQ_Mst_NNT.WAUserPassword // strUserPassword
					, objRQ_Mst_NNT.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  //// InosCreateOrg
					, objRQ_Mst_NNT.OS_Inos_Org.Id // objId_InosCreateOrg
					, objRQ_Mst_NNT.OS_Inos_Org.ParentId // objParentId
					, objRQ_Mst_NNT.OS_Inos_Org.Name // objName
					, objRQ_Mst_NNT.iNOS_Mst_BizType.BizType // objBizType
					, objRQ_Mst_NNT.iNOS_Mst_BizType.BizTypeName // objBizTypeName
					, objRQ_Mst_NNT.iNOS_Mst_BizField.BizFieldCode // objBizField
					, objRQ_Mst_NNT.iNOS_Mst_BizField.BizFieldName // objBizFieldName
					, objRQ_Mst_NNT.OS_Inos_Org.ContactName // objContactName
					, objRQ_Mst_NNT.OS_Inos_Org.Email // objEmail
					, objRQ_Mst_NNT.OS_Inos_Org.PhoneNo // objPhoneNo
					, objRQ_Mst_NNT.OS_Inos_Org.Description // objDescription
					, objRQ_Mst_NNT.OS_Inos_Org.Enable // objEnable
													   ////
					, objRQ_Mst_NNT.Mst_NNT.MST // objMST
					, objRQ_Mst_NNT.Mst_NNT.NNTFullName // objNNTFullName
					, objRQ_Mst_NNT.Mst_NNT.MSTParent // objMSTParent
					, objRQ_Mst_NNT.Mst_NNT.ProvinceCode // objProvinceCode
					, objRQ_Mst_NNT.Mst_NNT.DistrictCode // objDistrictCode
														 //, objRQ_Mst_NNT.Mst_NNT.NNTType // objNNTType
					, objRQ_Mst_NNT.Mst_NNT.DLCode // objDLCode
					, objRQ_Mst_NNT.Mst_NNT.NNTAddress // objAddress
					, objRQ_Mst_NNT.Mst_NNT.NNTMobile // objMobile
					, objRQ_Mst_NNT.Mst_NNT.NNTPhone // objPhone
					, objRQ_Mst_NNT.Mst_NNT.NNTFax // objFax
					, objRQ_Mst_NNT.Mst_NNT.PresentBy // objPresentBy
					, objRQ_Mst_NNT.Mst_NNT.BusinessRegNo // objBusinessRegNo
					, objRQ_Mst_NNT.Mst_NNT.NNTPosition // objPosition
					, objRQ_Mst_NNT.Mst_NNT.PresentIDNo // objPresentIDNo
					, objRQ_Mst_NNT.Mst_NNT.PresentIDType // objPresentIDType
					, objRQ_Mst_NNT.Mst_NNT.GovTaxID // objGovTaxID
					, objRQ_Mst_NNT.Mst_NNT.ContactName // objContactName
					, objRQ_Mst_NNT.Mst_NNT.ContactPhone // objContactPhone
					, objRQ_Mst_NNT.Mst_NNT.ContactEmail // objContactEmail
					, objRQ_Mst_NNT.Mst_NNT.Website // objWebsite
					, objRQ_Mst_NNT.Mst_NNT.CANumber // objCANumber
					, objRQ_Mst_NNT.Mst_NNT.CAOrg // objCAOrg
					, objRQ_Mst_NNT.Mst_NNT.CAEffDTimeUTCStart // objCAEffDTimeUTCStart
					, objRQ_Mst_NNT.Mst_NNT.CAEffDTimeUTCEnd // objCAEffDTimeUTCEnd
					, objRQ_Mst_NNT.Mst_NNT.PackageCode // objPackageCode
					, objRQ_Mst_NNT.Mst_NNT.CreatedDate // objCreatedDate
					, objRQ_Mst_NNT.Mst_NNT.AccNo // objAccNo
					, objRQ_Mst_NNT.Mst_NNT.AccHolder // objAccHolder
					, objRQ_Mst_NNT.Mst_NNT.BankName // objBankName
					, objRQ_Mst_NNT.Mst_NNT.BizType // objBizType
					, objRQ_Mst_NNT.Mst_NNT.BizFieldCode // objBizFieldCode
					, objRQ_Mst_NNT.Mst_NNT.BizSizeCode // objBizSizeCode
					, objRQ_Mst_NNT.Mst_NNT.DealerType // objDealerType
													   ////
					, "O" // objDepartmentCode Fix Cung
						  ////
					, objRQ_Mst_NNT.Mst_NNT.UserPassword // objUserPassword
					, objRQ_Mst_NNT.Mst_NNT.UserPasswordRepeat // objUserPasswordRepeat
															   //, dsData // dsData
															   ////
					, objRQ_Mst_NNT.Inos_LicOrder // Inos_LicOrder
												  // //
					, objRQ_Mst_NNT // objRQ_Mst_NNT
					, ref objRT_Mst_NNT // objRT_Mst_NNT
										////
					, objRQ_Mst_NNT.FlagIsCreateOrder // objFlagIsCreateOrder
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

		#region // MstSv_Mst_Network: RptSvLocal.
		public DataSet RptSvLocal_Mst_Network_InsertMQ(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			, object objOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "RptSv_Mst_Network_InsertMQ";
			string strErrorCodeDefault = TError.ErridnInventory.RptSv_Mst_Network_InsertMQ;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				, "objOrgIDSln", objOrgIDSln
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

				// RptSv_Sys_User_CheckAuthentication:
				RptSv_Sys_User_CheckAuthentication(
					ref alParamsCoupleError
					, strWAUserCode
					, strWAUserPassword
					);

				//// Check Access/Deny:
				//Sys_Access_CheckDenyV30(
				//    ref alParamsCoupleError
				//    , strWAUserCode
				//    , strFunctionName
				//    );
				#endregion

				#region // Refine and Check Input:
				////
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
				string strWSUrlAddr = null;
				bool bNetworkInitDone = false;
				//bool bNetworkInitDone = false;
				////
				//DataTable dtDB_Mst_NNT = null;
				//{

				//}
				#endregion

				#region // Save MQ_Mst_Network:
				////
				{
					string strSqlDelete = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								delete t
								from MQ_Mst_Network t
								where (1=1)
                                    and t.MST = @strMST
								;

							");
					_cf.db.ExecQuery(
						strSqlDelete
						, "@strMST", strMST
						);
				}

				//// Insert All:
				{
					////
					string zzzzClauseInsert_MQ_Mst_Network_zSave = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								insert into MQ_Mst_Network
								(
									NetworkID
									, MST
									, OrgID
									, OrgIDSln
									, WSUrlAddr
									, DBUrlAddr
									, FlagActive
									, LogLUDTimeUTC
									, LogLUBy
								)
								select
									msio.Id NetworkID
									, mnnt.MST MST
									, msio.Id OrgID
									, '@strOrgIDSln' OrgIDSln
									, null WSUrlAddr
									, null DBUrlAddr
									, '0' FlagActive
									, '@objLogLUDTimeUTC' LogLUDTimeUTC
									, '@objLogLUBy' LogLUBy 
								from Mst_NNT mnnt --//[mylock]
									left join MstSv_Inos_Org msio --//[mylock]
										on mnnt.MST = msio.MST
								where (1=1)
									and mnnt.MST = '@strMST'
								;
						"
						, "@strMST", strMST
						, "@strOrgIDSln", strOrgIDSln
						, "@objLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
						, "@objLogLUBy", strWAUserCode
						);

					////
					string strSqlExec = CmUtils.StringUtils.Replace(@"
							----
							zzzzClauseInsert_MQ_Mst_Network_zSave
							----							
						"
						, "zzzzClauseInsert_MQ_Mst_Network_zSave", zzzzClauseInsert_MQ_Mst_Network_zSave
						);
					////
					DataSet dsExec = _cf.db.ExecQuery(strSqlExec);

				}
				#endregion

				#region // Mst_NNT: Upd.
				{
					string strSqlUpd_Mst_NNT = CmUtils.StringUtils.Replace(@"
							---- Mst_NNT:
							update t
							set
								t.RegisterStatus = '@strRegisterStatus'
							from Mst_NNT t --//[mylock]
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						, "@strRegisterStatus", TConst.RegisterStatus.InsertMQ
						);

					_cf.db.ExecQuery(
						strSqlUpd_Mst_NNT
						);
				}
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db);


				#region // Check Init OK:
				_cf.db.BeginTransaction();
				////
				{
					////
					for (int nCheck = 0; nCheck < 10; nCheck++)
					{
						////
						string strGetDB_MQ_Mst_Network = CmUtils.StringUtils.Replace(@"
								---- MQ_Mst_Network:
								select top 1
									t.*
								from MQ_Mst_Network t --//[mylock]
								where (1=1)
									and t.MST = '@strMST'
								;
							"
							, "@strMST", strMST
							);

						DataTable dt_MQ_Mst_Network = _cf.db.ExecQuery(strGetDB_MQ_Mst_Network).Tables[0];

						if (!CmUtils.StringUtils.StringEqual(dt_MQ_Mst_Network.Rows[0]["FlagActive"], TConst.Flag.Active))
						{
							Thread.Sleep(10000);
							continue;
						}
						else
						{
							string strSqlUpd_Mst_NNT = CmUtils.StringUtils.Replace(@"
									---- Mst_NNT:
									update t
									set
										t.RegisterStatus = '@strRegisterStatus'
									from Mst_NNT t --//[mylock]
									where (1=1)
										and t.MST = '@strMST'
									;
								"
								, "@strMST", strMST
								, "@strRegisterStatus", TConst.RegisterStatus.Finish
								);

							_cf.db.ExecQuery(
								strSqlUpd_Mst_NNT
								);

							strWSUrlAddr = Convert.ToString(dt_MQ_Mst_Network.Rows[0]["WSUrlAddr"]);
							// Assign:
							CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strWSUrlAddr);

							bNetworkInitDone = true;
							break;
						}
					}

					if (!bNetworkInitDone)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_InitFail
							, null
							, alParamsCoupleError.ToArray()
							);
					}

				}

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db);
				#endregion

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

		public DataSet WAS_RptSvLocal_Mst_Network_InsertMQ(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSvLocal_Mst_Network_InsertMQ";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSvLocal_Mst_Network_InsertMQ;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				// //
				, "MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				//List<MstSv_Mst_NetworkInGroup> lst_MstSv_Mst_NetworkInGroup = new List<MstSv_Mst_NetworkInGroup>();
				#endregion

				#region // RptSvLocal_Mst_Network_InsertMQ:
				mdsResult = RptSvLocal_Mst_Network_InsertMQ(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, objRQ_MstSv_Mst_Network.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.MST // objNetworkID
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.OrgIDSln // objOrgIDSln
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

		#region // MstSv_Mst_Network: RptSvCenter.
		public DataSet RptSvCenter_Mst_Network_Gen(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref ArrayList alParamsCoupleError
			////
			, object objMST
			, object objOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Gen";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Gen;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			    ////
				, "objMST", objMST
				, "objOrgIDSln", objOrgIDSln
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

				#region // Refine and Check Input:
				////
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
				string strNetworkID = null;
				//bool bNetworkInitDone = false;
				////
				DataTable dtDB_Mst_NNT = null;
				{
					////
					Mst_NNT_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strMST // objMST
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, "" // strTCTStatusListToCheck
						, out dtDB_Mst_NNT // dtDB_Mst_NNT
						);

					string strRegisterStatus = TUtils.CUtils.StdParam(dtDB_Mst_NNT.Rows[0]["RegisterStatus"]);
					if (CmUtils.StringUtils.StringEqual(strRegisterStatus, TConst.RegisterStatus.InsertMQ)
						|| CmUtils.StringUtils.StringEqual(strRegisterStatus, TConst.RegisterStatus.Finish))
					{
						goto MyCodeLabel_Done; // Thành công.
					}
					////
					string strSqlGetDB_MstSv_Inos_User = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_User:
							select top 1 
								t.*
							from MstSv_Inos_User t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						);

					DataTable dtDB_MstSv_Inos_User = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_User).Tables[0];

					if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_User.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_User.Rows[0]["Id"]
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_User
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					string strSqlGetDB_MstSv_Inos_Org = CmUtils.StringUtils.Replace(@"
							---- MstSv_Inos_Org:
							select top 1 
								t.*
							from MstSv_Inos_Org t
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						);

					DataTable dtDB_MstSv_Inos_Org = _cf.db.ExecQuery(strSqlGetDB_MstSv_Inos_Org).Tables[0];

					if (CmUtils.StringUtils.StringEqual(dtDB_MstSv_Inos_Org.Rows[0]["Id"], TConst.InosMix.Default_Anonymous))
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strMST", strMST
							, "Check.Id", dtDB_MstSv_Inos_Org.Rows[0]["Id"]
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_Org
							, null
							, alParamsCoupleError.ToArray()
							);
					}

					strNetworkID = TUtils.CUtils.StdParam(dtDB_MstSv_Inos_Org.Rows[0]["Id"]);

					#region // Inventory_MstSv: WA_OS_MstSvTVAN_MstSv_Sys_User_Login.
					string strNetWorkUrl = null;
					{
						////
						string strINVENTORY_MstSv_URL = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_URL]);
						string strINVENTORY_MstSv_GwUserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_GwPassword]);
						string strINVENTORY_MstSv_GwPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_GwUserCode]);
						string strINVENTORY_MstSv_WAUserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_WAUserCode]);
						string strINVENTORY_MstSv_WAUserPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_WAUserPassword]);

						//string strNetWorkUrl_HDDT_MstSv = null;
						////
						RT_MstSv_Sys_User objRT_MstSv_Sys_User = null;
						{
							#region // WA_MstSv_Sys_User_Login:
							MstSv_Sys_User objMstSv_Sys_User = new MstSv_Sys_User();

							/////
							RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
							{

								Tid = strTid,
								TokenID = strINVENTORY_MstSv_URL,
								NetworkID = strNetworkID,
								OrgID = strNetworkID,
								GwUserCode = strINVENTORY_MstSv_GwUserCode,
								GwPassword = strINVENTORY_MstSv_GwPassword,
								WAUserCode = strINVENTORY_MstSv_WAUserCode,
								WAUserPassword = strINVENTORY_MstSv_WAUserPassword
							};

							////
							try
							{
								objRT_MstSv_Sys_User = OS_MstSvTVANService.Instance.WA_OS_MstSvTVAN_MstSv_Sys_User_Login(strINVENTORY_MstSv_URL, objRQ_MstSv_Sys_User);
								strNetWorkUrl = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
									TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                                    , null
									, alParamsCoupleError.ToArray()
									);
							}
							////
							#endregion
						}
					}

					if (!string.IsNullOrEmpty(strNetWorkUrl))
					{
						goto MyCodeLabel_Done; // Thành công.
					}
					#endregion
				}
				#endregion

				#region // HDDT_RptSvLocal: Mst_Network_InsertMQ.
				string strWSUrlAddr_Network = null;
				{
					////
					string strINVENTORY_RptSvLocal_URL = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_URL]);
					string strINVENTORY_RptSvLocal_GwUserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_GwPassword]);
					string strINVENTORY_RptSvLocal_GwPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_GwUserCode]);
					string strINVENTORY_RptSvLocal_WAUserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_WAUserCode]);
					string strINVENTORY_RptSvLocal_WAUserPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_RptSvLocal_WAUserPassword]);

					//string strNetWorkUrl_HDDT_RptSvLocal = null;
					////
					RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
					{
						#region // WA_MstSv_Mst_Network_Login:
						MstSv_Mst_Network objMstSv_Mst_Network = new MstSv_Mst_Network();

						/////
						RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network = new RQ_MstSv_Mst_Network()
						{

							Tid = strTid,
							TokenID = strINVENTORY_RptSvLocal_URL,
							GwUserCode = strINVENTORY_RptSvLocal_GwUserCode,
							GwPassword = strINVENTORY_RptSvLocal_GwPassword,
							WAUserCode = strINVENTORY_RptSvLocal_WAUserCode,
							WAUserPassword = strINVENTORY_RptSvLocal_WAUserPassword
						};

						objRQ_MstSv_Mst_Network.MstSv_Mst_Network = new MstSv_Mst_Network();
						objRQ_MstSv_Mst_Network.MstSv_Mst_Network.MST = strMST;
						objRQ_MstSv_Mst_Network.MstSv_Mst_Network.OrgIDSln = strOrgIDSln;
						////
						try
						{
							objRT_MstSv_Mst_Network = OS_MstSvInventory_RptSvLocal_Mst_NetworkService.Instance.WA_OS_RptSvLocal_Mst_Network_InsertMQ(strINVENTORY_RptSvLocal_URL, objRQ_MstSv_Mst_Network);
							strWSUrlAddr_Network = objRT_MstSv_Mst_Network.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
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
								TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                                , null
								, alParamsCoupleError.ToArray()
								);
						}
						////
						#endregion
					}
				}
				#endregion

				#region // Mst_NNT: Upd.
				{
					string strSqlUpd_Mst_NNT = CmUtils.StringUtils.Replace(@"
							---- Mst_NNT:
							update t
							set
								t.RegisterStatus = '@strRegisterStatus'
							from Mst_NNT t --//[mylock]
							where (1=1)
								and t.MST = '@strMST'
							;
						"
						, "@strMST", strMST
						, "@strRegisterStatus", TConst.RegisterStatus.Finish
						);

					_cf.db.ExecQuery(
						strSqlUpd_Mst_NNT
						);
				}
				#endregion

				#region // HDDT_RptSvLocal: WA_OS_MstSv_Mst_Network_Add.
				{
					////
					string strINVENTORY_MstSv_URL = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_URL]);
					string strINVENTORY_MstSv_GwUserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_GwPassword]);
					string strINVENTORY_MstSv_GwPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_GwUserCode]);
					string strINVENTORY_MstSv_WAUserCode = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_WAUserCode]);
					string strINVENTORY_MstSv_WAUserPassword = Convert.ToString(htCacheMstParam[TConst.Mst_Param.INVENTORY_MstSv_WAUserPassword]);

					//string strNetWorkUrl_HDDT_MstSv = null;
					////
					RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
					{
						#region // WA_MstSv_Mst_Network_Login:
						MstSv_Mst_Network objMstSv_Mst_Network = new MstSv_Mst_Network();

						/////
						RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network = new RQ_MstSv_Mst_Network()
						{

							Tid = strTid,
							TokenID = strINVENTORY_MstSv_URL,
							GwUserCode = strINVENTORY_MstSv_GwUserCode,
							GwPassword = strINVENTORY_MstSv_GwPassword,
							WAUserCode = strINVENTORY_MstSv_WAUserCode,
							WAUserPassword = strINVENTORY_MstSv_WAUserPassword
						};

						objRQ_MstSv_Mst_Network.MstSv_Mst_Network = new MstSv_Mst_Network();
						objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkID = strNetworkID;
						objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkName = strMST;
						objRQ_MstSv_Mst_Network.MstSv_Mst_Network.WSUrlAddr = strWSUrlAddr_Network;
						objRQ_MstSv_Mst_Network.MstSv_Mst_Network.DBUrlAddr = null;

						objRQ_MstSv_Mst_Network.MstSv_OrgInNetwork = new MstSv_OrgInNetwork();
						objRQ_MstSv_Mst_Network.MstSv_OrgInNetwork.MST = strMST;
						objRQ_MstSv_Mst_Network.MstSv_OrgInNetwork.OrgIDSln = strOrgIDSln;

						////
						try
						{
							objRT_MstSv_Mst_Network = OS_MstSvInventory_MstSv_Mst_NetworkService.Instance.WA_OS_MstSv_Mst_Network_Add(strINVENTORY_MstSv_URL, objRQ_MstSv_Mst_Network);
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
								TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INVENTORY" + "." + strErrorCodeOS
                                , null
								, alParamsCoupleError.ToArray()
								);
						}
						////
						#endregion
					}
				}
			#endregion

			// Return Good:
			MyCodeLabel_Done:
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

		public DataSet WAS_RptSvCenter_Mst_Network_Gen(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_RptSvCenter_Mst_Network_Gen";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Mst_Network_Gen;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				//List<MstSv_Mst_NetworkInGroup> lst_MstSv_Mst_NetworkInGroup = new List<MstSv_Mst_NetworkInGroup>();
				#endregion

				#region // MQ_Mst_Network: Get OrgIDSln.
				string strOrgIDSln = null;
				{
					TDAL.IEzDAL dbLocal = (TDAL.IEzDAL)_cf.db.Clone();

					try
					{
						dbLocal.BeginTransaction();
						// // Lock
						string strSqlUpd_MQ_Mst_Network = CmUtils.StringUtils.Replace(@"
								---- MstSv_OrgIDSln:
								update t
								set
									t.LogLUBy = @strLogLUBy
								from MstSv_OrgIDSln t 
								where (1=1)
								;
							"
							);

						dbLocal.ExecQuery(
							strSqlUpd_MQ_Mst_Network
							, "@strLogLUBy", objRQ_MstSv_Mst_Network.WAUserCode
							);

						// //
						string strSqlGetDB_MstSv_OrgIDSln = CmUtils.StringUtils.Replace(@"
								---- MstSv_OrgIDSln:
								select top 1
									t.OrgIDSln
								into #tbl_MstSv_OrgIDSln_Filter
								from MstSv_OrgIDSln t --//[mylock]
								where (1=1)
									and t.FlagActive = '1'
								order by
									t.AutoId asc
								;

								---- MstSv_OrgIDSln:
								update t
								set
									t.FlagActive = '0'
								from MstSv_OrgIDSln t --//[mylock]
									inner join #tbl_MstSv_OrgIDSln_Filter f --//[mylock]
										on t.OrgIDSln = f.OrgIDSln
								where (1=1)
								;

								---- Return:
								select * from #tbl_MstSv_OrgIDSln_Filter;
							");

						DataTable dtDB_MstSv_OrgIDSln = dbLocal.ExecQuery(
							strSqlGetDB_MstSv_OrgIDSln
							//, "@strDBName_HDDT_RptSv", _cf.nvcParams["Biz_DBName_HDDT_RptSv"]
							).Tables[0];

						strOrgIDSln = Convert.ToString(dtDB_MstSv_OrgIDSln.Rows[0]["OrgIDSln"]);
						// //
						dbLocal.Commit();
					}
					catch (Exception exc)
					{
						// Rollback:
						TDALUtils.DBUtils.RollbackSafety(dbLocal);

						// Return Bad:
						TUtils.CProcessExc.Process(
							ref mdsResult
							, exc
							, strErrorCodeDefault
							, alParamsCoupleError.ToArray()
							);

						throw exc;
					}
				}
				#endregion

				#region // MstSv_Mst_Network_Gen:
				mdsResult = RptSvCenter_Mst_Network_Gen(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, objRQ_MstSv_Mst_Network.AccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.MST // objNetworkID
					, strOrgIDSln // objOrgIDSln
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

		#region // MstSv_Mst_Network: MstSv.
		public DataSet MstSv_Mst_Network_Add(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, ref ArrayList alParamsCoupleError
			////
			, object objNetworkID
			, object objNetworkName
			, object objWSUrlAddr
			, object objDBUrlAddr
			// //
			, object objMST
			, object objOrgIDSln
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "MstSv_Mst_Network_Create";
			string strErrorCodeDefault = TError.ErridnInventory.MstSv_Mst_Network_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
				, "objNetworkID", objNetworkID
				, "objNetworkName", objNetworkName
				, "objWSUrlAddr", objWSUrlAddr
				, "objDBUrlAddr", objDBUrlAddr
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

				// Sys_User_CheckAuthentication:
				//Sys_User_CheckAuthentication(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);

				// Check Access/Deny:
				//MstSv_Sys_Administrator_CheckDeny(
				//	ref alParamsCoupleError
				//	, strWAUserCode
				//	, strWAUserPassword
				//	);
				#endregion

				#region // Refine and Check Input:
				////
				string strNetworkID = TUtils.CUtils.StdParam(objNetworkID);
				string strMST = TUtils.CUtils.StdParam(objMST);
				string strOrgIDSln = TUtils.CUtils.StdParam(objOrgIDSln);
				string strNetworkName = string.Format("{0}", objNetworkName).Trim();
				string strWSUrlAddr = string.Format("{0}", objWSUrlAddr).Trim();
				string strDBUrlAddr = string.Format("{0}", objDBUrlAddr).Trim();

				// drAbilityOfUser:
				//DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
				////
				DataTable dtDB_MstSv_Mst_Network = null;
				DataTable dtDB_MstSv_OrgInNetwork = null;
				{
					////
					if (strNetworkID == null || strNetworkID.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strNetworkID", strNetworkID
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Create_InvalidNetworkID
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					MstSv_Mst_Network_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkID // objNetworkID
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_MstSv_Mst_Network // dtDB_MstSv_Mst_Network
						);
					////
					if (strNetworkName.Length < 1)
					{
						alParamsCoupleError.AddRange(new object[]{
							"Check.strNetworkName", strNetworkName
							});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.MstSv_Mst_Network_Create_InvalidNetworkName
							, null
							, alParamsCoupleError.ToArray()
							);
					}
					////
					MstSv_OrgInNetwork_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strNetworkID // objNetworkID
						, strNetworkID // objOrgID
						, TConst.Flag.No // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_MstSv_OrgInNetwork // dtDB_MstSv_OrgInNetwork
						);
				}
				#endregion

				#region // SaveDB MstSv_Mst_Network:
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_MstSv_Mst_Network.NewRow();
					strFN = "NetworkID"; drDB[strFN] = strNetworkID;
					//strFN = "TenantId"; drDB[strFN] = nTenantId;
					strFN = "NetworkName"; drDB[strFN] = strNetworkName;
					strFN = "GroupNetworkID"; drDB[strFN] = "IDOCNET";
					strFN = "WSUrlAddr"; drDB[strFN] = strWSUrlAddr;
					strFN = "DBUrlAddr"; drDB[strFN] = strDBUrlAddr;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_MstSv_Mst_Network.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"MstSv_Mst_Network"
						, dtDB_MstSv_Mst_Network
						//, alColumnEffective.ToArray()
						);
				}
				#endregion

				#region // SaveDB MstSv_OrgInNetwork
				{
					// Init:
					//ArrayList alColumnEffective = new ArrayList();
					string strFN = "";
					DataRow drDB = dtDB_MstSv_OrgInNetwork.NewRow();
					strFN = "NetworkID"; drDB[strFN] = strNetworkID;
					strFN = "OrgID"; drDB[strFN] = strNetworkID;
					strFN = "MST"; drDB[strFN] = strMST;
					strFN = "OrgIDSln"; drDB[strFN] = strOrgIDSln;
					strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
					strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
					strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
					dtDB_MstSv_OrgInNetwork.Rows.Add(drDB);

					// Save:
					_cf.db.SaveData(
						"MstSv_OrgInNetwork"
						, dtDB_MstSv_OrgInNetwork
						//, alColumnEffective.ToArray()
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

		public DataSet WAS_MstSv_Mst_Network_Add(
			ref ArrayList alParamsCoupleError
			, RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network
			////
			, out RT_MstSv_Mst_Network objRT_MstSv_Mst_Network
			)
		{
			#region // Temp:
			string strTid = objRQ_MstSv_Mst_Network.Tid;
			objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
			DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			DateTime dtimeSys = DateTime.UtcNow;
			//DataSet mdsExec = null;
			//DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			//int nTidSeq = 0;
			//bool bNeedTransaction = true;
			string strFunctionName = "WAS_MstSv_Mst_Network_Add";
			string strErrorCodeDefault = TError.ErridnInventory.WAS_MstSv_Mst_Network_Add;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				, "MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_Mst_Network)
				, "MstSv_OrgInNetwork", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network.MstSv_OrgInNetwork)
				////
				});
			#endregion

			try
			{
				#region // Init:
				#endregion

				#region // Refine and Check Input:
				//List<MstSv_Mst_Network> lst_MstSv_Mst_Network = new List<MstSv_Mst_Network>();
				//List<MstSv_Mst_NetworkInGroup> lst_MstSv_Mst_NetworkInGroup = new List<MstSv_Mst_NetworkInGroup>();
				#endregion

				#region // MstSv_Mst_Network_Add:
				mdsResult = MstSv_Mst_Network_Add(
					objRQ_MstSv_Mst_Network.Tid // strTid
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					, objRQ_MstSv_Mst_Network.WAUserCode // strUserCode
					, objRQ_MstSv_Mst_Network.WAUserPassword // strUserPassword
					, ref alParamsCoupleError // alParamsCoupleError
											  ////
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkID // objNetworkID
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.NetworkName // objNetworkName
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.WSUrlAddr // objWSUrlAddr
					, objRQ_MstSv_Mst_Network.MstSv_Mst_Network.DBUrlAddr // objDBUrlAddr
																		  // //
					, objRQ_MstSv_Mst_Network.MstSv_OrgInNetwork.MST // objMST
					, objRQ_MstSv_Mst_Network.MstSv_OrgInNetwork.OrgIDSln // objOrgIDSln
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
