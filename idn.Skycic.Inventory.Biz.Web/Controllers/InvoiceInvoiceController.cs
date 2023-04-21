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
    public class InvoiceInvoiceController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Cancel(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Cancel";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Cancel";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Cancel(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                    ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Approved(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Approved";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Approved";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Approved(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_ApprovedMulti(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_ApprovedMulti";
            string strErrorCodeDefault = "WA_Invoice_Invoice_ApprovedMulti";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_ApprovedMulti(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Issued(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Issued";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Issued";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Issued(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Save_Root(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Save_Root";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Save_Root";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Save_Root_Neww20190705(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Calc(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Calc";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Calc";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Calc(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Save_Adj(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Save_Adj";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Save_Adj";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Save_Adj_New20190705(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Save_Replace(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Save_Replace";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Save_Replace";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Save_Replace_New20190705(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Get(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Get";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Get";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Get_New20190705(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_GetNoSession(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WAS_Invoice_Invoice_GetNoSession";
            string strErrorCodeDefault = "WAS_Invoice_Invoice_GetNoSession";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_GetNoSession_New20190705(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_AllocatedInv(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_AllocatedInv";
            string strErrorCodeDefault = "WA_Invoice_Invoice_AllocatedInv";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_AllocatedInv(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_AllocatedAndApprovedAndIssued(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_AllocatedAndApprovedAndIssued";
            string strErrorCodeDefault = "WA_Invoice_Invoice_AllocatedAndApprovedAndIssued";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_AllocatedAndApprovedAndIssued(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Change(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Change";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Change";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Change(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }
        
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Deleted(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Deleted";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Deleted";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Deleted_New20190816(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }
        
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_UpdAfterAllocated(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_UpdAfterAllocated";
            string strErrorCodeDefault = "WA_Invoice_Invoice_UpdAfterAllocated";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_UpdAfterAllocated(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }
        
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued";
            string strErrorCodeDefault = "WA_OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued";
            string strErrorCodeDefault = "WA_OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued";
            string strErrorCodeDefault = "WA_OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_OSDMS_Invoice_Invoice_Calc(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSDMS_Invoice_Invoice_Calc";
            string strErrorCodeDefault = "WA_OSDMS_Invoice_Invoice_Calc";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSDMS_Invoice_Invoice_Calc(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_SaveAndAllocatedInv(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_SaveAndAllocatedInv";
            string strErrorCodeDefault = "WA_Invoice_Invoice_SaveAndAllocatedInv";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_SaveAndAllocatedInv(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // RQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_ApprovedAndIssued(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_ApprovedAndIssued";
            string strErrorCodeDefault = "WA_Invoice_Invoice_ApprovedAndIssued";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_ApprovedAndIssued(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_UpdMailSentDTimeUTC(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_UpdMailSentDTimeUTC";
            string strErrorCodeDefault = "WA_Invoice_Invoice_UpdMailSentDTimeUTC";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_UpdMailSentDTimeUTC(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_Invoice> WA_Invoice_Invoice_Upd(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
            RT_Invoice_Invoice objRT_Invoice_Invoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_Invoice_Upd";
            string strErrorCodeDefault = "WA_Invoice_Invoice_Upd";

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
                    , objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_Invoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_Invoice_Upd(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_Invoice_Invoice // objRT_Invoice_Invoice
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
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
                if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
                objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
                #endregion
            }
        }
    }
}