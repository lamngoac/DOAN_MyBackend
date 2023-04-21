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
    public class InvInventorySecretController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_InventorySecret> WA_Inv_InventorySecret_Get(RQ_Inv_InventorySecret objRQ_Inv_InventorySecret)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventorySecret>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventorySecret.Tid);
            RT_Inv_InventorySecret objRT_Inv_InventorySecret = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_InventorySecret_Get";
            string strErrorCodeDefault = "WA_Inv_InventorySecret_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempInvoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempInvoice)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventorySecret.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventorySecret.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_InventorySecret_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventorySecret // objRQ_Inv_InventorySecret
                                                ////
                    , out objRT_Inv_InventorySecret // objRT_Inv_InventorySecret
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
                objRT_Inv_InventorySecret.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_InventorySecret>(objRT_Inv_InventorySecret);
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
                if (objRT_Inv_InventorySecret == null) objRT_Inv_InventorySecret = new RT_Inv_InventorySecret();
                objRT_Inv_InventorySecret.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_InventorySecret>(ex, objRT_Inv_InventorySecret);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_InventorySecret> WA_Inv_InventorySecret_UpdateFlagUsed(RQ_Inv_InventorySecret objRQ_Inv_InventorySecret)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventorySecret>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventorySecret.Tid);
            RT_Inv_InventorySecret objRT_Inv_InventorySecret = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_InventorySecret_UpdateFlagUsed";
            string strErrorCodeDefault = "WA_Inv_InventorySecret_UpdateFlagUsed";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempInvoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempInvoice)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventorySecret.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventorySecret.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_InventorySecret_UpdateFlagUsed(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventorySecret // objRQ_Inv_InventorySecret
                                                ////
                    , out objRT_Inv_InventorySecret // objRT_Inv_InventorySecret
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
                objRT_Inv_InventorySecret.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_InventorySecret>(objRT_Inv_InventorySecret);
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
                if (objRT_Inv_InventorySecret == null) objRT_Inv_InventorySecret = new RT_Inv_InventorySecret();
                objRT_Inv_InventorySecret.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_InventorySecret>(ex, objRT_Inv_InventorySecret);
                #endregion
            }
        }
    }
}