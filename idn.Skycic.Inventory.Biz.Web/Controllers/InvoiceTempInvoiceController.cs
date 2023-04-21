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
    public class InvoiceTempInvoiceController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempInvoice> WA_Invoice_TempInvoice_Get(RQ_Invoice_TempInvoice objRQ_Invoice_TempInvoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempInvoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempInvoice.Tid);
            RT_Invoice_TempInvoice objRT_Invoice_TempInvoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempInvoice_Get";
            string strErrorCodeDefault = "WA_Invoice_TempInvoice_Get";

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
                    , objRQ_Invoice_TempInvoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempInvoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempInvoice_Get_New20190705(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempInvoice // RQ_Invoice_TempInvoice
                                                ////
                    , out objRT_Invoice_TempInvoice // RT_Invoice_TempInvoice
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
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempInvoice>(objRT_Invoice_TempInvoice);
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
                if (objRT_Invoice_TempInvoice == null) objRT_Invoice_TempInvoice = new RT_Invoice_TempInvoice();
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempInvoice>(ex, objRT_Invoice_TempInvoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempInvoice> WA_Invoice_TempInvoice_Save(RQ_Invoice_TempInvoice objRQ_Invoice_TempInvoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempInvoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempInvoice.Tid);
            RT_Invoice_TempInvoice objRT_Invoice_TempInvoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempInvoice_Save";
            string strErrorCodeDefault = "WA_Invoice_TempInvoice_Save";

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
                    , objRQ_Invoice_TempInvoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempInvoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempInvoice_Save_New20190705(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempInvoice // RQ_Invoice_TempInvoice
                                                ////
                    , out objRT_Invoice_TempInvoice // RT_Invoice_TempInvoice
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
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempInvoice>(objRT_Invoice_TempInvoice);
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
                if (objRT_Invoice_TempInvoice == null) objRT_Invoice_TempInvoice = new RT_Invoice_TempInvoice();
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempInvoice>(ex, objRT_Invoice_TempInvoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempInvoice> WA_Invoice_TempInvoice_UpdQtyInvoiceNo(RQ_Invoice_TempInvoice objRQ_Invoice_TempInvoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempInvoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempInvoice.Tid);
            RT_Invoice_TempInvoice objRT_Invoice_TempInvoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempInvoice_UpdQtyInvoiceNo";
            string strErrorCodeDefault = "WA_Invoice_TempInvoice_UpdQtyInvoiceNo";

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
                    , objRQ_Invoice_TempInvoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempInvoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempInvoice_UpdQtyInvoiceNo(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempInvoice // RQ_Invoice_TempInvoice
                                                ////
                    , out objRT_Invoice_TempInvoice // RT_Invoice_TempInvoice
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
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempInvoice>(objRT_Invoice_TempInvoice);
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
                if (objRT_Invoice_TempInvoice == null) objRT_Invoice_TempInvoice = new RT_Invoice_TempInvoice();
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempInvoice>(ex, objRT_Invoice_TempInvoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempInvoice> WA_Invoice_TempInvoice_Issued(RQ_Invoice_TempInvoice objRQ_Invoice_TempInvoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempInvoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempInvoice.Tid);
            RT_Invoice_TempInvoice objRT_Invoice_TempInvoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempInvoice_Issued";
            string strErrorCodeDefault = "WA_Invoice_TempInvoice_Issued";

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
                    , objRQ_Invoice_TempInvoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempInvoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempInvoice_Issued_New20190919(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempInvoice // RQ_Invoice_TempInvoice
                                                ////
                    , out objRT_Invoice_TempInvoice // RT_Invoice_TempInvoice
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
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempInvoice>(objRT_Invoice_TempInvoice);
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
                if (objRT_Invoice_TempInvoice == null) objRT_Invoice_TempInvoice = new RT_Invoice_TempInvoice();
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempInvoice>(ex, objRT_Invoice_TempInvoice);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempInvoice> WA_Invoice_TempInvoice_InActive(RQ_Invoice_TempInvoice objRQ_Invoice_TempInvoice)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempInvoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempInvoice.Tid);
            RT_Invoice_TempInvoice objRT_Invoice_TempInvoice = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempInvoice_InActive";
            string strErrorCodeDefault = "WA_Invoice_TempInvoice_InActive";

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
                    , objRQ_Invoice_TempInvoice.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempInvoice.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempInvoice_InActive(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempInvoice // RQ_Invoice_TempInvoice
                                                ////
                    , out objRT_Invoice_TempInvoice // RT_Invoice_TempInvoice
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
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempInvoice>(objRT_Invoice_TempInvoice);
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
                if (objRT_Invoice_TempInvoice == null) objRT_Invoice_TempInvoice = new RT_Invoice_TempInvoice();
                objRT_Invoice_TempInvoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempInvoice>(ex, objRT_Invoice_TempInvoice);
                #endregion
            }
        }
    }
}