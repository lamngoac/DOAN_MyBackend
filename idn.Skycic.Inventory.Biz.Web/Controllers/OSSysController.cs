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
	public class OSSysController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Invoice_Invoice> WA_OS_Sys_AT_3A_Invoice_Delete(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
			RT_Invoice_Invoice objRT_Invoice_Invoice = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_OS_Sys_AT_3A_Invoice_Delete";
			string strErrorCodeDefault = "WA_OS_Sys_AT_3A_Invoice_Delete";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_Invoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
					, objRQ_Invoice_Invoice.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_OS_Sys_AT_3A_Invoice_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
										   ////
					, out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
				objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
				if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
				objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
				#endregion
			}
		}

	}
}
