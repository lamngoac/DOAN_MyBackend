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
    public class MstPartUnitController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartUnit> WA_Mst_PartUnit_Get(RQ_Mst_PartUnit objRQ_Mst_PartUnit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartUnit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartUnit.Tid);
            RT_Mst_PartUnit objRT_Mst_PartUnit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartUnit_Get";
            string strErrorCodeDefault = "WA_Mst_PartUnit_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Currency", TJson.JsonConvert.SerializeObject(objRQ_Mst_Currency)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartUnit.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartUnit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartUnit_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartUnit // RQ_Mst_PartUnit
                                          ////
                    , out objRT_Mst_PartUnit // RT_Mst_PartUnit
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
                objRT_Mst_PartUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartUnit>(objRT_Mst_PartUnit);
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
                if (objRT_Mst_PartUnit == null) objRT_Mst_PartUnit = new RT_Mst_PartUnit();
                objRT_Mst_PartUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartUnit>(ex, objRT_Mst_PartUnit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartUnit> WA_Mst_PartUnit_Create(RQ_Mst_PartUnit objRQ_Mst_PartUnit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartUnit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartUnit.Tid);
            RT_Mst_PartUnit objRT_Mst_PartUnit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartUnit_Create";
            string strErrorCodeDefault = "WA_Mst_PartUnit_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartUnit", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartUnit)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartUnit.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartUnit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartUnit_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartUnit // objRQ_Mst_PartUnit
                                          ////
                    , out objRT_Mst_PartUnit // RT_Mst_PartUnit
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
                objRT_Mst_PartUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartUnit>(objRT_Mst_PartUnit);
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
                if (objRT_Mst_PartUnit == null) objRT_Mst_PartUnit = new RT_Mst_PartUnit();
                objRT_Mst_PartUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartUnit>(ex, objRT_Mst_PartUnit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartUnit> WA_Mst_PartUnit_Update(RQ_Mst_PartUnit objRQ_Mst_PartUnit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartUnit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartUnit.Tid);
            RT_Mst_PartUnit objRT_Mst_PartUnit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartUnit_Update";
            string strErrorCodeDefault = "WA_Mst_PartUnit_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartUnit", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartUnit)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartUnit.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartUnit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartUnit_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartUnit // objRQ_Mst_PartUnit
                                          ////
                    , out objRT_Mst_PartUnit // objRT_Mst_PartUnit
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
                objRT_Mst_PartUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartUnit>(objRT_Mst_PartUnit);
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
                if (objRT_Mst_PartUnit == null) objRT_Mst_PartUnit = new RT_Mst_PartUnit();
                objRT_Mst_PartUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartUnit>(ex, objRT_Mst_PartUnit);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartUnit> WA_Mst_PartUnit_Delete(RQ_Mst_PartUnit objRQ_Mst_PartUnit)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartUnit>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartUnit.Tid);
            RT_Mst_PartUnit objRT_Mst_PartUnit = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartUnit_Delete";
            string strErrorCodeDefault = "WA_Mst_PartUnit_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartUnit", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartUnit)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartUnit.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartUnit.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartUnit_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartUnit // objRQ_Mst_PartUnit
                                          ////
                    , out objRT_Mst_PartUnit // objRT_Mst_PartUnit
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
                objRT_Mst_PartUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartUnit>(objRT_Mst_PartUnit);
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
                if (objRT_Mst_PartUnit == null) objRT_Mst_PartUnit = new RT_Mst_PartUnit();
                objRT_Mst_PartUnit.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartUnit>(ex, objRT_Mst_PartUnit);
                #endregion
            }
        }
    }
}
