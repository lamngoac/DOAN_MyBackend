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
    public class EmailController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Email_BatchSendEmail> WA_Email_BatchSendEmail_SaveAndSend(RQ_Email_BatchSendEmail objRQ_Email_BatchSendEmail)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Email_BatchSendEmail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Email_BatchSendEmail.Tid);
			RT_Email_BatchSendEmail objRT_Email_BatchSendEmail = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Email_BatchSendEmail_SaveAndSend";
			string strErrorCodeDefault = "WA_Email_BatchSendEmail_SaveAndSend";

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
					, objRQ_Email_BatchSendEmail.GwUserCode // strGwUserCode
					, objRQ_Email_BatchSendEmail.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Email_BatchSendEmail_SaveAndSend(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Email_BatchSendEmail // RQ_Email_BatchSendEmail
											  ////
					, out objRT_Email_BatchSendEmail // RT_Email_BatchSendEmail
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
				objRT_Email_BatchSendEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Email_BatchSendEmail>(objRT_Email_BatchSendEmail);
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
				if (objRT_Email_BatchSendEmail == null) objRT_Email_BatchSendEmail = new RT_Email_BatchSendEmail();
				objRT_Email_BatchSendEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Email_BatchSendEmail>(ex, objRT_Email_BatchSendEmail);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Email_BatchSendEmail> WA_Email_BatchSendEmail_MstSv_Inos_User_Send(RQ_Email_BatchSendEmail objRQ_Email_BatchSendEmail)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Email_BatchSendEmail>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Email_BatchSendEmail.Tid);
			RT_Email_BatchSendEmail objRT_Email_BatchSendEmail = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Email_BatchSendEmail_MstSv_Inos_User_Send";
			string strErrorCodeDefault = "WA_Email_BatchSendEmail_MstSv_Inos_User_Send";

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
					, objRQ_Email_BatchSendEmail.GwUserCode // strGwUserCode
					, objRQ_Email_BatchSendEmail.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Email_BatchSendEmail_MstSv_Inos_User_Send(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Email_BatchSendEmail // RQ_Email_BatchSendEmail
												 ////
					, out objRT_Email_BatchSendEmail // RT_Email_BatchSendEmail
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
				objRT_Email_BatchSendEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Email_BatchSendEmail>(objRT_Email_BatchSendEmail);
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
				if (objRT_Email_BatchSendEmail == null) objRT_Email_BatchSendEmail = new RT_Email_BatchSendEmail();
				objRT_Email_BatchSendEmail.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Email_BatchSendEmail>(ex, objRT_Email_BatchSendEmail);
				#endregion
			}
		}
	}
}
