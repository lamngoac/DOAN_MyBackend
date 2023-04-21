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
    public class OSInvoiceInvoiceTempController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Invoice_InvoiceTemp> WA_OS_Invoice_InvoiceTemp_Save(RQ_OS_Invoice_InvoiceTemp objRQ_OS_Invoice_InvoiceTemp)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Invoice_InvoiceTemp>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Invoice_InvoiceTemp.Tid);
            RT_OS_Invoice_InvoiceTemp objRT_OS_Invoice_InvoiceTemp = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Invoice_InvoiceTemp_Save";
            string strErrorCodeDefault = "WA_OS_Invoice_InvoiceTemp_Save";

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
                    , objRQ_OS_Invoice_InvoiceTemp.GwUserCode // strGwUserCode
                    , objRQ_OS_Invoice_InvoiceTemp.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Invoice_InvoiceTemp_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Invoice_InvoiceTemp // objRQ_OS_Invoice_InvoiceTemp
                                                   ////
                    , out objRT_OS_Invoice_InvoiceTemp // objRT_OS_Invoice_InvoiceTemp
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
                objRT_OS_Invoice_InvoiceTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Invoice_InvoiceTemp>(objRT_OS_Invoice_InvoiceTemp);
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
                if (objRT_OS_Invoice_InvoiceTemp == null) objRT_OS_Invoice_InvoiceTemp = new RT_OS_Invoice_InvoiceTemp();
                objRT_OS_Invoice_InvoiceTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Invoice_InvoiceTemp>(ex, objRT_OS_Invoice_InvoiceTemp);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Invoice_InvoiceTemp> WA_OS_Invoice_InvoiceTemp_Get(RQ_OS_Invoice_InvoiceTemp objRQ_OS_Invoice_InvoiceTemp)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Invoice_InvoiceTemp>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Invoice_InvoiceTemp.Tid);
            RT_OS_Invoice_InvoiceTemp objRT_OS_Invoice_InvoiceTemp = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Invoice_InvoiceTemp_Get";
            string strErrorCodeDefault = "WA_OS_Invoice_InvoiceTemp_Get";

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
                    , objRQ_OS_Invoice_InvoiceTemp.GwUserCode // strGwUserCode
                    , objRQ_OS_Invoice_InvoiceTemp.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Invoice_InvoiceTemp_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Invoice_InvoiceTemp // RQ_OS_Invoice_InvoiceTemp
                                                   ////
                    , out objRT_OS_Invoice_InvoiceTemp // RT_OS_Invoice_InvoiceTemp
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
                objRT_OS_Invoice_InvoiceTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Invoice_InvoiceTemp>(objRT_OS_Invoice_InvoiceTemp);
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
                if (objRT_OS_Invoice_InvoiceTemp == null) objRT_OS_Invoice_InvoiceTemp = new RT_OS_Invoice_InvoiceTemp();
                objRT_OS_Invoice_InvoiceTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Invoice_InvoiceTemp>(ex, objRT_OS_Invoice_InvoiceTemp);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Invoice_InvoiceTemp> WA_OS_Invoice_InvoiceTemp_Delete(RQ_OS_Invoice_InvoiceTemp objRQ_OS_Invoice_InvoiceTemp)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Invoice_InvoiceTemp>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Invoice_InvoiceTemp.Tid);
            RT_OS_Invoice_InvoiceTemp objRT_OS_Invoice_InvoiceTemp = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Invoice_InvoiceTemp_Delete";
            string strErrorCodeDefault = "WA_OS_Invoice_InvoiceTemp_Delete";

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
                    , objRQ_OS_Invoice_InvoiceTemp.GwUserCode // strGwUserCode
                    , objRQ_OS_Invoice_InvoiceTemp.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Invoice_InvoiceTemp_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Invoice_InvoiceTemp // objRQ_OS_Invoice_InvoiceTemp
                                                   ////
                    , out objRT_OS_Invoice_InvoiceTemp // objRT_OS_Invoice_InvoiceTemp
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
                objRT_OS_Invoice_InvoiceTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Invoice_InvoiceTemp>(objRT_OS_Invoice_InvoiceTemp);
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
                if (objRT_OS_Invoice_InvoiceTemp == null) objRT_OS_Invoice_InvoiceTemp = new RT_OS_Invoice_InvoiceTemp();
                objRT_OS_Invoice_InvoiceTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Invoice_InvoiceTemp>(ex, objRT_OS_Invoice_InvoiceTemp);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_Invoice_InvoiceTemp> WA_OS_Invoice_InvoiceTemp_UpdMultiSignStatus(RQ_OS_Invoice_InvoiceTemp objRQ_OS_Invoice_InvoiceTemp)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_Invoice_InvoiceTemp>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_Invoice_InvoiceTemp.Tid);
            RT_OS_Invoice_InvoiceTemp objRT_OS_Invoice_InvoiceTemp = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Invoice_InvoiceTemp_UpdMultiSignStatus";
            string strErrorCodeDefault = "WA_OS_Invoice_InvoiceTemp_UpdMultiSignStatus";

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
                    , objRQ_OS_Invoice_InvoiceTemp.GwUserCode // strGwUserCode
                    , objRQ_OS_Invoice_InvoiceTemp.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Invoice_InvoiceTemp_UpdMultiSignStatus(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_Invoice_InvoiceTemp // objRQ_OS_Invoice_InvoiceTemp
                                                   ////
                    , out objRT_OS_Invoice_InvoiceTemp // objRT_OS_Invoice_InvoiceTemp
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
                objRT_OS_Invoice_InvoiceTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_Invoice_InvoiceTemp>(objRT_OS_Invoice_InvoiceTemp);
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
                if (objRT_OS_Invoice_InvoiceTemp == null) objRT_OS_Invoice_InvoiceTemp = new RT_OS_Invoice_InvoiceTemp();
                objRT_OS_Invoice_InvoiceTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_Invoice_InvoiceTemp>(ex, objRT_OS_Invoice_InvoiceTemp);
                #endregion
            }
        }

    }
}