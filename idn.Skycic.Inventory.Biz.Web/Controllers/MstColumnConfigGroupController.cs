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
    public class MstColumnConfigGroupController : ApiControllerBase
    {

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ColumnConfigGroup> WA_Mst_ColumnConfigGroup_Get(RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfigGroup.Tid);
            RT_Mst_ColumnConfigGroup objRT_Mst_ColumnConfigGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ColumnConfigGroup_Get";
            string strErrorCodeDefault = "WA_Mst_ColumnConfigGroup_Get";

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
                    , objRQ_Mst_ColumnConfigGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfigGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ColumnConfigGroup_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ColumnConfigGroup // objRQ_Mst_ColumnConfigGroup
                                                  ////
                    , out objRT_Mst_ColumnConfigGroup // RT_Mst_ColumnConfigGroup
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
                objRT_Mst_ColumnConfigGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ColumnConfigGroup>(objRT_Mst_ColumnConfigGroup);
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
                if (objRT_Mst_ColumnConfigGroup == null) objRT_Mst_ColumnConfigGroup = new RT_Mst_ColumnConfigGroup();
                objRT_Mst_ColumnConfigGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ColumnConfigGroup>(ex, objRT_Mst_ColumnConfigGroup);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ColumnConfigGroup> WA_Mst_ColumnConfigGroup_Create(RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfigGroup.Tid);
            RT_Mst_ColumnConfigGroup objRT_Mst_ColumnConfigGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ColumnConfigGroup_Create";
            string strErrorCodeDefault = "WA_Mst_ColumnConfigGroup_Create";

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
                    , objRQ_Mst_ColumnConfigGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfigGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ColumnConfigGroup_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ColumnConfigGroup // objRQ_Mst_ColumnConfigGroup
                                                  ////
                    , out objRT_Mst_ColumnConfigGroup // RT_Mst_ColumnConfigGroup
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
                objRT_Mst_ColumnConfigGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ColumnConfigGroup>(objRT_Mst_ColumnConfigGroup);
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
                if (objRT_Mst_ColumnConfigGroup == null) objRT_Mst_ColumnConfigGroup = new RT_Mst_ColumnConfigGroup();
                objRT_Mst_ColumnConfigGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ColumnConfigGroup>(ex, objRT_Mst_ColumnConfigGroup);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ColumnConfigGroup> WA_Mst_ColumnConfigGroup_Update(RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfigGroup.Tid);
            RT_Mst_ColumnConfigGroup objRT_Mst_ColumnConfigGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ColumnConfigGroup_Update";
            string strErrorCodeDefault = "WA_Mst_ColumnConfigGroup_Update";

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
                    , objRQ_Mst_ColumnConfigGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfigGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ColumnConfigGroup_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ColumnConfigGroup // objRQ_Mst_ColumnConfigGroup
                                                  ////
                    , out objRT_Mst_ColumnConfigGroup // RT_Mst_ColumnConfigGroup
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
                objRT_Mst_ColumnConfigGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ColumnConfigGroup>(objRT_Mst_ColumnConfigGroup);
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
                if (objRT_Mst_ColumnConfigGroup == null) objRT_Mst_ColumnConfigGroup = new RT_Mst_ColumnConfigGroup();
                objRT_Mst_ColumnConfigGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ColumnConfigGroup>(ex, objRT_Mst_ColumnConfigGroup);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ColumnConfigGroup> WA_Mst_ColumnConfigGroup_Delete(RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ColumnConfigGroup.Tid);
            RT_Mst_ColumnConfigGroup objRT_Mst_ColumnConfigGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ColumnConfigGroup_Delete";
            string strErrorCodeDefault = "WA_Mst_ColumnConfigGroup_Delete";

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
                    , objRQ_Mst_ColumnConfigGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ColumnConfigGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ColumnConfigGroup_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ColumnConfigGroup // objRQ_Mst_ColumnConfigGroup
                                                  ////
                    , out objRT_Mst_ColumnConfigGroup // RT_Mst_ColumnConfigGroup
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
                objRT_Mst_ColumnConfigGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ColumnConfigGroup>(objRT_Mst_ColumnConfigGroup);
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
                if (objRT_Mst_ColumnConfigGroup == null) objRT_Mst_ColumnConfigGroup = new RT_Mst_ColumnConfigGroup();
                objRT_Mst_ColumnConfigGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ColumnConfigGroup>(ex, objRT_Mst_ColumnConfigGroup);
                #endregion
            }
        }
    }
}
