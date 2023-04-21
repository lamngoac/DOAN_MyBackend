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
    public class CustomerDynamicFieldController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Customer_DynamicField> WA_Customer_DynamicField_Save(RQ_Customer_DynamicField objRQ_Customer_DynamicField)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Customer_DynamicField.Tid);
            RT_Customer_DynamicField objRT_Customer_DynamicField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Customer_DynamicField_Save";
            string strErrorCodeDefault = "WA_Customer_DynamicField_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Customer_DynamicField", TJson.JsonConvert.SerializeObject(objRQ_Customer_DynamicField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Customer_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Customer_DynamicField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Customer_DynamicField_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Customer_DynamicField // objRQ_Customer_DynamicField
                                                        // //
                    , out objRT_Customer_DynamicField // RT_Customer_DynamicField
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
                objRT_Customer_DynamicField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Customer_DynamicField>(objRT_Customer_DynamicField);
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
                if (objRT_Customer_DynamicField == null) objRT_Customer_DynamicField = new RT_Customer_DynamicField();
                objRT_Customer_DynamicField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Customer_DynamicField>(ex, objRT_Customer_DynamicField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Customer_DynamicField> WA_Customer_DynamicField_Get(RQ_Customer_DynamicField objRQ_Customer_DynamicField)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Customer_DynamicField.Tid);
            RT_Customer_DynamicField objRT_Customer_DynamicField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Customer_DynamicField_Get";
            string strErrorCodeDefault = "WA_Customer_DynamicField_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Customer_DynamicField", TJson.JsonConvert.SerializeObject(objRQ_Customer_DynamicField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Customer_DynamicField.GwUserCode // strGwUserCode
                    , objRQ_Customer_DynamicField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Customer_DynamicField_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Customer_DynamicField // objRQ_Customer_DynamicField
                                                  // //
                    , out objRT_Customer_DynamicField // RT_Customer_DynamicField
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
                objRT_Customer_DynamicField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Customer_DynamicField>(objRT_Customer_DynamicField);
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
                if (objRT_Customer_DynamicField == null) objRT_Customer_DynamicField = new RT_Customer_DynamicField();
                objRT_Customer_DynamicField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Customer_DynamicField>(ex, objRT_Customer_DynamicField);
                #endregion
            }
        }
    }
}
