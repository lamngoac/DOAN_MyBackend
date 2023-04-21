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
    public class SysObjectController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Sys_Object> WA_Sys_Object_Get(RQ_Sys_Object objRQ_Sys_Object)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Object>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Object.Tid);
			RT_Sys_Object objRT_Sys_Object = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Sys_Object_Get";
			string strErrorCodeDefault = "WA_Sys_Object_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Object", TJson.JsonConvert.SerializeObject(objRQ_Sys_Object)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Sys_Object.GwUserCode // strGwUserCode
					, objRQ_Sys_Object.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Sys_Object_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Sys_Object // objRQ_Sys_Object
									  ////
					, out objRT_Sys_Object // RT_Sys_Object
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
				objRT_Sys_Object.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Sys_Object>(objRT_Sys_Object);
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
				if (objRT_Sys_Object == null) objRT_Sys_Object = new RT_Sys_Object();
				objRT_Sys_Object.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Sys_Object>(ex, objRT_Sys_Object);
				#endregion
			}
		}

        [AcceptVerbs("POST")]
        public ServiceResult<RT_RptSv_Sys_Object> WA_RptSv_Sys_Object_Get(RQ_RptSv_Sys_Object objRQ_RptSv_Sys_Object)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_Object>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Object.Tid);
            RT_RptSv_Sys_Object objRT_RptSv_Sys_Object = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Object_Get";
            string strErrorCodeDefault = "WA_RptSv_Sys_Object_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_Object", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Object)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Object.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Object.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Object_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Object // objRQ_RptSv_Sys_Object
                                       ////
                    , out objRT_RptSv_Sys_Object // RT_RptSv_Sys_Object
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
                objRT_RptSv_Sys_Object.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_RptSv_Sys_Object>(objRT_RptSv_Sys_Object);
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
                if (objRT_RptSv_Sys_Object == null) objRT_RptSv_Sys_Object = new RT_RptSv_Sys_Object();
                objRT_RptSv_Sys_Object.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_RptSv_Sys_Object>(ex, objRT_RptSv_Sys_Object);
                #endregion
            }
        }

    }
}
