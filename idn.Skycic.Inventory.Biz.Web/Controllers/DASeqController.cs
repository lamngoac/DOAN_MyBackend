using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using System.Collections;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class DASeqController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Seq_Common> WA_Seq_Common_Get(DA_RQ_Seq_Common objRQ_Seq_Common)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Seq_Common>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
            DA_RT_Seq_Common objRT_Seq_Common = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Seq_Common_Get";
            string strErrorCodeDefault = "WA_Seq_Common_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_Common.GwUserCode // strGwUserCode
                    , objRQ_Seq_Common.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DASeq_Common_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_Common // objRQ_Seq_Common
                                       ////
                    , out objRT_Seq_Common // RT_Seq_Common
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
                objRT_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Seq_Common>(objRT_Seq_Common);
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
                if (objRT_Seq_Common == null) objRT_Seq_Common = new DA_RT_Seq_Common();
                objRT_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Seq_Common>(ex, objRT_Seq_Common);
                #endregion
            }
        }
    }
}
