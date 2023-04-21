using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;


namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class MstInvOutTypeController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InvOutType> WA_Mst_InvOutType_Get(RQ_Mst_InvOutType objRQ_Mst_InvOutType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InvOutType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvOutType.Tid);
			RT_Mst_InvOutType objRT_Mst_InvOutType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InvOutType_Get";
			string strErrorCodeDefault = "WA_Mst_InvOutType_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvOutType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InvOutType_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvOutType // RQ_Mst_InvOutType
										   ////
					, out objRT_Mst_InvOutType // RT_Mst_InvOutType
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
				objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InvOutType>(objRT_Mst_InvOutType);
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
				if (objRT_Mst_InvOutType == null) objRT_Mst_InvOutType = new RT_Mst_InvOutType();
				objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InvOutType>(ex, objRT_Mst_InvOutType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InvOutType> WA_Mst_InvOutType_Create(RQ_Mst_InvOutType objRQ_Mst_InvOutType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InvOutType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvOutType.Tid);
			RT_Mst_InvOutType objRT_Mst_InvOutType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InvOutType_Create";
			string strErrorCodeDefault = "WA_Mst_InvOutType_Create";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvOutType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InvOutType_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvOutType // RQ_Mst_InvOutType
										   ////
					, out objRT_Mst_InvOutType // RT_Mst_InvOutType
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
				objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InvOutType>(objRT_Mst_InvOutType);
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
				if (objRT_Mst_InvOutType == null) objRT_Mst_InvOutType = new RT_Mst_InvOutType();
				objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InvOutType>(ex, objRT_Mst_InvOutType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InvOutType> WA_Mst_InvOutType_Update(RQ_Mst_InvOutType objRQ_Mst_InvOutType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InvOutType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvOutType.Tid);
			RT_Mst_InvOutType objRT_Mst_InvOutType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InvOutType_Create";
			string strErrorCodeDefault = "WA_Mst_InvOutType_Create";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvOutType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InvOutType_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvOutType // RQ_Mst_InvOutType
										   ////
					, out objRT_Mst_InvOutType // RT_Mst_InvOutType
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
				objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InvOutType>(objRT_Mst_InvOutType);
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
				if (objRT_Mst_InvOutType == null) objRT_Mst_InvOutType = new RT_Mst_InvOutType();
				objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InvOutType>(ex, objRT_Mst_InvOutType);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_InvOutType> WA_Mst_InvOutType_Delete(RQ_Mst_InvOutType objRQ_Mst_InvOutType)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InvOutType>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvOutType.Tid);
			RT_Mst_InvOutType objRT_Mst_InvOutType = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_InvOutType_Delete";
			string strErrorCodeDefault = "WA_Mst_InvOutType_Delete";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
					, objRQ_Mst_InvOutType.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_InvOutType_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_InvOutType // RQ_Mst_InvOutType
										   ////
					, out objRT_Mst_InvOutType // RT_Mst_InvOutType
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
				objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_InvOutType>(objRT_Mst_InvOutType);
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
				if (objRT_Mst_InvOutType == null) objRT_Mst_InvOutType = new RT_Mst_InvOutType();
				objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_InvOutType>(ex, objRT_Mst_InvOutType);
				#endregion
			}
		}

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_InvOutType> WA_OS_Mst_InvOutType_Get(RQ_Mst_InvOutType objRQ_Mst_InvOutType)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_InvOutType>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_InvOutType.Tid);
            RT_Mst_InvOutType objRT_Mst_InvOutType = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_Mst_InvOutType_Get";
            string strErrorCodeDefault = "WA_OS_Mst_InvOutType_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Dealer", TJson.JsonConvert.SerializeObject(objRQ_Mst_Dealer)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_InvOutType.GwUserCode // strGwUserCode
                    , objRQ_Mst_InvOutType.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_Mst_InvOutType_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_InvOutType // RQ_Mst_InvOutType
                                           ////
                    , out objRT_Mst_InvOutType // RT_Mst_InvOutType
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
                objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_InvOutType>(objRT_Mst_InvOutType);
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
                if (objRT_Mst_InvOutType == null) objRT_Mst_InvOutType = new RT_Mst_InvOutType();
                objRT_Mst_InvOutType.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_InvOutType>(ex, objRT_Mst_InvOutType);
                #endregion
            }
        }

    }
}
