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
    public class CommonController : ApiControllerBase
    {
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Common> WA_Common_GetDTime(RQ_Common objRQ_Common)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Common>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Common.Tid);
			RT_Common objRT_Common = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Common_GetDTime";
			string strErrorCodeDefault = "WA_Common_GetDTime";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Common", TJson.JsonConvert.SerializeObject(objRQ_Common)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Common.GwUserCode // strGwUserCode
					, objRQ_Common.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Cm_GetDTime(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Common // objRQ_Common
					////
					, out objRT_Common // RT_Common
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
				objRT_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Common>(objRT_Common);
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
				if (objRT_Common == null) objRT_Common = new RT_Common();
				objRT_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Common>(ex, objRT_Common);
				#endregion
			}
		}


	}
}
