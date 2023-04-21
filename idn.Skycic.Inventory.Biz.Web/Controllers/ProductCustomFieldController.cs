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
    public class ProductCustomFieldController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Product_CustomField> WA_Product_CustomField_Get(RQ_Product_CustomField objRQ_Product_CustomField)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Product_CustomField.Tid);
            RT_Product_CustomField objRT_Product_CustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Product_CustomField_Get";
            string strErrorCodeDefault = "WA_Product_CustomField_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Product_CustomField", TJson.JsonConvert.SerializeObject(objRQ_Product_CustomField)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Product_CustomField.GwUserCode // strGwUserCode
                    , objRQ_Product_CustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Product_CustomField_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Product_CustomField // objRQ_Product_CustomField
                                                // //
                    , out objRT_Product_CustomField // RT_Product_CustomField
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
                objRT_Product_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Product_CustomField>(objRT_Product_CustomField);
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
                if (objRT_Product_CustomField == null) objRT_Product_CustomField = new RT_Product_CustomField();
                objRT_Product_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Product_CustomField>(ex, objRT_Product_CustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Product_CustomField> WA_Product_CustomField_Save(RQ_Product_CustomField objRQ_Product_CustomField)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Product_CustomField.Tid);
            RT_Product_CustomField objRT_Product_CustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Product_CustomField_Save";
            string strErrorCodeDefault = "WA_Product_CustomField_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Product_CustomField", TJson.JsonConvert.SerializeObject(objRQ_Product_CustomField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Product_CustomField.GwUserCode // strGwUserCode
                    , objRQ_Product_CustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Product_CustomField_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Product_CustomField // objRQ_Product_CustomField
                                                // //
                    , out objRT_Product_CustomField // RT_Product_CustomField
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
                objRT_Product_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Product_CustomField>(objRT_Product_CustomField);
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
                if (objRT_Product_CustomField == null) objRT_Product_CustomField = new RT_Product_CustomField();
                objRT_Product_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Product_CustomField>(ex, objRT_Product_CustomField);
                #endregion
            }
        }
    }
}
