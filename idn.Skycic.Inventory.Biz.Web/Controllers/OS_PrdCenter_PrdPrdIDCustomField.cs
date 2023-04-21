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
    public class OS_PrdCenter_PrdPrdIDCustomFieldController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Prd_PrdIDCustomField> WA_OS_PrdCenter_Prd_PrdIDCustomField_Get(RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Prd_PrdIDCustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Tid);
            RT_OS_PrdCenter_Prd_PrdIDCustomField objRT_OS_PrdCenter_Prd_PrdIDCustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Prd_PrdIDCustomField_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Prd_PrdIDCustomField_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Prd_PrdIDCustomField", TJson.JsonConvert.SerializeObject(objRQ_Prd_PrdIDCustomField)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:

                mdsReturn = _biz.WAS_OS_PrdCenter_Prd_PrdIDCustomField_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField // objRQ_OS_PrdCenter_Prd_PrdIDCustomField
                                                              // //
                    , out objRT_OS_PrdCenter_Prd_PrdIDCustomField // objRT_OS_PrdCenter_Prd_PrdIDCustomField
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
                objRT_OS_PrdCenter_Prd_PrdIDCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Prd_PrdIDCustomField>(objRT_OS_PrdCenter_Prd_PrdIDCustomField);
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
                if (objRT_OS_PrdCenter_Prd_PrdIDCustomField == null) objRT_OS_PrdCenter_Prd_PrdIDCustomField = new RT_OS_PrdCenter_Prd_PrdIDCustomField();
                objRT_OS_PrdCenter_Prd_PrdIDCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Prd_PrdIDCustomField>(ex, objRT_OS_PrdCenter_Prd_PrdIDCustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Prd_PrdIDCustomField> WA_OS_PrdCenter_Prd_PrdIDCustomField_Update(RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Prd_PrdIDCustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Prd_PrdIDCustomField.Tid);
            RT_OS_PrdCenter_Prd_PrdIDCustomField objRT_OS_PrdCenter_Prd_PrdIDCustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Prd_PrdIDCustomField_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Prd_PrdIDCustomField_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Prd_PrdIDCustomField", TJson.JsonConvert.SerializeObject(objRQ_Prd_PrdIDCustomField)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Prd_PrdIDCustomField_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Prd_PrdIDCustomField // objRQ_Prd_PrdIDCustomField
                                                              // //
                    , out objRT_OS_PrdCenter_Prd_PrdIDCustomField // RT_Prd_PrdIDCustomField
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
                objRT_OS_PrdCenter_Prd_PrdIDCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Prd_PrdIDCustomField>(objRT_OS_PrdCenter_Prd_PrdIDCustomField);
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
                if (objRT_OS_PrdCenter_Prd_PrdIDCustomField == null) objRT_OS_PrdCenter_Prd_PrdIDCustomField = new RT_OS_PrdCenter_Prd_PrdIDCustomField();
                objRT_OS_PrdCenter_Prd_PrdIDCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Prd_PrdIDCustomField>(ex, objRT_OS_PrdCenter_Prd_PrdIDCustomField);
                #endregion
            }
        }

    }
}
