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
    public class DAPNewsTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_PowNewsType> WA_POW_NewsType_Get(DA_RQ_PowNewsType objRQ_PowNewsType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_PowNewsType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_PowNewsType.Tid);
            DA_RT_PowNewsType objRT_PowNewsType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_PowNewsType_Get";
            string strErrorCodeDefault = "WA_PowNewsType_Get";

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
                    , objRQ_PowNewsType.GwUserCode // strGwUserCode
                    , objRQ_PowNewsType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAPOW_NewsType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_PowNewsType // RQ_Mst_District
                                        ////
                    , out objRT_PowNewsType // RT_Mst_District
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
                objRT_PowNewsType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_PowNewsType>(objRT_PowNewsType);
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
                if (objRT_PowNewsType == null) objRT_PowNewsType = new DA_RT_PowNewsType();
                objRT_PowNewsType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_PowNewsType>(ex, objRT_PowNewsType);
                #endregion
            }
        }
    }
}
