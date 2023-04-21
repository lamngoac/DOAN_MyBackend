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
    public class OS_PrdCenter_MstCurrencyExController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_CurrencyEx> WA_OS_PrdCenter_Mst_CurrencyEx_Get(RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_CurrencyEx>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid);
            RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_CurrencyEx_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_CurrencyEx_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CurrencyEx", TJson.JsonConvert.SerializeObject(objRQ_Mst_CurrencyEx)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_CurrencyEx_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx // objRQ_Mst_CurrencyEx
                                                        // //
                    , out objRT_OS_PrdCenter_Mst_CurrencyEx // RT_Mst_CurrencyEx
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
                objRT_OS_PrdCenter_Mst_CurrencyEx.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_CurrencyEx>(objRT_OS_PrdCenter_Mst_CurrencyEx);
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
                if (objRT_OS_PrdCenter_Mst_CurrencyEx == null) objRT_OS_PrdCenter_Mst_CurrencyEx = new RT_OS_PrdCenter_Mst_CurrencyEx();
                objRT_OS_PrdCenter_Mst_CurrencyEx.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_CurrencyEx>(ex, objRT_OS_PrdCenter_Mst_CurrencyEx);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_CurrencyEx> WA_OS_PrdCenter_Mst_CurrencyEx_Create(RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_CurrencyEx>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid);
            RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_CurrencyEx_Create";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_CurrencyEx_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CurrencyEx", TJson.JsonConvert.SerializeObject(objRQ_Mst_CurrencyEx)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_CurrencyEx_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx // objRQ_Mst_CurrencyEx
                                                        // //
                    , out objRT_OS_PrdCenter_Mst_CurrencyEx // RT_Mst_CurrencyEx
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
                objRT_OS_PrdCenter_Mst_CurrencyEx.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_CurrencyEx>(objRT_OS_PrdCenter_Mst_CurrencyEx);
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
                if (objRT_OS_PrdCenter_Mst_CurrencyEx == null) objRT_OS_PrdCenter_Mst_CurrencyEx = new RT_OS_PrdCenter_Mst_CurrencyEx();
                objRT_OS_PrdCenter_Mst_CurrencyEx.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_CurrencyEx>(ex, objRT_OS_PrdCenter_Mst_CurrencyEx);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_CurrencyEx> WA_OS_PrdCenter_Mst_CurrencyEx_Update(RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_CurrencyEx>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid);
            RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_CurrencyEx_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_CurrencyEx_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CurrencyEx", TJson.JsonConvert.SerializeObject(objRQ_Mst_CurrencyEx)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_CurrencyEx_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx // objRQ_Mst_CurrencyEx
                                                        // //
                    , out objRT_OS_PrdCenter_Mst_CurrencyEx // RT_Mst_CurrencyEx
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
                objRT_OS_PrdCenter_Mst_CurrencyEx.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_CurrencyEx>(objRT_OS_PrdCenter_Mst_CurrencyEx);
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
                if (objRT_OS_PrdCenter_Mst_CurrencyEx == null) objRT_OS_PrdCenter_Mst_CurrencyEx = new RT_OS_PrdCenter_Mst_CurrencyEx();
                objRT_OS_PrdCenter_Mst_CurrencyEx.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_CurrencyEx>(ex, objRT_OS_PrdCenter_Mst_CurrencyEx);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_CurrencyEx> WA_OS_PrdCenter_Mst_CurrencyEx_Delete(RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_CurrencyEx>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_CurrencyEx.Tid);
            RT_OS_PrdCenter_Mst_CurrencyEx objRT_OS_PrdCenter_Mst_CurrencyEx = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_CurrencyEx_Delete";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_CurrencyEx_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CurrencyEx", TJson.JsonConvert.SerializeObject(objRQ_Mst_CurrencyEx)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_CurrencyEx_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_CurrencyEx // objRQ_Mst_CurrencyEx
                                                        // //
                    , out objRT_OS_PrdCenter_Mst_CurrencyEx // RT_Mst_CurrencyEx
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
                objRT_OS_PrdCenter_Mst_CurrencyEx.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_CurrencyEx>(objRT_OS_PrdCenter_Mst_CurrencyEx);
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
                if (objRT_OS_PrdCenter_Mst_CurrencyEx == null) objRT_OS_PrdCenter_Mst_CurrencyEx = new RT_OS_PrdCenter_Mst_CurrencyEx();
                objRT_OS_PrdCenter_Mst_CurrencyEx.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_CurrencyEx>(ex, objRT_OS_PrdCenter_Mst_CurrencyEx);
                #endregion
            }
        }
    }
}
