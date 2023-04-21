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
    public class OS_PrdCenter_MstSpecUnitController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecUnit> WA_OS_PrdCenter_Mst_SpecUnit_Get(RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecUnit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecUnit.Tid);
            RT_OS_PrdCenter_Mst_SpecUnit objRT_Mst_SpecUnit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecUnit_Get";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecUnit_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecUnit", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecUnit)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecUnit_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecUnit // objRQ_Mst_SpecUnit
                                         // //
                    , out objRT_Mst_SpecUnit // RT_Mst_SpecUnit
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
                objRT_Mst_SpecUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecUnit>(objRT_Mst_SpecUnit);
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
                if (objRT_Mst_SpecUnit == null) objRT_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
                objRT_Mst_SpecUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecUnit>(ex, objRT_Mst_SpecUnit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecUnit> WA_OS_PrdCenter_Mst_SpecUnit_Create(RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecUnit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecUnit.Tid);
            RT_OS_PrdCenter_Mst_SpecUnit objRT_Mst_SpecUnit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecUnit_Create";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecUnit_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecUnit", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecUnit)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecUnit_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecUnit // objRQ_Mst_SpecUnit
                                                      // //
                    , out objRT_Mst_SpecUnit // RT_Mst_SpecUnit
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
                objRT_Mst_SpecUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecUnit>(objRT_Mst_SpecUnit);
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
                if (objRT_Mst_SpecUnit == null) objRT_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
                objRT_Mst_SpecUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecUnit>(ex, objRT_Mst_SpecUnit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecUnit> WA_OS_PrdCenter_Mst_SpecUnit_Update(RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecUnit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecUnit.Tid);
            RT_OS_PrdCenter_Mst_SpecUnit objRT_Mst_SpecUnit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecUnit_Update";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecUnit_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecUnit", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecUnit)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecUnit_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecUnit // objRQ_Mst_SpecUnit
                                                      // //
                    , out objRT_Mst_SpecUnit // RT_Mst_SpecUnit
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
                objRT_Mst_SpecUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecUnit>(objRT_Mst_SpecUnit);
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
                if (objRT_Mst_SpecUnit == null) objRT_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
                objRT_Mst_SpecUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecUnit>(ex, objRT_Mst_SpecUnit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_Mst_SpecUnit> WA_OS_PrdCenter_Mst_SpecUnit_Delete(RQ_OS_PrdCenter_Mst_SpecUnit objRQ_OS_PrdCenter_Mst_SpecUnit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_Mst_SpecUnit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_Mst_SpecUnit.Tid);
            RT_OS_PrdCenter_Mst_SpecUnit objRT_Mst_SpecUnit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_Mst_SpecUnit_Delete";
            string strErrorCodeDefault = "WA_OS_PrdCenter_Mst_SpecUnit_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_SpecUnit", TJson.JsonConvert.SerializeObject(objRQ_Mst_SpecUnit)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_Mst_SpecUnit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                ////
                mdsReturn = _biz.WAS_OS_PrdCenter_Mst_SpecUnit_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_Mst_SpecUnit // objRQ_Mst_SpecUnit
                                                      // //
                    , out objRT_Mst_SpecUnit // RT_Mst_SpecUnit
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
                objRT_Mst_SpecUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_OS_PrdCenter_Mst_SpecUnit>(objRT_Mst_SpecUnit);
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
                if (objRT_Mst_SpecUnit == null) objRT_Mst_SpecUnit = new RT_OS_PrdCenter_Mst_SpecUnit();
                objRT_Mst_SpecUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_Mst_SpecUnit>(ex, objRT_Mst_SpecUnit);
                #endregion
            }
        }
    }
}
