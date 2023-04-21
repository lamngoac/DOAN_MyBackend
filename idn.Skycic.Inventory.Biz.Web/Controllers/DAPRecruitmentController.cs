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
    public class DAPRecruitmentController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_Recruitment> WA_POW_Recruitment_Get(DA_RQ_POW_Recruitment objRQ_POW_Recruitment)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_Recruitment>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_Recruitment.Tid);
            DA_RT_POW_Recruitment objRT_POW_Recruitment = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_Recruitment_Get";
            string strErrorCodeDefault = "WA_POW_Recruitment_Get";

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
                    , objRQ_POW_Recruitment.GwUserCode // strGwUserCode
                    , objRQ_POW_Recruitment.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_Recruitment_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Recruitment // RQ_Mst_District
                                            ////
                    , out objRT_POW_Recruitment // RT_Mst_District
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
                objRT_POW_Recruitment.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_Recruitment>(objRT_POW_Recruitment);
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
                if (objRT_POW_Recruitment == null) objRT_POW_Recruitment = new DA_RT_POW_Recruitment();
                objRT_POW_Recruitment.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_Recruitment>(ex, objRT_POW_Recruitment);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_Recruitment> WA_POW_Recruitment_Create(DA_RQ_POW_Recruitment objRQ_POW_Recruitment)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_Recruitment>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_Recruitment.Tid);
            DA_RT_POW_Recruitment objRT_POW_Recruitment = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_Recruitment_Create";
            string strErrorCodeDefault = "WA_POW_Recruitment_Create";

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
                    , objRQ_POW_Recruitment.GwUserCode // strGwUserCode
                    , objRQ_POW_Recruitment.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_Recruitment_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Recruitment // RQ_Mst_District
                                            ////
                    , out objRT_POW_Recruitment // RT_Mst_District
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
                objRT_POW_Recruitment.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_Recruitment>(objRT_POW_Recruitment);
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
                if (objRT_POW_Recruitment == null) objRT_POW_Recruitment = new DA_RT_POW_Recruitment();
                objRT_POW_Recruitment.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_Recruitment>(ex, objRT_POW_Recruitment);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_Recruitment> WA_POW_Recruitment_Update(DA_RQ_POW_Recruitment objRQ_POW_Recruitment)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_Recruitment>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_Recruitment.Tid);
            DA_RT_POW_Recruitment objRT_POW_Recruitment = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_Recruitment_Update";
            string strErrorCodeDefault = "WA_POW_Recruitment_Update";

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
                    , objRQ_POW_Recruitment.GwUserCode // strGwUserCode
                    , objRQ_POW_Recruitment.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_Recruitment_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Recruitment // RQ_Mst_District
                                            ////
                    , out objRT_POW_Recruitment // RT_Mst_District
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
                objRT_POW_Recruitment.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_Recruitment>(objRT_POW_Recruitment);
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
                if (objRT_POW_Recruitment == null) objRT_POW_Recruitment = new DA_RT_POW_Recruitment();
                objRT_POW_Recruitment.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_Recruitment>(ex, objRT_POW_Recruitment);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_Recruitment> WA_POW_Recruitment_Delete(DA_RQ_POW_Recruitment objRQ_POW_Recruitment)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_Recruitment>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_Recruitment.Tid);
            DA_RT_POW_Recruitment objRT_POW_Recruitment = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_Recruitment_Delete";
            string strErrorCodeDefault = "WA_POW_Recruitment_Delete";

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
                    , objRQ_POW_Recruitment.GwUserCode // strGwUserCode
                    , objRQ_POW_Recruitment.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_Recruitment_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_Recruitment // RQ_Mst_District
                                            ////
                    , out objRT_POW_Recruitment // RT_Mst_District
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
                objRT_POW_Recruitment.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_Recruitment>(objRT_POW_Recruitment);
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
                if (objRT_POW_Recruitment == null) objRT_POW_Recruitment = new DA_RT_POW_Recruitment();
                objRT_POW_Recruitment.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_Recruitment>(ex, objRT_POW_Recruitment);
                #endregion
            }
        }
    }
}
