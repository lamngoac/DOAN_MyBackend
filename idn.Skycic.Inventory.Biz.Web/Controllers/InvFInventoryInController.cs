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
    public class InvFInventoryInController : ApiControllerBase
    {
		[AcceptVerbs("POST")]
		public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_Get(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
			RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_InvF_InventoryIn_Get";
			string strErrorCodeDefault = "WA_InvF_InventoryIn_Get";

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
					, objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
					, objRQ_InvF_InventoryIn.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_InvF_InventoryIn_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
											   ////
					, out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
				objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
				if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
				objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_Save(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
			RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_InvF_InventoryIn_Save";
			string strErrorCodeDefault = "WA_InvF_InventoryIn_Save";

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
					, objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
					, objRQ_InvF_InventoryIn.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_InvF_InventoryIn_Save_New20210918(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
											   ////
					, out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
				objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
				if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
				objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
				#endregion
			}
		}

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_Appr(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_Appr";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_Appr";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryIn_Appr_New20220513(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_UpdAfterAppr(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_UpdAfterAppr";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_UpdAfterAppr";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryIn_UpdAfterAppr(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_Cancel(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_Cancel";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_Cancel";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryIn_Cancel(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_SaveAndAppr(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_SaveAndAppr";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_SaveAndAppr";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                // WAS_InvF_InventoryIn_SaveAndAppr_New20220513
                mdsReturn = _biz.WAS_InvF_InventoryIn_SaveAndAppr_New20210410(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_UpdAfterApprForEtem(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_UpdAfterApprForEtem";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_UpdAfterApprForEtem";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryIn_UpdAfterApprForEtem(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_OSEtem_InvF_InventoryIn_DelAfterAppr(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSEtem_InvF_InventoryIn_DelAfterAppr";
            string strErrorCodeDefault = "WA_OSEtem_InvF_InventoryIn_DelAfterAppr";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSEtem_InvF_InventoryIn_DelAfterAppr(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                              ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_OSEtem_InvF_InventoryIn_DelQRCode(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSEtem_InvF_InventoryIn_DelQRCode";
            string strErrorCodeDefault = "WA_OSEtem_InvF_InventoryIn_DelQRCode";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSEtem_InvF_InventoryIn_DelQRCode(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_OSEtem_InvF_InventoryIn_AddQRCode(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OSEtem_InvF_InventoryIn_AddQRCode";
            string strErrorCodeDefault = "WA_OSEtem_InvF_InventoryIn_AddQRCode";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OSEtem_InvF_InventoryIn_AddQRCode(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }

        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_SaveAndApprDemo(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_SaveAndAppr";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_SaveAndAppr";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                mdsReturn = _biz.WAS_InvF_InventoryIn_SaveAndAppr_New20220625(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_SaveMulti(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_SaveMulti";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_SaveMulti";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryIn_SaveMulti(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_ApprMulti(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_ApprMulti";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_ApprMulti";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryIn_ApprMulti(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_InvF_InventoryIn> WA_InvF_InventoryIn_SaveAndApprMulti(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_InvF_InventoryIn>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_InvF_InventoryIn.Tid);
            RT_InvF_InventoryIn objRT_InvF_InventoryIn = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_InvF_InventoryIn_SaveAndApprMulti";
            string strErrorCodeDefault = "WA_InvF_InventoryIn_SaveAndApprMulti";

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
                    , objRQ_InvF_InventoryIn.GwUserCode // strGwUserCode
                    , objRQ_InvF_InventoryIn.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_InvF_InventoryIn_SaveAndApprMulti(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_InvF_InventoryIn // objRQ_InvF_InventoryIn
                                             ////
                    , out objRT_InvF_InventoryIn // objRT_InvF_InventoryIn
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
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_InvF_InventoryIn>(objRT_InvF_InventoryIn);
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
                if (objRT_InvF_InventoryIn == null) objRT_InvF_InventoryIn = new RT_InvF_InventoryIn();
                objRT_InvF_InventoryIn.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_InvF_InventoryIn>(ex, objRT_InvF_InventoryIn);
                #endregion
            }
        }
    }
}
