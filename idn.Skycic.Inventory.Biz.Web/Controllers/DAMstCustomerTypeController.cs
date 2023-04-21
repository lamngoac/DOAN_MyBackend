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
    public class DAMstCustomerTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_CustomerType> WA_Mst_CustomerType_Get(DA_RQ_Mst_CustomerType objRQ_Mst_CustomerType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_CustomerType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerType.Tid);
            DA_RT_Mst_CustomerType objRT_Mst_CustomerType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerType_Get";
            string strErrorCodeDefault = "WA_Mst_CustomerType_Get";

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
                    , objRQ_Mst_CustomerType.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_CustomerType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerType
                    ////
                    , out objRT_Mst_CustomerType
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
                objRT_Mst_CustomerType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_CustomerType>(objRT_Mst_CustomerType);
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
                if (objRT_Mst_CustomerType == null) objRT_Mst_CustomerType = new DA_RT_Mst_CustomerType();
                objRT_Mst_CustomerType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_CustomerType>(ex, objRT_Mst_CustomerType);
                #endregion
            }
        }
    }
}
