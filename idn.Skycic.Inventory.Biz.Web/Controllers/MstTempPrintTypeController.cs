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
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstTempPrintTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_TempPrintType> WA_Mst_TempPrintType_Get(RQ_Mst_TempPrintType objRQ_Mst_TempPrintType)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TempPrintType.Tid);
            RT_Mst_TempPrintType objRT_Mst_TempPrintType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TempPrintType_Get";
            string strErrorCodeDefault = "WA_Mst_TempPrintType_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_TempPrintType", TJson.JsonConvert.SerializeObject(objRQ_Mst_TempPrintType)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TempPrintType.GwUserCode // strGwUserCode
                    , objRQ_Mst_TempPrintType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_TempPrintType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TempPrintType // objRQ_Mst_TempPrintType
                                              // //
                    , out objRT_Mst_TempPrintType // RT_Mst_TempPrintType
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
                objRT_Mst_TempPrintType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_TempPrintType>(objRT_Mst_TempPrintType);
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
                if (objRT_Mst_TempPrintType == null) objRT_Mst_TempPrintType = new RT_Mst_TempPrintType();
                objRT_Mst_TempPrintType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_TempPrintType>(ex, objRT_Mst_TempPrintType);
                #endregion
            }
        }
    }
}