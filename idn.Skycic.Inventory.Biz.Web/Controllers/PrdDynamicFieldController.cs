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

using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class PrdDynamicFieldController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Prd_DynamicField> WA_Prd_DynamicField_Get(RQ_Prd_DynamicField objRQ_Prd_DynamicField)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_DynamicField.Tid);
            RT_Prd_DynamicField objRT_Prd_DynamicField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Prd_DynamicField_Get";
            string strErrorCodeDefault = "WA_Prd_DynamicField_Get";

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
                    , objRQ_Prd_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Prd_DynamicField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Prd_DynamicField_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Prd_DynamicField // objRQ_Prd_DynamicField
                                             ////
                    , out objRT_Prd_DynamicField // RT_Prd_DynamicField
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
                objRT_Prd_DynamicField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Prd_DynamicField>(objRT_Prd_DynamicField);
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
                if (objRT_Prd_DynamicField == null) objRT_Prd_DynamicField = new RT_Prd_DynamicField();
                objRT_Prd_DynamicField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Prd_DynamicField>(ex, objRT_Prd_DynamicField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Prd_DynamicField> WA_Prd_DynamicField_Save(RQ_Prd_DynamicField objRQ_Prd_DynamicField)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Prd_DynamicField.Tid);
            RT_Prd_DynamicField objRT_Prd_DynamicField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Prd_DynamicField_Save";
            string strErrorCodeDefault = "WA_Prd_DynamicField_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Prd_DynamicField", TJson.JsonConvert.SerializeObject(objRQ_Prd_DynamicField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Prd_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Prd_DynamicField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Prd_DynamicField_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Prd_DynamicField // objRQ_Prd_DynamicField
                                      // //
                    , out objRT_Prd_DynamicField // RT_Prd_DynamicField
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
                objRT_Prd_DynamicField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Prd_DynamicField>(objRT_Prd_DynamicField);
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
                if (objRT_Prd_DynamicField == null) objRT_Prd_DynamicField = new RT_Prd_DynamicField();
                objRT_Prd_DynamicField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Prd_DynamicField>(ex, objRT_Prd_DynamicField);
                #endregion
            }
        }
    }
}
