using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstCustomerController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Customer> WA_Mst_Customer_Get(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            RT_Mst_Customer objRT_Mst_Customer = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Customer_Get";
            string strErrorCodeDefault = "WA_Mst_Customer_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer)
				////
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
                mdsReturn = _biz.WAS_Mst_Customer_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer // objRQ_Mst_Customer
                                         // //
                    , out objRT_Mst_Customer // RT_Mst_Customer
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
                return Success<RT_Mst_Customer>(objRT_Mst_Customer);
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
                if (objRT_Mst_Customer == null) objRT_Mst_Customer = new RT_Mst_Customer();
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Customer>(ex, objRT_Mst_Customer);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Customer> WA_Mst_Customer_Save(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            RT_Mst_Customer objRT_Mst_Customer = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Customer_Save";
            string strErrorCodeDefault = "WA_Mst_Customer_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Customer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Customer)
				////
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
                mdsReturn = _biz.WAS_Mst_Customer_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer // objRQ_Mst_Customer
                                         // //
                    , out objRT_Mst_Customer // RT_Mst_Customer
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
                return Success<RT_Mst_Customer>(objRT_Mst_Customer);
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
                if (objRT_Mst_Customer == null) objRT_Mst_Customer = new RT_Mst_Customer();
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Customer>(ex, objRT_Mst_Customer);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Customer> WA_Mst_Customer_UpdateDtimeUsed(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Customer.Tid);
            RT_Mst_Customer objRT_Mst_Customer = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Customer_UpdateDtimeUsed";
            string strErrorCodeDefault = "WA_Mst_Customer_UpdateDtimeUsed";

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
                    , objRQ_Mst_Customer.GwUserCode // strGwUserCode
                    , objRQ_Mst_Customer.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Customer_UpdateDtimeUsed(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Customer // objRQ_Mst_Partner
                                         ////
                    , out objRT_Mst_Customer // RT_Mst_Partner
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
                return Success<RT_Mst_Customer>(objRT_Mst_Customer);
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
                if (objRT_Mst_Customer == null) objRT_Mst_Customer = new RT_Mst_Customer();
                objRT_Mst_Customer.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Customer>(ex, objRT_Mst_Customer);
                #endregion
            }
        }
    }
}
