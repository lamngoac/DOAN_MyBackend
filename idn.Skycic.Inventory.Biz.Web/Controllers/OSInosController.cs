using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class OSInosController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_OS_Inos_Package> WA_OS_Inos_Package_Get(RQ_OS_Inos_Package objRQ_OS_Inos_Package)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_Package>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_Package.Tid);
			RT_OS_Inos_Package objRT_OS_Inos_Package = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_OS_Inos_Package_Get";
			string strErrorCodeDefault = "WA_OS_Inos_Package_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_Package", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_Package)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_OS_Inos_Package.GwUserCode // strGwUserCode
					, objRQ_OS_Inos_Package.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_OS_Inos_Package_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_OS_Inos_Package // objRQ_OS_Inos_Package
											////
					, out objRT_OS_Inos_Package // RT_OS_Inos_Package
					);

				if (CmUtils.CMyDataSet.HasError(mdsReturn))
				{
					throw CmUtils.CMyException.Raise(
						(string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
						, null
						, null
						);
				}
				#endregion

				// Return Good:
				objRT_OS_Inos_Package.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_OS_Inos_Package>(objRT_OS_Inos_Package);
			}
			catch (Exception ex)
			{
				#region // Catch of try:
				////
				TUtils.CProcessExc.Process(
					ref mdsReturn // mdsFinal
					, ex // exc
					, strErrorCodeDefault // strErrorCode
					, alParamsCoupleError.ToArray() // arrobjErrorParams
					);

				// Return Bad:
				if (objRT_OS_Inos_Package == null) objRT_OS_Inos_Package = new RT_OS_Inos_Package();
				objRT_OS_Inos_Package.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_OS_Inos_Package>(ex, objRT_OS_Inos_Package);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_OS_Inos_Package> WA_RptSv_OS_Inos_Package_Get(RQ_OS_Inos_Package objRQ_OS_Inos_Package)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_Package>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_Package.Tid);
			RT_OS_Inos_Package objRT_OS_Inos_Package = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_OS_Inos_Package_Get";
			string strErrorCodeDefault = "WA_RptSv_OS_Inos_Package_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_Package", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_Package)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_OS_Inos_Package.GwUserCode // strGwUserCode
					, objRQ_OS_Inos_Package.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_OS_Inos_Package_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_OS_Inos_Package // objRQ_OS_Inos_Package
											////
					, out objRT_OS_Inos_Package // RT_OS_Inos_Package
					);

				if (CmUtils.CMyDataSet.HasError(mdsReturn))
				{
					throw CmUtils.CMyException.Raise(
						(string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
						, null
						, null
						);
				}
				#endregion

				// Return Good:
				objRT_OS_Inos_Package.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_OS_Inos_Package>(objRT_OS_Inos_Package);
			}
			catch (Exception ex)
			{
				#region // Catch of try:
				////
				TUtils.CProcessExc.Process(
					ref mdsReturn // mdsFinal
					, ex // exc
					, strErrorCodeDefault // strErrorCode
					, alParamsCoupleError.ToArray() // arrobjErrorParams
					);

				// Return Bad:
				if (objRT_OS_Inos_Package == null) objRT_OS_Inos_Package = new RT_OS_Inos_Package();
				objRT_OS_Inos_Package.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_OS_Inos_Package>(ex, objRT_OS_Inos_Package);
				#endregion
			}
		}
        
		[AcceptVerbs("POST")]
		public ServiceResult<RT_OS_Inos_Org> WA_OS_Inos_Org_GetMyOrgList(RQ_OS_Inos_Org objRQ_OS_Inos_Org)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_Org>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_Org.Tid);
			RT_OS_Inos_Org objRT_OS_Inos_Org = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_OS_Inos_Org_GetMyOrgList";
			string strErrorCodeDefault = "WA_OS_Inos_Org_GetMyOrgList";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_Org", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_Org)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_OS_Inos_Org.GwUserCode // strGwUserCode
					, objRQ_OS_Inos_Org.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_OS_Inos_Org_GetMyOrgList(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_OS_Inos_Org // objRQ_OS_Inos_Org
											////
					, out objRT_OS_Inos_Org // RT_OS_Inos_Org
					);

				if (CmUtils.CMyDataSet.HasError(mdsReturn))
				{
					throw CmUtils.CMyException.Raise(
						(string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
						, null
						, null
						);
				}
				#endregion

				// Return Good:
				objRT_OS_Inos_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_OS_Inos_Org>(objRT_OS_Inos_Org);
			}
			catch (Exception ex)
			{
				#region // Catch of try:
				////
				TUtils.CProcessExc.Process(
					ref mdsReturn // mdsFinal
					, ex // exc
					, strErrorCodeDefault // strErrorCode
					, alParamsCoupleError.ToArray() // arrobjErrorParams
					);

				// Return Bad:
				if (objRT_OS_Inos_Org == null) objRT_OS_Inos_Org = new RT_OS_Inos_Org();
				objRT_OS_Inos_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_OS_Inos_Org>(ex, objRT_OS_Inos_Org);
				#endregion
			}
		}

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_OrgLicense> WA_OS_Inos_OrgLicense_GetAndSave(RQ_OS_Inos_OrgLicense objRQ_OS_Inos_OrgLicense)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_OrgLicense>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_OrgLicense.Tid);
            RT_OS_Inos_OrgLicense objRT_OS_Inos_OrgLicense = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Inos_OrgLicense_GetAndSave";
            string strErrorCodeDefault = "WA_OS_Inos_OrgLicense_GetAndSave";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_OrgLicense", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_OrgLicense)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_OrgLicense.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_OrgLicense.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_OrgLicense_GetAndSave(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_OrgLicense // objRQ_OS_Inos_OrgLicense
                                            ////
                    , out objRT_OS_Inos_OrgLicense // RT_OS_Inos_OrgLicense
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_OrgLicense.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_OrgLicense>(objRT_OS_Inos_OrgLicense);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_OrgLicense == null) objRT_OS_Inos_OrgLicense = new RT_OS_Inos_OrgLicense();
                objRT_OS_Inos_OrgLicense.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_OrgLicense>(ex, objRT_OS_Inos_OrgLicense);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_OrgSolution> WA_OS_Inos_OrgSolution_GetAndSave(RQ_OS_Inos_OrgSolution objRQ_OS_Inos_OrgSolution)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_OrgSolution>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_OrgSolution.Tid);
            RT_OS_Inos_OrgSolution objRT_OS_Inos_OrgSolution = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Inos_OrgSolution_GetAndSave";
            string strErrorCodeDefault = "WA_OS_Inos_OrgSolution_GetAndSave";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_OrgSolution", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_OrgSolution)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_OrgSolution.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_OrgSolution.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_OrgSolution_GetAndSave(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_OrgSolution // objRQ_OS_Inos_OrgSolution
                                               ////
                    , out objRT_OS_Inos_OrgSolution // RT_OS_Inos_OrgSolution
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_OrgSolution.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_OrgSolution>(objRT_OS_Inos_OrgSolution);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_OrgSolution == null) objRT_OS_Inos_OrgSolution = new RT_OS_Inos_OrgSolution();
                objRT_OS_Inos_OrgSolution.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_OrgSolution>(ex, objRT_OS_Inos_OrgSolution);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_Org> WA_OS_Inos_Org_Create(RQ_OS_Inos_Org objRQ_OS_Inos_Org)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_Org>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_Org.Tid);
            RT_OS_Inos_Org objRT_OS_Inos_Org = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Inos_Org_Create";
            string strErrorCodeDefault = "WA_OS_Inos_Org_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_Org", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_Org)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_Org.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_Org.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_Org_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_Org // objRQ_OS_Inos_Org
                                                ////
                    , out objRT_OS_Inos_Org // RT_OS_Inos_Org
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_Org>(objRT_OS_Inos_Org);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_Org == null) objRT_OS_Inos_Org = new RT_OS_Inos_Org();
                objRT_OS_Inos_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_Org>(ex, objRT_OS_Inos_Org);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_VerificationEmail> WA_OS_Inos_SendEmailVerificationEmail(RQ_VerificationEmail objRQ_VerificationEmail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_VerificationEmail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_VerificationEmail.Tid);
            RT_VerificationEmail objRT_VerificationEmail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_SendEmailVerificationEmail";
            string strErrorCodeDefault = "WA_SendEmailVerificationEmail";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_VerificationEmail", TJson.JsonConvert.SerializeObject(objRQ_VerificationEmail)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_VerificationEmail.GwUserCode // strGwUserCode
                    , objRQ_VerificationEmail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_SendEmailVerificationEmail(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_VerificationEmail // objRQ_VerificationEmail
                                            ////
                    , out objRT_VerificationEmail // RT_VerificationEmail
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_VerificationEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_VerificationEmail>(objRT_VerificationEmail);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_VerificationEmail == null) objRT_VerificationEmail = new RT_VerificationEmail();
                objRT_VerificationEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_VerificationEmail>(ex, objRT_VerificationEmail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_VerificationEmail> WA_OS_Inos_VerifyEmail(RQ_VerificationEmail objRQ_VerificationEmail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_VerificationEmail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_VerificationEmail.Tid);
            RT_VerificationEmail objRT_VerificationEmail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_SendEmailVerificationEmail";
            string strErrorCodeDefault = "WA_SendEmailVerificationEmail";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_VerificationEmail", TJson.JsonConvert.SerializeObject(objRQ_VerificationEmail)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_VerificationEmail.GwUserCode // strGwUserCode
                    , objRQ_VerificationEmail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_VerifyEmail(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_VerificationEmail // objRQ_VerificationEmail
                                              ////
                    , out objRT_VerificationEmail // RT_VerificationEmail
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_VerificationEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_VerificationEmail>(objRT_VerificationEmail);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_VerificationEmail == null) objRT_VerificationEmail = new RT_VerificationEmail();
                objRT_VerificationEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_VerificationEmail>(ex, objRT_VerificationEmail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_User> WA_OS_Inos_User_Create(RQ_OS_Inos_User objRQ_OS_Inos_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_User.Tid);
            RT_OS_Inos_User objRT_OS_Inos_User= null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Inos_User_Create";
            string strErrorCodeDefault = "WA_OS_Inos_User_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_User", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_User)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_User.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_User_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_User// objRQ_OS_Inos_User
                                        ////
                    , out objRT_OS_Inos_User// RT_OS_Inos_User
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_User>(objRT_OS_Inos_User);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_User== null) objRT_OS_Inos_User= new RT_OS_Inos_User();
                objRT_OS_Inos_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_User>(ex, objRT_OS_Inos_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_LicOrder> WA_OS_Inos_OrderService_GetDiscountCode(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_LicOrder>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Inos_OrderService_GetDiscountCode";
            string strErrorCodeDefault = "WA_OS_Inos_OrderService_GetDiscountCode";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_LicOrder", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_LicOrder)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_OrderService_GetDiscountCode(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder// objRQ_OS_Inos_LicOrder
                                        ////
                    , out objRT_OS_Inos_LicOrder// RT_OS_Inos_LicOrder
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_LicOrder>(objRT_OS_Inos_LicOrder);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_LicOrder == null) objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_LicOrder>(ex, objRT_OS_Inos_LicOrder);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_LicOrder> WA_OS_Inos_OrderService_CreateOrder(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_LicOrder>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Inos_OrderService_CreateOrder";
            string strErrorCodeDefault = "WA_OS_Inos_OrderService_CreateOrder";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_LicOrder", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_LicOrder)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_OrderService_CreateOrder(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder// objRQ_OS_Inos_LicOrder
                                            ////
                    , out objRT_OS_Inos_LicOrder// RT_OS_Inos_LicOrder
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_LicOrder>(objRT_OS_Inos_LicOrder);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_LicOrder == null) objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_LicOrder>(ex, objRT_OS_Inos_LicOrder);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_LicOrder> WA_OS_Inos_OrderService_CheckOrderStatus(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_LicOrder>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Inos_OrderService_CheckOrderStatus";
            string strErrorCodeDefault = "WA_OS_Inos_OrderService_CheckOrderStatus";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_LicOrder", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_LicOrder)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Inos_OrderService_CheckOrderStatus(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder// objRQ_OS_Inos_LicOrder
                                            ////
                    , out objRT_OS_Inos_LicOrder// RT_OS_Inos_LicOrder
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_LicOrder>(objRT_OS_Inos_LicOrder);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_LicOrder == null) objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_LicOrder>(ex, objRT_OS_Inos_LicOrder);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_LicOrder> WA_RptSv_OS_Inos_OrderService_GetDiscountCode(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_LicOrder>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_OS_Inos_OrderService_GetDiscountCode";
            string strErrorCodeDefault = "WA_RptSv_OS_Inos_OrderService_GetDiscountCode";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_LicOrder", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_LicOrder)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_OS_Inos_OrderService_GetDiscountCode(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder// objRQ_OS_Inos_LicOrder
                                            ////
                    , out objRT_OS_Inos_LicOrder// RT_OS_Inos_LicOrder
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_LicOrder>(objRT_OS_Inos_LicOrder);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_LicOrder == null) objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_LicOrder>(ex, objRT_OS_Inos_LicOrder);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_LicOrder> WA_RptSv_OS_Inos_OrderService_CreateOrder(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_LicOrder>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_OS_Inos_OrderService_CreateOrder";
            string strErrorCodeDefault = "WA_RptSv_OS_Inos_OrderService_CreateOrder";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_LicOrder", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_LicOrder)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_OS_Inos_OrderService_CreateOrder(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder// objRQ_OS_Inos_LicOrder
                                            ////
                    , out objRT_OS_Inos_LicOrder// RT_OS_Inos_LicOrder
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_LicOrder>(objRT_OS_Inos_LicOrder);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_LicOrder == null) objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_LicOrder>(ex, objRT_OS_Inos_LicOrder);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Inos_LicOrder> WA_RptSv_OS_Inos_OrderService_CheckOrderStatus(RQ_OS_Inos_LicOrder objRQ_OS_Inos_LicOrder)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Inos_LicOrder>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Inos_LicOrder.Tid);
            RT_OS_Inos_LicOrder objRT_OS_Inos_LicOrder = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_OS_Inos_OrderService_CheckOrderStatus";
            string strErrorCodeDefault = "WA_RptSv_OS_Inos_OrderService_CheckOrderStatus";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_OS_Inos_LicOrder", TJson.JsonConvert.SerializeObject(objRQ_OS_Inos_LicOrder)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder.GwUserCode // strGwUserCode
                    , objRQ_OS_Inos_LicOrder.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_OS_Inos_OrderService_CheckOrderStatus(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Inos_LicOrder// objRQ_OS_Inos_LicOrder
                                            ////
                    , out objRT_OS_Inos_LicOrder// RT_OS_Inos_LicOrder
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Inos_LicOrder>(objRT_OS_Inos_LicOrder);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_Inos_LicOrder == null) objRT_OS_Inos_LicOrder = new RT_OS_Inos_LicOrder();
                objRT_OS_Inos_LicOrder.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Inos_LicOrder>(ex, objRT_OS_Inos_LicOrder);
                #endregion
            }
        }
        
    }
}
