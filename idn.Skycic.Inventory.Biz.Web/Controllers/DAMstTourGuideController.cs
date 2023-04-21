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
    public class DAMstTourGuideController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourGuide> WA_Mst_TourGuide_Get(DA_RQ_Mst_TourGuide objRQ_Mst_TourGuide)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourGuide>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourGuide.Tid);
            DA_RT_Mst_TourGuide objRT_Mst_TourGuide = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourGuide_Get";
            string strErrorCodeDefault = "WA_Mst_TourGuide_Get";

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
                    , objRQ_Mst_TourGuide.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourGuide.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourGuide_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourGuide // RQ_Mst_District
                                          ////
                    , out objRT_Mst_TourGuide // RT_Mst_District
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
                objRT_Mst_TourGuide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourGuide>(objRT_Mst_TourGuide);
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
                if (objRT_Mst_TourGuide == null) objRT_Mst_TourGuide = new DA_RT_Mst_TourGuide();
                objRT_Mst_TourGuide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourGuide>(ex, objRT_Mst_TourGuide);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourGuide> WA_Mst_TourGuide_Create(DA_RQ_Mst_TourGuide objRQ_Mst_TourGuide)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourGuide>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourGuide.Tid);
            DA_RT_Mst_TourGuide objRT_Mst_TourGuide = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourGuide_Create";
            string strErrorCodeDefault = "WA_Mst_TourGuide_Create";

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
                    , objRQ_Mst_TourGuide.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourGuide.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourGuide_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourGuide // RQ_Mst_District
                                          ////
                    , out objRT_Mst_TourGuide // RT_Mst_District
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
                objRT_Mst_TourGuide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourGuide>(objRT_Mst_TourGuide);
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
                if (objRT_Mst_TourGuide == null) objRT_Mst_TourGuide = new DA_RT_Mst_TourGuide();
                objRT_Mst_TourGuide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourGuide>(ex, objRT_Mst_TourGuide);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourGuide> WA_Mst_TourGuide_Update(DA_RQ_Mst_TourGuide objRQ_Mst_TourGuide)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourGuide>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourGuide.Tid);
            DA_RT_Mst_TourGuide objRT_Mst_TourGuide = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourGuide_Update";
            string strErrorCodeDefault = "WA_Mst_TourGuide_Update";

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
                    , objRQ_Mst_TourGuide.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourGuide.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourGuide_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourGuide // RQ_Mst_District
                                          ////
                    , out objRT_Mst_TourGuide // RT_Mst_District
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
                objRT_Mst_TourGuide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourGuide>(objRT_Mst_TourGuide);
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
                if (objRT_Mst_TourGuide == null) objRT_Mst_TourGuide = new DA_RT_Mst_TourGuide();
                objRT_Mst_TourGuide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourGuide>(ex, objRT_Mst_TourGuide);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourGuide> WA_Mst_TourGuide_Delete(DA_RQ_Mst_TourGuide objRQ_Mst_TourGuide)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourGuide>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourGuide.Tid);
            DA_RT_Mst_TourGuide objRT_Mst_TourGuide = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourGuide_Delete";
            string strErrorCodeDefault = "WA_Mst_TourGuide_Delete";

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
                    , objRQ_Mst_TourGuide.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourGuide.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourGuide_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourGuide // RQ_Mst_District
                                          ////
                    , out objRT_Mst_TourGuide // RT_Mst_District
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
                objRT_Mst_TourGuide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourGuide>(objRT_Mst_TourGuide);
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
                if (objRT_Mst_TourGuide == null) objRT_Mst_TourGuide = new DA_RT_Mst_TourGuide();
                objRT_Mst_TourGuide.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourGuide>(ex, objRT_Mst_TourGuide);
                #endregion
            }
        }
    }
}
