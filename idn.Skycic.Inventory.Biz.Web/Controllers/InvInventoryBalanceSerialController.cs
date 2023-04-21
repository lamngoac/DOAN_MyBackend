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
    public class InvInventoryBalanceSerialController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_InventoryBalanceSerial> WA_Inv_InventoryBalanceSerial_Get(RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventoryBalanceSerial>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceSerial.Tid);
            RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_InventoryBalanceSerial_Get";
            string strErrorCodeDefault = "WA_Inv_InventoryBalanceSerial_Get";

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
                    , objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_InventoryBalanceSerial_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventoryBalanceSerial // objRQ_Inv_InventoryBalanceSerial
                                         ////
                    , out objRT_Inv_InventoryBalanceSerial // objRT_Inv_InventoryBalanceSerial
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
                objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_InventoryBalanceSerial>(objRT_Inv_InventoryBalanceSerial);
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
                if (objRT_Inv_InventoryBalanceSerial == null) objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
                objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_InventoryBalanceSerial>(ex, objRT_Inv_InventoryBalanceSerial);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_InventoryBalanceSerial> WA_Inv_InventoryBalanceSerial_Map(RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventoryBalanceSerial>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceSerial.Tid);
            RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_InventoryBalanceSerial_Map";
            string strErrorCodeDefault = "WA_Inv_InventoryBalanceSerial_Map";

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
                    , objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_InventoryBalanceSerial_Map(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventoryBalanceSerial // objRQ_Inv_InventoryBalanceSerial
                                         ////
                    , out objRT_Inv_InventoryBalanceSerial // objRT_Inv_InventoryBalanceSerial
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
                objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_InventoryBalanceSerial>(objRT_Inv_InventoryBalanceSerial);
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
                if (objRT_Inv_InventoryBalanceSerial == null) objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
                objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_InventoryBalanceSerial>(ex, objRT_Inv_InventoryBalanceSerial);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_InventoryBalanceSerial> WA_Inv_InventoryBalanceSerial_UpdCan(RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventoryBalanceSerial>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceSerial.Tid);
            RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_InventoryBalanceSerial_UpdCan";
            string strErrorCodeDefault = "WA_Inv_InventoryBalanceSerial_UpdCan";

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
                    , objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_InventoryBalanceSerial_UpdCan(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventoryBalanceSerial // objRQ_Inv_InventoryBalanceSerial
                                                       ////
                    , out objRT_Inv_InventoryBalanceSerial // objRT_Inv_InventoryBalanceSerial
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
                objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_InventoryBalanceSerial>(objRT_Inv_InventoryBalanceSerial);
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
                if (objRT_Inv_InventoryBalanceSerial == null) objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
                objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_InventoryBalanceSerial>(ex, objRT_Inv_InventoryBalanceSerial);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_InventoryBalanceSerial> WA_Inv_InventoryBalanceSerial_UpdCanFromBox(RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventoryBalanceSerial>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceSerial.Tid);
            RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_InventoryBalanceSerial_UpdCanFromBox";
            string strErrorCodeDefault = "WA_Inv_InventoryBalanceSerial_UpdCanFromBox";

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
                    , objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_InventoryBalanceSerial_UpdCanFromBox(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventoryBalanceSerial // objRQ_Inv_InventoryBalanceSerial
                                                       ////
                    , out objRT_Inv_InventoryBalanceSerial // objRT_Inv_InventoryBalanceSerial
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
                objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_InventoryBalanceSerial>(objRT_Inv_InventoryBalanceSerial);
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
                if (objRT_Inv_InventoryBalanceSerial == null) objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
                objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_InventoryBalanceSerial>(ex, objRT_Inv_InventoryBalanceSerial);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_InventoryBalanceSerial_OutInv> WA_Inv_InventoryBalanceSerial_OutInv(RQ_Inv_InventoryBalanceSerial_OutInv objRQ_Inv_InventoryBalanceSerial_OutInv)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventoryBalanceSerial_OutInv>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceSerial_OutInv.Tid);
            RT_Inv_InventoryBalanceSerial_OutInv objRT_Inv_InventoryBalanceSerial_OutInv = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_InventoryBalanceSerial_OutInv";
            string strErrorCodeDefault = "WA_Inv_InventoryBalanceSerial_OutInv";

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
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalanceSerial_OutInv.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_InventoryBalanceSerial_OutInv(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventoryBalanceSerial_OutInv // objRQ_Inv_InventoryBalanceSerial_OutInv
                                                              ////
                    , out objRT_Inv_InventoryBalanceSerial_OutInv // objRT_Inv_InventoryBalanceSerial
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
                objRT_Inv_InventoryBalanceSerial_OutInv.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_InventoryBalanceSerial_OutInv>(objRT_Inv_InventoryBalanceSerial_OutInv);
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
                if (objRT_Inv_InventoryBalanceSerial_OutInv == null) objRT_Inv_InventoryBalanceSerial_OutInv = new RT_Inv_InventoryBalanceSerial_OutInv();
                objRT_Inv_InventoryBalanceSerial_OutInv.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_InventoryBalanceSerial_OutInv>(ex, objRT_Inv_InventoryBalanceSerial_OutInv);
                #endregion
            }
        }
    }
}