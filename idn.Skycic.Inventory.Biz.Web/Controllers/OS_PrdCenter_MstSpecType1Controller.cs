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
    public class OS_PrdCenter_MstSpecType1Controller : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecType1> WA_OS_PrdCenter_Mst_SpecType1_Get(RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecType1>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecType1.Tid);
            RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecType1_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecType1_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecType1", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecType1)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecType1_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType1 // objRQ_Mst_SpecType1
                                                     // //
                    , out objRT_OS_PrdCenter_Mst_SpecType1 // RT_Mst_SpecType1
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
                objRT_OS_PrdCenter_Mst_SpecType1.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecType1>(objRT_OS_PrdCenter_Mst_SpecType1);
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
                if (objRT_OS_PrdCenter_Mst_SpecType1 == null) objRT_OS_PrdCenter_Mst_SpecType1 = new RT_OS_PrdCenter_Mst_SpecType1();
                objRT_OS_PrdCenter_Mst_SpecType1.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecType1>(ex, objRT_OS_PrdCenter_Mst_SpecType1);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecType1> WA_OS_PrdCenter_Mst_SpecType1_Create(RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecType1>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecType1.Tid);
            RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecType1_Create";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecType1_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecType1", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecType1)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecType1_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType1 // objRQ_Mst_SpecType1
                                                     // //
                    , out objRT_OS_PrdCenter_Mst_SpecType1 // RT_Mst_SpecType1
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
                objRT_OS_PrdCenter_Mst_SpecType1.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecType1>(objRT_OS_PrdCenter_Mst_SpecType1);
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
                if (objRT_OS_PrdCenter_Mst_SpecType1 == null) objRT_OS_PrdCenter_Mst_SpecType1 = new RT_OS_PrdCenter_Mst_SpecType1();
                objRT_OS_PrdCenter_Mst_SpecType1.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecType1>(ex, objRT_OS_PrdCenter_Mst_SpecType1);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecType1> WA_OS_PrdCenter_Mst_SpecType1_Update(RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecType1>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecType1.Tid);
            RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecType1_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecType1_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecType1", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecType1)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecType1_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType1 // objRQ_Mst_SpecType1
                                                     // //
                    , out objRT_OS_PrdCenter_Mst_SpecType1 // RT_Mst_SpecType1
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
                objRT_OS_PrdCenter_Mst_SpecType1.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecType1>(objRT_OS_PrdCenter_Mst_SpecType1);
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
                if (objRT_OS_PrdCenter_Mst_SpecType1 == null) objRT_OS_PrdCenter_Mst_SpecType1 = new RT_OS_PrdCenter_Mst_SpecType1();
                objRT_OS_PrdCenter_Mst_SpecType1.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecType1>(ex, objRT_OS_PrdCenter_Mst_SpecType1);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecType1> WA_OS_PrdCenter_Mst_SpecType1_Delete(RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecType1>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecType1.Tid);
            RT_OS_PrdCenter_Mst_SpecType1 objRT_OS_PrdCenter_Mst_SpecType1 = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecType1_Delete";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecType1_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecType1", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecType1)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecType1.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecType1_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecType1 // objRQ_Mst_SpecType1
                                                     // //
                    , out objRT_OS_PrdCenter_Mst_SpecType1 // RT_Mst_SpecType1
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
                objRT_OS_PrdCenter_Mst_SpecType1.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecType1>(objRT_OS_PrdCenter_Mst_SpecType1);
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
                if (objRT_OS_PrdCenter_Mst_SpecType1 == null) objRT_OS_PrdCenter_Mst_SpecType1 = new RT_OS_PrdCenter_Mst_SpecType1();
                objRT_OS_PrdCenter_Mst_SpecType1.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecType1>(ex, objRT_OS_PrdCenter_Mst_SpecType1);
                #endregion
            }
        }
    }
}
