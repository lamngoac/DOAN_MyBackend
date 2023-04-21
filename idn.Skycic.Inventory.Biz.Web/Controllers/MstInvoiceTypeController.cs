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
    public class MstInvoiceTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_InvoiceType> WA_Mst_InvoiceType_Get(RQ_Mst_InvoiceType objRQ_Mst_InvoiceType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InvoiceType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvoiceType.Tid);
            RT_Mst_InvoiceType objRT_Mst_InvoiceType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_InvoiceType_Get";
            string strErrorCodeDefault = "WA_Mst_InvoiceType_Get";

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
                    , objRQ_Mst_InvoiceType.GwUserCode // strGwUserCode
                    , objRQ_Mst_InvoiceType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_InvoiceType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_InvoiceType // objRQ_Mst_InvoiceType
                                           ////
                    , out objRT_Mst_InvoiceType // RT_Mst_InvoiceType
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
                objRT_Mst_InvoiceType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_InvoiceType>(objRT_Mst_InvoiceType);
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
                if (objRT_Mst_InvoiceType == null) objRT_Mst_InvoiceType = new RT_Mst_InvoiceType();
                objRT_Mst_InvoiceType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_InvoiceType>(ex, objRT_Mst_InvoiceType);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InvoiceType> WA_RptSv_Mst_InvoiceType_Get(RQ_Mst_InvoiceType objRQ_Mst_InvoiceType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InvoiceType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvoiceType.Tid);
			RT_Mst_InvoiceType objRT_Mst_InvoiceType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_InvoiceType_Get";
			string strErrorCodeDefault = "WA_RptSv_Mst_InvoiceType_Get";

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
					, objRQ_Mst_InvoiceType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvoiceType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_InvoiceType_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvoiceType // objRQ_Mst_InvoiceType
											////
					, out objRT_Mst_InvoiceType // RT_Mst_InvoiceType
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
				objRT_Mst_InvoiceType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InvoiceType>(objRT_Mst_InvoiceType);
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
				if (objRT_Mst_InvoiceType == null) objRT_Mst_InvoiceType = new RT_Mst_InvoiceType();
				objRT_Mst_InvoiceType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InvoiceType>(ex, objRT_Mst_InvoiceType);
				#endregion
			}
		}

	}
}