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
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class InvFTempPrintController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_TempPrint> WA_InvF_TempPrint_Get(RQ_InvF_TempPrint objRQ_InvF_TempPrint)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_TempPrint.Tid);
            RT_InvF_TempPrint objRT_InvF_TempPrint = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_TempPrint_Get";
            string strErrorCodeDefault = "WA_InvF_TempPrint_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_InvF_TempPrint", TJson.JsonConvert.SerializeObject(objRQ_InvF_TempPrint)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_TempPrint.GwUserCode // strGwUserCode
                    , objRQ_InvF_TempPrint.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_TempPrint_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_TempPrint // objRQ_InvF_TempPrint
                                           // //
                    , out objRT_InvF_TempPrint // RT_InvF_TempPrint
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
                objRT_InvF_TempPrint.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_TempPrint>(objRT_InvF_TempPrint);
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
                if (objRT_InvF_TempPrint == null) objRT_InvF_TempPrint = new RT_InvF_TempPrint();
                objRT_InvF_TempPrint.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_TempPrint>(ex, objRT_InvF_TempPrint);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_TempPrint> WA_InvF_TempPrint_Save(RQ_InvF_TempPrint objRQ_InvF_TempPrint)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_TempPrint.Tid);
            RT_InvF_TempPrint objRT_InvF_TempPrint = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_TempPrint_Save";
            string strErrorCodeDefault = "WA_InvF_TempPrint_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_InvF_TempPrint", TJson.JsonConvert.SerializeObject(objRQ_InvF_TempPrint)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_TempPrint.GwUserCode // strGwUserCode
                    , objRQ_InvF_TempPrint.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_TempPrint_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_TempPrint // objRQ_InvF_TempPrint
                                           // //
                    , out objRT_InvF_TempPrint // RT_InvF_TempPrint
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
                objRT_InvF_TempPrint.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_TempPrint>(objRT_InvF_TempPrint);
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
                if (objRT_InvF_TempPrint == null) objRT_InvF_TempPrint = new RT_InvF_TempPrint();
                objRT_InvF_TempPrint.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_TempPrint>(ex, objRT_InvF_TempPrint);
                #endregion
            }
        }


    }
}