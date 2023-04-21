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
    public class MstColumnConfigController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ColumnConfig> WA_Mst_ColumnConfig_Get(RQ_Mst_ColumnConfig objRQ_Mst_ColumnConfig)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfig.Tid);
            RT_Mst_ColumnConfig objRT_Mst_ColumnConfig = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ColumnConfig_Get";
            string strErrorCodeDefault = "WA_Mst_ColumnConfig_Get";

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
                    , objRQ_Mst_ColumnConfig.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfig.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ColumnConfig_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ColumnConfig // objRQ_Mst_ColumnConfig
                                             ////
                    , out objRT_Mst_ColumnConfig // RT_Mst_ColumnConfig

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
                objRT_Mst_ColumnConfig.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ColumnConfig>(objRT_Mst_ColumnConfig);
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
                if (objRT_Mst_ColumnConfig == null) objRT_Mst_ColumnConfig = new RT_Mst_ColumnConfig();
                objRT_Mst_ColumnConfig.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ColumnConfig>(ex, objRT_Mst_ColumnConfig);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ColumnConfig> WA_Mst_ColumnConfig_Update(RQ_Mst_ColumnConfig objRQ_Mst_ColumnConfig)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfig.Tid);
            RT_Mst_ColumnConfig objRT_Mst_ColumnConfig = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ColumnConfig_Update";
            string strErrorCodeDefault = "WA_Mst_ColumnConfig_Update";

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
                    , objRQ_Mst_ColumnConfig.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfig.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ColumnConfig_Update_New20200220(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ColumnConfig // objRQ_Mst_ColumnConfig
                                             ////
                    , out objRT_Mst_ColumnConfig // RT_Mst_ColumnConfig
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
                objRT_Mst_ColumnConfig.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ColumnConfig>(objRT_Mst_ColumnConfig);
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
                if (objRT_Mst_ColumnConfig == null) objRT_Mst_ColumnConfig = new RT_Mst_ColumnConfig();
                objRT_Mst_ColumnConfig.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ColumnConfig>(ex, objRT_Mst_ColumnConfig);
                #endregion
            }
        }
    }
}
