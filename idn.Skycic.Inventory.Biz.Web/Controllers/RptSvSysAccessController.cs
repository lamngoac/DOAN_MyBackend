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
    public class RptSvSysAccessController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_RptSv_Sys_Access> WA_RptSv_Sys_Access_Get(RQ_RptSv_Sys_Access objRQ_RptSv_Sys_Access)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_Access>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Access.Tid);
            RT_RptSv_Sys_Access objRT_RptSv_Sys_Access = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Access_Get";
            string strErrorCodeDefault = "WA_RptSv_Sys_Access_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_Access", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Access)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Access.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Access.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Access_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Access // objRQ_RptSv_Sys_Access
                                       // //
                    , out objRT_RptSv_Sys_Access // RT_RptSv_Sys_Access
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
                objRT_RptSv_Sys_Access.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_RptSv_Sys_Access>(objRT_RptSv_Sys_Access);
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
                if (objRT_RptSv_Sys_Access == null) objRT_RptSv_Sys_Access = new RT_RptSv_Sys_Access();
                objRT_RptSv_Sys_Access.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_RptSv_Sys_Access>(ex, objRT_RptSv_Sys_Access);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_RptSv_Sys_Access> WA_RptSv_Sys_Access_Save(RQ_RptSv_Sys_Access objRQ_RptSv_Sys_Access)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_Access>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Access.Tid);
            RT_RptSv_Sys_Access objRT_RptSv_Sys_Access = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Access_Save";
            string strErrorCodeDefault = "WA_RptSv_Sys_Access_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_Access", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Access)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Access.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Access.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Access_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Access // objRQ_RptSv_Sys_Access
                                       // //
                    , out objRT_RptSv_Sys_Access // RT_RptSv_Sys_Access
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
                objRT_RptSv_Sys_Access.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_RptSv_Sys_Access>(objRT_RptSv_Sys_Access);
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
                if (objRT_RptSv_Sys_Access == null) objRT_RptSv_Sys_Access = new RT_RptSv_Sys_Access();
                objRT_RptSv_Sys_Access.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_RptSv_Sys_Access>(ex, objRT_RptSv_Sys_Access);
                #endregion
            }
        }
    }
}
