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
    public class MstSvOrgInNetworkController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_MstSv_OrgInNetwork> WA_MstSv_OrgInNetwork_Create(RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_OrgInNetwork>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_OrgInNetwork.Tid);
            RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_MstSv_OrgInNetwork_Create";
            string strErrorCodeDefault = "WA_MstSv_OrgInNetwork_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_OrgInNetwork", TJson.JsonConvert.SerializeObject(objRQ_MstSv_OrgInNetwork)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_MstSv_OrgInNetwork.GwUserCode // strGwUserCode
                    , objRQ_MstSv_OrgInNetwork.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_MstSv_OrgInNetwork_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_MstSv_OrgInNetwork // objRQ_MstSv_OrgInNetwork
                                     ////
                    , out objRT_MstSv_OrgInNetwork // RT_MstSv_OrgInNetwork
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
                objRT_MstSv_OrgInNetwork.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_MstSv_OrgInNetwork>(objRT_MstSv_OrgInNetwork);
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
                if (objRT_MstSv_OrgInNetwork == null) objRT_MstSv_OrgInNetwork = new RT_MstSv_OrgInNetwork();
                objRT_MstSv_OrgInNetwork.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_MstSv_OrgInNetwork>(ex, objRT_MstSv_OrgInNetwork);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_OrgInNetwork> WA_MstSv_OrgInNetwork_Create_MstSv(RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_OrgInNetwork>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_OrgInNetwork.Tid);
			RT_MstSv_OrgInNetwork objRT_MstSv_OrgInNetwork = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_OrgInNetwork_Create_MstSv";
			string strErrorCodeDefault = "WA_MstSv_OrgInNetwork_Create_MstSv";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_OrgInNetwork", TJson.JsonConvert.SerializeObject(objRQ_MstSv_OrgInNetwork)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_OrgInNetwork.GwUserCode // strGwUserCode
					, objRQ_MstSv_OrgInNetwork.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_OrgInNetwork_Create_MstSv(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_OrgInNetwork // objRQ_MstSv_OrgInNetwork
											   ////
					, out objRT_MstSv_OrgInNetwork // RT_MstSv_OrgInNetwork
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
				objRT_MstSv_OrgInNetwork.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_OrgInNetwork>(objRT_MstSv_OrgInNetwork);
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
				if (objRT_MstSv_OrgInNetwork == null) objRT_MstSv_OrgInNetwork = new RT_MstSv_OrgInNetwork();
				objRT_MstSv_OrgInNetwork.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_OrgInNetwork>(ex, objRT_MstSv_OrgInNetwork);
				#endregion
			}
		}
	}
}
