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
    public class OS_PrdCenter_PrdProductIDController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Prd_ProductID> WA_OS_PrdCenter_Prd_ProductID_Get(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Prd_ProductID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Prd_ProductID.Tid);
            RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Prd_ProductID_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Prd_ProductID_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Prd_ProductID", TJson.JsonConvert.SerializeObject(objRQ_Prd_ProductID)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Prd_ProductID_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_ProductID // objRQ_Prd_ProductID
                                                       // //
                    , out objRT_OS_PrdCenter_Prd_ProductID // RT_Prd_ProductID
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
                objRT_OS_PrdCenter_Prd_ProductID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Prd_ProductID>(objRT_OS_PrdCenter_Prd_ProductID);
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
                if (objRT_OS_PrdCenter_Prd_ProductID == null) objRT_OS_PrdCenter_Prd_ProductID = new RT_OS_PrdCenter_Prd_ProductID();
                objRT_OS_PrdCenter_Prd_ProductID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Prd_ProductID>(ex, objRT_OS_PrdCenter_Prd_ProductID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Prd_ProductID> WA_OS_PrdCenter_Prd_ProductID_Create(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Prd_ProductID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Prd_ProductID.Tid);
            RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Prd_ProductID_Create";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Prd_ProductID_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Prd_ProductID", TJson.JsonConvert.SerializeObject(objRQ_Prd_ProductID)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Prd_ProductID_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_ProductID // objRQ_Prd_ProductID
                                                       // //
                    , out objRT_OS_PrdCenter_Prd_ProductID // RT_Prd_ProductID
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
                objRT_OS_PrdCenter_Prd_ProductID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Prd_ProductID>(objRT_OS_PrdCenter_Prd_ProductID);
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
                if (objRT_OS_PrdCenter_Prd_ProductID == null) objRT_OS_PrdCenter_Prd_ProductID = new RT_OS_PrdCenter_Prd_ProductID();
                objRT_OS_PrdCenter_Prd_ProductID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Prd_ProductID>(ex, objRT_OS_PrdCenter_Prd_ProductID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Prd_ProductID> WA_OS_PrdCenter_Prd_ProductID_Update(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Prd_ProductID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Prd_ProductID.Tid);
            RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Prd_ProductID_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Prd_ProductID_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Prd_ProductID", TJson.JsonConvert.SerializeObject(objRQ_Prd_ProductID)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Prd_ProductID_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_ProductID // objRQ_Prd_ProductID
                                                       // //
                    , out objRT_OS_PrdCenter_Prd_ProductID // RT_Prd_ProductID
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
                objRT_OS_PrdCenter_Prd_ProductID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Prd_ProductID>(objRT_OS_PrdCenter_Prd_ProductID);
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
                if (objRT_OS_PrdCenter_Prd_ProductID == null) objRT_OS_PrdCenter_Prd_ProductID = new RT_OS_PrdCenter_Prd_ProductID();
                objRT_OS_PrdCenter_Prd_ProductID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Prd_ProductID>(ex, objRT_OS_PrdCenter_Prd_ProductID);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Prd_ProductID> WA_OS_PrdCenter_Prd_ProductID_Delete(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Prd_ProductID>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Prd_ProductID.Tid);
            RT_OS_PrdCenter_Prd_ProductID objRT_OS_PrdCenter_Prd_ProductID = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Prd_ProductID_Delete";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Prd_ProductID_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Prd_ProductID", TJson.JsonConvert.SerializeObject(objRQ_Prd_ProductID)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_ProductID.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Prd_ProductID_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_ProductID // objRQ_Prd_ProductID
                                                       // //
                    , out objRT_OS_PrdCenter_Prd_ProductID // RT_Prd_ProductID
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
                objRT_OS_PrdCenter_Prd_ProductID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Prd_ProductID>(objRT_OS_PrdCenter_Prd_ProductID);
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
                if (objRT_OS_PrdCenter_Prd_ProductID == null) objRT_OS_PrdCenter_Prd_ProductID = new RT_OS_PrdCenter_Prd_ProductID();
                objRT_OS_PrdCenter_Prd_ProductID.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Prd_ProductID>(ex, objRT_OS_PrdCenter_Prd_ProductID);
                #endregion
            }
        }
    }
}
