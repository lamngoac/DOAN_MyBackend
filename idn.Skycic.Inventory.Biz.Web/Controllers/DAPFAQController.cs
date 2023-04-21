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
    public class DAPFAQController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_FAQ> WA_POW_FAQ_Get(DA_RQ_POW_FAQ objRQ_POW_FAQ)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_FAQ>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_FAQ.Tid);
            DA_RT_POW_FAQ objRT_POW_FAQ = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_FAQ_Get";
            string strErrorCodeDefault = "WA_POW_FAQ_Get";

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
                    , objRQ_POW_FAQ.GwUserCode // strGwUserCode
                    , objRQ_POW_FAQ.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_FAQ_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_FAQ // RQ_Mst_District
                                    ////
                    , out objRT_POW_FAQ // RT_Mst_District
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
                objRT_POW_FAQ.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_FAQ>(objRT_POW_FAQ);
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
                if (objRT_POW_FAQ == null) objRT_POW_FAQ = new DA_RT_POW_FAQ();
                objRT_POW_FAQ.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_FAQ>(ex, objRT_POW_FAQ);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_FAQ> WA_POW_FAQ_Create(DA_RQ_POW_FAQ objRQ_POW_FAQ)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_FAQ>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_FAQ.Tid);
            DA_RT_POW_FAQ objRT_POW_FAQ = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_FAQ_Create";
            string strErrorCodeDefault = "WA_POW_FAQ_Create";

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
                    , objRQ_POW_FAQ.GwUserCode // strGwUserCode
                    , objRQ_POW_FAQ.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_FAQ_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_FAQ // RQ_Mst_Part
                                    ////
                    , out objRT_POW_FAQ // RT_Mst_Part
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
                objRT_POW_FAQ.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_FAQ>(objRT_POW_FAQ);
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
                if (objRT_POW_FAQ == null) objRT_POW_FAQ = new DA_RT_POW_FAQ();
                objRT_POW_FAQ.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_FAQ>(ex, objRT_POW_FAQ);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_FAQ> WA_POW_FAQ_Update(DA_RQ_POW_FAQ objRQ_POW_FAQ)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_FAQ>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_FAQ.Tid);
            DA_RT_POW_FAQ objRT_POW_FAQ = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_FAQ_Update";
            string strErrorCodeDefault = "WA_POW_FAQ_Update";

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
                    , objRQ_POW_FAQ.GwUserCode // strGwUserCode
                    , objRQ_POW_FAQ.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_FAQ_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_FAQ // RQ_Mst_Part
                                    ////
                    , out objRT_POW_FAQ // RT_Mst_Part
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
                objRT_POW_FAQ.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_FAQ>(objRT_POW_FAQ);
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
                if (objRT_POW_FAQ == null) objRT_POW_FAQ = new DA_RT_POW_FAQ();
                objRT_POW_FAQ.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_FAQ>(ex, objRT_POW_FAQ);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_FAQ> WA_POW_FAQ_Delete(DA_RQ_POW_FAQ objRQ_POW_FAQ)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_FAQ>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_FAQ.Tid);
            DA_RT_POW_FAQ objRT_POW_FAQ = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_FAQ_Delete";
            string strErrorCodeDefault = "WA_POW_FAQ_Delete";

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
                    , objRQ_POW_FAQ.GwUserCode // strGwUserCode
                    , objRQ_POW_FAQ.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_FAQ_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_FAQ
                    ////
                    , out objRT_POW_FAQ
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
                objRT_POW_FAQ.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_FAQ>(objRT_POW_FAQ);
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
                if (objRT_POW_FAQ == null) objRT_POW_FAQ = new DA_RT_POW_FAQ();
                objRT_POW_FAQ.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_FAQ>(ex, objRT_POW_FAQ);
                #endregion
            }
        }
    }
}
