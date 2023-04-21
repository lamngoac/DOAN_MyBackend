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
    public class MstBOMTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_BOMType> WA_Mst_BOMType_Get(RQ_Mst_BOMType objRQ_Mst_BOMType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_BOMType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_BOMType.Tid);
            RT_Mst_BOMType objRT_Mst_BOMType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_BOMType_Get";
            string strErrorCodeDefault = "WA_Mst_BOMType_Get";

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
                    , objRQ_Mst_BOMType.GwUserCode // strGwUserCode
                    , objRQ_Mst_BOMType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_BOMType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_BOMType // RQ_Mst_BOMType
                                        ////
                    , out objRT_Mst_BOMType // RT_Mst_BOMType
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
                objRT_Mst_BOMType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_BOMType>(objRT_Mst_BOMType);
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
                if (objRT_Mst_BOMType == null) objRT_Mst_BOMType = new RT_Mst_BOMType();
                objRT_Mst_BOMType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_BOMType>(ex, objRT_Mst_BOMType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_BOMType> WA_Mst_BOMType_Create(RQ_Mst_BOMType objRQ_Mst_BOMType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_BOMType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_BOMType.Tid);
            RT_Mst_BOMType objRT_Mst_BOMType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_BOMType_Create";
            string strErrorCodeDefault = "WA_Mst_BOMType_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_BOMType", TJson.JsonConvert.SerializeObject(objRQ_Mst_BOMType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_BOMType.GwUserCode // strGwUserCode
                    , objRQ_Mst_BOMType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_BOMType_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_BOMType // objRQ_Mst_BOMType
                                        ////
                    , out objRT_Mst_BOMType // RT_Mst_BOMType
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
                objRT_Mst_BOMType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_BOMType>(objRT_Mst_BOMType);
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
                if (objRT_Mst_BOMType == null) objRT_Mst_BOMType = new RT_Mst_BOMType();
                objRT_Mst_BOMType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_BOMType>(ex, objRT_Mst_BOMType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_BOMType> WA_Mst_BOMType_Update(RQ_Mst_BOMType objRQ_Mst_BOMType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_BOMType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_BOMType.Tid);
            RT_Mst_BOMType objRT_Mst_BOMType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_BOMType_Update";
            string strErrorCodeDefault = "WA_Mst_BOMType_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_BOMType", TJson.JsonConvert.SerializeObject(objRQ_Mst_BOMType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_BOMType.GwUserCode // strGwUserCode
                    , objRQ_Mst_BOMType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_BOMType_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_BOMType // objRQ_Mst_BOMType
                                        ////
                    , out objRT_Mst_BOMType // objRT_Mst_BOMType
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
                objRT_Mst_BOMType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_BOMType>(objRT_Mst_BOMType);
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
                if (objRT_Mst_BOMType == null) objRT_Mst_BOMType = new RT_Mst_BOMType();
                objRT_Mst_BOMType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_BOMType>(ex, objRT_Mst_BOMType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_BOMType> WA_Mst_BOMType_Delete(RQ_Mst_BOMType objRQ_Mst_BOMType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_BOMType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_BOMType.Tid);
            RT_Mst_BOMType objRT_Mst_BOMType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_BOMType_Delete";
            string strErrorCodeDefault = "WA_Mst_BOMType_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_BOMType", TJson.JsonConvert.SerializeObject(objRQ_Mst_BOMType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_BOMType.GwUserCode // strGwUserCode
                    , objRQ_Mst_BOMType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_BOMType_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_BOMType // objRQ_Mst_BOMType
                                        ////
                    , out objRT_Mst_BOMType // objRT_Mst_BOMType
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
                objRT_Mst_BOMType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_BOMType>(objRT_Mst_BOMType);
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
                if (objRT_Mst_BOMType == null) objRT_Mst_BOMType = new RT_Mst_BOMType();
                objRT_Mst_BOMType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_BOMType>(ex, objRT_Mst_BOMType);
                #endregion
            }
        }
    }
}
