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
    public class InvoiceCustomFieldController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_CustomField> WA_Invoice_CustomField_Get(RQ_Invoice_CustomField objRQ_Invoice_CustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_CustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_CustomField.Tid);
            RT_Invoice_CustomField objRT_Invoice_CustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_CustomField_Get";
            string strErrorCodeDefault = "WA_Invoice_CustomField_Get";

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
                    , objRQ_Invoice_CustomField.GwUserCode // strGwUserCode
                    , objRQ_Invoice_CustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_CustomField_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_CustomField // RQ_Invoice_CustomField
                                         ////
                    , out objRT_Invoice_CustomField // RT_Invoice_CustomField
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
                objRT_Invoice_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_CustomField>(objRT_Invoice_CustomField);
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
                if (objRT_Invoice_CustomField == null) objRT_Invoice_CustomField = new RT_Invoice_CustomField();
                objRT_Invoice_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_CustomField>(ex, objRT_Invoice_CustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_CustomField> WA_Invoice_CustomField_Create(RQ_Invoice_CustomField objRQ_Invoice_CustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_CustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_CustomField.Tid);
            RT_Invoice_CustomField objRT_Invoice_CustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_CustomField_Create";
            string strErrorCodeDefault = "WA_Invoice_CustomField_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_CustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_CustomField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_CustomField.GwUserCode // strGwUserCode
                    , objRQ_Invoice_CustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_CustomField_Create_New20190703(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_CustomField // objRQ_Invoice_CustomField
                                         ////
                    , out objRT_Invoice_CustomField // RT_Invoice_CustomField
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
                objRT_Invoice_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_CustomField>(objRT_Invoice_CustomField);
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
                if (objRT_Invoice_CustomField == null) objRT_Invoice_CustomField = new RT_Invoice_CustomField();
                objRT_Invoice_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_CustomField>(ex, objRT_Invoice_CustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_CustomField> WA_Invoice_CustomField_Update(RQ_Invoice_CustomField objRQ_Invoice_CustomField)
         {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_CustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_CustomField.Tid);
            RT_Invoice_CustomField objRT_Invoice_CustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_CustomField_Update";
            string strErrorCodeDefault = "WA_Invoice_CustomField_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_CustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_CustomField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_CustomField.GwUserCode // strGwUserCode
                    , objRQ_Invoice_CustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_CustomField_Update_New20190703(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_CustomField // objRQ_Invoice_CustomField
                                         ////
                    , out objRT_Invoice_CustomField // objRT_Invoice_CustomField
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
                objRT_Invoice_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_CustomField>(objRT_Invoice_CustomField);
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
                if (objRT_Invoice_CustomField == null) objRT_Invoice_CustomField = new RT_Invoice_CustomField();
                objRT_Invoice_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_CustomField>(ex, objRT_Invoice_CustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_CustomField> WA_Invoice_CustomField_Delete(RQ_Invoice_CustomField objRQ_Invoice_CustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_CustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_CustomField.Tid);
            RT_Invoice_CustomField objRT_Invoice_CustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_CustomField_Delete";
            string strErrorCodeDefault = "WA_Invoice_CustomField_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_CustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_CustomField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_CustomField.GwUserCode // strGwUserCode
                    , objRQ_Invoice_CustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_CustomField_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_CustomField // objRQ_Invoice_CustomField
                                         ////
                    , out objRT_Invoice_CustomField // objRT_Invoice_CustomField
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
                objRT_Invoice_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_CustomField>(objRT_Invoice_CustomField);
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
                if (objRT_Invoice_CustomField == null) objRT_Invoice_CustomField = new RT_Invoice_CustomField();
                objRT_Invoice_CustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_CustomField>(ex, objRT_Invoice_CustomField);
                #endregion
            }
        }
    }
}
