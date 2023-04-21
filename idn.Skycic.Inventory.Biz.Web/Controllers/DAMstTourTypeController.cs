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
    public class DAMstTourTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_TourType> WA_Mst_TourType_Get(DA_RQ_Mst_TourType objRQ_Mst_TourType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_TourType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TourType.Tid);
            DA_RT_Mst_TourType objRT_Mst_TourType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TourType_Get";
            string strErrorCodeDefault = "WA_Mst_TourType_Get";

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
                    , objRQ_Mst_TourType.GwUserCode // strGwUserCode
                    , objRQ_Mst_TourType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_TourType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TourType 
                    ////
                    , out objRT_Mst_TourType 
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
                objRT_Mst_TourType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_TourType>(objRT_Mst_TourType);
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
                if (objRT_Mst_TourType == null) objRT_Mst_TourType = new DA_RT_Mst_TourType();
                objRT_Mst_TourType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_TourType>(ex, objRT_Mst_TourType);
                #endregion
            }
        }
    }
}
