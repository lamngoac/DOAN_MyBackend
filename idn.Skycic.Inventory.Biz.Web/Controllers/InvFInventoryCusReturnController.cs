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
    public class InvFInventoryCusReturnController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryCusReturn> WA_InvF_InventoryCusReturn_Get(RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryCusReturn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryCusReturn.Tid);
            RT_InvF_InventoryCusReturn objRT_InvF_InventoryCusReturn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryCusReturn_Get";
            string strErrorCodeDefault = "WA_InvF_InventoryCusReturn_Get";

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
                    , objRQ_InvF_InventoryCusReturn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryCusReturn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryCusReturn_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryCusReturn // objRQ_InvF_InventoryCusReturn
                                                    ////
                    , out objRT_InvF_InventoryCusReturn // objRT_InvF_InventoryCusReturn
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
                objRT_InvF_InventoryCusReturn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryCusReturn>(objRT_InvF_InventoryCusReturn);
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
                if (objRT_InvF_InventoryCusReturn == null) objRT_InvF_InventoryCusReturn = new RT_InvF_InventoryCusReturn();
                objRT_InvF_InventoryCusReturn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryCusReturn>(ex, objRT_InvF_InventoryCusReturn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryCusReturn> WA_InvF_InventoryCusReturn_Save(RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryCusReturn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryCusReturn.Tid);
            RT_InvF_InventoryCusReturn objRT_InvF_InventoryCusReturn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryCusReturn_Save";
            string strErrorCodeDefault = "WA_InvF_InventoryCusReturn_Save";

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
                    , objRQ_InvF_InventoryCusReturn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryCusReturn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryCusReturn_Save_New20210918(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryCusReturn // objRQ_InvF_InventoryCusReturn
                                                    ////
                    , out objRT_InvF_InventoryCusReturn // objRT_InvF_InventoryCusReturn
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
                objRT_InvF_InventoryCusReturn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryCusReturn>(objRT_InvF_InventoryCusReturn);
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
                if (objRT_InvF_InventoryCusReturn == null) objRT_InvF_InventoryCusReturn = new RT_InvF_InventoryCusReturn();
                objRT_InvF_InventoryCusReturn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryCusReturn>(ex, objRT_InvF_InventoryCusReturn);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryCusReturn> WA_InvF_InventoryCusReturn_Appr(RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryCusReturn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryCusReturn.Tid);
            RT_InvF_InventoryCusReturn objRT_InvF_InventoryCusReturn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryCusReturn_Appr";
            string strErrorCodeDefault = "WA_InvF_InventoryCusReturn_Appr";

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
                    , objRQ_InvF_InventoryCusReturn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryCusReturn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryCusReturn_Appr_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryCusReturn // objRQ_InvF_InventoryCusReturn
                                                    ////
                    , out objRT_InvF_InventoryCusReturn // objRT_InvF_InventoryCusReturn
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
                objRT_InvF_InventoryCusReturn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryCusReturn>(objRT_InvF_InventoryCusReturn);
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
                if (objRT_InvF_InventoryCusReturn == null) objRT_InvF_InventoryCusReturn = new RT_InvF_InventoryCusReturn();
                objRT_InvF_InventoryCusReturn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryCusReturn>(ex, objRT_InvF_InventoryCusReturn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryCusReturn> WA_InvF_InventoryCusReturn_Cancel(RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryCusReturn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryCusReturn.Tid);
            RT_InvF_InventoryCusReturn objRT_InvF_InventoryCusReturn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryCusReturn_Cancel";
            string strErrorCodeDefault = "WA_InvF_InventoryCusReturn_Cancel";

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
                    , objRQ_InvF_InventoryCusReturn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryCusReturn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryCusReturn_Cancel(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryCusReturn // objRQ_InvF_InventoryCusReturn
                                                    ////
                    , out objRT_InvF_InventoryCusReturn // objRT_InvF_InventoryCusReturn
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
                objRT_InvF_InventoryCusReturn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryCusReturn>(objRT_InvF_InventoryCusReturn);
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
                if (objRT_InvF_InventoryCusReturn == null) objRT_InvF_InventoryCusReturn = new RT_InvF_InventoryCusReturn();
                objRT_InvF_InventoryCusReturn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryCusReturn>(ex, objRT_InvF_InventoryCusReturn);
                #endregion
            }
        }

    }
}
