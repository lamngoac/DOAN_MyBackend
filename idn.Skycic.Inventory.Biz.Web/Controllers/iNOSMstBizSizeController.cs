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
    public class iNOSMstBizSizeController : ApiControllerBase
    {

        [AcceptVerbs("POST")]
        public ServiceResult<RT_iNOS_Mst_BizSize> WA_iNOS_Mst_BizSize_Get(RQ_iNOS_Mst_BizSize objRQ_iNOS_Mst_BizSize)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_iNOS_Mst_BizSize>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_iNOS_Mst_BizSize.Tid);
            RT_iNOS_Mst_BizSize objRT_iNOS_Mst_BizSize = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_iNOS_Mst_BizSize_Get";
            string strErrorCodeDefault = "WA_iNOS_Mst_BizSize_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Currency", TJson.JsonConvert.SerializeObject(objRQ_Mst_Currency)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_iNOS_Mst_BizSize.GwUserCode // strGwUserCode
                    , objRQ_iNOS_Mst_BizSize.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_iNOS_Mst_BizSize_Get_New20190813(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_iNOS_Mst_BizSize // RQ_iNOS_Mst_BizSize
                                             ////
                    , out objRT_iNOS_Mst_BizSize // RT_iNOS_Mst_BizSize
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
                objRT_iNOS_Mst_BizSize.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_iNOS_Mst_BizSize>(objRT_iNOS_Mst_BizSize);
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
                if (objRT_iNOS_Mst_BizSize == null) objRT_iNOS_Mst_BizSize = new RT_iNOS_Mst_BizSize();
                objRT_iNOS_Mst_BizSize.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_iNOS_Mst_BizSize>(ex, objRT_iNOS_Mst_BizSize);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_iNOS_Mst_BizSize> WA_RptSv_iNOS_Mst_BizSize_Get(RQ_iNOS_Mst_BizSize objRQ_iNOS_Mst_BizSize)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_iNOS_Mst_BizSize>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_iNOS_Mst_BizSize.Tid);
            RT_iNOS_Mst_BizSize objRT_iNOS_Mst_BizSize = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_iNOS_Mst_BizSize_Get";
            string strErrorCodeDefault = "WA_iNOS_Mst_BizSize_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Currency", TJson.JsonConvert.SerializeObject(objRQ_Mst_Currency)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_iNOS_Mst_BizSize.GwUserCode // strGwUserCode
                    , objRQ_iNOS_Mst_BizSize.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_iNOS_Mst_BizSize_Get_New20190813(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_iNOS_Mst_BizSize // RQ_iNOS_Mst_BizSize
                                             ////
                    , out objRT_iNOS_Mst_BizSize // RT_iNOS_Mst_BizSize
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
                objRT_iNOS_Mst_BizSize.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_iNOS_Mst_BizSize>(objRT_iNOS_Mst_BizSize);
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
                if (objRT_iNOS_Mst_BizSize == null) objRT_iNOS_Mst_BizSize = new RT_iNOS_Mst_BizSize();
                objRT_iNOS_Mst_BizSize.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_iNOS_Mst_BizSize>(ex, objRT_iNOS_Mst_BizSize);
                #endregion
            }
        }

    }
}