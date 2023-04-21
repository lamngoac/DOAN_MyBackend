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
    public class DAMstTourController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Tour> WA_Mst_Tour_Get(DA_RQ_Mst_Tour objRQ_Mst_Tour)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Tour>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Tour.Tid);
            DA_RT_Mst_Tour objRT_Mst_Tour = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Tour_Get";
            string strErrorCodeDefault = "WA_Mst_Tour_Get";

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
                    , objRQ_Mst_Tour.GwUserCode // strGwUserCode
                    , objRQ_Mst_Tour.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Tour_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Tour // RQ_Mst_District
                                     ////
                    , out objRT_Mst_Tour // RT_Mst_District
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
                objRT_Mst_Tour.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Tour>(objRT_Mst_Tour);
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
                if (objRT_Mst_Tour == null) objRT_Mst_Tour = new DA_RT_Mst_Tour();
                objRT_Mst_Tour.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Tour>(ex, objRT_Mst_Tour);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Tour> WA_Mst_Tour_Create(DA_RQ_Mst_Tour objRQ_Mst_Tour)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Tour>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Tour.Tid);
            DA_RT_Mst_Tour objRT_Mst_Tour = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Tour_Create";
            string strErrorCodeDefault = "WA_Mst_Tour_Create";

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
                    , objRQ_Mst_Tour.GwUserCode // strGwUserCode
                    , objRQ_Mst_Tour.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Tour_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Tour // RQ_Mst_Part
                                     ////
                    , out objRT_Mst_Tour // RT_Mst_Part
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
                objRT_Mst_Tour.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Tour>(objRT_Mst_Tour);
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
                if (objRT_Mst_Tour == null) objRT_Mst_Tour = new DA_RT_Mst_Tour();
                objRT_Mst_Tour.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Tour>(ex, objRT_Mst_Tour);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Tour> WA_Mst_Tour_Update(DA_RQ_Mst_Tour objRQ_Mst_Tour)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Tour>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Tour.Tid);
            DA_RT_Mst_Tour objRT_Mst_Tour = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Tour_Update";
            string strErrorCodeDefault = "WA_Mst_Tour_Update";

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
                    , objRQ_Mst_Tour.GwUserCode // strGwUserCode
                    , objRQ_Mst_Tour.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Tour_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Tour // RQ_Mst_Part
                                     ////
                    , out objRT_Mst_Tour // RT_Mst_Part
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
                objRT_Mst_Tour.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Tour>(objRT_Mst_Tour);
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
                if (objRT_Mst_Tour == null) objRT_Mst_Tour = new DA_RT_Mst_Tour();
                objRT_Mst_Tour.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Tour>(ex, objRT_Mst_Tour);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Tour> WA_Mst_Tour_Delete(DA_RQ_Mst_Tour objRQ_Mst_Tour)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Tour>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Tour.Tid);
            DA_RT_Mst_Tour objRT_Mst_Tour = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Tour_Delete";
            string strErrorCodeDefault = "WA_Mst_Tour_Delete";

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
                    , objRQ_Mst_Tour.GwUserCode // strGwUserCode
                    , objRQ_Mst_Tour.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Tour_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Tour
                    ////
                    , out objRT_Mst_Tour
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
                objRT_Mst_Tour.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Tour>(objRT_Mst_Tour);
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
                if (objRT_Mst_Tour == null) objRT_Mst_Tour = new DA_RT_Mst_Tour();
                objRT_Mst_Tour.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Tour>(ex, objRT_Mst_Tour);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourDetail> WA_Mst_TourDetail_Get(DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourDetail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourDetail.Tid);
            DA_RT_Mst_TourDetail objRT_Mst_TourDetail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourDetail_Get";
            string strErrorCodeDefault = "WA_Mst_TourDetail_Get";

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
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourDetail_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourDetail // RQ_Mst_District
                                           ////
                    , out objRT_Mst_TourDetail // RT_Mst_District
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
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourDetail>(objRT_Mst_TourDetail);
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
                if (objRT_Mst_TourDetail == null) objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourDetail>(ex, objRT_Mst_TourDetail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourDetail> WA_Mst_TourDetail_Create(DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourDetail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourDetail.Tid);
            DA_RT_Mst_TourDetail objRT_Mst_TourDetail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourDetail_Create";
            string strErrorCodeDefault = "WA_Mst_TourDetail_Create";

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
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourDetail_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourDetail // RQ_Mst_District
                                           ////
                    , out objRT_Mst_TourDetail // RT_Mst_District
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
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourDetail>(objRT_Mst_TourDetail);
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
                if (objRT_Mst_TourDetail == null) objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourDetail>(ex, objRT_Mst_TourDetail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourDetail> WA_Mst_TourDetail_Update(DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourDetail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourDetail.Tid);
            DA_RT_Mst_TourDetail objRT_Mst_TourDetail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourDetail_Update";
            string strErrorCodeDefault = "WA_Mst_TourDetail_Update";

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
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourDetail_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourDetail // RQ_Mst_District
                                           ////
                    , out objRT_Mst_TourDetail // RT_Mst_District
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
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourDetail>(objRT_Mst_TourDetail);
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
                if (objRT_Mst_TourDetail == null) objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourDetail>(ex, objRT_Mst_TourDetail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourDetail> WA_Mst_TourDetail_Delete(DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourDetail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourDetail.Tid);
            DA_RT_Mst_TourDetail objRT_Mst_TourDetail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourDetail_Delete";
            string strErrorCodeDefault = "WA_Mst_TourDetail_Delete";

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
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourDetail_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourDetail // RQ_Mst_District
                                           ////
                    , out objRT_Mst_TourDetail // RT_Mst_District
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
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourDetail>(objRT_Mst_TourDetail);
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
                if (objRT_Mst_TourDetail == null) objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourDetail>(ex, objRT_Mst_TourDetail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourDetail> WA_Mst_TourDetail_GetForView(DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourDetail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourDetail.Tid);
            DA_RT_Mst_TourDetail objRT_Mst_TourDetail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourDetail_Get";
            string strErrorCodeDefault = "WA_Mst_TourDetail_Get";

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
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourDetail_GetForView(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourDetail // RQ_Mst_District
                                           ////
                    , out objRT_Mst_TourDetail // RT_Mst_District
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
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourDetail>(objRT_Mst_TourDetail);
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
                if (objRT_Mst_TourDetail == null) objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourDetail>(ex, objRT_Mst_TourDetail);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourDetail> WA_Mst_TourDetail_GetForViewAll(DA_RQ_Mst_TourDetail objRQ_Mst_TourDetail)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourDetail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourDetail.Tid);
            DA_RT_Mst_TourDetail objRT_Mst_TourDetail = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourDetail_Get";
            string strErrorCodeDefault = "WA_Mst_TourDetail_Get";

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
                    , objRQ_Mst_TourDetail.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourDetail.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_TourDetail_GetForViewAll(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourDetail // RQ_Mst_District
                                           ////
                    , out objRT_Mst_TourDetail // RT_Mst_District
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
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourDetail>(objRT_Mst_TourDetail);
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
                if (objRT_Mst_TourDetail == null) objRT_Mst_TourDetail = new DA_RT_Mst_TourDetail();
                objRT_Mst_TourDetail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourDetail>(ex, objRT_Mst_TourDetail);
                #endregion
            }
        }
    }
}
