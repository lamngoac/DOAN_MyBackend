using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using System.Collections;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class SeqController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Seq_TranNo> WA_Seq_Common_Get_TranNo(RQ_Seq_TranNo objRQ_Seq_TranNo)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_TranNo>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_TranNo.Tid);
			RT_Seq_TranNo objRT_Seq_TranNo = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Seq_Common_Get_TranNo";
			string strErrorCodeDefault = "WA_Seq_Common_Get_TranNo";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_TranNo.GwUserCode // strGwUserCode
					, objRQ_Seq_TranNo.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Seq_Common_Get_TranNo(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_TranNo // objRQ_Seq_TranNo
									   ////
					, out objRT_Seq_TranNo // RT_Seq_TranNo
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
				objRT_Seq_TranNo.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Seq_TranNo>(objRT_Seq_TranNo);
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
				if (objRT_Seq_TranNo == null) objRT_Seq_TranNo = new RT_Seq_TranNo();
				objRT_Seq_TranNo.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Seq_TranNo>(ex, objRT_Seq_TranNo);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Seq_TCTTranNo> WA_Seq_TCTTranNo_Get(RQ_Seq_TCTTranNo objRQ_Seq_TCTTranNo)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_TCTTranNo>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_TCTTranNo.Tid);
			RT_Seq_TCTTranNo objRT_Seq_TCTTranNo = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Seq_TCTTranNo_Get";
			string strErrorCodeDefault = "WA_Seq_TCTTranNo_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_TCTTranNo.GwUserCode // strGwUserCode
					, objRQ_Seq_TCTTranNo.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Seq_TCTTranNo_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_TCTTranNo // objRQ_Seq_TCTTranNo
									   ////
					, out objRT_Seq_TCTTranNo // RT_Seq_TCTTranNo
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
				objRT_Seq_TCTTranNo.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Seq_TCTTranNo>(objRT_Seq_TCTTranNo);
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
				if (objRT_Seq_TCTTranNo == null) objRT_Seq_TCTTranNo = new RT_Seq_TCTTranNo();
				objRT_Seq_TCTTranNo.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Seq_TCTTranNo>(ex, objRT_Seq_TCTTranNo);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Seq_Common> WA_Seq_Common_Get(RQ_Seq_Common objRQ_Seq_Common)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_Common>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
			RT_Seq_Common objRT_Seq_Common = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Seq_Common_Get";
			string strErrorCodeDefault = "WA_Seq_Common_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_Common.GwUserCode // strGwUserCode
					, objRQ_Seq_Common.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Seq_Common_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_Common // objRQ_Seq_Common
										  ////
					, out objRT_Seq_Common // RT_Seq_Common
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
				objRT_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Seq_Common>(objRT_Seq_Common);
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
				if (objRT_Seq_Common == null) objRT_Seq_Common = new RT_Seq_Common();
				objRT_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Seq_Common>(ex, objRT_Seq_Common);
				#endregion
			}
        }

        [AcceptVerbs("POST")]
		public ServiceResult<RT_Seq_InvoiceCode> WA_Seq_InvoiceCode_Get(RQ_Seq_InvoiceCode objRQ_Seq_InvoiceCode)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_InvoiceCode>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_InvoiceCode.Tid);
			RT_Seq_InvoiceCode objRT_Seq_InvoiceCode = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Seq_InvoiceCode_Get";
			string strErrorCodeDefault = "WA_Seq_InvoiceCode_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_InvoiceCode.GwUserCode // strGwUserCode
					, objRQ_Seq_InvoiceCode.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Seq_InvoiceCode_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_InvoiceCode // objRQ_Seq_InvoiceCode
									   ////
					, out objRT_Seq_InvoiceCode // RT_Seq_InvoiceCode
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
				objRT_Seq_InvoiceCode.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Seq_InvoiceCode>(objRT_Seq_InvoiceCode);
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
				if (objRT_Seq_InvoiceCode == null) objRT_Seq_InvoiceCode = new RT_Seq_InvoiceCode();
				objRT_Seq_InvoiceCode.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Seq_InvoiceCode>(ex, objRT_Seq_InvoiceCode);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Seq_InvoiceCode> WA_Seq_InvoiceCode_GetByAmount(RQ_Seq_InvoiceCode objRQ_Seq_InvoiceCode)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_InvoiceCode>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_InvoiceCode.Tid);
			RT_Seq_InvoiceCode objRT_Seq_InvoiceCode = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Seq_InvoiceCode_GetByAmount";
			string strErrorCodeDefault = "WA_Seq_InvoiceCode_GetByAmount";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_InvoiceCode.GwUserCode // strGwUserCode
					, objRQ_Seq_InvoiceCode.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Seq_InvoiceCode_GetByAmount(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Seq_InvoiceCode // objRQ_Seq_InvoiceCode
											////
					, out objRT_Seq_InvoiceCode // RT_Seq_InvoiceCode
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
				objRT_Seq_InvoiceCode.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Seq_InvoiceCode>(objRT_Seq_InvoiceCode);
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
				if (objRT_Seq_InvoiceCode == null) objRT_Seq_InvoiceCode = new RT_Seq_InvoiceCode();
				objRT_Seq_InvoiceCode.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Seq_InvoiceCode>(ex, objRT_Seq_InvoiceCode);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
        public ServiceResult<RT_Seq_FormNo> WA_Seq_FormNo_Get(RQ_Seq_FormNo objRQ_Seq_FormNo)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_FormNo>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_FormNo.Tid);
            RT_Seq_FormNo objRT_Seq_FormNo = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Seq_FormNo_Get";
            string strErrorCodeDefault = "WA_Seq_FormNo_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_FormNo.GwUserCode // strGwUserCode
                    , objRQ_Seq_FormNo.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Seq_FormNo_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_FormNo // objRQ_Seq_Common
                                       ////
                    , out objRT_Seq_FormNo // objRT_Seq_FormNo
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
                objRT_Seq_FormNo.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Seq_FormNo>(objRT_Seq_FormNo);
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
                if (objRT_Seq_FormNo == null) objRT_Seq_FormNo = new RT_Seq_FormNo();
                objRT_Seq_FormNo.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Seq_FormNo>(ex, objRT_Seq_FormNo);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Seq_InvoiceCode> WAS_Seq_GenEngine_Get(RQ_Seq_InvoiceCode objRQ_Seq_InvoiceCode)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_InvoiceCode>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_InvoiceCode.Tid);
            RT_Seq_InvoiceCode objRT_Seq_InvoiceCode = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WAS_Seq_GenEngine_Get";
            string strErrorCodeDefault = "WAS_Seq_GenEngine_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_InvoiceCode.GwUserCode // strGwUserCode
                    , objRQ_Seq_InvoiceCode.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Seq_GenEngine_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_InvoiceCode // objRQ_Seq_InvoiceCode
                                            ////
                    , out objRT_Seq_InvoiceCode // RT_Seq_InvoiceCode
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
                objRT_Seq_InvoiceCode.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Seq_InvoiceCode>(objRT_Seq_InvoiceCode);
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
                if (objRT_Seq_InvoiceCode == null) objRT_Seq_InvoiceCode = new RT_Seq_InvoiceCode();
                objRT_Seq_InvoiceCode.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Seq_InvoiceCode>(ex, objRT_Seq_InvoiceCode);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Seq_ObjCode> WA_Seq_GenObjCode_Get(RQ_Seq_ObjCode objRQ_Seq_ObjCode)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_ObjCode>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_ObjCode.Tid);
            RT_Seq_ObjCode objRT_Seq_ObjCode = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Seq_GenObjCode_Get";
            string strErrorCodeDefault = "WA_Seq_GenObjCode_Get";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_ObjCode.GwUserCode // strGwUserCode
                    , objRQ_Seq_ObjCode.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Seq_GenObjCode_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_ObjCode // objRQ_Seq_ObjCode
                                        ////
                    , out objRT_Seq_ObjCode // RT_Seq_ObjCode
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
                objRT_Seq_ObjCode.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Seq_ObjCode>(objRT_Seq_ObjCode);
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
                if (objRT_Seq_ObjCode == null) objRT_Seq_ObjCode = new RT_Seq_ObjCode();
                objRT_Seq_ObjCode.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Seq_ObjCode>(ex, objRT_Seq_ObjCode);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Seq_Common> WA_Seq_Common_GetMulti(RQ_Seq_Common objRQ_Seq_Common)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_Common>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
            RT_Seq_Common objRT_Seq_Common = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Seq_Common_GetMulti";
            string strErrorCodeDefault = "WA_Seq_Common_GetMulti";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_Common.GwUserCode // strGwUserCode
                    , objRQ_Seq_Common.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Seq_Common_GetMulti(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Seq_Common // objRQ_Seq_Common
                                       ////
                    , out objRT_Seq_Common // RT_Seq_Common
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
                objRT_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Seq_Common>(objRT_Seq_Common);
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
                if (objRT_Seq_Common == null) objRT_Seq_Common = new RT_Seq_Common();
                objRT_Seq_Common.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Seq_Common>(ex, objRT_Seq_Common);
                #endregion
            }
        }
    }
}
