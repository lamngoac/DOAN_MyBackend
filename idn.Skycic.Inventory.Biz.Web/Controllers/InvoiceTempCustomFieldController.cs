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
    public class InvoiceTempCustomFieldController : ApiControllerBase
    {
    //    [AcceptVerbs("POST")]
    //    public ServiceResult<RT_Invoice_TempCustomField> WA_Invoice_TempCustomField_Get(RQ_Invoice_TempCustomField objRQ_Invoice_TempCustomField)
    //    {
    //        #region // Temp:
    //        if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempCustomField>();
    //        DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempCustomField.Tid);
    //        RT_Invoice_TempCustomField objRT_Invoice_TempCustomField = null;
    //        DateTime dtimeSys = DateTime.Now;
    //        string strControllerName = "WA_Invoice_TempCustomField_Get";
    //        string strErrorCodeDefault = "WA_Invoice_TempCustomField_Get";

    //        ArrayList alParamsCoupleError = new ArrayList();
    //        alParamsCoupleError.AddRange(new object[] {
    //            "strControllerName", strControllerName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				////, "_biz.RQ_Mst_Currency", TJson.JsonConvert.SerializeObject(objRQ_Mst_Currency)
				//////
				//});
    //        #endregion

    //        try
    //        {
    //            #region // CheckGatewayAuthentication:
    //            TUtils.CConnectionManager.CheckGatewayAuthentication(
    //                _biz._cf.nvcParams // nvcParams
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                , objRQ_Invoice_TempCustomField.GwUserCode // strGwUserCode
    //                , objRQ_Invoice_TempCustomField.GwPassword // strGwPassword
    //                );
    //            #endregion

    //            #region // Process:
    //            //
    //            mdsReturn = _biz.WAS_Invoice_TempCustomField_Get(
    //                ref alParamsCoupleError // alParamsCoupleError
    //                , objRQ_Invoice_TempCustomField // RQ_Invoice_TempCustomField
    //                                            ////
    //                , out objRT_Invoice_TempCustomField // RT_Invoice_TempCustomField
    //                );

    //            if (CmUtils.CMyDataSet.HasError(mdsReturn))
    //            {
    //                throw CmUtils.CMyException.Raise(
    //                    (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
    //                    , null
    //                    , null
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            objRT_Invoice_TempCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
    //            return Success<RT_Invoice_TempCustomField>(objRT_Invoice_TempCustomField);
    //        }
    //        catch (Exception ex)
    //        {
    //            #region // Catch of try:
    //            ////
    //            TUtils.CProcessExc.Process(
    //                ref mdsReturn // mdsFinal
    //                , ex // exc
    //                , strErrorCodeDefault // strErrorCode
    //                , alParamsCoupleError.ToArray() // arrobjErrorParams
    //                );

    //            // Return Bad:
    //            if (objRT_Invoice_TempCustomField == null) objRT_Invoice_TempCustomField = new RT_Invoice_TempCustomField();
    //            objRT_Invoice_TempCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
    //            return Error<RT_Invoice_TempCustomField>(ex, objRT_Invoice_TempCustomField);
    //            #endregion
    //        }
    //    }

    //    [AcceptVerbs("POST")]
    //    public ServiceResult<RT_Invoice_TempCustomField> WA_Invoice_TempCustomField_Create(RQ_Invoice_TempCustomField objRQ_Invoice_TempCustomField)
    //    {
    //        #region // Temp:
    //        if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempCustomField>();
    //        DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempCustomField.Tid);
    //        RT_Invoice_TempCustomField objRT_Invoice_TempCustomField = null;
    //        DateTime dtimeSys = DateTime.Now;
    //        string strControllerName = "WA_Invoice_TempCustomField_Create";
    //        string strErrorCodeDefault = "WA_Invoice_TempCustomField_Create";

    //        ArrayList alParamsCoupleError = new ArrayList();
    //        alParamsCoupleError.AddRange(new object[] {
    //            "strControllerName", strControllerName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				////, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				////, "_biz.RQ_Invoice_TempCustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempCustomField)
				//});
    //        #endregion

    //        try
    //        {
    //            #region // CheckGatewayAuthentication:
    //            TUtils.CConnectionManager.CheckGatewayAuthentication(
    //                _biz._cf.nvcParams // nvcParams
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                , objRQ_Invoice_TempCustomField.GwUserCode // strGwUserCode
    //                , objRQ_Invoice_TempCustomField.GwPassword // strGwPassword
    //                );
    //            #endregion

    //            #region // Process:
    //            //
    //            mdsReturn = _biz.WAS_Invoice_TempCustomField_Create(
    //                ref alParamsCoupleError // alParamsCoupleError
    //                , objRQ_Invoice_TempCustomField // objRQ_Invoice_TempCustomField
    //                                            ////
    //                , out objRT_Invoice_TempCustomField // RT_Invoice_TempCustomField
    //                );

    //            if (CmUtils.CMyDataSet.HasError(mdsReturn))
    //            {
    //                throw CmUtils.CMyException.Raise(
    //                    (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
    //                    , null
    //                    , null
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            objRT_Invoice_TempCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
    //            return Success<RT_Invoice_TempCustomField>(objRT_Invoice_TempCustomField);
    //        }
    //        catch (Exception ex)
    //        {
    //            #region // Catch of try:
    //            ////
    //            TUtils.CProcessExc.Process(
    //                ref mdsReturn // mdsFinal
    //                , ex // exc
    //                , strErrorCodeDefault // strErrorCode
    //                , alParamsCoupleError.ToArray() // arrobjErrorParams
    //                );

    //            // Return Bad:
    //            if (objRT_Invoice_TempCustomField == null) objRT_Invoice_TempCustomField = new RT_Invoice_TempCustomField();
    //            objRT_Invoice_TempCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
    //            return Error<RT_Invoice_TempCustomField>(ex, objRT_Invoice_TempCustomField);
    //            #endregion
    //        }
    //    }

    //    [AcceptVerbs("POST")]
    //    public ServiceResult<RT_Invoice_TempCustomField> WA_Invoice_TempCustomField_Update(RQ_Invoice_TempCustomField objRQ_Invoice_TempCustomField)
    //    {
    //        #region // Temp:
    //        if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempCustomField>();
    //        DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempCustomField.Tid);
    //        RT_Invoice_TempCustomField objRT_Invoice_TempCustomField = null;
    //        DateTime dtimeSys = DateTime.Now;
    //        string strControllerName = "WA_Invoice_TempCustomField_Update";
    //        string strErrorCodeDefault = "WA_Invoice_TempCustomField_Update";

    //        ArrayList alParamsCoupleError = new ArrayList();
    //        alParamsCoupleError.AddRange(new object[] {
    //            "strControllerName", strControllerName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				////, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				////, "_biz.RQ_Invoice_TempCustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempCustomField)
				//});
    //        #endregion

    //        try
    //        {
    //            #region // CheckGatewayAuthentication:
    //            TUtils.CConnectionManager.CheckGatewayAuthentication(
    //                _biz._cf.nvcParams // nvcParams
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                , objRQ_Invoice_TempCustomField.GwUserCode // strGwUserCode
    //                , objRQ_Invoice_TempCustomField.GwPassword // strGwPassword
    //                );
    //            #endregion

    //            #region // Process:
    //            //
    //            mdsReturn = _biz.WAS_Invoice_TempCustomField_Update(
    //                ref alParamsCoupleError // alParamsCoupleError
    //                , objRQ_Invoice_TempCustomField // objRQ_Invoice_TempCustomField
    //                                            ////
    //                , out objRT_Invoice_TempCustomField // objRT_Invoice_TempCustomField
    //                );

    //            if (CmUtils.CMyDataSet.HasError(mdsReturn))
    //            {
    //                throw CmUtils.CMyException.Raise(
    //                    (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
    //                    , null
    //                    , null
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            objRT_Invoice_TempCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
    //            return Success<RT_Invoice_TempCustomField>(objRT_Invoice_TempCustomField);
    //        }
    //        catch (Exception ex)
    //        {
    //            #region // Catch of try:
    //            ////
    //            TUtils.CProcessExc.Process(
    //                ref mdsReturn // mdsFinal
    //                , ex // exc
    //                , strErrorCodeDefault // strErrorCode
    //                , alParamsCoupleError.ToArray() // arrobjErrorParams
    //                );

    //            // Return Bad:
    //            if (objRT_Invoice_TempCustomField == null) objRT_Invoice_TempCustomField = new RT_Invoice_TempCustomField();
    //            objRT_Invoice_TempCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
    //            return Error<RT_Invoice_TempCustomField>(ex, objRT_Invoice_TempCustomField);
    //            #endregion
    //        }
    //    }

    //    [AcceptVerbs("POST")]
    //    public ServiceResult<RT_Invoice_TempCustomField> WA_Invoice_TempCustomField_Delete(RQ_Invoice_TempCustomField objRQ_Invoice_TempCustomField)
    //    {
    //        #region // Temp:
    //        if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempCustomField>();
    //        DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempCustomField.Tid);
    //        RT_Invoice_TempCustomField objRT_Invoice_TempCustomField = null;
    //        DateTime dtimeSys = DateTime.Now;
    //        string strControllerName = "WA_Invoice_TempCustomField_Delete";
    //        string strErrorCodeDefault = "WA_Invoice_TempCustomField_Delete";

    //        ArrayList alParamsCoupleError = new ArrayList();
    //        alParamsCoupleError.AddRange(new object[] {
    //            "strControllerName", strControllerName
    //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//////
				////, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				////, "_biz.RQ_Invoice_TempCustomField", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempCustomField)
				//});
    //        #endregion

    //        try
    //        {
    //            #region // CheckGatewayAuthentication:
    //            TUtils.CConnectionManager.CheckGatewayAuthentication(
    //                _biz._cf.nvcParams // nvcParams
    //                , ref alParamsCoupleError // alParamsCoupleError
    //                , objRQ_Invoice_TempCustomField.GwUserCode // strGwUserCode
    //                , objRQ_Invoice_TempCustomField.GwPassword // strGwPassword
    //                );
    //            #endregion

    //            #region // Process:
    //            //
    //            mdsReturn = _biz.WAS_Invoice_TempCustomField_Delete(
    //                ref alParamsCoupleError // alParamsCoupleError
    //                , objRQ_Invoice_TempCustomField // objRQ_Invoice_TempCustomField
    //                                            ////
    //                , out objRT_Invoice_TempCustomField // objRT_Invoice_TempCustomField
    //                );

    //            if (CmUtils.CMyDataSet.HasError(mdsReturn))
    //            {
    //                throw CmUtils.CMyException.Raise(
    //                    (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
    //                    , null
    //                    , null
    //                    );
    //            }
    //            #endregion

    //            // Return Good:
    //            objRT_Invoice_TempCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
    //            return Success<RT_Invoice_TempCustomField>(objRT_Invoice_TempCustomField);
    //        }
    //        catch (Exception ex)
    //        {
    //            #region // Catch of try:
    //            ////
    //            TUtils.CProcessExc.Process(
    //                ref mdsReturn // mdsFinal
    //                , ex // exc
    //                , strErrorCodeDefault // strErrorCode
    //                , alParamsCoupleError.ToArray() // arrobjErrorParams
    //                );

    //            // Return Bad:
    //            if (objRT_Invoice_TempCustomField == null) objRT_Invoice_TempCustomField = new RT_Invoice_TempCustomField();
    //            objRT_Invoice_TempCustomField.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
    //            return Error<RT_Invoice_TempCustomField>(ex, objRT_Invoice_TempCustomField);
    //            #endregion
    //        }
    //    }

    }
}
