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
    public class InvoiceInvoiceInputController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_InvoiceInput> WA_Invoice_InvoiceInput_Save(RQ_Invoice_InvoiceInput objRQ_Invoice_InvoiceInput)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_InvoiceInput>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_InvoiceInput.Tid);
            RT_Invoice_InvoiceInput objRT_Invoice_InvoiceInput = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_InvoiceInput_Save";
            string strErrorCodeDefault = "WA_Invoice_InvoiceInput_Save";

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
                    , objRQ_Invoice_InvoiceInput.GwUserCode // strGwUserCode
                    , objRQ_Invoice_InvoiceInput.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_InvoiceInput_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_InvoiceInput // objRQ_Invoice_InvoiceInput
                                            ////
                    , out objRT_Invoice_InvoiceInput // objRT_Invoice_InvoiceInput
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
                objRT_Invoice_InvoiceInput.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_InvoiceInput>(objRT_Invoice_InvoiceInput);
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
                if (objRT_Invoice_InvoiceInput == null) objRT_Invoice_InvoiceInput = new RT_Invoice_InvoiceInput();
                objRT_Invoice_InvoiceInput.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_InvoiceInput>(ex, objRT_Invoice_InvoiceInput);
                #endregion
            }
        }
        
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_InvoiceInput> WA_Invoice_InvoiceInput_Get(RQ_Invoice_InvoiceInput objRQ_Invoice_InvoiceInput)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_InvoiceInput>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_InvoiceInput.Tid);
            RT_Invoice_InvoiceInput objRT_Invoice_InvoiceInput = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_InvoiceInput_Get";
            string strErrorCodeDefault = "WA_Invoice_InvoiceInput_Get";

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
                    , objRQ_Invoice_InvoiceInput.GwUserCode // strGwUserCode
                    , objRQ_Invoice_InvoiceInput.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_InvoiceInput_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_InvoiceInput // RQ_Invoice_InvoiceInput
                                            ////
                    , out objRT_Invoice_InvoiceInput // RT_Invoice_InvoiceInput
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
                objRT_Invoice_InvoiceInput.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_InvoiceInput>(objRT_Invoice_InvoiceInput);
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
                if (objRT_Invoice_InvoiceInput == null) objRT_Invoice_InvoiceInput = new RT_Invoice_InvoiceInput();
                objRT_Invoice_InvoiceInput.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_InvoiceInput>(ex, objRT_Invoice_InvoiceInput);
                #endregion
            }
        }
        
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_InvoiceInput> WA_Invoice_InvoiceInput_Delete(RQ_Invoice_InvoiceInput objRQ_Invoice_InvoiceInput)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_InvoiceInput>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_InvoiceInput.Tid);
            RT_Invoice_InvoiceInput objRT_Invoice_InvoiceInput = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_InvoiceInput_Delete";
            string strErrorCodeDefault = "WA_Invoice_InvoiceInput_Delete";

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
                    , objRQ_Invoice_InvoiceInput.GwUserCode // strGwUserCode
                    , objRQ_Invoice_InvoiceInput.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_InvoiceInput_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_InvoiceInput // objRQ_Invoice_InvoiceInput
                                            ////
                    , out objRT_Invoice_InvoiceInput // objRT_Invoice_InvoiceInput
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
                objRT_Invoice_InvoiceInput.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_InvoiceInput>(objRT_Invoice_InvoiceInput);
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
                if (objRT_Invoice_InvoiceInput == null) objRT_Invoice_InvoiceInput = new RT_Invoice_InvoiceInput();
                objRT_Invoice_InvoiceInput.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_InvoiceInput>(ex, objRT_Invoice_InvoiceInput);
                #endregion
            }
        }

    }
}