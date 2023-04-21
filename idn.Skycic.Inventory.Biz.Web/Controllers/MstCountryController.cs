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
    public class MstCountryController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Country> WA_Mst_Country_Get(RQ_Mst_Country objRQ_Mst_Country)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Country>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Country.Tid);
            RT_Mst_Country objRT_Mst_Country = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Country_Get";
            string strErrorCodeDefault = "WA_Mst_Country_Get";

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
                    , objRQ_Mst_Country.GwUserCode // strGwUserCode
                    , objRQ_Mst_Country.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Country_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Country // RQ_Mst_Country
                                         ////
                    , out objRT_Mst_Country // RT_Mst_Country
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
                objRT_Mst_Country.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Country>(objRT_Mst_Country);
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
                if (objRT_Mst_Country == null) objRT_Mst_Country = new RT_Mst_Country();
                objRT_Mst_Country.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Country>(ex, objRT_Mst_Country);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Country> WA_Mst_Country_Create(RQ_Mst_Country objRQ_Mst_Country)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Country>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Country.Tid);
            RT_Mst_Country objRT_Mst_Country = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Country_Create";
            string strErrorCodeDefault = "WA_Mst_Country_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Country", TJson.JsonConvert.SerializeObject(objRQ_Mst_Country)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Country.GwUserCode // strGwUserCode
                    , objRQ_Mst_Country.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Country_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Country // objRQ_Mst_Country
                                         ////
                    , out objRT_Mst_Country // RT_Mst_Country
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
                objRT_Mst_Country.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Country>(objRT_Mst_Country);
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
                if (objRT_Mst_Country == null) objRT_Mst_Country = new RT_Mst_Country();
                objRT_Mst_Country.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Country>(ex, objRT_Mst_Country);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Country> WA_Mst_Country_Update(RQ_Mst_Country objRQ_Mst_Country)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Country>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Country.Tid);
            RT_Mst_Country objRT_Mst_Country = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Country_Update";
            string strErrorCodeDefault = "WA_Mst_Country_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Country", TJson.JsonConvert.SerializeObject(objRQ_Mst_Country)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Country.GwUserCode // strGwUserCode
                    , objRQ_Mst_Country.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Country_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Country // objRQ_Mst_Country
                                         ////
                    , out objRT_Mst_Country // objRT_Mst_Country
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
                objRT_Mst_Country.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Country>(objRT_Mst_Country);
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
                if (objRT_Mst_Country == null) objRT_Mst_Country = new RT_Mst_Country();
                objRT_Mst_Country.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Country>(ex, objRT_Mst_Country);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_Country> WA_Mst_Country_Delete(RQ_Mst_Country objRQ_Mst_Country)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_Country>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_Country.Tid);
            RT_Mst_Country objRT_Mst_Country = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_Country_Delete";
            string strErrorCodeDefault = "WA_Mst_Country_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Country", TJson.JsonConvert.SerializeObject(objRQ_Mst_Country)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Country.GwUserCode // strGwUserCode
                    , objRQ_Mst_Country.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_Country_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_Country // objRQ_Mst_Country
                                         ////
                    , out objRT_Mst_Country // objRT_Mst_Country
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
                objRT_Mst_Country.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_Country>(objRT_Mst_Country);
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
                if (objRT_Mst_Country == null) objRT_Mst_Country = new RT_Mst_Country();
                objRT_Mst_Country.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_Country>(ex, objRT_Mst_Country);
                #endregion
            }
        }
    }
}
