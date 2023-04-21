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
    public class DAPContactController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_Contact> WA_POW_Contact_Get(DA_RQ_POW_Contact objRQ_POW_Contact)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_Contact>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_Contact.Tid);
            DA_RT_POW_Contact objRT_POW_Contact = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_Contact_Get";
            string strErrorCodeDefault = "WA_POW_Contact_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Contact.GwUserCode // strGwUserCode
                    , objRQ_POW_Contact.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_Contact_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Contact // RQ_Mst_District
                                        ////
                    , out objRT_POW_Contact // RT_Mst_District
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
                objRT_POW_Contact.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_Contact>(objRT_POW_Contact);
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
                if (objRT_POW_Contact == null) objRT_POW_Contact = new DA_RT_POW_Contact();
                objRT_POW_Contact.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_Contact>(ex, objRT_POW_Contact);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_Contact> WA_POW_Contact_Create(DA_RQ_POW_Contact objRQ_POW_Contact)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_Contact>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_Contact.Tid);
            DA_RT_POW_Contact objRT_POW_Contact = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_Contact_Create";
            string strErrorCodeDefault = "WA_POW_Contact_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Contact.GwUserCode // strGwUserCode
                    , objRQ_POW_Contact.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_Contact_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Contact // RQ_Mst_District
                                        ////
                    , out objRT_POW_Contact // RT_Mst_District
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
                objRT_POW_Contact.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_Contact>(objRT_POW_Contact);
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
                if (objRT_POW_Contact == null) objRT_POW_Contact = new DA_RT_POW_Contact();
                objRT_POW_Contact.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_Contact>(ex, objRT_POW_Contact);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_Contact> WA_POW_Contact_Update(DA_RQ_POW_Contact objRQ_POW_Contact)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_Contact>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_Contact.Tid);
            DA_RT_POW_Contact objRT_POW_Contact = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_Contact_Update";
            string strErrorCodeDefault = "WA_POW_Contact_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Contact.GwUserCode // strGwUserCode
                    , objRQ_POW_Contact.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_Contact_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Contact // RQ_Mst_District
                                        ////
                    , out objRT_POW_Contact // RT_Mst_District
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
                objRT_POW_Contact.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_Contact>(objRT_POW_Contact);
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
                if (objRT_POW_Contact == null) objRT_POW_Contact = new DA_RT_POW_Contact();
                objRT_POW_Contact.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_Contact>(ex, objRT_POW_Contact);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_Contact> WA_POW_Contact_Delete(DA_RQ_POW_Contact objRQ_POW_Contact)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_Contact>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_Contact.Tid);
            DA_RT_POW_Contact objRT_POW_Contact = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_Contact_Delete";
            string strErrorCodeDefault = "WA_POW_Contact_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Contact.GwUserCode // strGwUserCode
                    , objRQ_POW_Contact.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_Contact_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Contact // RQ_Mst_District
                                        ////
                    , out objRT_POW_Contact // RT_Mst_District
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
                objRT_POW_Contact.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_Contact>(objRT_POW_Contact);
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
                if (objRT_POW_Contact == null) objRT_POW_Contact = new DA_RT_POW_Contact();
                objRT_POW_Contact.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_Contact>(ex, objRT_POW_Contact);
                #endregion
            }
        }

        // ContactEmail
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_ContactEmail> WA_POW_ContactEmail_Get(DA_RQ_POW_ContactEmail objRQ_POW_ContactEmail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_ContactEmail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_ContactEmail.Tid);
            DA_RT_POW_ContactEmail objRT_POW_ContactEmail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_ContactEmail_Get";
            string strErrorCodeDefault = "WA_POW_ContactEmail_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_ContactEmail.GwUserCode // strGwUserCode
                    , objRQ_POW_ContactEmail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_ContactEmail_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_ContactEmail // RQ_Mst_District
                                             ////
                    , out objRT_POW_ContactEmail // RT_Mst_District
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
                objRT_POW_ContactEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_ContactEmail>(objRT_POW_ContactEmail);
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
                if (objRT_POW_ContactEmail == null) objRT_POW_ContactEmail = new DA_RT_POW_ContactEmail();
                objRT_POW_ContactEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_ContactEmail>(ex, objRT_POW_ContactEmail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_ContactEmail> WA_POW_ContactEmail_Create(DA_RQ_POW_ContactEmail objRQ_POW_ContactEmail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_ContactEmail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_ContactEmail.Tid);
            DA_RT_POW_ContactEmail objRT_POW_ContactEmail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_ContactEmail_Create";
            string strErrorCodeDefault = "WA_POW_ContactEmail_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_ContactEmail.GwUserCode // strGwUserCode
                    , objRQ_POW_ContactEmail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_ContactEmail_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_ContactEmail // RQ_Mst_District
                                             ////
                    , out objRT_POW_ContactEmail // RT_Mst_District
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
                objRT_POW_ContactEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_ContactEmail>(objRT_POW_ContactEmail);
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
                if (objRT_POW_ContactEmail == null) objRT_POW_ContactEmail = new DA_RT_POW_ContactEmail();
                objRT_POW_ContactEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_ContactEmail>(ex, objRT_POW_ContactEmail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_ContactEmail> WA_POW_ContactEmail_Delete(DA_RQ_POW_ContactEmail objRQ_POW_ContactEmail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_ContactEmail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_ContactEmail.Tid);
            DA_RT_POW_ContactEmail objRT_POW_ContactEmail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_ContactEmail_Delete";
            string strErrorCodeDefault = "WA_POW_ContactEmail_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_ContactEmail.GwUserCode // strGwUserCode
                    , objRQ_POW_ContactEmail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_ContactEmail_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_ContactEmail // RQ_Mst_District
                                             ////
                    , out objRT_POW_ContactEmail // RT_Mst_District
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
                objRT_POW_ContactEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_ContactEmail>(objRT_POW_ContactEmail);
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
                if (objRT_POW_ContactEmail == null) objRT_POW_ContactEmail = new DA_RT_POW_ContactEmail();
                objRT_POW_ContactEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_ContactEmail>(ex, objRT_POW_ContactEmail);
                #endregion
            }
        }
    }
}
