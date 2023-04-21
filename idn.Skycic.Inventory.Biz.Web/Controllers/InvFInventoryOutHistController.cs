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
    public class InvFInventoryOutHistController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOutHist> WA_InvF_InventoryOutHist_Get(RQ_InvF_InventoryOutHist objRQ_InvF_InventoryOutHist)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOutHist>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOutHist.Tid);
            RT_InvF_InventoryOutHist objRT_InvF_InventoryOutHist = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOutHist_Get";
            string strErrorCodeDefault = "WA_InvF_InventoryOutHist_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempInvoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempInvoice)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOutHist.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutHist.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOutHist_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOutHist // objRQ_InvF_InventoryOutHist
                                                  ////
                    , out objRT_InvF_InventoryOutHist // objRT_InvF_InventoryOutHist
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
                objRT_InvF_InventoryOutHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOutHist>(objRT_InvF_InventoryOutHist);
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
                if (objRT_InvF_InventoryOutHist == null) objRT_InvF_InventoryOutHist = new RT_InvF_InventoryOutHist();
                objRT_InvF_InventoryOutHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOutHist>(ex, objRT_InvF_InventoryOutHist);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOutHist> WA_InvF_InventoryOutHist_SaveSpecial(RQ_InvF_InventoryOutHist objRQ_InvF_InventoryOutHist)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOutHist>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOutHist.Tid);
            RT_InvF_InventoryOutHist objRT_InvF_InventoryOutHist = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOutHist_SaveSpecial";
            string strErrorCodeDefault = "WA_InvF_InventoryOutHist_SaveSpecial";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempInvoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempInvoice)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOutHist.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutHist.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOutHist_SaveSpecial(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOutHist // objRQ_InvF_InventoryOutHist
                                                  ////
                    , out objRT_InvF_InventoryOutHist // objRT_InvF_InventoryOutHist
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
                objRT_InvF_InventoryOutHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOutHist>(objRT_InvF_InventoryOutHist);
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
                if (objRT_InvF_InventoryOutHist == null) objRT_InvF_InventoryOutHist = new RT_InvF_InventoryOutHist();
                objRT_InvF_InventoryOutHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOutHist>(ex, objRT_InvF_InventoryOutHist);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOutHist> WA_InvF_InventoryOutHist_Approve(RQ_InvF_InventoryOutHist objRQ_InvF_InventoryOutHist)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOutHist>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOutHist.Tid);
            RT_InvF_InventoryOutHist objRT_InvF_InventoryOutHist = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOutHist_Approve";
            string strErrorCodeDefault = "WA_InvF_InventoryOutHist_Approve";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempInvoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempInvoice)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOutHist.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutHist.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOutHist_Approve(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOutHist // objRQ_InvF_InventoryOutHist
                                                  ////
                    , out objRT_InvF_InventoryOutHist // objRT_InvF_InventoryOutHist
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
                objRT_InvF_InventoryOutHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOutHist>(objRT_InvF_InventoryOutHist);
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
                if (objRT_InvF_InventoryOutHist == null) objRT_InvF_InventoryOutHist = new RT_InvF_InventoryOutHist();
                objRT_InvF_InventoryOutHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOutHist>(ex, objRT_InvF_InventoryOutHist);
                #endregion
            }
        }
    }
}