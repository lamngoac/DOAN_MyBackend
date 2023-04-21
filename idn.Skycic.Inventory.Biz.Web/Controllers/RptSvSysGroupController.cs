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
    public class RptSvSysGroupController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_RptSv_Sys_Group> WA_RptSv_Sys_Group_Get(RQ_RptSv_Sys_Group objRQ_RptSv_Sys_Group)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_Group>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Group.Tid);
            RT_RptSv_Sys_Group objRT_RptSv_Sys_Group = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Group_Get";
            string strErrorCodeDefault = "WA_RptSv_Sys_Group_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Group)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Group.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Group_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Group // objRQ_RptSv_Sys_Group
                                      ////
                    , out objRT_RptSv_Sys_Group // RT_RptSv_Sys_Group
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
                objRT_RptSv_Sys_Group.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_RptSv_Sys_Group>(objRT_RptSv_Sys_Group);
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
                if (objRT_RptSv_Sys_Group == null) objRT_RptSv_Sys_Group = new RT_RptSv_Sys_Group();
                objRT_RptSv_Sys_Group.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_RptSv_Sys_Group>(ex, objRT_RptSv_Sys_Group);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_RptSv_Sys_Group> WA_RptSv_Sys_Group_Create(RQ_RptSv_Sys_Group objRQ_RptSv_Sys_Group)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_Group>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Group.Tid);
            RT_RptSv_Sys_Group objRT_RptSv_Sys_Group = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Group_Create";
            string strErrorCodeDefault = "WA_RptSv_Sys_Group_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Group)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Group.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Group_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Group // objRQ_RptSv_Sys_Group
                                      ////
                    , out objRT_RptSv_Sys_Group // RT_RptSv_Sys_Group
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
                objRT_RptSv_Sys_Group.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_RptSv_Sys_Group>(objRT_RptSv_Sys_Group);
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
                if (objRT_RptSv_Sys_Group == null) objRT_RptSv_Sys_Group = new RT_RptSv_Sys_Group();
                objRT_RptSv_Sys_Group.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_RptSv_Sys_Group>(ex, objRT_RptSv_Sys_Group);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_RptSv_Sys_Group> WA_RptSv_Sys_Group_Update(RQ_RptSv_Sys_Group objRQ_RptSv_Sys_Group)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_Group>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Group.Tid);
            RT_RptSv_Sys_Group objRT_RptSv_Sys_Group = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Group_Update";
            string strErrorCodeDefault = "WA_RptSv_Sys_Group_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Group)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Group.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Group_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Group // objRQ_RptSv_Sys_Group
                                      ////
                    , out objRT_RptSv_Sys_Group // RT_RptSv_Sys_Group
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
                objRT_RptSv_Sys_Group.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_RptSv_Sys_Group>(objRT_RptSv_Sys_Group);
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
                if (objRT_RptSv_Sys_Group == null) objRT_RptSv_Sys_Group = new RT_RptSv_Sys_Group();
                objRT_RptSv_Sys_Group.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_RptSv_Sys_Group>(ex, objRT_RptSv_Sys_Group);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_RptSv_Sys_Group> WA_RptSv_Sys_Group_Delete(RQ_RptSv_Sys_Group objRQ_RptSv_Sys_Group)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_RptSv_Sys_Group>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_RptSv_Sys_Group.Tid);
            RT_RptSv_Sys_Group objRT_RptSv_Sys_Group = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Sys_Group_Delete";
            string strErrorCodeDefault = "WA_RptSv_Sys_Group_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_RptSv_Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_RptSv_Sys_Group)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_RptSv_Sys_Group.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Sys_Group_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_RptSv_Sys_Group // objRQ_RptSv_Sys_Group
                                      ////
                    , out objRT_RptSv_Sys_Group // RT_RptSv_Sys_Group
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
                objRT_RptSv_Sys_Group.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_RptSv_Sys_Group>(objRT_RptSv_Sys_Group);
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
                if (objRT_RptSv_Sys_Group == null) objRT_RptSv_Sys_Group = new RT_RptSv_Sys_Group();
                objRT_RptSv_Sys_Group.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_RptSv_Sys_Group>(ex, objRT_RptSv_Sys_Group);
                #endregion
            }
        }
    }
}
