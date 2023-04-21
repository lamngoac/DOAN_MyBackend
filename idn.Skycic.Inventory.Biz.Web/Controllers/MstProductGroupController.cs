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

using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstProductGroupController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ProductGroup> WA_Mst_ProductGroup_Get(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ProductGroup.Tid);
            RT_Mst_ProductGroup objRT_Mst_ProductGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ProductGroup_Get";
            string strErrorCodeDefault = "WA_Mst_ProductGroup_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_ProductGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_ProductGroup)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ProductGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ProductGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ProductGroup_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ProductGroup // objRQ_Mst_ProductGroup
                                             // //
                    , out objRT_Mst_ProductGroup // RT_Mst_ProductGroup
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
                objRT_Mst_ProductGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ProductGroup>(objRT_Mst_ProductGroup);
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
                if (objRT_Mst_ProductGroup == null) objRT_Mst_ProductGroup = new RT_Mst_ProductGroup();
                objRT_Mst_ProductGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ProductGroup>(ex, objRT_Mst_ProductGroup);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_ProductGroup> WA_Mst_ProductGroup_Save(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_ProductGroup.Tid);
            RT_Mst_ProductGroup objRT_Mst_ProductGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_ProductGroup_Save";
            string strErrorCodeDefault = "WA_Mst_ProductGroup_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_ProductGroup", TJson.JsonConvert.SerializeObject(objRQ_Mst_ProductGroup)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ProductGroup.GwUserCode // strGwUserCode
                    , objRQ_Mst_ProductGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_ProductGroup_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_ProductGroup // objRQ_Mst_ProductGroup
                                             // //
                    , out objRT_Mst_ProductGroup // RT_Mst_ProductGroup
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
                objRT_Mst_ProductGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_ProductGroup>(objRT_Mst_ProductGroup);
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
                if (objRT_Mst_ProductGroup == null) objRT_Mst_ProductGroup = new RT_Mst_ProductGroup();
                objRT_Mst_ProductGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_ProductGroup>(ex, objRT_Mst_ProductGroup);
                #endregion
            }
        }
    } 
}
