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
    public class MstNNTController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_Mst_NNT_Get(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNT_Get";
            string strErrorCodeDefault = "WA_Mst_NNT_Get";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNT_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_Mst_NNT_Create(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNT_Create";
            string strErrorCodeDefault = "WA_Mst_NNT_Create";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNT_Create_New20200110(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_Mst_NNT_Registry(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNT_Registry";
            string strErrorCodeDefault = "WA_Mst_NNT_Registry";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_NNT_Register(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_Add(RQ_Mst_NNT objRQ_Mst_NNT)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_NNT_Add";
			string strErrorCodeDefault = "WA_RptSv_Mst_NNT_Add";

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
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_NNT_Add_New20200208(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT // objRQ_Mst_NNT
									////
					, out objRT_Mst_NNT // RT_Mst_NNT
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
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
				if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_Calc(RQ_Mst_NNT objRQ_Mst_NNT)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_NNT_Calc";
			string strErrorCodeDefault = "WA_RptSv_Mst_NNT_Calc";

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
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:

				//
				mdsReturn = _biz.WAS_RptSv_Mst_NNT_Calc(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT // objRQ_Mst_NNT
									////
					, out objRT_Mst_NNT // RT_Mst_NNT
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
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
				if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_CalcByUserExist(RQ_Mst_NNT objRQ_Mst_NNT)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_NNT_CalcByUserExist";
			string strErrorCodeDefault = "WA_RptSv_Mst_NNT_CalcByUserExist";

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
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_NNT_CalcByUserExist(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT // objRQ_Mst_NNT
									////
					, out objRT_Mst_NNT // RT_Mst_NNT
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
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
				if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_AddByUserExist(RQ_Mst_NNT objRQ_Mst_NNT)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_NNT_AddByUserExist";
			string strErrorCodeDefault = "WA_RptSv_Mst_NNT_AddByUserExist";

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
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_NNT_AddByUserExist_New20200207(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT // objRQ_Mst_NNT
									////
					, out objRT_Mst_NNT // RT_Mst_NNT
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
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
				if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist(RQ_Mst_NNT objRQ_Mst_NNT)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist";
			string strErrorCodeDefault = "WA_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist";

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
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT // objRQ_Mst_NNT
									////
					, out objRT_Mst_NNT // RT_Mst_NNT
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
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
				if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_Mst_NNT_Update(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNT_Update";
            string strErrorCodeDefault = "WA_Mst_NNT_Update";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNT_Update_New20190822(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_Update(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_NNT_Update";
            string strErrorCodeDefault = "WA_RptSv_Mst_NNT_Update";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_NNT_Update_New20200208(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_Mst_NNT_UpdateRegisterStatus(RQ_Mst_NNT objRQ_Mst_NNT)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Mst_NNT_UpdateRegisterStatus";
			string strErrorCodeDefault = "WA_Mst_NNT_UpdateRegisterStatus";

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
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Mst_NNT_UpdateRegisterStatus(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT // objRQ_Mst_NNT
									////
					, out objRT_Mst_NNT // RT_Mst_NNT
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
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
				if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_Mst_NNT_Delete(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNT_Delete";
            string strErrorCodeDefault = "WA_Mst_NNT_Delete";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNT_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_Delete(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_NNT_Delete";
            string strErrorCodeDefault = "WA_RptSv_Mst_NNT_Delete";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_NNT_Delete(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_Mst_NNT_MSTChild_Registry(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNT_MSTChild_Registry";
            string strErrorCodeDefault = "WA_Mst_NNT_MSTChild_Registry";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WA_Mst_NNT_MSTChild_Registry(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }
        
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_UpdateRegisterStatus(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Mst_NNT_UpdateRegisterStatus";
            string strErrorCodeDefault = "WA_RptSv_Mst_NNT_UpdateRegisterStatus";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_Mst_NNT_UpdateRegisterStatus(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Mst_NNT> WA_Mst_NNT_CreateForNetwork(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
            RT_Mst_NNT objRT_Mst_NNT = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Mst_NNT_CreateForNetwork";
            string strErrorCodeDefault = "WA_Mst_NNT_CreateForNetwork";

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
                    , objRQ_Mst_NNT.GwUserCode // strGwUserCode
                    , objRQ_Mst_NNT.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Mst_NNT_CreateForNetwork_New20200417(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Mst_NNT // objRQ_Mst_NNT
                                    ////
                    , out objRT_Mst_NNT // RT_Mst_NNT
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
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
                if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
                objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_CreateForNetwork(RQ_Mst_NNT objRQ_Mst_NNT)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_NNT_Create";
			string strErrorCodeDefault = "WA_RptSv_Mst_NNT_Create";

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
					, objRQ_Mst_NNT.GwUserCode // strGwUserCode
					, objRQ_Mst_NNT.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Mst_NNT_CreateForNetwork(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Mst_NNT // objRQ_Mst_NNT
									////
					, out objRT_Mst_NNT // RT_Mst_NNT
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
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Mst_NNT>(objRT_Mst_NNT);
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
				if (objRT_Mst_NNT == null) objRT_Mst_NNT = new RT_Mst_NNT();
				objRT_Mst_NNT.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Mst_NNT>(ex, objRT_Mst_NNT);
				#endregion
			}
		}
	}
}
