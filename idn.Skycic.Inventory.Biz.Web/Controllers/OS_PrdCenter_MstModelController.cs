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
    public class OS_PrdCenter_MstModelController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Model> WA_OS_PrdCenter_Mst_Model_Get(RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Model>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Model.Tid);
            RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Model_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Model_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Model", TJson.JsonConvert.SerializeObject(objRQ_Mst_Model)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Model.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Model_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Model // objRQ_Mst_Model
                                                   // //
                    , out objRT_OS_PrdCenter_Mst_Model // RT_Mst_Model
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
                objRT_OS_PrdCenter_Mst_Model.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Model>(objRT_OS_PrdCenter_Mst_Model);
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
                if (objRT_OS_PrdCenter_Mst_Model == null) objRT_OS_PrdCenter_Mst_Model = new RT_OS_PrdCenter_Mst_Model();
                objRT_OS_PrdCenter_Mst_Model.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Model>(ex, objRT_OS_PrdCenter_Mst_Model);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Model> WA_OS_PrdCenter_Mst_Model_Create(RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Model>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Model.Tid);
            RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Model_Create";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Model_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Model", TJson.JsonConvert.SerializeObject(objRQ_Mst_Model)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Model.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Model_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Model // objRQ_Mst_Model
                                                   // //
                    , out objRT_OS_PrdCenter_Mst_Model // RT_Mst_Model
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
                objRT_OS_PrdCenter_Mst_Model.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Model>(objRT_OS_PrdCenter_Mst_Model);
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
                if (objRT_OS_PrdCenter_Mst_Model == null) objRT_OS_PrdCenter_Mst_Model = new RT_OS_PrdCenter_Mst_Model();
                objRT_OS_PrdCenter_Mst_Model.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Model>(ex, objRT_OS_PrdCenter_Mst_Model);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Model> WA_OS_PrdCenter_Mst_Model_Update(RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Model>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Model.Tid);
            RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Model_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Model_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Model", TJson.JsonConvert.SerializeObject(objRQ_Mst_Model)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Model.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Model_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Model // objRQ_Mst_Model
                                                   // //
                    , out objRT_OS_PrdCenter_Mst_Model // RT_Mst_Model
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
                objRT_OS_PrdCenter_Mst_Model.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Model>(objRT_OS_PrdCenter_Mst_Model);
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
                if (objRT_OS_PrdCenter_Mst_Model == null) objRT_OS_PrdCenter_Mst_Model = new RT_OS_PrdCenter_Mst_Model();
                objRT_OS_PrdCenter_Mst_Model.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Model>(ex, objRT_OS_PrdCenter_Mst_Model);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Model> WA_OS_PrdCenter_Mst_Model_Delete(RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Model>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Model.Tid);
            RT_OS_PrdCenter_Mst_Model objRT_OS_PrdCenter_Mst_Model = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Model_Delete";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Model_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Model", TJson.JsonConvert.SerializeObject(objRQ_Mst_Model)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Model.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Model.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Model_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Model // objRQ_Mst_Model
                                                   // //
                    , out objRT_OS_PrdCenter_Mst_Model // RT_Mst_Model
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
                objRT_OS_PrdCenter_Mst_Model.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Model>(objRT_OS_PrdCenter_Mst_Model);
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
                if (objRT_OS_PrdCenter_Mst_Model == null) objRT_OS_PrdCenter_Mst_Model = new RT_OS_PrdCenter_Mst_Model();
                objRT_OS_PrdCenter_Mst_Model.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Model>(ex, objRT_OS_PrdCenter_Mst_Model);
                #endregion
            }
        }
    }
}
