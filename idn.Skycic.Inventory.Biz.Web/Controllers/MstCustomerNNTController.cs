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
    public class MstCustomerNNTController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerNNT> WA_Mst_CustomerNNT_Get(RQ_Mst_CustomerNNT objRQ_Mst_CustomerNNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_CustomerNNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerNNT.Tid);
            RT_Mst_CustomerNNT objRT_Mst_CustomerNNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerNNT_Get";
            string strErrorCodeDefault = "WA_Mst_CustomerNNT_Get";

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
                    , objRQ_Mst_CustomerNNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerNNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerNNT_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNT // RQ_Mst_CustomerNNT
                                       ////
                    , out objRT_Mst_CustomerNNT // RT_Mst_CustomerNNT
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
                objRT_Mst_CustomerNNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerNNT>(objRT_Mst_CustomerNNT);
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
                if (objRT_Mst_CustomerNNT == null) objRT_Mst_CustomerNNT = new RT_Mst_CustomerNNT();
                objRT_Mst_CustomerNNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerNNT>(ex, objRT_Mst_CustomerNNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerNNT> WA_Mst_CustomerNNT_Create(RQ_Mst_CustomerNNT objRQ_Mst_CustomerNNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_CustomerNNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerNNT.Tid);
            RT_Mst_CustomerNNT objRT_Mst_CustomerNNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerNNT_Create";
            string strErrorCodeDefault = "WA_Mst_CustomerNNT_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerNNT", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerNNT)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerNNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerNNT_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNT // objRQ_Mst_CustomerNNT
                                       ////
                    , out objRT_Mst_CustomerNNT // RT_Mst_CustomerNNT
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
                objRT_Mst_CustomerNNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerNNT>(objRT_Mst_CustomerNNT);
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
                if (objRT_Mst_CustomerNNT == null) objRT_Mst_CustomerNNT = new RT_Mst_CustomerNNT();
                objRT_Mst_CustomerNNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerNNT>(ex, objRT_Mst_CustomerNNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerNNT> WA_Mst_CustomerNNT_Update(RQ_Mst_CustomerNNT objRQ_Mst_CustomerNNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_CustomerNNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerNNT.Tid);
            RT_Mst_CustomerNNT objRT_Mst_CustomerNNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerNNT_Update";
            string strErrorCodeDefault = "WA_Mst_CustomerNNT_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerNNT", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerNNT)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerNNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerNNT_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNT // objRQ_Mst_CustomerNNT
                                       ////
                    , out objRT_Mst_CustomerNNT // objRT_Mst_CustomerNNT
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
                objRT_Mst_CustomerNNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerNNT>(objRT_Mst_CustomerNNT);
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
                if (objRT_Mst_CustomerNNT == null) objRT_Mst_CustomerNNT = new RT_Mst_CustomerNNT();
                objRT_Mst_CustomerNNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerNNT>(ex, objRT_Mst_CustomerNNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_CustomerNNT> WA_Mst_CustomerNNT_Delete(RQ_Mst_CustomerNNT objRQ_Mst_CustomerNNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_CustomerNNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_CustomerNNT.Tid);
            RT_Mst_CustomerNNT objRT_Mst_CustomerNNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_CustomerNNT_Delete";
            string strErrorCodeDefault = "WA_Mst_CustomerNNT_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_CustomerNNT", TJson.JsonConvert.SerializeObject(objRQ_Mst_CustomerNNT)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_CustomerNNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_CustomerNNT_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_CustomerNNT // objRQ_Mst_CustomerNNT
                                       ////
                    , out objRT_Mst_CustomerNNT // objRT_Mst_CustomerNNT
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
                objRT_Mst_CustomerNNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_CustomerNNT>(objRT_Mst_CustomerNNT);
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
                if (objRT_Mst_CustomerNNT == null) objRT_Mst_CustomerNNT = new RT_Mst_CustomerNNT();
                objRT_Mst_CustomerNNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_CustomerNNT>(ex, objRT_Mst_CustomerNNT);
                #endregion
            }
        }
    }
}
