using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using System.Collections;
namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstPaymentMethodsController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PaymentMethods> WA_Mst_PaymentMethods_Get(RQ_Mst_PaymentMethods objRQ_Mst_PaymentMethods)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PaymentMethods>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PaymentMethods.Tid);
            RT_Mst_PaymentMethods objRT_Mst_PaymentMethods = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PaymentMethods_Get";
            string strErrorCodeDefault = "WA_Mst_PaymentMethods_Get";

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
                    , objRQ_Mst_PaymentMethods.GwUserCode // strGwUserCode
                    , objRQ_Mst_PaymentMethods.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PaymentMethods_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PaymentMethods // objRQ_Mst_PaymentMethods
                                           ////
                    , out objRT_Mst_PaymentMethods // RT_Mst_PaymentMethods
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
                objRT_Mst_PaymentMethods.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PaymentMethods>(objRT_Mst_PaymentMethods);
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
                if (objRT_Mst_PaymentMethods == null) objRT_Mst_PaymentMethods = new RT_Mst_PaymentMethods();
                objRT_Mst_PaymentMethods.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PaymentMethods>(ex, objRT_Mst_PaymentMethods);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_PaymentMethods> WA_RptSv_Mst_PaymentMethods_Get(RQ_Mst_PaymentMethods objRQ_Mst_PaymentMethods)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PaymentMethods>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PaymentMethods.Tid);
			RT_Mst_PaymentMethods objRT_Mst_PaymentMethods = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_PaymentMethods_Get";
			string strErrorCodeDefault = "WA_RptSv_Mst_PaymentMethods_Get";

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
					, objRQ_Mst_PaymentMethods.GwUserCode // strGwUserCode
					, objRQ_Mst_PaymentMethods.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_PaymentMethods_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_PaymentMethods // objRQ_Mst_PaymentMethods
											   ////
					, out objRT_Mst_PaymentMethods // RT_Mst_PaymentMethods
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
				objRT_Mst_PaymentMethods.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_PaymentMethods>(objRT_Mst_PaymentMethods);
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
				if (objRT_Mst_PaymentMethods == null) objRT_Mst_PaymentMethods = new RT_Mst_PaymentMethods();
				objRT_Mst_PaymentMethods.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_PaymentMethods>(ex, objRT_Mst_PaymentMethods);
				#endregion
			}
		}

	}
}