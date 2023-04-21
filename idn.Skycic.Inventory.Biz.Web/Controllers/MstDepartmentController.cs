using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using System.Collections;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstDepartmentController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Department> WA_Mst_Department_Get(RQ_Mst_Department objRQ_Mst_Department)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Department>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Department.Tid);
            RT_Mst_Department objRT_Mst_Department = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Department_Get";
            string strErrorCodeDefault = "WA_Mst_Department_Get";

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
                    , objRQ_Mst_Department.GwUserCode // strGwUserCode
                    , objRQ_Mst_Department.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Department_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Department // objRQ_Mst_Department
                                           ////
                    , out objRT_Mst_Department // RT_Mst_Department
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
                objRT_Mst_Department.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Department>(objRT_Mst_Department);
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
                if (objRT_Mst_Department == null) objRT_Mst_Department = new RT_Mst_Department();
                objRT_Mst_Department.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Department>(ex, objRT_Mst_Department);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Department> WA_Mst_Department_Create(RQ_Mst_Department objRQ_Mst_Department)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Department>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Department.Tid);
            RT_Mst_Department objRT_Mst_Department = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Department_Create";
            string strErrorCodeDefault = "WA_Mst_Department_Create";

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
                    , objRQ_Mst_Department.GwUserCode // strGwUserCode
                    , objRQ_Mst_Department.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Department_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Department // objRQ_Mst_Department
                                           ////
                    , out objRT_Mst_Department // RT_Mst_Department
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
                objRT_Mst_Department.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Department>(objRT_Mst_Department);
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
                if (objRT_Mst_Department == null) objRT_Mst_Department = new RT_Mst_Department();
                objRT_Mst_Department.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Department>(ex, objRT_Mst_Department);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Department> WA_Mst_Department_Update(RQ_Mst_Department objRQ_Mst_Department)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Department>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Department.Tid);
            RT_Mst_Department objRT_Mst_Department = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Department_Update";
            string strErrorCodeDefault = "WA_Mst_Department_Update";

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
                    , objRQ_Mst_Department.GwUserCode // strGwUserCode
                    , objRQ_Mst_Department.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Department_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Department // objRQ_Mst_Department
                                           ////
                    , out objRT_Mst_Department // RT_Mst_Department
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
                objRT_Mst_Department.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Department>(objRT_Mst_Department);
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
                if (objRT_Mst_Department == null) objRT_Mst_Department = new RT_Mst_Department();
                objRT_Mst_Department.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Department>(ex, objRT_Mst_Department);
                #endregion
            }
        }
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Department> WA_Mst_Department_Delete(RQ_Mst_Department objRQ_Mst_Department)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Department>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Department.Tid);
            RT_Mst_Department objRT_Mst_Department = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Department_Delete";
            string strErrorCodeDefault = "WA_Mst_Department_Delete";

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
                    , objRQ_Mst_Department.GwUserCode // strGwUserCode
                    , objRQ_Mst_Department.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Department_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Department // objRQ_Mst_Department
                                           ////
                    , out objRT_Mst_Department // RT_Mst_Department
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
                objRT_Mst_Department.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Department>(objRT_Mst_Department);
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
                if (objRT_Mst_Department == null) objRT_Mst_Department = new RT_Mst_Department();
                objRT_Mst_Department.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Department>(ex, objRT_Mst_Department);
                #endregion
            }
        }

    }
}
