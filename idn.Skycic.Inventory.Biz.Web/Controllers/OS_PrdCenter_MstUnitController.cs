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
    public class OS_PrdCenter_MstUnitController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Unit> WA_OS_PrdCenter_Mst_Unit_Get(RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Unit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Unit.Tid);
            RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Unit_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Unit_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Unit", TJson.JsonConvert.SerializeObject(objRQ_Mst_Unit)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Unit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Unit_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Unit // objRQ_Mst_Unit
                                                 // //
                    , out objRT_OS_PrdCenter_Mst_Unit // RT_Mst_Unit
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
                objRT_OS_PrdCenter_Mst_Unit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Unit>(objRT_OS_PrdCenter_Mst_Unit);
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
                if (objRT_OS_PrdCenter_Mst_Unit == null) objRT_OS_PrdCenter_Mst_Unit = new RT_OS_PrdCenter_Mst_Unit();
                objRT_OS_PrdCenter_Mst_Unit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Unit>(ex, objRT_OS_PrdCenter_Mst_Unit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Unit> WA_OS_PrdCenter_Mst_Unit_Create(RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Unit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Unit.Tid);
            RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Unit_Create";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Unit_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Unit", TJson.JsonConvert.SerializeObject(objRQ_Mst_Unit)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Unit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Unit_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Unit // objRQ_Mst_Unit
                                                 // //
                    , out objRT_OS_PrdCenter_Mst_Unit // RT_Mst_Unit
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
                objRT_OS_PrdCenter_Mst_Unit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Unit>(objRT_OS_PrdCenter_Mst_Unit);
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
                if (objRT_OS_PrdCenter_Mst_Unit == null) objRT_OS_PrdCenter_Mst_Unit = new RT_OS_PrdCenter_Mst_Unit();
                objRT_OS_PrdCenter_Mst_Unit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Unit>(ex, objRT_OS_PrdCenter_Mst_Unit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Unit> WA_OS_PrdCenter_Mst_Unit_Update(RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Unit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Unit.Tid);
            RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_Unit_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_Unit_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Unit", TJson.JsonConvert.SerializeObject(objRQ_Mst_Unit)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Unit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Unit_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Unit // objRQ_Mst_Unit
                                                 // //
                    , out objRT_OS_PrdCenter_Mst_Unit // RT_Mst_Unit
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
                objRT_OS_PrdCenter_Mst_Unit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Unit>(objRT_OS_PrdCenter_Mst_Unit);
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
                if (objRT_OS_PrdCenter_Mst_Unit == null) objRT_OS_PrdCenter_Mst_Unit = new RT_OS_PrdCenter_Mst_Unit();
                objRT_OS_PrdCenter_Mst_Unit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Unit>(ex, objRT_OS_PrdCenter_Mst_Unit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_Unit> WA_OS_PrdCenter_Mst_Unit_Delete(RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_Unit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_Unit.Tid);
            RT_OS_PrdCenter_Mst_Unit objRT_OS_PrdCenter_Mst_Unit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Unit_Delete";
            string strErrorCodeDefault = "WA_Mst_Unit_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Unit", TJson.JsonConvert.SerializeObject(objRQ_Mst_Unit)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Unit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_Unit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_Unit_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_Unit // objRQ_Mst_Unit
                                                 // //
                    , out objRT_OS_PrdCenter_Mst_Unit // RT_Mst_Unit
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
                objRT_OS_PrdCenter_Mst_Unit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_Unit>(objRT_OS_PrdCenter_Mst_Unit);
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
                if (objRT_OS_PrdCenter_Mst_Unit == null) objRT_OS_PrdCenter_Mst_Unit = new RT_OS_PrdCenter_Mst_Unit();
                objRT_OS_PrdCenter_Mst_Unit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_Unit>(ex, objRT_OS_PrdCenter_Mst_Unit);
                #endregion
            }
        }
    }
}
