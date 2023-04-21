using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstCustomerInAreaController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerInArea> WA_Mst_CustomerInArea_Save(RQ_Mst_CustomerInArea objRQ_Mst_CustomerInArea)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerInArea.Tid);
            RT_Mst_CustomerInArea objRT_Mst_CustomerInArea = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerInArea_Save";
            string strErrorCodeDefault = "WA_Mst_CustomerInArea_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerInArea", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerInArea)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerInArea.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerInArea.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerInArea_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerInArea // objRQ_Mst_CustomerInArea
                                               // //
                    , out objRT_Mst_CustomerInArea // RT_Mst_CustomerInArea
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
                objRT_Mst_CustomerInArea.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerInArea>(objRT_Mst_CustomerInArea);
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
                if (objRT_Mst_CustomerInArea == null) objRT_Mst_CustomerInArea = new RT_Mst_CustomerInArea();
                objRT_Mst_CustomerInArea.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerInArea>(ex, objRT_Mst_CustomerInArea);
                #endregion
            }
        }

    }
}
