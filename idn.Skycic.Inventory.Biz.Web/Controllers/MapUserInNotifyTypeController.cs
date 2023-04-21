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
    public class MapUserInNotifyTypeController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Map_UserInNotifyType> WA_Map_UserInNotifyType_Save(RQ_Map_UserInNotifyType objRQ_Map_UserInNotifyType)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Map_UserInNotifyType.Tid);
            RT_Map_UserInNotifyType objRT_Map_UserInNotifyType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Map_UserInNotifyType_Save";
            string strErrorCodeDefault = "WA_Map_UserInNotifyType_Save";

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
                    , objRQ_Map_UserInNotifyType.GwUserCode // strGwUserCode
                    , objRQ_Map_UserInNotifyType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Map_UserInNotifyType_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Map_UserInNotifyType // objRQ_Map_UserInNotifyType
                                                 ////
                    , out objRT_Map_UserInNotifyType // RT_Map_UserInNotifyType
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
                objRT_Map_UserInNotifyType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Map_UserInNotifyType>(objRT_Map_UserInNotifyType);
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
                if (objRT_Map_UserInNotifyType == null) objRT_Map_UserInNotifyType = new RT_Map_UserInNotifyType();
                objRT_Map_UserInNotifyType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Map_UserInNotifyType>(ex, objRT_Map_UserInNotifyType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Map_UserInNotifyType> WA_Map_UserInNotifyType_Get(RQ_Map_UserInNotifyType objRQ_Map_UserInNotifyType)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Map_UserInNotifyType.Tid);
            RT_Map_UserInNotifyType objRT_Map_UserInNotifyType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Map_UserInNotifyType_Get";
            string strErrorCodeDefault = "WA_Map_UserInNotifyType_Get";

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
                    , objRQ_Map_UserInNotifyType.GwUserCode // strGwUserCode
                    , objRQ_Map_UserInNotifyType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Map_UserInNotifyType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Map_UserInNotifyType // objRQ_Map_UserInNotifyType
                                                 ////
                    , out objRT_Map_UserInNotifyType // RT_Map_UserInNotifyType
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
                objRT_Map_UserInNotifyType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Map_UserInNotifyType>(objRT_Map_UserInNotifyType);
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
                if (objRT_Map_UserInNotifyType == null) objRT_Map_UserInNotifyType = new RT_Map_UserInNotifyType();
                objRT_Map_UserInNotifyType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Map_UserInNotifyType>(ex, objRT_Map_UserInNotifyType);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ManageNotify> WA_Mst_ManageNotify_Create(RQ_Mst_ManageNotify objRQ_Mst_ManageNotify)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ManageNotify.Tid);
            RT_Mst_ManageNotify objRT_Mst_ManageNotify = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ManageNotify_Create";
            string strErrorCodeDefault = "WA_Mst_ManageNotify_Create";

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
                    , objRQ_Mst_ManageNotify.GwUserCode // strGwUserCode
                    , objRQ_Mst_ManageNotify.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ManageNotify_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ManageNotify // objRQ_Mst_ManageNotify
                                             ////
                    , out objRT_Mst_ManageNotify // RT_Mst_ManageNotify
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
                objRT_Mst_ManageNotify.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ManageNotify>(objRT_Mst_ManageNotify);
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
                if (objRT_Mst_ManageNotify == null) objRT_Mst_ManageNotify = new RT_Mst_ManageNotify();
                objRT_Mst_ManageNotify.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ManageNotify>(ex, objRT_Mst_ManageNotify);
                #endregion
            }
        }
    }
}
