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
    public class MstPartColorController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartColor> WA_Mst_PartColor_Get(RQ_Mst_PartColor objRQ_Mst_PartColor)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartColor>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartColor.Tid);
            RT_Mst_PartColor objRT_Mst_PartColor = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartColor_Get";
            string strErrorCodeDefault = "WA_Mst_PartColor_Get";

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
                    , objRQ_Mst_PartColor.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartColor.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartColor_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartColor // RQ_Mst_PartColor
                                                 ////
                    , out objRT_Mst_PartColor // RT_Mst_PartColor
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
                objRT_Mst_PartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartColor>(objRT_Mst_PartColor);
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
                if (objRT_Mst_PartColor == null) objRT_Mst_PartColor = new RT_Mst_PartColor();
                objRT_Mst_PartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartColor>(ex, objRT_Mst_PartColor);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartColor> WA_Mst_PartColor_Create(RQ_Mst_PartColor objRQ_Mst_PartColor)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartColor>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartColor.Tid);
            RT_Mst_PartColor objRT_Mst_PartColor = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartColor_Create";
            string strErrorCodeDefault = "WA_Mst_PartColor_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartColor", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartColor)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartColor.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartColor.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartColor_Create(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartColor // objRQ_Mst_PartColor
                                                 ////
                    , out objRT_Mst_PartColor // RT_Mst_PartColor
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
                objRT_Mst_PartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartColor>(objRT_Mst_PartColor);
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
                if (objRT_Mst_PartColor == null) objRT_Mst_PartColor = new RT_Mst_PartColor();
                objRT_Mst_PartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartColor>(ex, objRT_Mst_PartColor);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartColor> WA_Mst_PartColor_Update(RQ_Mst_PartColor objRQ_Mst_PartColor)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartColor>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartColor.Tid);
            RT_Mst_PartColor objRT_Mst_PartColor = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartColor_Update";
            string strErrorCodeDefault = "WA_Mst_PartColor_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartColor", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartColor)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartColor.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartColor.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartColor_Update(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartColor // objRQ_Mst_PartColor
                                                 ////
                    , out objRT_Mst_PartColor // objRT_Mst_PartColor
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
                objRT_Mst_PartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartColor>(objRT_Mst_PartColor);
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
                if (objRT_Mst_PartColor == null) objRT_Mst_PartColor = new RT_Mst_PartColor();
                objRT_Mst_PartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartColor>(ex, objRT_Mst_PartColor);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_PartColor> WA_Mst_PartColor_Delete(RQ_Mst_PartColor objRQ_Mst_PartColor)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_PartColor>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_PartColor.Tid);
            RT_Mst_PartColor objRT_Mst_PartColor = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_PartColor_Delete";
            string strErrorCodeDefault = "WA_Mst_PartColor_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_PartColor", TJson.JsonConvert.SerializeObject(objRQ_Mst_PartColor)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartColor.GwUserCode // strGwUserCode
                    , objRQ_Mst_PartColor.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_PartColor_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_PartColor // objRQ_Mst_PartColor
                                                 ////
                    , out objRT_Mst_PartColor // objRT_Mst_PartColor
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
                objRT_Mst_PartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_PartColor>(objRT_Mst_PartColor);
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
                if (objRT_Mst_PartColor == null) objRT_Mst_PartColor = new RT_Mst_PartColor();
                objRT_Mst_PartColor.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_PartColor>(ex, objRT_Mst_PartColor);
                #endregion
            }
        }
    }
}
