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
    public class DAPNewsNewsController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_NewsNews> WA_POW_NewsNews_Get(DA_RQ_POW_NewsNews objRQ_POW_NewsNews)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_NewsNews>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_NewsNews.Tid);
            DA_RT_POW_NewsNews objRT_POW_NewsNews = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_NewsNews_Get";
            string strErrorCodeDefault = "WA_POW_NewsNews_Get";

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
                    , objRQ_POW_NewsNews.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsNews.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_NewsNews_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_NewsNews // RQ_Mst_District
                                         ////
                    , out objRT_POW_NewsNews // RT_Mst_District
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
                objRT_POW_NewsNews.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_NewsNews>(objRT_POW_NewsNews);
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
                if (objRT_POW_NewsNews == null) objRT_POW_NewsNews = new DA_RT_POW_NewsNews();
                objRT_POW_NewsNews.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_NewsNews>(ex, objRT_POW_NewsNews);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_NewsNews> WA_POW_NewsNews_Create(DA_RQ_POW_NewsNews objRQ_POW_NewsNews)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_NewsNews>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_NewsNews.Tid);
            DA_RT_POW_NewsNews objRT_POW_NewsNews = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_NewsNews_Create";
            string strErrorCodeDefault = "WA_POW_NewsNews_Create";

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
                    , objRQ_POW_NewsNews.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsNews.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_NewsNews_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_NewsNews // RQ_Mst_District
                                         ////
                    , out objRT_POW_NewsNews // RT_Mst_District
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
                objRT_POW_NewsNews.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_NewsNews>(objRT_POW_NewsNews);
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
                if (objRT_POW_NewsNews == null) objRT_POW_NewsNews = new DA_RT_POW_NewsNews();
                objRT_POW_NewsNews.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_NewsNews>(ex, objRT_POW_NewsNews);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_NewsNews> WA_POW_NewsNews_Update(DA_RQ_POW_NewsNews objRQ_POW_NewsNews)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_NewsNews>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_NewsNews.Tid);
            DA_RT_POW_NewsNews objRT_POW_NewsNews = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_NewsNews_Update";
            string strErrorCodeDefault = "WA_POW_NewsNews_Update";

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
                    , objRQ_POW_NewsNews.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsNews.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_NewsNews_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_NewsNews // RQ_Mst_District
                                         ////
                    , out objRT_POW_NewsNews // RT_Mst_District
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
                objRT_POW_NewsNews.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_NewsNews>(objRT_POW_NewsNews);
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
                if (objRT_POW_NewsNews == null) objRT_POW_NewsNews = new DA_RT_POW_NewsNews();
                objRT_POW_NewsNews.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_NewsNews>(ex, objRT_POW_NewsNews);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_NewsNews> WA_POW_NewsNews_Delete(DA_RQ_POW_NewsNews objRQ_POW_NewsNews)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_NewsNews>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_NewsNews.Tid);
            DA_RT_POW_NewsNews objRT_POW_NewsNews = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_NewsNews_Delete";
            string strErrorCodeDefault = "WA_POW_NewsNews_Delete";

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
                    , objRQ_POW_NewsNews.GwUserCode // strGwUserCode
                    , objRQ_POW_NewsNews.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_NewsNews_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_NewsNews // RQ_Mst_District
                                         ////
                    , out objRT_POW_NewsNews // RT_Mst_District
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
                objRT_POW_NewsNews.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_NewsNews>(objRT_POW_NewsNews);
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
                if (objRT_POW_NewsNews == null) objRT_POW_NewsNews = new DA_RT_POW_NewsNews();
                objRT_POW_NewsNews.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_NewsNews>(ex, objRT_POW_NewsNews);
                #endregion
            }
        }
    }
}
