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
    public class ReportServerController : ApiControllerBase
	{
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Invoice_Invoice> WA_RptSv_Invoice_Invoice_Get(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
			RT_Invoice_Invoice objRT_Invoice_Invoice = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Invoice_Invoice_Get";
			string strErrorCodeDefault = "WA_RptSv_Invoice_Invoice_Get";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_Invoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
					, objRQ_Invoice_Invoice.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Invoice_Invoice_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
											////
					, out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
				objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
				if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
				objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Invoice_Invoice> WA_RptSv_Invoice_Invoice_GetByMST(RQ_Invoice_Invoice objRQ_Invoice_Invoice)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Invoice_Invoice>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Invoice_Invoice.Tid);
			RT_Invoice_Invoice objRT_Invoice_Invoice = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Invoice_Invoice_GetByMST";
			string strErrorCodeDefault = "WA_RptSv_Invoice_Invoice_GetByMST";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Invoice_Invoice", TJson.JsonConvert.SerializeObject(objRQ_Invoice_Invoice)
				////
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_Invoice.GwUserCode // strGwUserCode
					, objRQ_Invoice_Invoice.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Invoice_Invoice_GetByMST(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Invoice_Invoice // objRQ_Invoice_Invoice
											////
					, out objRT_Invoice_Invoice // RT_Invoice_Invoice
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
				objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Invoice_Invoice>(objRT_Invoice_Invoice);
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
				if (objRT_Invoice_Invoice == null) objRT_Invoice_Invoice = new RT_Invoice_Invoice();
				objRT_Invoice_Invoice.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Invoice_Invoice>(ex, objRT_Invoice_Invoice);
				#endregion
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Mst_NNT> WA_RptSv_Mst_NNT_Get(RQ_Mst_NNT objRQ_Mst_NNT)
		{
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Mst_NNT>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Mst_NNT.Tid);
			RT_Mst_NNT objRT_Mst_NNT = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Mst_NNT_Get";
			string strErrorCodeDefault = "WA_RptSv_Mst_NNT_Get";

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
				mdsReturn = _biz.WAS_RptSv_Mst_NNT_Get(
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
        public ServiceResult<RT_MstSv_Inos_Org> WA_RptSv_MstSv_Inos_Org_Get(RQ_MstSv_Inos_Org objRQ_MstSv_Inos_Org)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Inos_Org>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Inos_Org.Tid);
            RT_MstSv_Inos_Org objRT_MstSv_Inos_Org = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_MstSv_Inos_Org_Get";
            string strErrorCodeDefault = "WA_RptSv_MstSv_Inos_Org_Get";

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
                    , objRQ_MstSv_Inos_Org.GwUserCode // strGwUserCode
                    , objRQ_MstSv_Inos_Org.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_MstSv_Inos_Org_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_MstSv_Inos_Org // objRQ_MstSv_Inos_Org
                                    ////
                    , out objRT_MstSv_Inos_Org // RT_MstSv_Inos_Org
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
                objRT_MstSv_Inos_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_MstSv_Inos_Org>(objRT_MstSv_Inos_Org);
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
                if (objRT_MstSv_Inos_Org == null) objRT_MstSv_Inos_Org = new RT_MstSv_Inos_Org();
                objRT_MstSv_Inos_Org.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_MstSv_Inos_Org>(ex, objRT_MstSv_Inos_Org);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_MstSv_Inos_User> WA_RptSv_MstSv_Inos_User_Get(RQ_MstSv_Inos_User objRQ_MstSv_Inos_User)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_MstSv_Inos_User>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_MstSv_Inos_User.Tid);
            RT_MstSv_Inos_User objRT_MstSv_Inos_User = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_MstSv_Inos_User_Get";
            string strErrorCodeDefault = "WA_RptSv_MstSv_Inos_User_Get";

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
                    , objRQ_MstSv_Inos_User.GwUserCode // strGwUserCode
                    , objRQ_MstSv_Inos_User.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_RptSv_MstSv_Inos_User_Get(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_MstSv_Inos_User // objRQ_MstSv_Inos_User
                                           ////
                    , out objRT_MstSv_Inos_User // RT_MstSv_Inos_User
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
                objRT_MstSv_Inos_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_MstSv_Inos_User>(objRT_MstSv_Inos_User);
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
                if (objRT_MstSv_Inos_User == null) objRT_MstSv_Inos_User = new RT_MstSv_Inos_User();
                objRT_MstSv_Inos_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_MstSv_Inos_User>(ex, objRT_MstSv_Inos_User);
                #endregion
            }
        }


		[AcceptVerbs("POST")]
		public ServiceResult<RT_Inv_InventoryBalanceSerial> WA_RptSv_Inv_InventoryBalanceSerial_Get(RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Inv_InventoryBalanceSerial>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Inv_InventoryBalanceSerial.Tid);
			RT_Inv_InventoryBalanceSerial objRT_Inv_InventoryBalanceSerial = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Inv_InventoryBalanceSerial_Get";
			string strErrorCodeDefault = "WA_RptSv_Inv_InventoryBalanceSerial_Get";

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
					, objRQ_Inv_InventoryBalanceSerial.GwUserCode // strGwUserCode
					, objRQ_Inv_InventoryBalanceSerial.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Inv_InventoryBalanceSerial_Get(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Inv_InventoryBalanceSerial // objRQ_Inv_InventoryBalanceSerial
													   ////
					, out objRT_Inv_InventoryBalanceSerial // RT_Inv_InventoryBalanceSerial
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
				objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Inv_InventoryBalanceSerial>(objRT_Inv_InventoryBalanceSerial);
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
				if (objRT_Inv_InventoryBalanceSerial == null) objRT_Inv_InventoryBalanceSerial = new RT_Inv_InventoryBalanceSerial();
				objRT_Inv_InventoryBalanceSerial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Inv_InventoryBalanceSerial>(ex, objRT_Inv_InventoryBalanceSerial);
				#endregion
			}
		}
		
		[AcceptVerbs("POST")]
		public ServiceResult<RT_Rpt_Inv_InventoryBalanceSerialForSearch> WA_RptSv_Rpt_Inv_InventoryBalanceSerialForSearch(RQ_Rpt_Inv_InventoryBalanceSerialForSearch objRQ_Rpt_Inv_InventoryBalanceSerialForSearch)
		{
			#region // Temp:
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InventoryBalanceSerialForSearch>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.Tid);
			RT_Rpt_Inv_InventoryBalanceSerialForSearch objRT_Rpt_Inv_InventoryBalanceSerialForSearch = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_RptSv_Rpt_Inv_InventoryBalanceSerialForSearch";
			string strErrorCodeDefault = "WA_RptSv_Rpt_Inv_InventoryBalanceSerialForSearch";

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
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.GwUserCode // strGwUserCode
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_RptSv_Rpt_Inv_InventoryBalanceSerialForSearch(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Rpt_Inv_InventoryBalanceSerialForSearch // objRQ_Rpt_Inv_InventoryBalanceSerialForSearch
																	////
					, out objRT_Rpt_Inv_InventoryBalanceSerialForSearch // RT_Rpt_Inv_InventoryBalanceSerialForSearch
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
				objRT_Rpt_Inv_InventoryBalanceSerialForSearch.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Rpt_Inv_InventoryBalanceSerialForSearch>(objRT_Rpt_Inv_InventoryBalanceSerialForSearch);
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
				if (objRT_Rpt_Inv_InventoryBalanceSerialForSearch == null) objRT_Rpt_Inv_InventoryBalanceSerialForSearch = new RT_Rpt_Inv_InventoryBalanceSerialForSearch();
				objRT_Rpt_Inv_InventoryBalanceSerialForSearch.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Rpt_Inv_InventoryBalanceSerialForSearch>(ex, objRT_Rpt_Inv_InventoryBalanceSerialForSearch);
				#endregion
			}
		}

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Seq_Common> WA_RptSv_Seq_MST_Get(RQ_Seq_Common objRQ_Seq_Common)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Seq_Common>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Seq_Common.Tid);
            RT_Seq_Common objRT_Seq_Common = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_RptSv_Seq_MST_Get";
            string strErrorCodeDefault = "WA_RptSv_Seq_MST_Get";

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
                mdsReturn = _biz.WAS_RptSv_Seq_MST_Get(
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
