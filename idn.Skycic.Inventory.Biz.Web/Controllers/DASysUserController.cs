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
    public class DASysUserController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Sys_User> WA_Sys_User_Login(DA_RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            DA_RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_Login";
            string strErrorCodeDefault = "WA_Sys_User_Login";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DASys_User_Login(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User // objRQ_Sys_User
                                     ////
                    , out objRT_Sys_User // RT_Sys_User
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
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new DA_RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Sys_User> WA_Sys_User_ChangePassword(DA_RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            DA_RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_ChangePassword";
            string strErrorCodeDefault = "WA_Sys_User_ChangePassword";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DASys_User_ChangePassword(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User // objRQ_Sys_User
                                     ////
                    , out objRT_Sys_User // RT_Sys_User
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
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new DA_RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Sys_User> WA_Sys_User_GetForCurrentUser(DA_RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            DA_RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_GetForCurrentUser";
            string strErrorCodeDefault = "WA_Sys_User_GetForCurrentUser";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DASys_User_GetForCurrentUser(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User // objRQ_Sys_User
                                     ////
                    , out objRT_Sys_User // RT_Sys_User
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
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new DA_RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Sys_User> WA_Sys_User_Get(DA_RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            DA_RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_Get";
            string strErrorCodeDefault = "WA_Sys_User_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DASys_User_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User // objRQ_Sys_User
                                     ////
                    , out objRT_Sys_User // RT_Sys_User
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
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new DA_RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Sys_User> WA_Sys_User_Create(DA_RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            DA_RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_Get";
            string strErrorCodeDefault = "WA_Sys_User_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DASys_User_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User // objRQ_Sys_User
                                     ////
                    , out objRT_Sys_User // RT_Sys_User
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
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new DA_RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Sys_User> WA_Sys_User_Activate(DA_RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            DA_RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_Activate";
            string strErrorCodeDefault = "WA_Sys_User_Activate";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DASys_User_Activate(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_User // objRQ_Sys_User
                                     ////
                    , out objRT_Sys_User // RT_Sys_User
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
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new DA_RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }
    }
}
