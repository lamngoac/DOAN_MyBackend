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
    public class MstCustomerSourceController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerSource> WA_Mst_CustomerSource_Save(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerSource.Tid);
            RT_Mst_CustomerSource objRT_Mst_CustomerSource = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerSource_Save";
            string strErrorCodeDefault = "WA_Mst_CustomerSource_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerSource", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerSource)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerSource.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerSource.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerSource_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerSource // objRQ_Mst_CustomerSource
                                              // //
                    , out objRT_Mst_CustomerSource // RT_Mst_CustomerSource
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
                objRT_Mst_CustomerSource.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerSource>(objRT_Mst_CustomerSource);
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
                if (objRT_Mst_CustomerSource == null) objRT_Mst_CustomerSource = new RT_Mst_CustomerSource();
                objRT_Mst_CustomerSource.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerSource>(ex, objRT_Mst_CustomerSource);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerSource> WA_Mst_CustomerSource_Get(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerSource.Tid);
            RT_Mst_CustomerSource objRT_Mst_CustomerSource = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerSource_Get";
            string strErrorCodeDefault = "WA_Mst_CustomerSource_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerSource", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerSource)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerSource.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerSource.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerSource_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerSource // objRQ_Mst_CustomerSource
                                               // //
                    , out objRT_Mst_CustomerSource // RT_Mst_CustomerSource
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
                objRT_Mst_CustomerSource.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerSource>(objRT_Mst_CustomerSource);
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
                if (objRT_Mst_CustomerSource == null) objRT_Mst_CustomerSource = new RT_Mst_CustomerSource();
                objRT_Mst_CustomerSource.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerSource>(ex, objRT_Mst_CustomerSource);
                #endregion
            }
        }
    }
}
