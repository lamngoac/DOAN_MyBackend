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
    public class SysModulesController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Modules> WA_Sys_Modules_Get(RQ_Sys_Modules objRQ_Sys_Modules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Modules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            RT_Sys_Modules objRT_Sys_Modules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_Modules_Get";
            string strErrorCodeDefault = "WA_Sys_Modules_Get";

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
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Sys_Modules_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules // RQ_Sys_Modules
                                    ////
                    , out objRT_Sys_Modules // RT_Sys_Modules
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
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Modules>(objRT_Sys_Modules);
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
                if (objRT_Sys_Modules == null) objRT_Sys_Modules = new RT_Sys_Modules();
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Modules>(ex, objRT_Sys_Modules);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Modules> WA_Sys_Modules_Create(RQ_Sys_Modules objRQ_Sys_Modules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Modules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            RT_Sys_Modules objRT_Sys_Modules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_Modules_Create";
            string strErrorCodeDefault = "WA_Sys_Modules_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Sys_Modules_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules // objRQ_Sys_Modules
                                    ////
                    , out objRT_Sys_Modules // RT_Sys_Modules
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
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Modules>(objRT_Sys_Modules);
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
                if (objRT_Sys_Modules == null) objRT_Sys_Modules = new RT_Sys_Modules();
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Modules>(ex, objRT_Sys_Modules);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Modules> WA_Sys_Modules_Update(RQ_Sys_Modules objRQ_Sys_Modules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Modules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            RT_Sys_Modules objRT_Sys_Modules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_Modules_Update";
            string strErrorCodeDefault = "WA_Sys_Modules_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Sys_Modules_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules // objRQ_Sys_Modules
                                    ////
                    , out objRT_Sys_Modules // objRT_Sys_Modules
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
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Modules>(objRT_Sys_Modules);
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
                if (objRT_Sys_Modules == null) objRT_Sys_Modules = new RT_Sys_Modules();
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Modules>(ex, objRT_Sys_Modules);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Modules> WA_Sys_Modules_Delete(RQ_Sys_Modules objRQ_Sys_Modules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Modules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            RT_Sys_Modules objRT_Sys_Modules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_Modules_Delete";
            string strErrorCodeDefault = "WA_Sys_Modules_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Sys_Modules_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules // objRQ_Sys_Modules
                                    ////
                    , out objRT_Sys_Modules // objRT_Sys_Modules
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
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Modules>(objRT_Sys_Modules);
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
                if (objRT_Sys_Modules == null) objRT_Sys_Modules = new RT_Sys_Modules();
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Modules>(ex, objRT_Sys_Modules);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Modules> WA_RptSv_Sys_Modules_Get(RQ_Sys_Modules objRQ_Sys_Modules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Modules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            RT_Sys_Modules objRT_Sys_Modules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Modules_Get";
            string strErrorCodeDefault = "WA_RptSv_Sys_Modules_Get";

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
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Modules_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules // RQ_Sys_Modules
                                        ////
                    , out objRT_Sys_Modules // RT_Sys_Modules
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
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Modules>(objRT_Sys_Modules);
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
                if (objRT_Sys_Modules == null) objRT_Sys_Modules = new RT_Sys_Modules();
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Modules>(ex, objRT_Sys_Modules);
                #endregion
            }
        }
        
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Modules> WA_RptSv_Sys_Modules_Create(RQ_Sys_Modules objRQ_Sys_Modules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Modules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            RT_Sys_Modules objRT_Sys_Modules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Modules_Create";
            string strErrorCodeDefault = "WA_RptSv_Sys_Modules_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Modules_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules // objRQ_Sys_Modules
                                        ////
                    , out objRT_Sys_Modules // RT_Sys_Modules
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
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Modules>(objRT_Sys_Modules);
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
                if (objRT_Sys_Modules == null) objRT_Sys_Modules = new RT_Sys_Modules();
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Modules>(ex, objRT_Sys_Modules);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Modules> WA_RptSv_Sys_Modules_Update(RQ_Sys_Modules objRQ_Sys_Modules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Modules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            RT_Sys_Modules objRT_Sys_Modules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Modules_Update";
            string strErrorCodeDefault = "WA_RptSv_Sys_Modules_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Modules_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules // objRQ_Sys_Modules
                                        ////
                    , out objRT_Sys_Modules // objRT_Sys_Modules
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
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Modules>(objRT_Sys_Modules);
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
                if (objRT_Sys_Modules == null) objRT_Sys_Modules = new RT_Sys_Modules();
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Modules>(ex, objRT_Sys_Modules);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Modules> WA_RptSv_Sys_Modules_Delete(RQ_Sys_Modules objRQ_Sys_Modules)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Modules>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            RT_Sys_Modules objRT_Sys_Modules = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Modules_Delete";
            string strErrorCodeDefault = "WA_RptSv_Sys_Modules_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Modules_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Modules // objRQ_Sys_Modules
                                        ////
                    , out objRT_Sys_Modules // objRT_Sys_Modules
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
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Modules>(objRT_Sys_Modules);
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
                if (objRT_Sys_Modules == null) objRT_Sys_Modules = new RT_Sys_Modules();
                objRT_Sys_Modules.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Modules>(ex, objRT_Sys_Modules);
                #endregion
            }
        }

    }
}
