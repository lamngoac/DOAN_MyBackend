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
    public class MstVATRateController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_VATRate> WA_Mst_VATRate_Get(RQ_Mst_VATRate objRQ_Mst_VATRate)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_VATRate>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_VATRate.Tid);
            RT_Mst_VATRate objRT_Mst_VATRate = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_VATRate_Get";
            string strErrorCodeDefault = "WA_Mst_VATRate_Get";

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
                    , objRQ_Mst_VATRate.GwUserCode // strGwUserCode
                    , objRQ_Mst_VATRate.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_VATRate_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_VATRate // objRQ_Mst_VATRate
                                           ////
                    , out objRT_Mst_VATRate // RT_Mst_VATRate
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
                objRT_Mst_VATRate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_VATRate>(objRT_Mst_VATRate);
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
                if (objRT_Mst_VATRate == null) objRT_Mst_VATRate = new RT_Mst_VATRate();
                objRT_Mst_VATRate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_VATRate>(ex, objRT_Mst_VATRate);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_VATRate> WA_RptSv_Mst_VATRate_Get(RQ_Mst_VATRate objRQ_Mst_VATRate)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_VATRate>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_VATRate.Tid);
			RT_Mst_VATRate objRT_Mst_VATRate = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_VATRate_Get";
			string strErrorCodeDefault = "WA_RptSv_Mst_VATRate_Get";

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
					, objRQ_Mst_VATRate.GwUserCode // strGwUserCode
					, objRQ_Mst_VATRate.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_VATRate_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_VATRate // objRQ_Mst_VATRate
										////
					, out objRT_Mst_VATRate // RT_Mst_VATRate
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
				objRT_Mst_VATRate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_VATRate>(objRT_Mst_VATRate);
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
				if (objRT_Mst_VATRate == null) objRT_Mst_VATRate = new RT_Mst_VATRate();
				objRT_Mst_VATRate.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_VATRate>(ex, objRT_Mst_VATRate);
				#endregion
			}
		}

	}
}