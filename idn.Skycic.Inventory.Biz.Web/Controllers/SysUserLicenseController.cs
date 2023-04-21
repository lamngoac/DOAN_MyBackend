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
    public class SysUserLicenseController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Sys_UserLicense> WA_Sys_UserLicense_Save(RQ_Sys_UserLicense objRQ_Sys_UserLicense)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_UserLicense>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_UserLicense.Tid);
			RT_Sys_UserLicense objRT_Sys_UserLicense = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Sys_UserLicense_Save";
			string strErrorCodeDefault = "WA_Sys_UserLicense_Save";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_UserLicense", TJson.JsonConvert.SerializeObject(objRQ_Sys_UserLicense)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Sys_UserLicense.GwUserCode // strGwUserCode
					, objRQ_Sys_UserLicense.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Sys_UserLicense_Save(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Sys_UserLicense // objRQ_Sys_UserLicense
									   // //
					, out objRT_Sys_UserLicense // RT_Sys_UserLicense
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
				objRT_Sys_UserLicense.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Sys_UserLicense>(objRT_Sys_UserLicense);
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
				if (objRT_Sys_UserLicense == null) objRT_Sys_UserLicense = new RT_Sys_UserLicense();
				objRT_Sys_UserLicense.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Sys_UserLicense>(ex, objRT_Sys_UserLicense);
				#endregion
			}
		}
	}
}
