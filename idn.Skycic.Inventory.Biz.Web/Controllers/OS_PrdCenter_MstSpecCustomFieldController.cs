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
    public class OS_PrdCenter_MstSpecCustomFieldController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecCustomField> WA_OS_PrdCenter_Mst_SpecCustomField_Get(RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecCustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecCustomField.Tid);
            RT_OS_PrdCenter_Mst_SpecCustomField objRT_OS_PrdCenter_Mst_SpecCustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecCustomField_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecCustomField_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecCustomField", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecCustomField)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:

                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecCustomField_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField // objRQ_OS_PrdCenter_Mst_SpecCustomField
                    // //
                    , out objRT_OS_PrdCenter_Mst_SpecCustomField // objRT_OS_PrdCenter_Mst_SpecCustomField
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
                objRT_OS_PrdCenter_Mst_SpecCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecCustomField>(objRT_OS_PrdCenter_Mst_SpecCustomField);
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
                if (objRT_OS_PrdCenter_Mst_SpecCustomField == null) objRT_OS_PrdCenter_Mst_SpecCustomField = new RT_OS_PrdCenter_Mst_SpecCustomField();
                objRT_OS_PrdCenter_Mst_SpecCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecCustomField>(ex, objRT_OS_PrdCenter_Mst_SpecCustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecCustomField> WA_OS_PrdCenter_Mst_SpecCustomField_Update(RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecCustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecCustomField.Tid);
            RT_OS_PrdCenter_Mst_SpecCustomField objRT_OS_PrdCenter_Mst_SpecCustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecCustomField_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecCustomField_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecCustomField", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecCustomField)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecCustomField_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecCustomField // objRQ_Mst_SpecCustomField
                                                   // //
                    , out objRT_OS_PrdCenter_Mst_SpecCustomField // RT_Mst_SpecCustomField
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
                objRT_OS_PrdCenter_Mst_SpecCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecCustomField>(objRT_OS_PrdCenter_Mst_SpecCustomField);
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
                if (objRT_OS_PrdCenter_Mst_SpecCustomField == null) objRT_OS_PrdCenter_Mst_SpecCustomField = new RT_OS_PrdCenter_Mst_SpecCustomField();
                objRT_OS_PrdCenter_Mst_SpecCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecCustomField>(ex, objRT_OS_PrdCenter_Mst_SpecCustomField);
                #endregion
            }
        }

    }
}
