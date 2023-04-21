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
    public class MstUserMapInventoryController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_UserMapInventory> WA_Mst_UserMapInventory_Get(RQ_Mst_UserMapInventory objRQ_Mst_UserMapInventory)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_UserMapInventory.Tid);
            RT_Mst_UserMapInventory objRT_Mst_UserMapInventory = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_UserMapInventory_Get";
            string strErrorCodeDefault = "WA_Mst_UserMapInventory_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_UserMapInventory", TJson.JsonConvert.SerializeObject(objRQ_Mst_UserMapInventory)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_UserMapInventory.GwUserCode // strGwUserCode
                    , objRQ_Mst_UserMapInventory.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_UserMapInventory_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_UserMapInventory // objRQ_Mst_UserMapInventory
                                                 // //
                    , out objRT_Mst_UserMapInventory // RT_Mst_UserMapInventory
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
                objRT_Mst_UserMapInventory.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_UserMapInventory>(objRT_Mst_UserMapInventory);
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
                if (objRT_Mst_UserMapInventory == null) objRT_Mst_UserMapInventory = new RT_Mst_UserMapInventory();
                objRT_Mst_UserMapInventory.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_UserMapInventory>(ex, objRT_Mst_UserMapInventory);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_UserMapInventory> WA_Mst_UserMapInventory_Save(RQ_Mst_UserMapInventory objRQ_Mst_UserMapInventory)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_UserMapInventory.Tid);
            RT_Mst_UserMapInventory objRT_Mst_UserMapInventory = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_UserMapInventory_Save";
            string strErrorCodeDefault = "WA_Mst_UserMapInventory_Save";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_UserMapInventory", TJson.JsonConvert.SerializeObject(objRQ_Mst_UserMapInventory)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_UserMapInventory.GwUserCode // strGwUserCode
                    , objRQ_Mst_UserMapInventory.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_UserMapInventory_Save(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_UserMapInventory // objRQ_Mst_UserMapInventory
                                                 // //
                    , out objRT_Mst_UserMapInventory // RT_Mst_UserMapInventory
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
                objRT_Mst_UserMapInventory.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_UserMapInventory>(objRT_Mst_UserMapInventory);
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
                if (objRT_Mst_UserMapInventory == null) objRT_Mst_UserMapInventory = new RT_Mst_UserMapInventory();
                objRT_Mst_UserMapInventory.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_UserMapInventory>(ex, objRT_Mst_UserMapInventory);
                #endregion
            }
        }
    }
}