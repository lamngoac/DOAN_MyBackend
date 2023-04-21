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
    public class InvFMoveOrdController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_MoveOrd> WA_InvF_MoveOrd_Get(RQ_InvF_MoveOrd objRQ_InvF_MoveOrd)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_MoveOrd>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_MoveOrd.Tid);
            RT_InvF_MoveOrd objRT_InvF_MoveOrd = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_MoveOrd_Get";
            string strErrorCodeDefault = "WA_InvF_MoveOrd_Get";

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
                    , objRQ_InvF_MoveOrd.GwUserCode // strGwUserCode
                    , objRQ_InvF_MoveOrd.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_MoveOrd_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_MoveOrd // objRQ_InvF_MoveOrd
                                         ////
                    , out objRT_InvF_MoveOrd // objRT_InvF_MoveOrd
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
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_MoveOrd>(objRT_InvF_MoveOrd);
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
                if (objRT_InvF_MoveOrd == null) objRT_InvF_MoveOrd = new RT_InvF_MoveOrd();
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_MoveOrd>(ex, objRT_InvF_MoveOrd);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_MoveOrd> WA_InvF_MoveOrd_Save(RQ_InvF_MoveOrd objRQ_InvF_MoveOrd)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_MoveOrd>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_MoveOrd.Tid);
            RT_InvF_MoveOrd objRT_InvF_MoveOrd = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_MoveOrd_Save";
            string strErrorCodeDefault = "WA_InvF_MoveOrd_Save";

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
                    , objRQ_InvF_MoveOrd.GwUserCode // strGwUserCode
                    , objRQ_InvF_MoveOrd.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_MoveOrd_Save_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_MoveOrd // objRQ_InvF_MoveOrd
                                         ////
                    , out objRT_InvF_MoveOrd // objRT_InvF_MoveOrd
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
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_MoveOrd>(objRT_InvF_MoveOrd);
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
                if (objRT_InvF_MoveOrd == null) objRT_InvF_MoveOrd = new RT_InvF_MoveOrd();
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_MoveOrd>(ex, objRT_InvF_MoveOrd);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_MoveOrd> WA_InvF_MoveOrd_Appr(RQ_InvF_MoveOrd objRQ_InvF_MoveOrd)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_MoveOrd>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_MoveOrd.Tid);
            RT_InvF_MoveOrd objRT_InvF_MoveOrd = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_MoveOrd_Appr";
            string strErrorCodeDefault = "WA_InvF_MoveOrd_Appr";

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
                    , objRQ_InvF_MoveOrd.GwUserCode // strGwUserCode
                    , objRQ_InvF_MoveOrd.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_MoveOrd_Appr_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_MoveOrd // objRQ_InvF_MoveOrd
                                         ////
                    , out objRT_InvF_MoveOrd // objRT_InvF_MoveOrd
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
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_MoveOrd>(objRT_InvF_MoveOrd);
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
                if (objRT_InvF_MoveOrd == null) objRT_InvF_MoveOrd = new RT_InvF_MoveOrd();
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_MoveOrd>(ex, objRT_InvF_MoveOrd);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_MoveOrd> WA_InvF_MoveOrd_Cancel(RQ_InvF_MoveOrd objRQ_InvF_MoveOrd)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_MoveOrd>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_MoveOrd.Tid);
            RT_InvF_MoveOrd objRT_InvF_MoveOrd = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_MoveOrd_Cancel";
            string strErrorCodeDefault = "WA_InvF_MoveOrd_Cancel";

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
                    , objRQ_InvF_MoveOrd.GwUserCode // strGwUserCode
                    , objRQ_InvF_MoveOrd.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_MoveOrd_Cancel_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_MoveOrd // objRQ_InvF_MoveOrd
                                         ////
                    , out objRT_InvF_MoveOrd // objRT_InvF_MoveOrd
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
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_MoveOrd>(objRT_InvF_MoveOrd);
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
                if (objRT_InvF_MoveOrd == null) objRT_InvF_MoveOrd = new RT_InvF_MoveOrd();
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_MoveOrd>(ex, objRT_InvF_MoveOrd);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_MoveOrd> WA_InvF_MoveOrd_SaveAndAppr(RQ_InvF_MoveOrd objRQ_InvF_MoveOrd)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_MoveOrd>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_MoveOrd.Tid);
            RT_InvF_MoveOrd objRT_InvF_MoveOrd = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_MoveOrd_SaveAndAppr";
            string strErrorCodeDefault = "WA_InvF_MoveOrd_SaveAndAppr";

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
                    , objRQ_InvF_MoveOrd.GwUserCode // strGwUserCode
                    , objRQ_InvF_MoveOrd.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_MoveOrd_SaveAndAppr(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_MoveOrd // objRQ_InvF_MoveOrd
                                         ////
                    , out objRT_InvF_MoveOrd // objRT_InvF_MoveOrd
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
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_MoveOrd>(objRT_InvF_MoveOrd);
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
                if (objRT_InvF_MoveOrd == null) objRT_InvF_MoveOrd = new RT_InvF_MoveOrd();
                objRT_InvF_MoveOrd.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_MoveOrd>(ex, objRT_InvF_MoveOrd);
                #endregion
            }

        }
    }
}
