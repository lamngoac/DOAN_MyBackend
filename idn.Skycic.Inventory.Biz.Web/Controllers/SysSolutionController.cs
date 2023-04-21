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
    public class SysSolutionController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Solution> WA_Sys_Solution_Get(RQ_Sys_Solution objRQ_Sys_Solution)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Solution>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Solution.Tid);
            RT_Sys_Solution objRT_Sys_Solution = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Sys_Solution_Get";
            string strErrorCodeDefault = "WA_Sys_Solution_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Solution", TJson.JsonConvert.SerializeObject(objRQ_Sys_Solution)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Solution.GwUserCode // strGwUserCode
                    , objRQ_Sys_Solution.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Sys_Solution_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Solution // objRQ_Sys_Solution
                                       ////
                    , out objRT_Sys_Solution // RT_Sys_Solution
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
                objRT_Sys_Solution.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Solution>(objRT_Sys_Solution);
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
                if (objRT_Sys_Solution == null) objRT_Sys_Solution = new RT_Sys_Solution();
                objRT_Sys_Solution.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Solution>(ex, objRT_Sys_Solution);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Sys_Solution> WA_RptSv_Sys_Solution_Get(RQ_Sys_Solution objRQ_Sys_Solution)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Sys_Solution>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Solution.Tid);
            RT_Sys_Solution objRT_Sys_Solution = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Solution_Get";
            string strErrorCodeDefault = "WA_RptSv_Sys_Solution_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Sys_Solution", TJson.JsonConvert.SerializeObject(objRQ_Sys_Solution)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Solution.GwUserCode // strGwUserCode
                    , objRQ_Sys_Solution.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Solution_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Sys_Solution // objRQ_Sys_Solution
                                         ////
                    , out objRT_Sys_Solution // RT_Sys_Solution
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
                objRT_Sys_Solution.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Sys_Solution>(objRT_Sys_Solution);
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
                if (objRT_Sys_Solution == null) objRT_Sys_Solution = new RT_Sys_Solution();
                objRT_Sys_Solution.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Sys_Solution>(ex, objRT_Sys_Solution);
                #endregion
            }
        }
    }
}
