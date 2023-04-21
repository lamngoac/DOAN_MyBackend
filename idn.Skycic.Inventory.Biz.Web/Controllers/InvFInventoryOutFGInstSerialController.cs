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
    public class InvFInventoryOutFGInstSerialController : ApiControllerBase
    {

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryOutFGInstSerial> WA_InvF_InventoryOutFGInstSerial_Get(RQ_InvF_InventoryOutFGInstSerial objRQ_InvF_InventoryOutFGInstSerial)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryOutFGInstSerial>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryOutFGInstSerial.Tid);
            RT_InvF_InventoryOutFGInstSerial objRT_InvF_InventoryOutFGInstSerial = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryOutFGInstSerial_Get";
            string strErrorCodeDefault = "WA_InvF_InventoryOutFGInstSerial_Get";

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
                    , objRQ_InvF_InventoryOutFGInstSerial.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryOutFGInstSerial.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryOutFGInstSerial_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryOutFGInstSerial // objRQ_InvF_InventoryOutFGInstSerial
                                                         ////
                    , out objRT_InvF_InventoryOutFGInstSerial // objRT_InvF_InventoryOutFGInstSerial
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
                objRT_InvF_InventoryOutFGInstSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryOutFGInstSerial>(objRT_InvF_InventoryOutFGInstSerial);
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
                if (objRT_InvF_InventoryOutFGInstSerial == null) objRT_InvF_InventoryOutFGInstSerial = new RT_InvF_InventoryOutFGInstSerial();
                objRT_InvF_InventoryOutFGInstSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryOutFGInstSerial>(ex, objRT_InvF_InventoryOutFGInstSerial);
                #endregion
            }
        }

    }
}