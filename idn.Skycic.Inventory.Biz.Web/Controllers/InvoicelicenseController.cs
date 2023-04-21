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
    public class InvoicelicenseController : ApiControllerBase
    {

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_license> WA_Invoice_license_Get(RQ_Invoice_license objRQ_Invoice_license)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_license>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_license.Tid);
            RT_Invoice_license objRT_Invoice_license = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_license_Get";
            string strErrorCodeDefault = "WA_Invoice_license_Get";

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
                    , objRQ_Invoice_license.GwUserCode // strGwUserCode
                    , objRQ_Invoice_license.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_license_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_license // RQ_Invoice_license
                                            ////
                    , out objRT_Invoice_license // RT_Invoice_license
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
                objRT_Invoice_license.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_license>(objRT_Invoice_license);
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
                if (objRT_Invoice_license == null) objRT_Invoice_license = new RT_Invoice_license();
                objRT_Invoice_license.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_license>(ex, objRT_Invoice_license);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_license> WA_Invoice_license_IncreaseQty(RQ_Invoice_license objRQ_Invoice_license)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_license>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_license.Tid);
            RT_Invoice_license objRT_Invoice_license = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_license_IncreaseQty";
            string strErrorCodeDefault = "WA_Invoice_license_IncreaseQty";

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
                    , objRQ_Invoice_license.GwUserCode // strGwUserCode
                    , objRQ_Invoice_license.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_license_IncreaseQty(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_license // RQ_Invoice_license
                                            ////
                    , out objRT_Invoice_license // RT_Invoice_license
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
                objRT_Invoice_license.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_license>(objRT_Invoice_license);
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
                if (objRT_Invoice_license == null) objRT_Invoice_license = new RT_Invoice_license();
                objRT_Invoice_license.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_license>(ex, objRT_Invoice_license);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_licenseCreHist> WA_Invoice_licenseCreHist_Get(RQ_Invoice_licenseCreHist objRQ_Invoice_licenseCreHist)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_licenseCreHist>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_licenseCreHist.Tid);
            RT_Invoice_licenseCreHist objRT_Invoice_licenseCreHist = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_licenseCreHist_Get";
            string strErrorCodeDefault = "WA_Invoice_licenseCreHist_Get";

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
                    , objRQ_Invoice_licenseCreHist.GwUserCode // strGwUserCode
                    , objRQ_Invoice_licenseCreHist.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_licenseCreHist_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_licenseCreHist // RQ_Invoice_licenseCreHist
                                            ////
                    , out objRT_Invoice_licenseCreHist // RT_Invoice_licenseCreHist
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
                objRT_Invoice_licenseCreHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_licenseCreHist>(objRT_Invoice_licenseCreHist);
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
                if (objRT_Invoice_licenseCreHist == null) objRT_Invoice_licenseCreHist = new RT_Invoice_licenseCreHist();
                objRT_Invoice_licenseCreHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_licenseCreHist>(ex, objRT_Invoice_licenseCreHist);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_licenseCreHist> WA_Invoice_licenseCreHist_GetAndSave(RQ_Invoice_licenseCreHist objRQ_Invoice_licenseCreHist)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_licenseCreHist>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_licenseCreHist.Tid);
            RT_Invoice_licenseCreHist objRT_Invoice_licenseCreHist = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_licenseCreHist_GetAndSave";
            string strErrorCodeDefault = "WA_Invoice_licenseCreHist_GetAndSave";

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
                    , objRQ_Invoice_licenseCreHist.GwUserCode // strGwUserCode
                    , objRQ_Invoice_licenseCreHist.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_licenseCreHist_GetAndSave(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_licenseCreHist // RQ_Invoice_licenseCreHist
                                                   ////
                    , out objRT_Invoice_licenseCreHist // RT_Invoice_licenseCreHist
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
                objRT_Invoice_licenseCreHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_licenseCreHist>(objRT_Invoice_licenseCreHist);
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
                if (objRT_Invoice_licenseCreHist == null) objRT_Invoice_licenseCreHist = new RT_Invoice_licenseCreHist();
                objRT_Invoice_licenseCreHist.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_licenseCreHist>(ex, objRT_Invoice_licenseCreHist);
                #endregion
            }
        }

    }
}