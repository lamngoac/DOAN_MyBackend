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
    public class InvoiceTempGroupController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempGroup> WA_Invoice_TempGroup_Get(RQ_Invoice_TempGroup objRQ_Invoice_TempGroup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempGroup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            RT_Invoice_TempGroup objRT_Invoice_TempGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempGroup_Get";
            string strErrorCodeDefault = "WA_Invoice_TempGroup_Get";

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
                    , objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempGroup_Get_New20190706(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempGroup // RQ_Invoice_TempGroup
                                              ////
                    , out objRT_Invoice_TempGroup // RT_Invoice_TempGroup
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
                objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempGroup>(objRT_Invoice_TempGroup);
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
                if (objRT_Invoice_TempGroup == null) objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
                objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempGroup>(ex, objRT_Invoice_TempGroup);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempGroup> WA_Invoice_TempGroup_Create(RQ_Invoice_TempGroup objRQ_Invoice_TempGroup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempGroup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            RT_Invoice_TempGroup objRT_Invoice_TempGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempGroup_Create";
            string strErrorCodeDefault = "WA_Invoice_TempGroup_Create";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempGroup_Create_New20190924(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempGroup // objRQ_Invoice_TempGroup
                                              ////
                    , out objRT_Invoice_TempGroup // RT_Invoice_TempGroup
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
                objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempGroup>(objRT_Invoice_TempGroup);
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
                if (objRT_Invoice_TempGroup == null) objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
                objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempGroup>(ex, objRT_Invoice_TempGroup);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempGroup> WA_Invoice_TempGroup_Update(RQ_Invoice_TempGroup objRQ_Invoice_TempGroup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempGroup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            RT_Invoice_TempGroup objRT_Invoice_TempGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempGroup_Update";
            string strErrorCodeDefault = "WA_Invoice_TempGroup_Update";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempGroup_Update_New20190925(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempGroup // objRQ_Invoice_TempGroup
                                              ////
                    , out objRT_Invoice_TempGroup // objRT_Invoice_TempGroup
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
                objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempGroup>(objRT_Invoice_TempGroup);
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
                if (objRT_Invoice_TempGroup == null) objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
                objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempGroup>(ex, objRT_Invoice_TempGroup);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Invoice_TempGroup> WA_Invoice_TempGroup_Delete(RQ_Invoice_TempGroup objRQ_Invoice_TempGroup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempGroup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
            RT_Invoice_TempGroup objRT_Invoice_TempGroup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Invoice_TempGroup_Delete";
            string strErrorCodeDefault = "WA_Invoice_TempGroup_Delete";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
                    , objRQ_Invoice_TempGroup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Invoice_TempGroup_Delete_New20190706(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Invoice_TempGroup // objRQ_Invoice_TempGroup
                                              ////
                    , out objRT_Invoice_TempGroup // objRT_Invoice_TempGroup
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
                objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Invoice_TempGroup>(objRT_Invoice_TempGroup);
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
                if (objRT_Invoice_TempGroup == null) objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
                objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Invoice_TempGroup>(ex, objRT_Invoice_TempGroup);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Invoice_TempGroup> WA_RptSv_Invoice_TempGroup_Create(RQ_Invoice_TempGroup objRQ_Invoice_TempGroup)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempGroup>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
			RT_Invoice_TempGroup objRT_Invoice_TempGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Invoice_TempGroup_Create";
			string strErrorCodeDefault = "WA_RptSv_Invoice_TempGroup_Create";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
					, objRQ_Invoice_TempGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Invoice_TempGroup_Create(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_TempGroup // objRQ_Invoice_TempGroup
											  ////
					, out objRT_Invoice_TempGroup // RT_Invoice_TempGroup
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
				objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Invoice_TempGroup>(objRT_Invoice_TempGroup);
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
				if (objRT_Invoice_TempGroup == null) objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
				objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Invoice_TempGroup>(ex, objRT_Invoice_TempGroup);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Invoice_TempGroup> WA_RptSv_Invoice_TempGroup_Get(RQ_Invoice_TempGroup objRQ_Invoice_TempGroup)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempGroup>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
			RT_Invoice_TempGroup objRT_Invoice_TempGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Invoice_TempGroup_Get";
			string strErrorCodeDefault = "WA_RptSv_Invoice_TempGroup_Get";

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
					, objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
					, objRQ_Invoice_TempGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Invoice_TempGroup_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_TempGroup // RQ_Invoice_TempGroup
											  ////
					, out objRT_Invoice_TempGroup // RT_Invoice_TempGroup
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
				objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Invoice_TempGroup>(objRT_Invoice_TempGroup);
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
				if (objRT_Invoice_TempGroup == null) objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
				objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Invoice_TempGroup>(ex, objRT_Invoice_TempGroup);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Invoice_TempGroup> WA_RptSv_Invoice_TempGroup_Update(RQ_Invoice_TempGroup objRQ_Invoice_TempGroup)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempGroup>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
			RT_Invoice_TempGroup objRT_Invoice_TempGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Invoice_TempGroup_Update";
			string strErrorCodeDefault = "WA_RptSv_Invoice_TempGroup_Update";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
					, objRQ_Invoice_TempGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Invoice_TempGroup_Update(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_TempGroup // objRQ_Invoice_TempGroup
											  ////
					, out objRT_Invoice_TempGroup // objRT_Invoice_TempGroup
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
				objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Invoice_TempGroup>(objRT_Invoice_TempGroup);
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
				if (objRT_Invoice_TempGroup == null) objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
				objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Invoice_TempGroup>(ex, objRT_Invoice_TempGroup);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Invoice_TempGroup> WA_RptSv_Invoice_TempGroup_Delete(RQ_Invoice_TempGroup objRQ_Invoice_TempGroup)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_TempGroup>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_TempGroup.Tid);
			RT_Invoice_TempGroup objRT_Invoice_TempGroup = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Invoice_TempGroup_Delete";
			string strErrorCodeDefault = "WA_RptSv_Invoice_TempGroup_Delete";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_TempGroup", TJson.JsonConvert.SerializeObject(objRQ_Invoice_TempGroup)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_TempGroup.GwUserCode // strGwUserCode
					, objRQ_Invoice_TempGroup.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Invoice_TempGroup_Delete(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_TempGroup // objRQ_Invoice_TempGroup
											  ////
					, out objRT_Invoice_TempGroup // objRT_Invoice_TempGroup
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
				objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Invoice_TempGroup>(objRT_Invoice_TempGroup);
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
				if (objRT_Invoice_TempGroup == null) objRT_Invoice_TempGroup = new RT_Invoice_TempGroup();
				objRT_Invoice_TempGroup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Invoice_TempGroup>(ex, objRT_Invoice_TempGroup);
				#endregion
			}
		}

	}
}
