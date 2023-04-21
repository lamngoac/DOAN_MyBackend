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
    public class OS_PrdCenter_MstSpecController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Spec> WA_OS_PrdCenter_Mst_Spec_Get(RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Spec>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Spec.Tid);
            RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Spec_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Spec_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Spec.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Spec_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Spec // objRQ_Mst_Spec
                                     // //
                    , out objRT_OS_PrdCenter_Mst_Spec // RT_Mst_Spec
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
                objRT_OS_PrdCenter_Mst_Spec.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Spec>(objRT_OS_PrdCenter_Mst_Spec);
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
                if (objRT_OS_PrdCenter_Mst_Spec == null) objRT_OS_PrdCenter_Mst_Spec = new RT_OS_PrdCenter_Mst_Spec();
                objRT_OS_PrdCenter_Mst_Spec.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Spec>(ex, objRT_OS_PrdCenter_Mst_Spec);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Spec> WA_OS_PrdCenter_Mst_Spec_Add(RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Spec>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Spec.Tid);
            RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Spec_Add";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Spec_Add";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Spec.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Spec_Add(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Spec // objRQ_Mst_Spec
                                                  // //
                    , out objRT_OS_PrdCenter_Mst_Spec // RT_Mst_Spec
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
                objRT_OS_PrdCenter_Mst_Spec.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Spec>(objRT_OS_PrdCenter_Mst_Spec);
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
                if (objRT_OS_PrdCenter_Mst_Spec == null) objRT_OS_PrdCenter_Mst_Spec = new RT_OS_PrdCenter_Mst_Spec();
                objRT_OS_PrdCenter_Mst_Spec.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Spec>(ex, objRT_OS_PrdCenter_Mst_Spec);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Spec> WA_OS_PrdCenter_Mst_Spec_Upd(RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Spec>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Spec.Tid);
            RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Spec_Upd";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Spec_Upd";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Spec.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Spec_Upd(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Spec // objRQ_Mst_Spec
                                                  // //
                    , out objRT_OS_PrdCenter_Mst_Spec // RT_Mst_Spec
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
                objRT_OS_PrdCenter_Mst_Spec.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Spec>(objRT_OS_PrdCenter_Mst_Spec);
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
                if (objRT_OS_PrdCenter_Mst_Spec == null) objRT_OS_PrdCenter_Mst_Spec = new RT_OS_PrdCenter_Mst_Spec();
                objRT_OS_PrdCenter_Mst_Spec.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Spec>(ex, objRT_OS_PrdCenter_Mst_Spec);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Spec> WA_OS_PrdCenter_Mst_Spec_Del(RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Spec>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Spec.Tid);
            RT_OS_PrdCenter_Mst_Spec objRT_OS_PrdCenter_Mst_Spec = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Spec_Del";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Spec_Del";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Spec", TJson.JsonConvert.SerializeObject(objRQ_Mst_Spec)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Spec.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Spec.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Spec_Del(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Spec // objRQ_Mst_Spec
                                                  // //
                    , out objRT_OS_PrdCenter_Mst_Spec // RT_Mst_Spec
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
                objRT_OS_PrdCenter_Mst_Spec.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Spec>(objRT_OS_PrdCenter_Mst_Spec);
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
                if (objRT_OS_PrdCenter_Mst_Spec == null) objRT_OS_PrdCenter_Mst_Spec = new RT_OS_PrdCenter_Mst_Spec();
                objRT_OS_PrdCenter_Mst_Spec.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Spec>(ex, objRT_OS_PrdCenter_Mst_Spec);
                #endregion
            }
        }
    }
}
