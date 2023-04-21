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
    public class TempPrintTempController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Temp_PrintTemp> WA_Temp_PrintTemp_Cancel(RQ_Temp_PrintTemp objRQ_Temp_PrintTemp)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Temp_PrintTemp>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Temp_PrintTemp.Tid);
            RT_Temp_PrintTemp objRT_Temp_PrintTemp = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Temp_PrintTemp_Cancel";
            string strErrorCodeDefault = "WA_Temp_PrintTemp_Cancel";

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
                    , objRQ_Temp_PrintTemp.GwUserCode // strGwUserCode
                    , objRQ_Temp_PrintTemp.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Temp_PrintTemp_Cancel(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Temp_PrintTemp // objRQ_Temp_PrintTemp
                                            ////
                    , out objRT_Temp_PrintTemp // objRT_Temp_PrintTemp
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
                objRT_Temp_PrintTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Temp_PrintTemp>(objRT_Temp_PrintTemp);
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
                if (objRT_Temp_PrintTemp == null) objRT_Temp_PrintTemp = new RT_Temp_PrintTemp();
                objRT_Temp_PrintTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Temp_PrintTemp>(ex, objRT_Temp_PrintTemp);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Temp_PrintTemp> WA_Temp_PrintTemp_Approved(RQ_Temp_PrintTemp objRQ_Temp_PrintTemp)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Temp_PrintTemp>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Temp_PrintTemp.Tid);
            RT_Temp_PrintTemp objRT_Temp_PrintTemp = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Temp_PrintTemp_Approved";
            string strErrorCodeDefault = "WA_Temp_PrintTemp_Approved";

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
                    , objRQ_Temp_PrintTemp.GwUserCode // strGwUserCode
                    , objRQ_Temp_PrintTemp.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Temp_PrintTemp_Approved(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Temp_PrintTemp // objRQ_Temp_PrintTemp
                                            ////
                    , out objRT_Temp_PrintTemp // objRT_Temp_PrintTemp
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
                objRT_Temp_PrintTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Temp_PrintTemp>(objRT_Temp_PrintTemp);
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
                if (objRT_Temp_PrintTemp == null) objRT_Temp_PrintTemp = new RT_Temp_PrintTemp();
                objRT_Temp_PrintTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Temp_PrintTemp>(ex, objRT_Temp_PrintTemp);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Temp_PrintTemp> WA_Temp_PrintTemp_Save(RQ_Temp_PrintTemp objRQ_Temp_PrintTemp)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Temp_PrintTemp>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Temp_PrintTemp.Tid);
            RT_Temp_PrintTemp objRT_Temp_PrintTemp = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Temp_PrintTemp_Save";
            string strErrorCodeDefault = "WA_Temp_PrintTemp_Save";

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
                    , objRQ_Temp_PrintTemp.GwUserCode // strGwUserCode
                    , objRQ_Temp_PrintTemp.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Temp_PrintTemp_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Temp_PrintTemp // RQ_Temp_PrintTemp
                                           ////
                    , out objRT_Temp_PrintTemp // RT_Temp_PrintTemp
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
                objRT_Temp_PrintTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Temp_PrintTemp>(objRT_Temp_PrintTemp);
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
                if (objRT_Temp_PrintTemp == null) objRT_Temp_PrintTemp = new RT_Temp_PrintTemp();
                objRT_Temp_PrintTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Temp_PrintTemp>(ex, objRT_Temp_PrintTemp);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Temp_PrintTemp> WA_Temp_PrintTemp_Get(RQ_Temp_PrintTemp objRQ_Temp_PrintTemp)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Temp_PrintTemp>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Temp_PrintTemp.Tid);
            RT_Temp_PrintTemp objRT_Temp_PrintTemp = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Temp_PrintTemp_Get";
            string strErrorCodeDefault = "WA_Temp_PrintTemp_Get";

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
                    , objRQ_Temp_PrintTemp.GwUserCode // strGwUserCode
                    , objRQ_Temp_PrintTemp.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Temp_PrintTemp_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Temp_PrintTemp // RQ_Temp_PrintTemp
                                            ////
                    , out objRT_Temp_PrintTemp // RT_Temp_PrintTemp
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
                objRT_Temp_PrintTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Temp_PrintTemp>(objRT_Temp_PrintTemp);
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
                if (objRT_Temp_PrintTemp == null) objRT_Temp_PrintTemp = new RT_Temp_PrintTemp();
                objRT_Temp_PrintTemp.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Temp_PrintTemp>(ex, objRT_Temp_PrintTemp);
                #endregion
            }
        }
    }
}