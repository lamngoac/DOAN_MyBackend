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
    public class MstGovIDTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovIDType> WA_Mst_GovIDType_Get(RQ_Mst_GovIDType objRQ_Mst_GovIDType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovIDType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovIDType.Tid);
            RT_Mst_GovIDType objRT_Mst_GovIDType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_GovIDType_Get";
            string strErrorCodeDefault = "WA_Mst_GovIDType_Get";

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
                    , objRQ_Mst_GovIDType.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovIDType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_GovIDType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovIDType // objRQ_Mst_GovIDType
                                           ////
                    , out objRT_Mst_GovIDType // RT_Mst_GovIDType
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
                objRT_Mst_GovIDType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovIDType>(objRT_Mst_GovIDType);
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
                if (objRT_Mst_GovIDType == null) objRT_Mst_GovIDType = new RT_Mst_GovIDType();
                objRT_Mst_GovIDType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovIDType>(ex, objRT_Mst_GovIDType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_GovIDType> WA_RptSv_Mst_GovIDType_Get(RQ_Mst_GovIDType objRQ_Mst_GovIDType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_GovIDType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_GovIDType.Tid);
            RT_Mst_GovIDType objRT_Mst_GovIDType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_GovIDType_Get";
            string strErrorCodeDefault = "WA_RptSv_Mst_GovIDType_Get";

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
                    , objRQ_Mst_GovIDType.GwUserCode // strGwUserCode
                    , objRQ_Mst_GovIDType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_GovIDType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_GovIDType // objRQ_Mst_GovIDType
                                          ////
                    , out objRT_Mst_GovIDType // RT_Mst_GovIDType
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
                objRT_Mst_GovIDType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_GovIDType>(objRT_Mst_GovIDType);
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
                if (objRT_Mst_GovIDType == null) objRT_Mst_GovIDType = new RT_Mst_GovIDType();
                objRT_Mst_GovIDType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_GovIDType>(ex, objRT_Mst_GovIDType);
                #endregion
            }
        }

    }
}