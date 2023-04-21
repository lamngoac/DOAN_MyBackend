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
    public class iNOSMstBizTypeController : ApiControllerBase
    {

        [AcceptVerbs("POST")]
        public ServiceResult<RT_iNOS_Mst_BizType> WA_iNOS_Mst_BizType_Get(RQ_iNOS_Mst_BizType objRQ_iNOS_Mst_BizType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_iNOS_Mst_BizType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_iNOS_Mst_BizType.Tid);
            RT_iNOS_Mst_BizType objRT_iNOS_Mst_BizType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_iNOS_Mst_BizType_Get";
            string strErrorCodeDefault = "WA_iNOS_Mst_BizType_Get";

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
                    , objRQ_iNOS_Mst_BizType.GwUserCode // strGwUserCode
                    , objRQ_iNOS_Mst_BizType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_iNOS_Mst_BizType_Get_New20190813(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_iNOS_Mst_BizType // RQ_iNOS_Mst_BizType
                                             ////
                    , out objRT_iNOS_Mst_BizType // RT_iNOS_Mst_BizType
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
                objRT_iNOS_Mst_BizType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_iNOS_Mst_BizType>(objRT_iNOS_Mst_BizType);
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
                if (objRT_iNOS_Mst_BizType == null) objRT_iNOS_Mst_BizType = new RT_iNOS_Mst_BizType();
                objRT_iNOS_Mst_BizType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_iNOS_Mst_BizType>(ex, objRT_iNOS_Mst_BizType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_iNOS_Mst_BizType> WA_RptSv_iNOS_Mst_BizType_Get(RQ_iNOS_Mst_BizType objRQ_iNOS_Mst_BizType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_iNOS_Mst_BizType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_iNOS_Mst_BizType.Tid);
            RT_iNOS_Mst_BizType objRT_iNOS_Mst_BizType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_iNOS_Mst_BizType_Get";
            string strErrorCodeDefault = "WA_RptSv_iNOS_Mst_BizType_Get";

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
                    , objRQ_iNOS_Mst_BizType.GwUserCode // strGwUserCode
                    , objRQ_iNOS_Mst_BizType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_iNOS_Mst_BizType_Get_New20190813(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_iNOS_Mst_BizType // RQ_iNOS_Mst_BizType
                                             ////
                    , out objRT_iNOS_Mst_BizType // RT_iNOS_Mst_BizType
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
                objRT_iNOS_Mst_BizType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_iNOS_Mst_BizType>(objRT_iNOS_Mst_BizType);
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
                if (objRT_iNOS_Mst_BizType == null) objRT_iNOS_Mst_BizType = new RT_iNOS_Mst_BizType();
                objRT_iNOS_Mst_BizType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_iNOS_Mst_BizType>(ex, objRT_iNOS_Mst_BizType);
                #endregion
            }
        }

    }
}