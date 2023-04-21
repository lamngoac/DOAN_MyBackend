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
    public class SysUserController : ApiControllerBase
    {
        //[AcceptVerbs("POST")]
        //public ServiceResult<RT_Sys_User> WA_Sys_User_Get(SysUser objsysuser)
        //{
        //	try
        //	{
        //		//var lstSysUserCur = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysUser>>(objsysuser);
        //		var objRT_Sys_User = new RT_Sys_User();

        //		var objRQ_Sys_User = new RQ_Sys_User();

        //		objRQ_Sys_User.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
        //		objRQ_Sys_User.UserCode = "SYSADMIN";
        //		objRQ_Sys_User.UserPassword = "1";
        //		objRQ_Sys_User.Ft_RecordStart = "0";
        //		objRQ_Sys_User.Ft_RecordCount = "12345600";
        //		objRQ_Sys_User.Ft_WhereClause = "";
        //		objRQ_Sys_User.Rt_Cols_Sys_User = "*";
        //		objRQ_Sys_User.Rt_Cols_Sys_UserInGroup = "*";

        //		var objRQ_Sys_User_Json = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Sys_User);

        //		objRT_Sys_User = _biz.WebAPI_Sys_User_Get(objRQ_Sys_User);

        //		return Success<RT_Sys_User>(objRT_Sys_User);
        //	}
        //	catch (Exception ex)
        //	{
        //		return Error<RT_Sys_User>(ex);
        //	}
        //}

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_Login(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
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
                mdsReturn = _biz.WAS_Sys_User_Login(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_RefreshToken(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_RefreshToken";
            string strErrorCodeDefault = "WA_Sys_User_RefreshToken";

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
                mdsReturn = _biz.WAS_Sys_User_RefreshToken(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_Get(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
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
                mdsReturn = _biz.WAS_Sys_User_Get(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_GetForCurrentUser(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
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
                mdsReturn = _biz.WAS_Sys_User_GetForCurrentUser(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_ChangePassword(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_ChangePassword";
            string strErrorCodeDefault = "WA_Sys_User_ChangePassword";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
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
                mdsReturn = _biz.WAS_Sys_User_ChangePassword(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_Create(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_Create";
            string strErrorCodeDefault = "WA_Sys_User_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
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
                mdsReturn = _biz.WAS_Sys_User_Create(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_Activate(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_Activate";
            string strErrorCodeDefault = "WA_Sys_User_Activate";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
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
                mdsReturn = _biz.WAS_Sys_User_Activate(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_Update(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_Update";
            string strErrorCodeDefault = "WA_Sys_User_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
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
                mdsReturn = _biz.WAS_Sys_User_Update(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_Delete(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_Delete";
            string strErrorCodeDefault = "WA_Sys_User_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User)
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
                mdsReturn = _biz.WAS_Sys_User_Delete(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Mix_Sys_User_Get(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mix_Sys_User_Get";
            string strErrorCodeDefault = "WA_Mix_Sys_User_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Process:
                ////
                //mdsReturn = _biz.WS_Mix_Sys_User_Get(ref alParamsCoupleError
                //	, objRQ_Sys_User
                //	////
                //	, out objRT_Sys_User // RT_Sys_User
                //	);

                //if (CmUtils.CMyDataSet.HasError(mdsReturn))
                //{
                //	throw CmUtils.CMyException.Raise(
                //		"WA_Sys_User_Get"
                //		, null
                //		, null
                //		);
                //}
                #endregion

                #region // GetData:
                ////
                DataTable dt_Sys_User = mdsReturn.Tables["Sys_User_1"].Copy();

                #endregion

                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_User>(objRT_Sys_User);
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

                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_User> WA_Sys_User_GetForUserMapInv(RQ_Sys_User objRQ_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            RT_Sys_User objRT_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_User_GetForUserMapInv";
            string strErrorCodeDefault = "WA_Sys_User_GetForUserMapInv";

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
                mdsReturn = _biz.WAS_Sys_User_GetForUserMapInv(
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
                return Success<RT_Sys_User>(objRT_Sys_User);
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
                if (objRT_Sys_User == null) objRT_Sys_User = new RT_Sys_User();
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_User>(ex, objRT_Sys_User);
                #endregion
            }
        }
    }
}
