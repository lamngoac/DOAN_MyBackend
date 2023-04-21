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
    public class InvInventoryBalanceController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Inv_InventoryBalance> WA_Inv_InventoryBalance_Get(RQ_Inv_InventoryBalance objRQ_Inv_InventoryBalance)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventoryBalance>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalance.Tid);
            RT_Inv_InventoryBalance objRT_Inv_InventoryBalance = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Inv_InventoryBalance_Get";
            string strErrorCodeDefault = "WA_Inv_InventoryBalance_Get";

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
                    , objRQ_Inv_InventoryBalance.GwUserCode // strGwUserCode
                    , objRQ_Inv_InventoryBalance.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Inv_InventoryBalance_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Inv_InventoryBalance // objRQ_Inv_InventoryBalance
                                                 ////
                    , out objRT_Inv_InventoryBalance // objRT_Inv_InventoryBalance
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
                objRT_Inv_InventoryBalance.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Inv_InventoryBalance>(objRT_Inv_InventoryBalance);
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
                if (objRT_Inv_InventoryBalance == null) objRT_Inv_InventoryBalance = new RT_Inv_InventoryBalance();
                objRT_Inv_InventoryBalance.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Inv_InventoryBalance>(ex, objRT_Inv_InventoryBalance);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Inv_InventoryBalance> WA_OS_Inv_InventoryBalance_GetQty(RQ_Inv_InventoryBalance objRQ_Inv_InventoryBalance)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventoryBalance>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalance.Tid);
			RT_Inv_InventoryBalance objRT_Inv_InventoryBalance = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_OS_Inv_InventoryBalance_GetQty";
			string strErrorCodeDefault = "WA_OS_Inv_InventoryBalance_GetQty";

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
					, objRQ_Inv_InventoryBalance.GwUserCode // strGwUserCode
					, objRQ_Inv_InventoryBalance.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_OS_Inv_InventoryBalance_GetQty(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Inv_InventoryBalance // objRQ_Inv_InventoryBalance
												 ////
					, out objRT_Inv_InventoryBalance // objRT_Inv_InventoryBalance
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
				objRT_Inv_InventoryBalance.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Inv_InventoryBalance>(objRT_Inv_InventoryBalance);
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
				if (objRT_Inv_InventoryBalance == null) objRT_Inv_InventoryBalance = new RT_Inv_InventoryBalance();
				objRT_Inv_InventoryBalance.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Inv_InventoryBalance>(ex, objRT_Inv_InventoryBalance);
				#endregion
			}
		}
	}
}