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
    public class InvoiceDtlCustomFieldController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_DtlCustomField> WA_Invoice_DtlCustomField_Get(RQ_Invoice_DtlCustomField objRQ_Invoice_DtlCustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_DtlCustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_DtlCustomField.Tid);
            RT_Invoice_DtlCustomField objRT_Invoice_DtlCustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_DtlCustomField_Get";
            string strErrorCodeDefault = "WA_Invoice_DtlCustomField_Get";

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
                    , objRQ_Invoice_DtlCustomField.GwUserCode // strGwUserCode
                    , objRQ_Invoice_DtlCustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_DtlCustomField_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_DtlCustomField // RQ_Invoice_DtlCustomField
                                                ////
                    , out objRT_Invoice_DtlCustomField // RT_Invoice_DtlCustomField
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
                objRT_Invoice_DtlCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_DtlCustomField>(objRT_Invoice_DtlCustomField);
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
                if (objRT_Invoice_DtlCustomField == null) objRT_Invoice_DtlCustomField = new RT_Invoice_DtlCustomField();
                objRT_Invoice_DtlCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_DtlCustomField>(ex, objRT_Invoice_DtlCustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_DtlCustomField> WA_Invoice_DtlCustomField_Create(RQ_Invoice_DtlCustomField objRQ_Invoice_DtlCustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_DtlCustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_DtlCustomField.Tid);
            RT_Invoice_DtlCustomField objRT_Invoice_DtlCustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_DtlCustomField_Create";
            string strErrorCodeDefault = "WA_Invoice_DtlCustomField_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_DtlCustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_DtlCustomField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_DtlCustomField.GwUserCode // strGwUserCode
                    , objRQ_Invoice_DtlCustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_DtlCustomField_Create_New20190703(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_DtlCustomField // objRQ_Invoice_DtlCustomField
                                                ////
                    , out objRT_Invoice_DtlCustomField // RT_Invoice_DtlCustomField
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
                objRT_Invoice_DtlCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_DtlCustomField>(objRT_Invoice_DtlCustomField);
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
                if (objRT_Invoice_DtlCustomField == null) objRT_Invoice_DtlCustomField = new RT_Invoice_DtlCustomField();
                objRT_Invoice_DtlCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_DtlCustomField>(ex, objRT_Invoice_DtlCustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_DtlCustomField> WA_Invoice_DtlCustomField_Update(RQ_Invoice_DtlCustomField objRQ_Invoice_DtlCustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_DtlCustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_DtlCustomField.Tid);
            RT_Invoice_DtlCustomField objRT_Invoice_DtlCustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_DtlCustomField_Update";
            string strErrorCodeDefault = "WA_Invoice_DtlCustomField_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_DtlCustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_DtlCustomField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_DtlCustomField.GwUserCode // strGwUserCode
                    , objRQ_Invoice_DtlCustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_DtlCustomField_Update_New20190703(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_DtlCustomField // objRQ_Invoice_DtlCustomField
                                                ////
                    , out objRT_Invoice_DtlCustomField // objRT_Invoice_DtlCustomField
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
                objRT_Invoice_DtlCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_DtlCustomField>(objRT_Invoice_DtlCustomField);
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
                if (objRT_Invoice_DtlCustomField == null) objRT_Invoice_DtlCustomField = new RT_Invoice_DtlCustomField();
                objRT_Invoice_DtlCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_DtlCustomField>(ex, objRT_Invoice_DtlCustomField);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_DtlCustomField> WA_Invoice_DtlCustomField_Delete(RQ_Invoice_DtlCustomField objRQ_Invoice_DtlCustomField)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_DtlCustomField>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_DtlCustomField.Tid);
            RT_Invoice_DtlCustomField objRT_Invoice_DtlCustomField = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_DtlCustomField_Delete";
            string strErrorCodeDefault = "WA_Invoice_DtlCustomField_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_DtlCustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_DtlCustomField)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_DtlCustomField.GwUserCode // strGwUserCode
                    , objRQ_Invoice_DtlCustomField.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_DtlCustomField_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_DtlCustomField // objRQ_Invoice_DtlCustomField
                                                ////
                    , out objRT_Invoice_DtlCustomField // objRT_Invoice_DtlCustomField
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
                objRT_Invoice_DtlCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_DtlCustomField>(objRT_Invoice_DtlCustomField);
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
                if (objRT_Invoice_DtlCustomField == null) objRT_Invoice_DtlCustomField = new RT_Invoice_DtlCustomField();
                objRT_Invoice_DtlCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_DtlCustomField>(ex, objRT_Invoice_DtlCustomField);
                #endregion
            }
        }
    }
}
