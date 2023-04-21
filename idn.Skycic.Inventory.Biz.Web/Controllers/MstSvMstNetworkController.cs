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
    public class MstSvMstNetworkController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Mst_Network> WA_MstSv_Mst_Network_GetByMST(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Mst_Network>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Mst_Network_GetByMST";
			string strErrorCodeDefault = "WA_MstSv_Mst_Network_GetByMST";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_Mst_Network_GetByMST(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network // objRQ_MstSv_Mst_Network
										   ////
					, out objRT_MstSv_Mst_Network // RT_MstSv_Mst_Network
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
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Mst_Network>(objRT_MstSv_Mst_Network);
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
				if (objRT_MstSv_Mst_Network == null) objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Mst_Network>(ex, objRT_MstSv_Mst_Network);
				#endregion
			}
		}

        [AcceptVerbs("POST")]
        public ServiceResult<RT_MstSv_Mst_Network> WA_MstSv_Mst_Network_Get(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Mst_Network>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
            RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_MstSv_Mst_Network_Get";
            string strErrorCodeDefault = "WA_MstSv_Mst_Network_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
                    , objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_MstSv_Mst_Network_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_MstSv_Mst_Network // objRQ_MstSv_Mst_Network
                                              ////
                    , out objRT_MstSv_Mst_Network // RT_MstSv_Mst_Network
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
                objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_MstSv_Mst_Network>(objRT_MstSv_Mst_Network);
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
                if (objRT_MstSv_Mst_Network == null) objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
                objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_MstSv_Mst_Network>(ex, objRT_MstSv_Mst_Network);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Mst_Network> WA_MstSv_Mst_Network_Gen(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Mst_Network>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Mst_Network_Gen";
			string strErrorCodeDefault = "WA_MstSv_Mst_Network_Gen";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_Mst_Network_Gen(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network // objRQ_MstSv_Mst_Network
											  ////
					, out objRT_MstSv_Mst_Network // RT_MstSv_Mst_Network
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
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Mst_Network>(objRT_MstSv_Mst_Network);
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
				if (objRT_MstSv_Mst_Network == null) objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Mst_Network>(ex, objRT_MstSv_Mst_Network);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Mst_Network> WA_RptSvCenter_Mst_Network_Gen(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Mst_Network>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSvCenter_Mst_Network_Gen";
			string strErrorCodeDefault = "WA_RptSvCenter_Mst_Network_Gen";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSvCenter_Mst_Network_Gen(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network // objRQ_MstSv_Mst_Network
											  ////
					, out objRT_MstSv_Mst_Network // RT_MstSv_Mst_Network
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
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Mst_Network>(objRT_MstSv_Mst_Network);
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
				if (objRT_MstSv_Mst_Network == null) objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Mst_Network>(ex, objRT_MstSv_Mst_Network);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Mst_Network> WA_MstSv_Mst_Network_Add(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Mst_Network>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_MstSv_Mst_Network_Add";
			string strErrorCodeDefault = "WA_MstSv_Mst_Network_Add";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_MstSv_Mst_Network_Add(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network // objRQ_MstSv_Mst_Network
											  ////
					, out objRT_MstSv_Mst_Network // RT_MstSv_Mst_Network
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
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Mst_Network>(objRT_MstSv_Mst_Network);
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
				if (objRT_MstSv_Mst_Network == null) objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Mst_Network>(ex, objRT_MstSv_Mst_Network);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_MstSv_Mst_Network> WA_RptSvLocal_Mst_Network_InsertMQ(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Mst_Network>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Mst_Network.Tid);
			RT_MstSv_Mst_Network objRT_MstSv_Mst_Network = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSvLocal_Mst_Network_InsertMQ";
			string strErrorCodeDefault = "WA_RptSvLocal_Mst_Network_InsertMQ";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_MstSv_Mst_Network", TJson.JsonConvert.SerializeObject(objRQ_MstSv_Mst_Network)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network.GwUserCode // strGwUserCode
					, objRQ_MstSv_Mst_Network.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSvLocal_Mst_Network_InsertMQ(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_MstSv_Mst_Network // objRQ_MstSv_Mst_Network
											  ////
					, out objRT_MstSv_Mst_Network // RT_MstSv_Mst_Network
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
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_MstSv_Mst_Network>(objRT_MstSv_Mst_Network);
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
				if (objRT_MstSv_Mst_Network == null) objRT_MstSv_Mst_Network = new RT_MstSv_Mst_Network();
				objRT_MstSv_Mst_Network.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_MstSv_Mst_Network>(ex, objRT_MstSv_Mst_Network);
				#endregion
			}
		}
	}
}
