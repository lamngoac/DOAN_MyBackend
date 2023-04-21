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
    public class SysObjectInModulesController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_ObjectInModules> WA_Sys_ObjectInModules_Get(RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_ObjectInModules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_ObjectInModules.Tid);
            RT_Sys_ObjectInModules objRT_Sys_ObjectInModules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_ObjectInModules_Get";
            string strErrorCodeDefault = "WA_Sys_ObjectInModules_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_ObjectInModules", TJson.JsonConvert.SerializeObject(objRQ_Sys_ObjectInModules)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_ObjectInModules.GwUserCode // strGwUserCode
                    , objRQ_Sys_ObjectInModules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Sys_ObjectInModules_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_ObjectInModules // objRQ_Sys_ObjectInModules
                                         ////
                    , out objRT_Sys_ObjectInModules // RT_Sys_ObjectInModules
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
                objRT_Sys_ObjectInModules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_ObjectInModules>(objRT_Sys_ObjectInModules);
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
                if (objRT_Sys_ObjectInModules == null) objRT_Sys_ObjectInModules = new RT_Sys_ObjectInModules();
                objRT_Sys_ObjectInModules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_ObjectInModules>(ex, objRT_Sys_ObjectInModules);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_ObjectInModules> WA_Sys_ObjectInModules_Save(RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_ObjectInModules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_ObjectInModules.Tid);
            RT_Sys_ObjectInModules objRT_Sys_ObjectInModules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_ObjectInModules_Save";
            string strErrorCodeDefault = "WA_Sys_ObjectInModules_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_ObjectInModules", TJson.JsonConvert.SerializeObject(objRQ_Sys_ObjectInModules)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_ObjectInModules.GwUserCode // strGwUserCode
                    , objRQ_Sys_ObjectInModules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Sys_ObjectInModules_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_ObjectInModules // objRQ_Sys_ObjectInModules
                                            ////
                    , out objRT_Sys_ObjectInModules // RT_Sys_ObjectInModules
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
                objRT_Sys_ObjectInModules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_ObjectInModules>(objRT_Sys_ObjectInModules);
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
                if (objRT_Sys_ObjectInModules == null) objRT_Sys_ObjectInModules = new RT_Sys_ObjectInModules();
                objRT_Sys_ObjectInModules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_ObjectInModules>(ex, objRT_Sys_ObjectInModules);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_ObjectInModules> WA_RptSv_Sys_ObjectInModules_Get(RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_ObjectInModules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_ObjectInModules.Tid);
            RT_Sys_ObjectInModules objRT_Sys_ObjectInModules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_ObjectInModules_Get";
            string strErrorCodeDefault = "WA_RptSv_Sys_ObjectInModules_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_ObjectInModules", TJson.JsonConvert.SerializeObject(objRQ_Sys_ObjectInModules)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_ObjectInModules.GwUserCode // strGwUserCode
                    , objRQ_Sys_ObjectInModules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_ObjectInModules_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_ObjectInModules // objRQ_Sys_ObjectInModules
                                                ////
                    , out objRT_Sys_ObjectInModules // RT_Sys_ObjectInModules
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
                objRT_Sys_ObjectInModules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_ObjectInModules>(objRT_Sys_ObjectInModules);
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
                if (objRT_Sys_ObjectInModules == null) objRT_Sys_ObjectInModules = new RT_Sys_ObjectInModules();
                objRT_Sys_ObjectInModules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_ObjectInModules>(ex, objRT_Sys_ObjectInModules);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_ObjectInModules> WA_RptSv_Sys_ObjectInModules_Save(RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_ObjectInModules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_ObjectInModules.Tid);
            RT_Sys_ObjectInModules objRT_Sys_ObjectInModules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_ObjectInModules_Save";
            string strErrorCodeDefault = "WA_RptSv_Sys_ObjectInModules_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_ObjectInModules", TJson.JsonConvert.SerializeObject(objRQ_Sys_ObjectInModules)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_ObjectInModules.GwUserCode // strGwUserCode
                    , objRQ_Sys_ObjectInModules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_ObjectInModules_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_ObjectInModules // objRQ_Sys_ObjectInModules
                                                ////
                    , out objRT_Sys_ObjectInModules // RT_Sys_ObjectInModules
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
                objRT_Sys_ObjectInModules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_ObjectInModules>(objRT_Sys_ObjectInModules);
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
                if (objRT_Sys_ObjectInModules == null) objRT_Sys_ObjectInModules = new RT_Sys_ObjectInModules();
                objRT_Sys_ObjectInModules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_ObjectInModules>(ex, objRT_Sys_ObjectInModules);
                #endregion
            }
        }

    }
}
