using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using System.Collections;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstGovTaxIDController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovTaxID> WA_Mst_GovTaxID_Get(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovTaxID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovTaxID.Tid);
            RT_Mst_GovTaxID objRT_Mst_GovTaxID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_GovTaxID_Get";
            string strErrorCodeDefault = "WA_Mst_GovTaxID_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovTaxID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_GovTaxID_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID // objRQ_Mst_GovTaxID
                                           ////
                    , out objRT_Mst_GovTaxID // RT_Mst_GovTaxID
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
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovTaxID>(objRT_Mst_GovTaxID);
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
                if (objRT_Mst_GovTaxID == null) objRT_Mst_GovTaxID = new RT_Mst_GovTaxID();
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovTaxID>(ex, objRT_Mst_GovTaxID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovTaxID> WA_Mst_GovTaxID_Create(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovTaxID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovTaxID.Tid);
            RT_Mst_GovTaxID objRT_Mst_GovTaxID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_GovTaxID_Create";
            string strErrorCodeDefault = "WA_Mst_GovTaxID_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovTaxID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_GovTaxID_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID // objRQ_Mst_GovTaxID
                                           ////
                    , out objRT_Mst_GovTaxID // RT_Mst_GovTaxID
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
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovTaxID>(objRT_Mst_GovTaxID);
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
                if (objRT_Mst_GovTaxID == null) objRT_Mst_GovTaxID = new RT_Mst_GovTaxID();
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovTaxID>(ex, objRT_Mst_GovTaxID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovTaxID> WA_Mst_GovTaxID_Update(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovTaxID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovTaxID.Tid);
            RT_Mst_GovTaxID objRT_Mst_GovTaxID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_GovTaxID_Update";
            string strErrorCodeDefault = "WA_Mst_GovTaxID_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovTaxID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_GovTaxID_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID // objRQ_Mst_GovTaxID
                                           ////
                    , out objRT_Mst_GovTaxID // RT_Mst_GovTaxID
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
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovTaxID>(objRT_Mst_GovTaxID);
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
                if (objRT_Mst_GovTaxID == null) objRT_Mst_GovTaxID = new RT_Mst_GovTaxID();
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovTaxID>(ex, objRT_Mst_GovTaxID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovTaxID> WA_Mst_GovTaxID_Delete(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovTaxID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovTaxID.Tid);
            RT_Mst_GovTaxID objRT_Mst_GovTaxID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_GovTaxID_Delete";
            string strErrorCodeDefault = "WA_Mst_GovTaxID_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovTaxID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_GovTaxID_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID // objRQ_Mst_GovTaxID
                                           ////
                    , out objRT_Mst_GovTaxID // RT_Mst_GovTaxID
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
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovTaxID>(objRT_Mst_GovTaxID);
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
                if (objRT_Mst_GovTaxID == null) objRT_Mst_GovTaxID = new RT_Mst_GovTaxID();
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovTaxID>(ex, objRT_Mst_GovTaxID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovTaxID> WA_RptSv_Mst_GovTaxID_Get(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovTaxID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovTaxID.Tid);
            RT_Mst_GovTaxID objRT_Mst_GovTaxID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_GovTaxID_Get";
            string strErrorCodeDefault = "WA_RptSv_Mst_GovTaxID_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovTaxID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_GovTaxID_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID // objRQ_Mst_GovTaxID
                                         ////
                    , out objRT_Mst_GovTaxID // RT_Mst_GovTaxID
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
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovTaxID>(objRT_Mst_GovTaxID);
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
                if (objRT_Mst_GovTaxID == null) objRT_Mst_GovTaxID = new RT_Mst_GovTaxID();
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovTaxID>(ex, objRT_Mst_GovTaxID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovTaxID> WA_RptSv_Mst_GovTaxID_Create(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovTaxID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovTaxID.Tid);
            RT_Mst_GovTaxID objRT_Mst_GovTaxID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_GovTaxID_Create";
            string strErrorCodeDefault = "WA_RptSv_Mst_GovTaxID_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovTaxID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_GovTaxID_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID // objRQ_Mst_GovTaxID
                                         ////
                    , out objRT_Mst_GovTaxID // RT_Mst_GovTaxID
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
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovTaxID>(objRT_Mst_GovTaxID);
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
                if (objRT_Mst_GovTaxID == null) objRT_Mst_GovTaxID = new RT_Mst_GovTaxID();
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovTaxID>(ex, objRT_Mst_GovTaxID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovTaxID> WA_RptSv_Mst_GovTaxID_Update(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovTaxID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovTaxID.Tid);
            RT_Mst_GovTaxID objRT_Mst_GovTaxID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_GovTaxID_Update";
            string strErrorCodeDefault = "WA_RptSv_Mst_GovTaxID_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovTaxID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_GovTaxID_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID // objRQ_Mst_GovTaxID
                                         ////
                    , out objRT_Mst_GovTaxID // RT_Mst_GovTaxID
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
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovTaxID>(objRT_Mst_GovTaxID);
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
                if (objRT_Mst_GovTaxID == null) objRT_Mst_GovTaxID = new RT_Mst_GovTaxID();
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovTaxID>(ex, objRT_Mst_GovTaxID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovTaxID> WA_RptSv_Mst_GovTaxID_Delete(RQ_Mst_GovTaxID objRQ_Mst_GovTaxID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovTaxID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovTaxID.Tid);
            RT_Mst_GovTaxID objRT_Mst_GovTaxID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_GovTaxID_Delete";
            string strErrorCodeDefault = "WA_RptSv_Mst_GovTaxID_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovTaxID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_GovTaxID_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovTaxID // objRQ_Mst_GovTaxID
                                         ////
                    , out objRT_Mst_GovTaxID // RT_Mst_GovTaxID
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
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovTaxID>(objRT_Mst_GovTaxID);
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
                if (objRT_Mst_GovTaxID == null) objRT_Mst_GovTaxID = new RT_Mst_GovTaxID();
                objRT_Mst_GovTaxID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovTaxID>(ex, objRT_Mst_GovTaxID);
                #endregion
            }
        }

    }
}
