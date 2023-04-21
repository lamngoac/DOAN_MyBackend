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
    public class MstWardController : ApiControllerBase
    {

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Ward> WA_Mst_Ward_Get(RQ_Mst_Ward objRQ_Mst_Ward)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Ward.Tid);
            RT_Mst_Ward objRT_Mst_Ward = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Ward_Get";
            string strErrorCodeDefault = "WA_Mst_Ward_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Ward", TJson.JsonConvert.SerializeObject(objRQ_Mst_Ward)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Ward.GwUserCode // strGwUserCode
                    , objRQ_Mst_Ward.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Ward_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Ward // objRQ_Mst_Ward
                                     // //
                    , out objRT_Mst_Ward // RT_Mst_Ward
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
                objRT_Mst_Ward.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Ward>(objRT_Mst_Ward);
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
                if (objRT_Mst_Ward == null) objRT_Mst_Ward = new RT_Mst_Ward();
                objRT_Mst_Ward.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Ward>(ex, objRT_Mst_Ward);
                #endregion
            }
        }
    }
}
