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
    public class OS_MstSvPrdCenter_MstSvSysUserController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_MstSvPrdCentrer_MstSv_Sys_User> WA_OS_MstSvPrdCenter_MstSv_Sys_User_Login(RQ_OS_MstSvPrdCenter_MstSv_Sys_User objRQ_MstSv_Sys_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_MstSvPrdCentrer_MstSv_Sys_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Sys_User.Tid);
            RT_OS_MstSvPrdCentrer_MstSv_Sys_User objRT_OS_MstSvPrdCentrer_MstSv_Sys_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_MstSvPrdCenter_MstSv_Sys_User_Login";
            string strErrorCodeDefault = "WA_OS_MstSvPrdCenter_MstSv_Sys_User_Login";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Sys_User", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Sys_User)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_MstSv_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_MstSv_Sys_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_MstSvPrdCenter_MstSv_Sys_User_Login(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_MstSv_Sys_User // objRQ_MstSv_Sys_User
                                           ////
                    , out objRT_OS_MstSvPrdCentrer_MstSv_Sys_User // RT_MstSv_Sys_User
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
                objRT_OS_MstSvPrdCentrer_MstSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_MstSvPrdCentrer_MstSv_Sys_User>(objRT_OS_MstSvPrdCentrer_MstSv_Sys_User);
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
                if (objRT_OS_MstSvPrdCentrer_MstSv_Sys_User == null) objRT_OS_MstSvPrdCentrer_MstSv_Sys_User = new RT_OS_MstSvPrdCentrer_MstSv_Sys_User();
                objRT_OS_MstSvPrdCentrer_MstSv_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_MstSvPrdCentrer_MstSv_Sys_User>(ex, objRT_OS_MstSvPrdCentrer_MstSv_Sys_User);
                #endregion
            }
        }
    }
}