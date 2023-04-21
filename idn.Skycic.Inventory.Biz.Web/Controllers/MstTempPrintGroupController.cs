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
    public class MstTempPrintGroupController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_TempPrintGroup> WA_Mst_TempPrintGroup_Get(RQ_Mst_TempPrintGroup objRQ_Mst_TempPrintGroup)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TempPrintGroup.Tid);
            RT_Mst_TempPrintGroup objRT_Mst_TempPrintGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TempPrintGroup_Get";
            string strErrorCodeDefault = "WA_Mst_TempPrintGroup_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_TempPrintGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_TempPrintGroup)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TempPrintGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_TempPrintGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_TempPrintGroup_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TempPrintGroup // objRQ_Mst_TempPrintGroup
                                               // //
                    , out objRT_Mst_TempPrintGroup // RT_Mst_TempPrintGroup
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
                objRT_Mst_TempPrintGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_TempPrintGroup>(objRT_Mst_TempPrintGroup);
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
                if (objRT_Mst_TempPrintGroup == null) objRT_Mst_TempPrintGroup = new RT_Mst_TempPrintGroup();
                objRT_Mst_TempPrintGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_TempPrintGroup>(ex, objRT_Mst_TempPrintGroup);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_TempPrintGroup> WA_Mst_TempPrintGroup_Save(RQ_Mst_TempPrintGroup objRQ_Mst_TempPrintGroup)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_TempPrintGroup.Tid);
            RT_Mst_TempPrintGroup objRT_Mst_TempPrintGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_TempPrintGroup_Save";
            string strErrorCodeDefault = "WA_Mst_TempPrintGroup_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_TempPrintGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_TempPrintGroup)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TempPrintGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_TempPrintGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_TempPrintGroup_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_TempPrintGroup // objRQ_Mst_TempPrintGroup
                                               // //
                    , out objRT_Mst_TempPrintGroup // RT_Mst_TempPrintGroup
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
                objRT_Mst_TempPrintGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_TempPrintGroup>(objRT_Mst_TempPrintGroup);
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
                if (objRT_Mst_TempPrintGroup == null) objRT_Mst_TempPrintGroup = new RT_Mst_TempPrintGroup();
                objRT_Mst_TempPrintGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_TempPrintGroup>(ex, objRT_Mst_TempPrintGroup);
                #endregion
            }
        }


    }
}