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
    public class DAMstProvinceController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Province> WA_Mst_Province_Get(DA_RQ_Mst_Province objRQ_Mst_Province)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Province>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Province.Tid);
            DA_RT_Mst_Province objRT_Mst_Province = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Province_Get";
            string strErrorCodeDefault = "WA_Mst_Province_Get";

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
                    , objRQ_Mst_Province.GwUserCode // strGwUserCode
                    , objRQ_Mst_Province.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_Province_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Province // RQ_Mst_District
                                         ////
                    , out objRT_Mst_Province // RT_Mst_District
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
                objRT_Mst_Province.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Province>(objRT_Mst_Province);
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
                if (objRT_Mst_Province == null) objRT_Mst_Province = new DA_RT_Mst_Province();
                objRT_Mst_Province.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Province>(ex, objRT_Mst_Province);
                #endregion
            }
        }
    }
}
