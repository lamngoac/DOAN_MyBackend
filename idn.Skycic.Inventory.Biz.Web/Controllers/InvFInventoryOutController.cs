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
    public class InvFInventoryOutController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_Get(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_Get";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_Get";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOut_Get_New20210707(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                             ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_Save(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_Save";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_Save";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOut_Save_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                             ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_SaveAndAppr(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_SaveAndAppr";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_SaveAndAppr";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOut_SaveAndAppr(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_SaveAndAppr_New(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_SaveAndAppr";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_SaveAndAppr";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                // WAS_InvF_InventoryOut_SaveAndAppr_New20220513
                mdsReturn = _biz.WAS_InvF_InventoryOut_SaveAndAppr_New20210410(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_SaveAndAppr_Cheo(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_SaveAndAppr_Cheo";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_SaveAndAppr_Cheo";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOut_SaveAndAppr_Cheo_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_Appr(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_Appr";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_Appr";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOut_Appr_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_Cancel(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_Cancel";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_Cancel";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOut_Cancel_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_UpdAfterApprForEtem(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_UpdAfterApprForEtem";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_UpdAfterApprForEtem";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOut_UpdAfterApprForEtem(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_OSEtem_InvF_InventoryOut_DelAfterAppr(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSEtem_InvF_InventoryOut_DelAfterAppr";
            string strErrorCodeDefault = "WA_OSEtem_InvF_InventoryOut_DelAfterAppr";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSEtem_InvF_InventoryOut_DelAfterAppr(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_UpdProfile(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_UpdProfile";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_UpdProfile";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOut_UpdProfile(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_OSEtem_InvF_InventoryOut_DelQRCode(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSEtem_InvF_InventoryOut_DelQRCode";
            string strErrorCodeDefault = "WA_OSEtem_InvF_InventoryOut_DelQRCode";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSEtem_InvF_InventoryOut_DelQRCode(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                             ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_OSEtem_InvF_InventoryOut_AddQRCode(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSEtem_InvF_InventoryOut_AddQRCode";
            string strErrorCodeDefault = "WA_OSEtem_InvF_InventoryOut_AddQRCode";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSEtem_InvF_InventoryOut_AddQRCode(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOut> WA_InvF_InventoryOut_SaveAndApprDemo(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOut.Tid);
            RT_InvF_InventoryOut objRT_InvF_InventoryOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOut_SaveAndAppr";
            string strErrorCodeDefault = "WA_InvF_InventoryOut_SaveAndAppr";

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
                    , objRQ_InvF_InventoryOut.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                // WAS_InvF_InventoryOut_SaveAndAppr_New20220513
                mdsReturn = _biz.WAS_InvF_InventoryOut_SaveAndAppr_New20220625(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOut // objRQ_InvF_InventoryOut
                                              ////
                    , out objRT_InvF_InventoryOut // objRT_InvF_InventoryOut
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
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOut>(objRT_InvF_InventoryOut);
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
                if (objRT_InvF_InventoryOut == null) objRT_InvF_InventoryOut = new RT_InvF_InventoryOut();
                objRT_InvF_InventoryOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOut>(ex, objRT_InvF_InventoryOut);
                #endregion
            }

        }
    }
}
