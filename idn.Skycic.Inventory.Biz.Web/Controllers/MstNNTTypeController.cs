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
    public class MstNNTTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNTType> WA_Mst_NNTType_Get(RQ_Mst_NNTType objRQ_Mst_NNTType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNTType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNTType.Tid);
            RT_Mst_NNTType objRT_Mst_NNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNTType_Get";
            string strErrorCodeDefault = "WA_Mst_NNTType_Get";

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
                    , objRQ_Mst_NNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNTType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNTType // RQ_Mst_NNTType
                                       ////
                    , out objRT_Mst_NNTType // RT_Mst_NNTType
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
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNTType>(objRT_Mst_NNTType);
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
                if (objRT_Mst_NNTType == null) objRT_Mst_NNTType = new RT_Mst_NNTType();
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNTType>(ex, objRT_Mst_NNTType);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNTType> WA_RptSv_Mst_NNTType_Get(RQ_Mst_NNTType objRQ_Mst_NNTType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNTType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNTType.Tid);
            RT_Mst_NNTType objRT_Mst_NNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_NNTType_Get";
            string strErrorCodeDefault = "WA_RptSv_Mst_NNTType_Get";

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
                    , objRQ_Mst_NNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_NNTType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNTType // RQ_Mst_NNTType
                                        ////
                    , out objRT_Mst_NNTType // RT_Mst_NNTType
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
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNTType>(objRT_Mst_NNTType);
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
                if (objRT_Mst_NNTType == null) objRT_Mst_NNTType = new RT_Mst_NNTType();
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNTType>(ex, objRT_Mst_NNTType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNTType> WA_Mst_NNTType_Create(RQ_Mst_NNTType objRQ_Mst_NNTType)
        {
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNTType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNTType.Tid);
            RT_Mst_NNTType objRT_Mst_NNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNTType_Create";
            string strErrorCodeDefault = "WA_Mst_NNTType_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_NNTType", TJson.JsonConvert.SerializeObject(objRQ_Mst_NNTType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNTType_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNTType // objRQ_Mst_NNTType
                                       ////
                    , out objRT_Mst_NNTType // RT_Mst_NNTType
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
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNTType>(objRT_Mst_NNTType);
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
                if (objRT_Mst_NNTType == null) objRT_Mst_NNTType = new RT_Mst_NNTType();
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNTType>(ex, objRT_Mst_NNTType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNTType> WA_Mst_NNTType_Update(RQ_Mst_NNTType objRQ_Mst_NNTType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNTType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNTType.Tid);
            RT_Mst_NNTType objRT_Mst_NNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNTType_Update";
            string strErrorCodeDefault = "WA_Mst_NNTType_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_NNTType", TJson.JsonConvert.SerializeObject(objRQ_Mst_NNTType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNTType_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNTType // objRQ_Mst_NNTType
                                       ////
                    , out objRT_Mst_NNTType // objRT_Mst_NNTType
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
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNTType>(objRT_Mst_NNTType);
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
                if (objRT_Mst_NNTType == null) objRT_Mst_NNTType = new RT_Mst_NNTType();
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNTType>(ex, objRT_Mst_NNTType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNTType> WA_Mst_NNTType_Delete(RQ_Mst_NNTType objRQ_Mst_NNTType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNTType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNTType.Tid);
            RT_Mst_NNTType objRT_Mst_NNTType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNTType_Delete";
            string strErrorCodeDefault = "WA_Mst_NNTType_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_NNTType", TJson.JsonConvert.SerializeObject(objRQ_Mst_NNTType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNTType.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNTType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNTType_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNTType // objRQ_Mst_NNTType
                                       ////
                    , out objRT_Mst_NNTType // objRT_Mst_NNTType
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
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNTType>(objRT_Mst_NNTType);
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
                if (objRT_Mst_NNTType == null) objRT_Mst_NNTType = new RT_Mst_NNTType();
                objRT_Mst_NNTType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNTType>(ex, objRT_Mst_NNTType);
                #endregion
            }
        }
    }
}
