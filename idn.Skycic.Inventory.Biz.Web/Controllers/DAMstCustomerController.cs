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
    public class DAMstCustomerController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Customer> WA_Mst_Customer_Get(DA_RQ_Mst_Customer objRQ_Mst_Customer)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Customer>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            DA_RT_Mst_Customer objRT_Mst_Customer = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Customer_Get";
            string strErrorCodeDefault = "WA_Mst_Customer_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_Customer_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer
                    ////
                    , out objRT_Mst_Customer
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
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Customer>(objRT_Mst_Customer);
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
                if (objRT_Mst_Customer == null) objRT_Mst_Customer = new DA_RT_Mst_Customer();
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Customer>(ex, objRT_Mst_Customer);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Customer> WA_Mst_Customer_Create(DA_RQ_Mst_Customer objRQ_Mst_Customer)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Customer>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            DA_RT_Mst_Customer objRT_Mst_Customer = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Customer_Create";
            string strErrorCodeDefault = "WA_Mst_Customer_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_Customer_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer
                    ////
                    , out objRT_Mst_Customer
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
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Customer>(objRT_Mst_Customer);
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
                if (objRT_Mst_Customer == null) objRT_Mst_Customer = new DA_RT_Mst_Customer();
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Customer>(ex, objRT_Mst_Customer);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Customer> WA_Mst_Customer_Update(DA_RQ_Mst_Customer objRQ_Mst_Customer)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Customer>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            DA_RT_Mst_Customer objRT_Mst_Customer = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Customer_Create";
            string strErrorCodeDefault = "WA_Mst_Customer_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_Customer_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer
                    ////
                    , out objRT_Mst_Customer
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
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Customer>(objRT_Mst_Customer);
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
                if (objRT_Mst_Customer == null) objRT_Mst_Customer = new DA_RT_Mst_Customer();
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Customer>(ex, objRT_Mst_Customer);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<DA_RT_Mst_Customer> WA_Mst_Customer_Delete(DA_RQ_Mst_Customer objRQ_Mst_Customer)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<DA_RT_Mst_Customer>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            DA_RT_Mst_Customer objRT_Mst_Customer = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Customer_Create";
            string strErrorCodeDefault = "WA_Mst_Customer_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_DAMst_Customer_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer
                    ////
                    , out objRT_Mst_Customer
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
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<DA_RT_Mst_Customer>(objRT_Mst_Customer);
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
                if (objRT_Mst_Customer == null) objRT_Mst_Customer = new DA_RT_Mst_Customer();
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<DA_RT_Mst_Customer>(ex, objRT_Mst_Customer);
                #endregion
            }
        }
    }
}
