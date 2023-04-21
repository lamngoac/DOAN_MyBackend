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
    public class OS_PrdCenter_MstSpecPriceController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecPrice> WA_OS_PrdCenter_Mst_SpecPrice_Get(RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecPrice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecPrice.Tid);
            RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecPrice_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecPrice_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecPrice", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecPrice)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecPrice_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecPrice // objRQ_Mst_SpecPrice
                                          // //
                    , out objRT_OS_PrdCenter_Mst_SpecPrice // objRT_OS_PrdCenter_Mst_SpecPrice
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
                objRT_OS_PrdCenter_Mst_SpecPrice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecPrice>(objRT_OS_PrdCenter_Mst_SpecPrice);
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
                if (objRT_OS_PrdCenter_Mst_SpecPrice == null) objRT_OS_PrdCenter_Mst_SpecPrice = new RT_OS_PrdCenter_Mst_SpecPrice();
                objRT_OS_PrdCenter_Mst_SpecPrice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecPrice>(ex, objRT_OS_PrdCenter_Mst_SpecPrice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecPrice> WA_OS_PrdCenter_Mst_SpecPrice_Create(RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecPrice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecPrice.Tid);
            RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecPrice_Create";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecPrice_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecPrice", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecPrice)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecPrice_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecPrice // objRQ_Mst_SpecPrice
                                                       // //
                    , out objRT_OS_PrdCenter_Mst_SpecPrice // objRT_OS_PrdCenter_Mst_SpecPrice
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
                objRT_OS_PrdCenter_Mst_SpecPrice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecPrice>(objRT_OS_PrdCenter_Mst_SpecPrice);
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
                if (objRT_OS_PrdCenter_Mst_SpecPrice == null) objRT_OS_PrdCenter_Mst_SpecPrice = new RT_OS_PrdCenter_Mst_SpecPrice();
                objRT_OS_PrdCenter_Mst_SpecPrice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecPrice>(ex, objRT_OS_PrdCenter_Mst_SpecPrice);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecPrice> WA_OS_PrdCenter_Mst_SpecPrice_Update(RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecPrice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecPrice.Tid);
            RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecPrice_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecPrice_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecPrice", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecPrice)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecPrice_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecPrice // objRQ_Mst_SpecPrice
                                                       // //
                    , out objRT_OS_PrdCenter_Mst_SpecPrice // objRT_OS_PrdCenter_Mst_SpecPrice
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
                objRT_OS_PrdCenter_Mst_SpecPrice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecPrice>(objRT_OS_PrdCenter_Mst_SpecPrice);
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
                if (objRT_OS_PrdCenter_Mst_SpecPrice == null) objRT_OS_PrdCenter_Mst_SpecPrice = new RT_OS_PrdCenter_Mst_SpecPrice();
                objRT_OS_PrdCenter_Mst_SpecPrice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecPrice>(ex, objRT_OS_PrdCenter_Mst_SpecPrice);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecPrice> WA_OS_PrdCenter_Mst_SpecPrice_Delete(RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecPrice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecPrice.Tid);
            RT_OS_PrdCenter_Mst_SpecPrice objRT_OS_PrdCenter_Mst_SpecPrice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecPrice_Delete";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecPrice_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecPrice", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecPrice)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecPrice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecPrice_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecPrice // objRQ_Mst_SpecPrice
                                                       // //
                    , out objRT_OS_PrdCenter_Mst_SpecPrice // objRT_OS_PrdCenter_Mst_SpecPrice
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
                objRT_OS_PrdCenter_Mst_SpecPrice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecPrice>(objRT_OS_PrdCenter_Mst_SpecPrice);
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
                if (objRT_OS_PrdCenter_Mst_SpecPrice == null) objRT_OS_PrdCenter_Mst_SpecPrice = new RT_OS_PrdCenter_Mst_SpecPrice();
                objRT_OS_PrdCenter_Mst_SpecPrice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecPrice>(ex, objRT_OS_PrdCenter_Mst_SpecPrice);
                #endregion
            }
        }
    }
}
