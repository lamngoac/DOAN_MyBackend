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
    public class InvFInventoryInFGController : ApiControllerBase
    {

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryInFG> WA_InvF_InventoryInFG_Get(RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryInFG>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            RT_InvF_InventoryInFG objRT_InvF_InventoryInFG = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryInFG_Get";
            string strErrorCodeDefault = "WA_InvF_InventoryInFG_Get";

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
                    , objRQ_InvF_InventoryInFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryInFG.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryInFG_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryInFG // objRQ_InvF_InventoryInFG
                                               ////
                    , out objRT_InvF_InventoryInFG // objRT_InvF_InventoryInFG
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
                objRT_InvF_InventoryInFG.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryInFG>(objRT_InvF_InventoryInFG);
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
                if (objRT_InvF_InventoryInFG == null) objRT_InvF_InventoryInFG = new RT_InvF_InventoryInFG();
                objRT_InvF_InventoryInFG.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryInFG>(ex, objRT_InvF_InventoryInFG);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryInFG> WA_InvF_InventoryInFG_Save(RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryInFG>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            RT_InvF_InventoryInFG objRT_InvF_InventoryInFG = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_FInventoryInFG_Save";
            string strErrorCodeDefault = "WA_Inv_FInventoryInFG_Save";

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
                    , objRQ_InvF_InventoryInFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryInFG.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryInFG_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryInFG // objRQ_InvF_InventoryInFG
                                         ////
                    , out objRT_InvF_InventoryInFG // objRT_InvF_InventoryInFG
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
                objRT_InvF_InventoryInFG.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryInFG>(objRT_InvF_InventoryInFG);
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
                if (objRT_InvF_InventoryInFG == null) objRT_InvF_InventoryInFG = new RT_InvF_InventoryInFG();
                objRT_InvF_InventoryInFG.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryInFG>(ex, objRT_InvF_InventoryInFG);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryInFG> WA_InvF_InventoryInFG_Approve(RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryInFG>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryInFG.Tid);
            RT_InvF_InventoryInFG objRT_InvF_InventoryInFG = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryInFG_Approve";
            string strErrorCodeDefault = "WA_InvF_InventoryInFG_Approve";

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
                    , objRQ_InvF_InventoryInFG.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryInFG.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryInFG_Approve(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryInFG // objRQ_InvF_InventoryInFG
                                               ////
                    , out objRT_InvF_InventoryInFG // objRT_InvF_InventoryInFG
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
                objRT_InvF_InventoryInFG.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryInFG>(objRT_InvF_InventoryInFG);
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
                if (objRT_InvF_InventoryInFG == null) objRT_InvF_InventoryInFG = new RT_InvF_InventoryInFG();
                objRT_InvF_InventoryInFG.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryInFG>(ex, objRT_InvF_InventoryInFG);
                #endregion
            }
        }
    }
}