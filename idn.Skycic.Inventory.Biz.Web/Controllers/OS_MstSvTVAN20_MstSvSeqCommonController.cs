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
    public class OS_MstSvTVAN20_MstSvSeqCommonController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_MstSvTVAN20_MstSv_Seq_Common> WA_OS_MstSvTVAN20_MstSv_Seq_Common_Get(RQ_OS_MstSvTVAN20_MstSv_Seq_Common objRQ_OS_MstSvTVAN20_MstSv_Seq_Common)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_MstSvTVAN20_MstSv_Seq_Common>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_MstSvTVAN20_MstSv_Seq_Common.Tid);
            RT_OS_MstSvTVAN20_MstSv_Seq_Common objRT_OS_MstSvTVAN20_MstSv_Seq_Common = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_MstSvTVAN20_MstSv_Seq_Common_Get";
            string strErrorCodeDefault = "WA_OS_MstSvTVAN20_MstSv_Seq_Common_Get";

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
                    , objRQ_OS_MstSvTVAN20_MstSv_Seq_Common.GwUserCode // strGwUserCode
                    , objRQ_OS_MstSvTVAN20_MstSv_Seq_Common.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_MstSvTVAN20_MstSv_Seq_Common_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_MstSvTVAN20_MstSv_Seq_Common // objRQ_Invoice_Invoice
                                            ////
                    , out objRT_OS_MstSvTVAN20_MstSv_Seq_Common // objRT_OS_MstSvTVAN20_MstSv_Seq_Common
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
                objRT_OS_MstSvTVAN20_MstSv_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_MstSvTVAN20_MstSv_Seq_Common>(objRT_OS_MstSvTVAN20_MstSv_Seq_Common);
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
                if (objRT_OS_MstSvTVAN20_MstSv_Seq_Common == null) objRT_OS_MstSvTVAN20_MstSv_Seq_Common = new RT_OS_MstSvTVAN20_MstSv_Seq_Common();
                objRT_OS_MstSvTVAN20_MstSv_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_MstSvTVAN20_MstSv_Seq_Common>(ex, objRT_OS_MstSvTVAN20_MstSv_Seq_Common);
                #endregion
            }
        }
    }
}