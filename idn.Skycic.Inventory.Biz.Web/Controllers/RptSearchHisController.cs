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
    public class RptSearchHisController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_SearchHis> WA_Rpt_SearchHis_Get(RQ_Rpt_SearchHis objRQ_Rpt_SearchHis)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_SearchHis>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_SearchHis.Tid);
            RT_Rpt_SearchHis objRT_Rpt_SearchHis = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_SearchHis_Get";
            string strErrorCodeDefault = "WA_Rpt_SearchHis_Get";

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
                    , objRQ_Rpt_SearchHis.GwUserCode // strGwUserCode
                    , objRQ_Rpt_SearchHis.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_SearchHis_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_SearchHis // RQ_Rpt_SearchHis
                                          ////
                    , out objRT_Rpt_SearchHis // RT_Rpt_SearchHis
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
                objRT_Rpt_SearchHis.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_SearchHis>(objRT_Rpt_SearchHis);
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
                if (objRT_Rpt_SearchHis == null) objRT_Rpt_SearchHis = new RT_Rpt_SearchHis();
                objRT_Rpt_SearchHis.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_SearchHis>(ex, objRT_Rpt_SearchHis);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_SearchHis> WA_Rpt_SearchHis_Add(RQ_Rpt_SearchHis objRQ_Rpt_SearchHis)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_SearchHis>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_SearchHis.Tid);
            RT_Rpt_SearchHis objRT_Rpt_SearchHis = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_SearchHis_Add";
            string strErrorCodeDefault = "WA_Rpt_SearchHis_Add";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Rpt_SearchHis", TJson.JsonConvert.SerializeObject(objRQ_Rpt_SearchHis)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_SearchHis.GwUserCode // strGwUserCode
                    , objRQ_Rpt_SearchHis.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_SearchHis_Add(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_SearchHis // objRQ_Rpt_SearchHis
                                          ////
                    , out objRT_Rpt_SearchHis // RT_Rpt_SearchHis
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
                objRT_Rpt_SearchHis.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_SearchHis>(objRT_Rpt_SearchHis);
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
                if (objRT_Rpt_SearchHis == null) objRT_Rpt_SearchHis = new RT_Rpt_SearchHis();
                objRT_Rpt_SearchHis.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_SearchHis>(ex, objRT_Rpt_SearchHis);
                #endregion
            }
        }
    }
}
