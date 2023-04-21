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
    public class InvGenTimesCartonController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_GenTimesCarton> WA_Inv_GenTimesCarton_Get(RQ_Inv_GenTimesCarton objRQ_Inv_GenTimesCarton)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_GenTimesCarton>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_GenTimesCarton.Tid);
            RT_Inv_GenTimesCarton objRT_Inv_GenTimesCarton = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_GenTimesCarton_Get";
            string strErrorCodeDefault = "WA_Inv_GenTimesCarton_Get";

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
                    , objRQ_Inv_GenTimesCarton.GwUserCode // strGwUserCode
                    , objRQ_Inv_GenTimesCarton.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_GenTimesCarton_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_GenTimesCarton // objRQ_Inv_GenTimesCarton
                                            ////
                    , out objRT_Inv_GenTimesCarton // objRT_Inv_GenTimesCarton
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
                objRT_Inv_GenTimesCarton.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_GenTimesCarton>(objRT_Inv_GenTimesCarton);
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
                if (objRT_Inv_GenTimesCarton == null) objRT_Inv_GenTimesCarton = new RT_Inv_GenTimesCarton();
                objRT_Inv_GenTimesCarton.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_GenTimesCarton>(ex, objRT_Inv_GenTimesCarton);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_GenTimesCarton> WA_Inv_GenTimesCarton_Add(RQ_Inv_GenTimesCarton objRQ_Inv_GenTimesCarton)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_GenTimesCarton>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_GenTimesCarton.Tid);
            RT_Inv_GenTimesCarton objRT_Inv_GenTimesCarton = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_GenTimesCarton_Add";
            string strErrorCodeDefault = "WA_Inv_GenTimesCarton_Add";

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
                    , objRQ_Inv_GenTimesCarton.GwUserCode // strGwUserCode
                    , objRQ_Inv_GenTimesCarton.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_GenTimesCarton_Add(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_GenTimesCarton // objRQ_Inv_GenTimesCarton
                                            ////
                    , out objRT_Inv_GenTimesCarton // objRT_Inv_GenTimesCarton
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
                objRT_Inv_GenTimesCarton.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_GenTimesCarton>(objRT_Inv_GenTimesCarton);
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
                if (objRT_Inv_GenTimesCarton == null) objRT_Inv_GenTimesCarton = new RT_Inv_GenTimesCarton();
                objRT_Inv_GenTimesCarton.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_GenTimesCarton>(ex, objRT_Inv_GenTimesCarton);
                #endregion
            }
        }
    }
}