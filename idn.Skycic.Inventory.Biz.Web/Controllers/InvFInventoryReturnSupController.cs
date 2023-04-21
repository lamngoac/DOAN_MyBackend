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
    public class InvFInventoryReturnSupController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryReturnSup> WA_InvF_InventoryReturnSup_Get(RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryReturnSup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryReturnSup.Tid);
            RT_InvF_InventoryReturnSup objRT_InvF_InventoryReturnSup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryReturnSup_Get";
            string strErrorCodeDefault = "WA_InvF_InventoryReturnSup_Get";

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
                    , objRQ_InvF_InventoryReturnSup.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryReturnSup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryReturnSup_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryReturnSup // objRQ_InvF_InventoryReturnSup
                                                    ////
                    , out objRT_InvF_InventoryReturnSup // objRT_InvF_InventoryReturnSup
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
                objRT_InvF_InventoryReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryReturnSup>(objRT_InvF_InventoryReturnSup);
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
                if (objRT_InvF_InventoryReturnSup == null) objRT_InvF_InventoryReturnSup = new RT_InvF_InventoryReturnSup();
                objRT_InvF_InventoryReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryReturnSup>(ex, objRT_InvF_InventoryReturnSup);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryReturnSup> WA_InvF_InventoryReturnSup_Save(RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryReturnSup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryReturnSup.Tid);
            RT_InvF_InventoryReturnSup objRT_InvF_InventoryReturnSup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryReturnSup_Save";
            string strErrorCodeDefault = "WA_InvF_InventoryReturnSup_Save";

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
                    , objRQ_InvF_InventoryReturnSup.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryReturnSup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
              mdsReturn = _biz.WAS_InvF_InventoryReturnSup_Save_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryReturnSup // objRQ_InvF_InventoryReturnSup
                                                    ////
                    , out objRT_InvF_InventoryReturnSup // objRT_InvF_InventoryReturnSup
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
                objRT_InvF_InventoryReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryReturnSup>(objRT_InvF_InventoryReturnSup);
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
                if (objRT_InvF_InventoryReturnSup == null) objRT_InvF_InventoryReturnSup = new RT_InvF_InventoryReturnSup();
                objRT_InvF_InventoryReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryReturnSup>(ex, objRT_InvF_InventoryReturnSup);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryReturnSup> WA_InvF_InventoryReturnSup_Appr(RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryReturnSup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryReturnSup.Tid);
            RT_InvF_InventoryReturnSup objRT_InvF_InventoryReturnSup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryReturnSup_Appr";
            string strErrorCodeDefault = "WA_InvF_InventoryReturnSup_Appr";

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
                    , objRQ_InvF_InventoryReturnSup.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryReturnSup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryReturnSup_Appr_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryReturnSup // objRQ_InvF_InventoryReturnSup
                                                    ////
                    , out objRT_InvF_InventoryReturnSup // objRT_InvF_InventoryReturnSup
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
                objRT_InvF_InventoryReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryReturnSup>(objRT_InvF_InventoryReturnSup);
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
                if (objRT_InvF_InventoryReturnSup == null) objRT_InvF_InventoryReturnSup = new RT_InvF_InventoryReturnSup();
                objRT_InvF_InventoryReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryReturnSup>(ex, objRT_InvF_InventoryReturnSup);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryReturnSup> WA_InvF_InventoryReturnSup_Cancel(RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryReturnSup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryReturnSup.Tid);
            RT_InvF_InventoryReturnSup objRT_InvF_InventoryReturnSup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryReturnSup_Cancel";
            string strErrorCodeDefault = "WA_InvF_InventoryReturnSup_Cancel";

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
                    , objRQ_InvF_InventoryReturnSup.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryReturnSup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryReturnSup_Cancel_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryReturnSup // objRQ_InvF_InventoryReturnSup
                                                    ////
                    , out objRT_InvF_InventoryReturnSup // objRT_InvF_InventoryReturnSup
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
                objRT_InvF_InventoryReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryReturnSup>(objRT_InvF_InventoryReturnSup);
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
                if (objRT_InvF_InventoryReturnSup == null) objRT_InvF_InventoryReturnSup = new RT_InvF_InventoryReturnSup();
                objRT_InvF_InventoryReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryReturnSup>(ex, objRT_InvF_InventoryReturnSup);
                #endregion
            }
        }

    }
}
