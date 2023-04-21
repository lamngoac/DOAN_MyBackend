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
    public class MstPartMaterialTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartMaterialType> WA_Mst_PartMaterialType_Get(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartMaterialType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartMaterialType.Tid);
            RT_Mst_PartMaterialType objRT_Mst_PartMaterialType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartMaterialType_Get";
            string strErrorCodeDefault = "WA_Mst_PartMaterialType_Get";

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
                    , objRQ_Mst_PartMaterialType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartMaterialType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartMaterialType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartMaterialType // RQ_Mst_PartMaterialType
                                         ////
                    , out objRT_Mst_PartMaterialType // RT_Mst_PartMaterialType
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
                objRT_Mst_PartMaterialType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartMaterialType>(objRT_Mst_PartMaterialType);
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
                if (objRT_Mst_PartMaterialType == null) objRT_Mst_PartMaterialType = new RT_Mst_PartMaterialType();
                objRT_Mst_PartMaterialType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartMaterialType>(ex, objRT_Mst_PartMaterialType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartMaterialType> WA_Mst_PartMaterialType_Create(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartMaterialType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartMaterialType.Tid);
            RT_Mst_PartMaterialType objRT_Mst_PartMaterialType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartMaterialType_Create";
            string strErrorCodeDefault = "WA_Mst_PartMaterialType_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartMaterialType", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartMaterialType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartMaterialType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartMaterialType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartMaterialType_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartMaterialType // objRQ_Mst_PartMaterialType
                                         ////
                    , out objRT_Mst_PartMaterialType // RT_Mst_PartMaterialType
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
                objRT_Mst_PartMaterialType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartMaterialType>(objRT_Mst_PartMaterialType);
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
                if (objRT_Mst_PartMaterialType == null) objRT_Mst_PartMaterialType = new RT_Mst_PartMaterialType();
                objRT_Mst_PartMaterialType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartMaterialType>(ex, objRT_Mst_PartMaterialType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartMaterialType> WA_Mst_PartMaterialType_Update(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartMaterialType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartMaterialType.Tid);
            RT_Mst_PartMaterialType objRT_Mst_PartMaterialType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartMaterialType_Update";
            string strErrorCodeDefault = "WA_Mst_PartMaterialType_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartMaterialType", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartMaterialType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartMaterialType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartMaterialType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartMaterialType_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartMaterialType // objRQ_Mst_PartMaterialType
                                         ////
                    , out objRT_Mst_PartMaterialType // objRT_Mst_PartMaterialType
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
                objRT_Mst_PartMaterialType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartMaterialType>(objRT_Mst_PartMaterialType);
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
                if (objRT_Mst_PartMaterialType == null) objRT_Mst_PartMaterialType = new RT_Mst_PartMaterialType();
                objRT_Mst_PartMaterialType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartMaterialType>(ex, objRT_Mst_PartMaterialType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartMaterialType> WA_Mst_PartMaterialType_Delete(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartMaterialType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartMaterialType.Tid);
            RT_Mst_PartMaterialType objRT_Mst_PartMaterialType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartMaterialType_Delete";
            string strErrorCodeDefault = "WA_Mst_PartMaterialType_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartMaterialType", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartMaterialType)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartMaterialType.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartMaterialType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartMaterialType_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartMaterialType // objRQ_Mst_PartMaterialType
                                         ////
                    , out objRT_Mst_PartMaterialType // objRT_Mst_PartMaterialType
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
                objRT_Mst_PartMaterialType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartMaterialType>(objRT_Mst_PartMaterialType);
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
                if (objRT_Mst_PartMaterialType == null) objRT_Mst_PartMaterialType = new RT_Mst_PartMaterialType();
                objRT_Mst_PartMaterialType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartMaterialType>(ex, objRT_Mst_PartMaterialType);
                #endregion
            }
        }
    }
}
