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
    public class MstSysConfigController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Sys_Config> WA_Mst_Sys_Config_Get(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Sys_Config.Tid);
            RT_Mst_Sys_Config objRT_Mst_Sys_Config = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Sys_Config_Get";
            string strErrorCodeDefault = "WA_Mst_Sys_Config_Get";

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
                    , objRQ_Mst_Sys_Config.GwUserCode // strGwUserCode
                    , objRQ_Mst_Sys_Config.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Sys_Config_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Sys_Config // objRQ_Mst_Sys_Config
                                           ////
                    , out objRT_Mst_Sys_Config // RT_Mst_Sys_Config

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
                objRT_Mst_Sys_Config.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Sys_Config>(objRT_Mst_Sys_Config);
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
                if (objRT_Mst_Sys_Config == null) objRT_Mst_Sys_Config = new RT_Mst_Sys_Config();
                objRT_Mst_Sys_Config.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Sys_Config>(ex, objRT_Mst_Sys_Config);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Sys_Config> WA_Mst_Sys_Config_Update(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Sys_Config.Tid);
            RT_Mst_Sys_Config objRT_Mst_Sys_Config = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Sys_Config_Update";
            string strErrorCodeDefault = "WA_Mst_Sys_Config_Update";

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
                    , objRQ_Mst_Sys_Config.GwUserCode // strGwUserCode
                    , objRQ_Mst_Sys_Config.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Sys_Config_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Sys_Config // objRQ_Mst_Sys_Config
                                           ////
                    , out objRT_Mst_Sys_Config // RT_Mst_Sys_Config
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
                objRT_Mst_Sys_Config.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Sys_Config>(objRT_Mst_Sys_Config);
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
                if (objRT_Mst_Sys_Config == null) objRT_Mst_Sys_Config = new RT_Mst_Sys_Config();
                objRT_Mst_Sys_Config.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Sys_Config>(ex, objRT_Mst_Sys_Config);
                #endregion
            }
        }
    }
}
