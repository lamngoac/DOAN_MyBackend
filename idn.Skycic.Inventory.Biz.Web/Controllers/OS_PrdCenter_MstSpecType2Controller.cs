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
    public class OS_PrdCenter_MstSpecType2Controller : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecType2> WA_OS_PrdCenter_Mst_SpecType2_Get(RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecType2>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecType2.Tid);
            RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecType2_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecType2_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecType2", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecType2)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecType2_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType2 // objRQ_Mst_SpecType2
                                                     // //
                    , out objRT_OS_PrdCenter_Mst_SpecType2 // RT_Mst_SpecType2
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
                objRT_OS_PrdCenter_Mst_SpecType2.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecType2>(objRT_OS_PrdCenter_Mst_SpecType2);
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
                if (objRT_OS_PrdCenter_Mst_SpecType2 == null) objRT_OS_PrdCenter_Mst_SpecType2 = new RT_OS_PrdCenter_Mst_SpecType2();
                objRT_OS_PrdCenter_Mst_SpecType2.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecType2>(ex, objRT_OS_PrdCenter_Mst_SpecType2);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecType2> WA_OS_PrdCenter_Mst_SpecType2_Create(RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecType2>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecType2.Tid);
            RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecType2_Create";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecType2_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecType2", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecType2)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecType2_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType2 // objRQ_Mst_SpecType2
                                                     // //
                    , out objRT_OS_PrdCenter_Mst_SpecType2 // RT_Mst_SpecType2
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
                objRT_OS_PrdCenter_Mst_SpecType2.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecType2>(objRT_OS_PrdCenter_Mst_SpecType2);
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
                if (objRT_OS_PrdCenter_Mst_SpecType2 == null) objRT_OS_PrdCenter_Mst_SpecType2 = new RT_OS_PrdCenter_Mst_SpecType2();
                objRT_OS_PrdCenter_Mst_SpecType2.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecType2>(ex, objRT_OS_PrdCenter_Mst_SpecType2);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecType2> WA_OS_PrdCenter_Mst_SpecType2_Update(RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecType2>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecType2.Tid);
            RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecType2_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecType2_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecType2", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecType2)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecType2_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType2 // objRQ_Mst_SpecType2
                                                     // //
                    , out objRT_OS_PrdCenter_Mst_SpecType2 // RT_Mst_SpecType2
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
                objRT_OS_PrdCenter_Mst_SpecType2.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecType2>(objRT_OS_PrdCenter_Mst_SpecType2);
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
                if (objRT_OS_PrdCenter_Mst_SpecType2 == null) objRT_OS_PrdCenter_Mst_SpecType2 = new RT_OS_PrdCenter_Mst_SpecType2();
                objRT_OS_PrdCenter_Mst_SpecType2.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecType2>(ex, objRT_OS_PrdCenter_Mst_SpecType2);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecType2> WA_OS_PrdCenter_Mst_SpecType2_Delete(RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecType2>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecType2.Tid);
            RT_OS_PrdCenter_Mst_SpecType2 objRT_OS_PrdCenter_Mst_SpecType2 = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecType2_Delete";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecType2_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecType2", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecType2)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType2.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecType2_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType2 // objRQ_Mst_SpecType2
                                                     // //
                    , out objRT_OS_PrdCenter_Mst_SpecType2 // RT_Mst_SpecType2
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
                objRT_OS_PrdCenter_Mst_SpecType2.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecType2>(objRT_OS_PrdCenter_Mst_SpecType2);
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
                if (objRT_OS_PrdCenter_Mst_SpecType2 == null) objRT_OS_PrdCenter_Mst_SpecType2 = new RT_OS_PrdCenter_Mst_SpecType2();
                objRT_OS_PrdCenter_Mst_SpecType2.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecType2>(ex, objRT_OS_PrdCenter_Mst_SpecType2);
                #endregion
            }
        }
    }
}
