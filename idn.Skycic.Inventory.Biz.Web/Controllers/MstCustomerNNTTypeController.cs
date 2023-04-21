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
    public class MstCustomerNNTTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerNNTType> WA_Mst_CustomerNNTType_Get(RQ_Mst_CustomerNNTType objRQ_Mst_CustomerNNTType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_CustomerNNTType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerNNTType.Tid);
            RT_Mst_CustomerNNTType objRT_Mst_CustomerNNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerNNTType_Get";
            string strErrorCodeDefault = "WA_Mst_CustomerNNTType_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Currency", TJson.JsonConvert.SerializeObject(objRQ_Mst_Currency)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerNNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerNNTType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNTType // RQ_Mst_CustomerNNTType
                                                ////
                    , out objRT_Mst_CustomerNNTType // RT_Mst_CustomerNNTType
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
                objRT_Mst_CustomerNNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerNNTType>(objRT_Mst_CustomerNNTType);
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
                if (objRT_Mst_CustomerNNTType == null) objRT_Mst_CustomerNNTType = new RT_Mst_CustomerNNTType();
                objRT_Mst_CustomerNNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerNNTType>(ex, objRT_Mst_CustomerNNTType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerNNTType> WA_Mst_CustomerNNTType_Create(RQ_Mst_CustomerNNTType objRQ_Mst_CustomerNNTType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_CustomerNNTType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerNNTType.Tid);
            RT_Mst_CustomerNNTType objRT_Mst_CustomerNNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerNNTType_Create";
            string strErrorCodeDefault = "WA_Mst_CustomerNNTType_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerNNTType", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerNNTType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerNNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerNNTType_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNTType // objRQ_Mst_CustomerNNTType
                                                ////
                    , out objRT_Mst_CustomerNNTType // RT_Mst_CustomerNNTType
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
                objRT_Mst_CustomerNNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerNNTType>(objRT_Mst_CustomerNNTType);
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
                if (objRT_Mst_CustomerNNTType == null) objRT_Mst_CustomerNNTType = new RT_Mst_CustomerNNTType();
                objRT_Mst_CustomerNNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerNNTType>(ex, objRT_Mst_CustomerNNTType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerNNTType> WA_Mst_CustomerNNTType_Update(RQ_Mst_CustomerNNTType objRQ_Mst_CustomerNNTType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_CustomerNNTType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerNNTType.Tid);
            RT_Mst_CustomerNNTType objRT_Mst_CustomerNNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerNNTType_Update";
            string strErrorCodeDefault = "WA_Mst_CustomerNNTType_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerNNTType", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerNNTType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerNNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerNNTType_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNTType // objRQ_Mst_CustomerNNTType
                                                ////
                    , out objRT_Mst_CustomerNNTType // objRT_Mst_CustomerNNTType
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
                objRT_Mst_CustomerNNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerNNTType>(objRT_Mst_CustomerNNTType);
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
                if (objRT_Mst_CustomerNNTType == null) objRT_Mst_CustomerNNTType = new RT_Mst_CustomerNNTType();
                objRT_Mst_CustomerNNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerNNTType>(ex, objRT_Mst_CustomerNNTType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerNNTType> WA_Mst_CustomerNNTType_Delete(RQ_Mst_CustomerNNTType objRQ_Mst_CustomerNNTType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_CustomerNNTType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerNNTType.Tid);
            RT_Mst_CustomerNNTType objRT_Mst_CustomerNNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerNNTType_Delete";
            string strErrorCodeDefault = "WA_Mst_CustomerNNTType_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerNNTType", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerNNTType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerNNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerNNTType_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNTType // objRQ_Mst_CustomerNNTType
                                                ////
                    , out objRT_Mst_CustomerNNTType // objRT_Mst_CustomerNNTType
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
                objRT_Mst_CustomerNNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerNNTType>(objRT_Mst_CustomerNNTType);
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
                if (objRT_Mst_CustomerNNTType == null) objRT_Mst_CustomerNNTType = new RT_Mst_CustomerNNTType();
                objRT_Mst_CustomerNNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerNNTType>(ex, objRT_Mst_CustomerNNTType);
                #endregion
            }
        }
    }
}
