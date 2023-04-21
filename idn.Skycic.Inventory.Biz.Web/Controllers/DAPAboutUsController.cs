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
    public class DAPAboutUsController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_POW_AboutUs> WA_POW_AboutUs_Get(DA_RQ_POW_AboutUs objRQ_POW_AboutUs)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_POW_AboutUs>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_POW_AboutUs.Tid);
            DA_RT_POW_AboutUs objRT_POW_AboutUs = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_POW_AboutUs_Get";
            string strErrorCodeDefault = "WA_POW_AboutUs_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_AboutUs.GwUserCode // strGwUserCode
                    , objRQ_POW_AboutUs.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_AboutUs_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_POW_AboutUs // RQ_Mst_District
                                        ////
                    , out objRT_POW_AboutUs // RT_Mst_District
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
                objRT_POW_AboutUs.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_POW_AboutUs>(objRT_POW_AboutUs);
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
                if (objRT_POW_AboutUs == null) objRT_POW_AboutUs = new DA_RT_POW_AboutUs();
                objRT_POW_AboutUs.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_POW_AboutUs>(ex, objRT_POW_AboutUs);
                #endregion
            }
        }
    }
}
