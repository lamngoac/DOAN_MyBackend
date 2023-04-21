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
    public class InvFInvAuditController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InvAudit> WA_InvF_InvAudit_Get(RQ_InvF_InvAudit objRQ_InvF_InvAudit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InvAudit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InvAudit.Tid);
            RT_InvF_InvAudit objRT_InvF_InvAudit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InvAudit_Get";
            string strErrorCodeDefault = "WA_InvF_InvAudit_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit.GwUserCode // strGwUserCode
                    , objRQ_InvF_InvAudit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InvAudit_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit // objRQ_InvF_InvAudit
                                          ////
                    , out objRT_InvF_InvAudit // objRT_InvF_InvAudit
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
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InvAudit>(objRT_InvF_InvAudit);
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
                if (objRT_InvF_InvAudit == null) objRT_InvF_InvAudit = new RT_InvF_InvAudit();
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InvAudit>(ex, objRT_InvF_InvAudit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InvAudit> WA_InvF_InvAudit_Save(RQ_InvF_InvAudit objRQ_InvF_InvAudit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InvAudit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InvAudit.Tid);
            RT_InvF_InvAudit objRT_InvF_InvAudit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InvAudit_Save";
            string strErrorCodeDefault = "WA_InvF_InvAudit_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit.GwUserCode // strGwUserCode
                    , objRQ_InvF_InvAudit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InvAudit_Save_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit // objRQ_InvF_InvAudit
                                          ////
                    , out objRT_InvF_InvAudit // objRT_InvF_InvAudit
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
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InvAudit>(objRT_InvF_InvAudit);
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
                if (objRT_InvF_InvAudit == null) objRT_InvF_InvAudit = new RT_InvF_InvAudit();
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InvAudit>(ex, objRT_InvF_InvAudit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InvAudit> WA_InvF_InvAudit_Appr(RQ_InvF_InvAudit objRQ_InvF_InvAudit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InvAudit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InvAudit.Tid);
            RT_InvF_InvAudit objRT_InvF_InvAudit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InvAudit_Appr";
            string strErrorCodeDefault = "WA_InvF_InvAudit_Appr";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit.GwUserCode // strGwUserCode
                    , objRQ_InvF_InvAudit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InvAudit_Appr(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit // objRQ_InvF_InvAudit
                                          ////
                    , out objRT_InvF_InvAudit // objRT_InvF_InvAudit
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
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InvAudit>(objRT_InvF_InvAudit);
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
                if (objRT_InvF_InvAudit == null) objRT_InvF_InvAudit = new RT_InvF_InvAudit();
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InvAudit>(ex, objRT_InvF_InvAudit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InvAudit> WA_InvF_InvAudit_Cancel(RQ_InvF_InvAudit objRQ_InvF_InvAudit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InvAudit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InvAudit.Tid);
            RT_InvF_InvAudit objRT_InvF_InvAudit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InvAudit_Cancel";
            string strErrorCodeDefault = "WA_InvF_InvAudit_Cancel";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit.GwUserCode // strGwUserCode
                    , objRQ_InvF_InvAudit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InvAudit_Cancel(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit // objRQ_InvF_InvAudit
                                          ////
                    , out objRT_InvF_InvAudit // objRT_InvF_InvAudit
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
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InvAudit>(objRT_InvF_InvAudit);
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
                if (objRT_InvF_InvAudit == null) objRT_InvF_InvAudit = new RT_InvF_InvAudit();
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InvAudit>(ex, objRT_InvF_InvAudit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InvAudit> WA_InvF_InvAudit_Finish(RQ_InvF_InvAudit objRQ_InvF_InvAudit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InvAudit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InvAudit.Tid);
            RT_InvF_InvAudit objRT_InvF_InvAudit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InvAudit_Finish";
            string strErrorCodeDefault = "WA_InvF_InvAudit_Finish";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit.GwUserCode // strGwUserCode
                    , objRQ_InvF_InvAudit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InvAudit_Finish_New20230105(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit // objRQ_InvF_InvAudit
                                          ////
                    , out objRT_InvF_InvAudit // objRT_InvF_InvAudit
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
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InvAudit>(objRT_InvF_InvAudit);
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
                if (objRT_InvF_InvAudit == null) objRT_InvF_InvAudit = new RT_InvF_InvAudit();
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InvAudit>(ex, objRT_InvF_InvAudit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InvAudit> WA_InvF_InvAuditDtl_FlagAudit(RQ_InvF_InvAudit objRQ_InvF_InvAudit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InvAudit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InvAudit.Tid);
            RT_InvF_InvAudit objRT_InvF_InvAudit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InvAuditDtl_FlagAudit";
            string strErrorCodeDefault = "WA_InvF_InvAuditDtl_FlagAudit";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit.GwUserCode // strGwUserCode
                    , objRQ_InvF_InvAudit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InvAuditDtl_FlagAudit(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit // objRQ_InvF_InvAudit
                                          ////
                    , out objRT_InvF_InvAudit // objRT_InvF_InvAudit
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
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InvAudit>(objRT_InvF_InvAudit);
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
                if (objRT_InvF_InvAudit == null) objRT_InvF_InvAudit = new RT_InvF_InvAudit();
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InvAudit>(ex, objRT_InvF_InvAudit);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InvAudit> WA_InvF_InvAuditDtl_FlagAuditSpecial(RQ_InvF_InvAudit objRQ_InvF_InvAudit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InvAudit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InvAudit.Tid);
            RT_InvF_InvAudit objRT_InvF_InvAudit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InvAuditDtl_FlagAuditSpecial";
            string strErrorCodeDefault = "WA_InvF_InvAuditDtl_FlagAuditSpecial";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit.GwUserCode // strGwUserCode
                    , objRQ_InvF_InvAudit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InvAuditDtl_FlagAuditSpecial(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InvAudit // objRQ_InvF_InvAudit
                                          ////
                    , out objRT_InvF_InvAudit // objRT_InvF_InvAudit
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
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InvAudit>(objRT_InvF_InvAudit);
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
                if (objRT_InvF_InvAudit == null) objRT_InvF_InvAudit = new RT_InvF_InvAudit();
                objRT_InvF_InvAudit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InvAudit>(ex, objRT_InvF_InvAudit);
                #endregion
            }
        }
    }
}
