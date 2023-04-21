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
    public class MstOrgController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_Org> WA_Mst_Org_Get(RQ_Mst_Org objRQ_Mst_Org)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Org>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Org.Tid);
			RT_Mst_Org objRT_Mst_Org = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_Org_Get";
			string strErrorCodeDefault = "WA_Mst_Org_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Currency", TJson.JsonConvert.SerializeObject(objRQ_Mst_Currency)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Org.GwUserCode // strGwUserCode
					, objRQ_Mst_Org.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_Org_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_Org // RQ_Mst_Org
					////
					, out objRT_Mst_Org // RT_Mst_Org
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
				objRT_Mst_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_Org>(objRT_Mst_Org);
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
				if (objRT_Mst_Org == null) objRT_Mst_Org = new RT_Mst_Org();
				objRT_Mst_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_Org>(ex, objRT_Mst_Org);
				#endregion
			}
		}

	}
}
