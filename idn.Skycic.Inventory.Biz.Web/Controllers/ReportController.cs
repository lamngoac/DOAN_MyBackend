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
    public class ReportController : ApiControllerBase
    {

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_InvoiceInvoice_ResultUsed> WA_Rpt_InvoiceInvoice_ResultUsed(RQ_Rpt_InvoiceInvoice_ResultUsed objRQ_Rpt_InvoiceInvoice_ResultUsed)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_InvoiceInvoice_ResultUsed>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_InvoiceInvoice_ResultUsed.Tid);
            RT_Rpt_InvoiceInvoice_ResultUsed objRT_Rpt_InvoiceInvoice_ResultUsed = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_InvoiceInvoice_ResultUsed";
            string strErrorCodeDefault = "WA_Rpt_InvoiceInvoice_ResultUsed";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_InvoiceInvoice_ResultUsed_New20191007(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvoiceInvoice_ResultUsed // objRQ_Rpt_InvoiceInvoice_ResultUsed
                                                          ////
                    , out objRT_Rpt_InvoiceInvoice_ResultUsed // objRT_Rpt_InvoiceInvoice_ResultUsed
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
                objRT_Rpt_InvoiceInvoice_ResultUsed.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_InvoiceInvoice_ResultUsed>(objRT_Rpt_InvoiceInvoice_ResultUsed);
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
                if (objRT_Rpt_InvoiceInvoice_ResultUsed == null) objRT_Rpt_InvoiceInvoice_ResultUsed = new RT_Rpt_InvoiceInvoice_ResultUsed();
                objRT_Rpt_InvoiceInvoice_ResultUsed.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_InvoiceInvoice_ResultUsed>(ex, objRT_Rpt_InvoiceInvoice_ResultUsed);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_InvoiceForDashboard> WA_Rpt_InvoiceForDashboard(RQ_Rpt_InvoiceForDashboard objRQ_Rpt_InvoiceForDashboard)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_InvoiceForDashboard>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_InvoiceForDashboard.Tid);
            RT_Rpt_InvoiceForDashboard objRT_Rpt_InvoiceForDashboard = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_InvoiceForDashboard";
            string strErrorCodeDefault = "WA_Rpt_InvoiceForDashboard";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvoiceForDashboard.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvoiceForDashboard.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_InvoiceForDashboard(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvoiceForDashboard // objRQ_Rpt_InvoiceForDashboard
                                                    ////
                    , out objRT_Rpt_InvoiceForDashboard // objRT_Rpt_InvoiceForDashboard
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
                objRT_Rpt_InvoiceForDashboard.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_InvoiceForDashboard>(objRT_Rpt_InvoiceForDashboard);
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
                if (objRT_Rpt_InvoiceForDashboard == null) objRT_Rpt_InvoiceForDashboard = new RT_Rpt_InvoiceForDashboard();
                objRT_Rpt_InvoiceForDashboard.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_InvoiceForDashboard>(ex, objRT_Rpt_InvoiceForDashboard);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_InvFInventoryInFGSum> WA_Rpt_InvFInventoryInFGSum(RQ_Rpt_InvFInventoryInFGSum objRQ_Rpt_InvFInventoryInFGSum)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_InvFInventoryInFGSum>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_InvFInventoryInFGSum.Tid);
            RT_Rpt_InvFInventoryInFGSum objRT_Rpt_InvFInventoryInFGSum = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_InvFInventoryInFGSum";
            string strErrorCodeDefault = "WA_Rpt_InvFInventoryInFGSum";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvFInventoryInFGSum.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvFInventoryInFGSum.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_InvFInventoryInFGSum(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvFInventoryInFGSum // objRQ_Rpt_InvFInventoryInFGSum
                                                     ////
                    , out objRT_Rpt_InvFInventoryInFGSum // objRT_Rpt_InvFInventoryInFGSum
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
                objRT_Rpt_InvFInventoryInFGSum.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_InvFInventoryInFGSum>(objRT_Rpt_InvFInventoryInFGSum);
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
                if (objRT_Rpt_InvFInventoryInFGSum == null) objRT_Rpt_InvFInventoryInFGSum = new RT_Rpt_InvFInventoryInFGSum();
                objRT_Rpt_InvFInventoryInFGSum.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_InvFInventoryInFGSum>(ex, objRT_Rpt_InvFInventoryInFGSum);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_InvFInventoryOutFGSum> WA_Rpt_InvFInventoryOutFGSum(RQ_Rpt_InvFInventoryOutFGSum objRQ_Rpt_InvFInventoryOutFGSum)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_InvFInventoryOutFGSum>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_InvFInventoryOutFGSum.Tid);
            RT_Rpt_InvFInventoryOutFGSum objRT_Rpt_InvFInventoryOutFGSum = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_InvFInventoryOutFGSum";
            string strErrorCodeDefault = "WA_Rpt_InvFInventoryOutFGSum";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvFInventoryOutFGSum.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvFInventoryOutFGSum.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_InvFInventoryOutFGSum(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvFInventoryOutFGSum // objRQ_Rpt_InvFInventoryOutFGSum
                                                      ////
                    , out objRT_Rpt_InvFInventoryOutFGSum // objRT_Rpt_InvFInventoryOutFGSum
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
                objRT_Rpt_InvFInventoryOutFGSum.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_InvFInventoryOutFGSum>(objRT_Rpt_InvFInventoryOutFGSum);
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
                if (objRT_Rpt_InvFInventoryOutFGSum == null) objRT_Rpt_InvFInventoryOutFGSum = new RT_Rpt_InvFInventoryOutFGSum();
                objRT_Rpt_InvFInventoryOutFGSum.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_InvFInventoryOutFGSum>(ex, objRT_Rpt_InvFInventoryOutFGSum);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_InvInventoryBalanceMonth> WA_Rpt_InvInventoryBalanceMonth(RQ_Rpt_InvInventoryBalanceMonth objRQ_Rpt_InvInventoryBalanceMonth)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_InvInventoryBalanceMonth>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_InvInventoryBalanceMonth.Tid);
            RT_Rpt_InvInventoryBalanceMonth objRT_Rpt_InvInventoryBalanceMonth = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_InvInventoryBalanceMonth";
            string strErrorCodeDefault = "WA_Rpt_InvInventoryBalanceMonth";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvInventoryBalanceMonth.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvInventoryBalanceMonth.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_InvInventoryBalanceMonth(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvInventoryBalanceMonth // objRQ_Rpt_InvInventoryBalanceMonth
                                                         ////
                    , out objRT_Rpt_InvInventoryBalanceMonth // objRT_Rpt_InvInventoryBalanceMonth
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
                objRT_Rpt_InvInventoryBalanceMonth.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_InvInventoryBalanceMonth>(objRT_Rpt_InvInventoryBalanceMonth);
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
                if (objRT_Rpt_InvInventoryBalanceMonth == null) objRT_Rpt_InvInventoryBalanceMonth = new RT_Rpt_InvInventoryBalanceMonth();
                objRT_Rpt_InvInventoryBalanceMonth.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_InvInventoryBalanceMonth>(ex, objRT_Rpt_InvInventoryBalanceMonth);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inv_InventoryBalanceSerialForSearch> WA_Rpt_Inv_InventoryBalanceSerialForSearch(RQ_Rpt_Inv_InventoryBalanceSerialForSearch objRQ_Rpt_Inv_InventoryBalanceSerialForSearch)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InventoryBalanceSerialForSearch>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalanceSerialForSearch.Tid);
            RT_Rpt_Inv_InventoryBalanceSerialForSearch objRT_Rpt_Inv_InventoryBalanceSerialForSearch = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inv_InventoryBalanceSerialForSearch";
            string strErrorCodeDefault = "WA_Rpt_Inv_InventoryBalanceSerialForSearch";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
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
                mdsReturn = _biz.WAS_Rpt_Inv_InventoryBalanceSerialForSearch(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalanceSerialForSearch // objRQ_Rpt_Inv_InventoryBalanceSerialForSearch
                                                                    ////
                    , out objRT_Rpt_Inv_InventoryBalanceSerialForSearch // objRT_Rpt_Inv_InventoryBalanceSerialForSearch
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

        /// <summary>
        /// Báo cáo thẻ kho
        /// </summary>
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_InvF_WarehouseCard> WA_Rpt_InvF_WarehouseCard(RQ_Rpt_InvF_WarehouseCard objRQ_Rpt_InvF_WarehouseCard)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_InvF_WarehouseCard>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_InvF_WarehouseCard.Tid);
            RT_Rpt_InvF_WarehouseCard objRT_Rpt_InvF_WarehouseCard = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_InvF_WarehouseCard";
            string strErrorCodeDefault = "WA_Rpt_InvF_WarehouseCard";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvF_WarehouseCard.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvF_WarehouseCard.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_InvF_WarehouseCard(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvF_WarehouseCard // objRQ_Rpt_InvF_WarehouseCard
                                                   ////
                    , out objRT_Rpt_InvF_WarehouseCard // objRT_Rpt_InvF_WarehouseCard
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
                objRT_Rpt_InvF_WarehouseCard.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_InvF_WarehouseCard>(objRT_Rpt_InvF_WarehouseCard);
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
                if (objRT_Rpt_InvF_WarehouseCard == null) objRT_Rpt_InvF_WarehouseCard = new RT_Rpt_InvF_WarehouseCard();
                objRT_Rpt_InvF_WarehouseCard.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_InvF_WarehouseCard>(ex, objRT_Rpt_InvF_WarehouseCard);
                #endregion
            }
        }

        /// <summary>
        /// Báo cáo tồn kho
        /// </summary>
        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inv_InventoryBalance> WA_Rpt_Inv_InventoryBalance(RQ_Rpt_Inv_InventoryBalance objRQ_Rpt_Inv_InventoryBalance)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InventoryBalance>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalance.Tid);
            RT_Rpt_Inv_InventoryBalance objRT_Rpt_Inv_InventoryBalance = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inv_InventoryBalance";
            string strErrorCodeDefault = "WA_Rpt_Inv_InventoryBalance";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Inv_InventoryBalance_New20230105(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance // objRQ_Rpt_Inv_InventoryBalance
                                                     ////
                    , out objRT_Rpt_Inv_InventoryBalance // objRT_Rpt_Inv_InventoryBalance
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
                objRT_Rpt_Inv_InventoryBalance.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inv_InventoryBalance>(objRT_Rpt_Inv_InventoryBalance);
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
                if (objRT_Rpt_Inv_InventoryBalance == null) objRT_Rpt_Inv_InventoryBalance = new RT_Rpt_Inv_InventoryBalance();
                objRT_Rpt_Inv_InventoryBalance.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inv_InventoryBalance>(ex, objRT_Rpt_Inv_InventoryBalance);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inventory_In_Out_Inv> WA_Rpt_Inventory_In_Out_Inv(RQ_Rpt_Inventory_In_Out_Inv objRQ_Rpt_Inventory_In_Out_Inv)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inventory_In_Out_Inv>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inventory_In_Out_Inv.Tid);
            RT_Rpt_Inventory_In_Out_Inv objRT_Rpt_Inventory_In_Out_Inv = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inventory_In_Out_Inv";
            string strErrorCodeDefault = "WA_Rpt_Inventory_In_Out_Inv";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inventory_In_Out_Inv.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inventory_In_Out_Inv.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Inventory_In_Out_Inv(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inventory_In_Out_Inv // objRQ_Rpt_Inventory_In_Out_Inv
                                                     ////
                    , out objRT_Rpt_Inventory_In_Out_Inv // objRT_Rpt_Inventory_In_Out_Inv
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
                objRT_Rpt_Inventory_In_Out_Inv.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inventory_In_Out_Inv>(objRT_Rpt_Inventory_In_Out_Inv);
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
                if (objRT_Rpt_Inventory_In_Out_Inv == null) objRT_Rpt_Inventory_In_Out_Inv = new RT_Rpt_Inventory_In_Out_Inv();
                objRT_Rpt_Inventory_In_Out_Inv.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inventory_In_Out_Inv>(ex, objRT_Rpt_Inventory_In_Out_Inv);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inventory_In_Out_Inv> WA_Rpt_In_Out_Inv_JobAuto(RQ_Rpt_Inventory_In_Out_Inv objRQ_Rpt_Inventory_In_Out_Inv)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inventory_In_Out_Inv>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inventory_In_Out_Inv.Tid);
            RT_Rpt_Inventory_In_Out_Inv objRT_Rpt_Inventory_In_Out_Inv = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_In_Out_Inv_JobAuto";
            string strErrorCodeDefault = "WA_Rpt_In_Out_Inv_JobAuto";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inventory_In_Out_Inv.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inventory_In_Out_Inv.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_In_Out_Inv_JobAuto(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inventory_In_Out_Inv // objRQ_Rpt_Inventory_In_Out_Inv
                                                     ////
                    , out objRT_Rpt_Inventory_In_Out_Inv // objRT_Rpt_Inventory_In_Out_Inv
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
                objRT_Rpt_Inventory_In_Out_Inv.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inventory_In_Out_Inv>(objRT_Rpt_Inventory_In_Out_Inv);
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
                if (objRT_Rpt_Inventory_In_Out_Inv == null) objRT_Rpt_Inventory_In_Out_Inv = new RT_Rpt_Inventory_In_Out_Inv();
                objRT_Rpt_Inventory_In_Out_Inv.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inventory_In_Out_Inv>(ex, objRT_Rpt_Inventory_In_Out_Inv);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inv_InvBalance_LastUpdInvByProduct> WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct objRQ_Rpt_Inv_InvBalance_LastUpdInvByProduct)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InvBalance_LastUpdInvByProduct>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InvBalance_LastUpdInvByProduct.Tid);
            RT_Rpt_Inv_InvBalance_LastUpdInvByProduct objRT_Rpt_Inv_InvBalance_LastUpdInvByProduct = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inv_InvBalance_LastUpdInvByProduct";
            string strErrorCodeDefault = "WA_Rpt_Inv_InvBalance_LastUpdInvByProduct";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InvBalance_LastUpdInvByProduct.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InvBalance_LastUpdInvByProduct.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Inv_InvBalance_LastUpdInvByProduct(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InvBalance_LastUpdInvByProduct // objRQ_Rpt_Inventory_In_Out_Inv
                                                     ////
                    , out objRT_Rpt_Inv_InvBalance_LastUpdInvByProduct // objRT_Rpt_Inventory_In_Out_Inv
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
                objRT_Rpt_Inv_InvBalance_LastUpdInvByProduct.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inv_InvBalance_LastUpdInvByProduct>(objRT_Rpt_Inv_InvBalance_LastUpdInvByProduct);
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
                if (objRT_Rpt_Inv_InvBalance_LastUpdInvByProduct == null) objRT_Rpt_Inv_InvBalance_LastUpdInvByProduct = new RT_Rpt_Inv_InvBalance_LastUpdInvByProduct();
                objRT_Rpt_Inv_InvBalance_LastUpdInvByProduct.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inv_InvBalance_LastUpdInvByProduct>(ex, objRT_Rpt_Inv_InvBalance_LastUpdInvByProduct);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Summary_InAndReturnSup> WA_Rpt_Summary_InAndReturnSup(RQ_Rpt_Summary_InAndReturnSup objRQ_Rpt_Summary_InAndReturnSup)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Summary_InAndReturnSup>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Summary_InAndReturnSup.Tid);
            RT_Rpt_Summary_InAndReturnSup objRT_Rpt_Summary_InAndReturnSup = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Summary_InAndReturnSup";
            string strErrorCodeDefault = "WA_Rpt_Summary_InAndReturnSup";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_InAndReturnSup.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Summary_InAndReturnSup.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Summary_InAndReturnSup_New20210929(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_InAndReturnSup // objRQ_Rpt_Inventory_In_Out_Inv
                                                                   ////
                    , out objRT_Rpt_Summary_InAndReturnSup // objRT_Rpt_Inventory_In_Out_Inv
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
                objRT_Rpt_Summary_InAndReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Summary_InAndReturnSup>(objRT_Rpt_Summary_InAndReturnSup);
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
                if (objRT_Rpt_Summary_InAndReturnSup == null) objRT_Rpt_Summary_InAndReturnSup = new RT_Rpt_Summary_InAndReturnSup();
                objRT_Rpt_Summary_InAndReturnSup.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Summary_InAndReturnSup>(ex, objRT_Rpt_Summary_InAndReturnSup);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Summary_QtyInvByPeriod> WA_Rpt_Summary_QtyInvByPeriod(RQ_Rpt_Summary_QtyInvByPeriod objRQ_Rpt_Summary_QtyInvByPeriod)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Summary_QtyInvByPeriod>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Summary_QtyInvByPeriod.Tid);
            RT_Rpt_Summary_QtyInvByPeriod objRT_Rpt_Summary_QtyInvByPeriod = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Summary_QtyInvByPeriod";
            string strErrorCodeDefault = "WA_Rpt_Summary_QtyInvByPeriod";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_QtyInvByPeriod.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Summary_QtyInvByPeriod.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Summary_QtyInvByPeriod(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_QtyInvByPeriod // objRQ_Rpt_Inventory_In_Out_Inv
                                                       ////
                    , out objRT_Rpt_Summary_QtyInvByPeriod // objRT_Rpt_Inventory_In_Out_Inv
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
                objRT_Rpt_Summary_QtyInvByPeriod.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Summary_QtyInvByPeriod>(objRT_Rpt_Summary_QtyInvByPeriod);
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
                if (objRT_Rpt_Summary_QtyInvByPeriod == null) objRT_Rpt_Summary_QtyInvByPeriod = new RT_Rpt_Summary_QtyInvByPeriod();
                objRT_Rpt_Summary_QtyInvByPeriod.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Summary_QtyInvByPeriod>(ex, objRT_Rpt_Summary_QtyInvByPeriod);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Summary_In_Out> WA_Rpt_Summary_In_Out(RQ_Rpt_Summary_In_Out objRQ_Rpt_Summary_In_Out)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Summary_In_Out>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Summary_In_Out.Tid);
            RT_Rpt_Summary_In_Out objRT_Rpt_Summary_In_Out = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Summary_In_Out";
            string strErrorCodeDefault = "WA_Rpt_Summary_In_Out";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_In_Out.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Summary_In_Out.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Summary_In_Out(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_In_Out // objRQ_Rpt_Inventory_In_Out_Inv
                                                       ////
                    , out objRT_Rpt_Summary_In_Out // objRT_Rpt_Inventory_In_Out_Inv
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
                objRT_Rpt_Summary_In_Out.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Summary_In_Out>(objRT_Rpt_Summary_In_Out);
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
                if (objRT_Rpt_Summary_In_Out == null) objRT_Rpt_Summary_In_Out = new RT_Rpt_Summary_In_Out();
                objRT_Rpt_Summary_In_Out.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Summary_In_Out>(ex, objRT_Rpt_Summary_In_Out);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Summary_In_Out_Sup_Pivot> WA_Rpt_Summary_In_Out_Sup_Pivot(RQ_Rpt_Summary_In_Out_Sup_Pivot objRQ_Rpt_Summary_In_Out_Sup_Pivot)
        {
            #region // Temp:Rpt_Summary_In_Out_Sup_Pivot
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Summary_In_Out_Sup_Pivot>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Summary_In_Out_Sup_Pivot.Tid);
            RT_Rpt_Summary_In_Out_Sup_Pivot objRT_Rpt_Summary_In_Out_Sup_Pivot = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Summary_In_Out_Sup_Pivot";
            string strErrorCodeDefault = "WA_Rpt_Summary_In_Out_Sup_Pivot";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Summary_In_Out_Sup_Pivot(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_In_Out_Sup_Pivot // objRQ_Rpt_Summary_In_Out_Sup_Pivot
                                                   ////
                    , out objRT_Rpt_Summary_In_Out_Sup_Pivot // objRT_Rpt_Summary_In_Out_Sup_Pivot
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
                objRT_Rpt_Summary_In_Out_Sup_Pivot.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Summary_In_Out_Sup_Pivot>(objRT_Rpt_Summary_In_Out_Sup_Pivot);
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
                if (objRT_Rpt_Summary_In_Out_Sup_Pivot == null) objRT_Rpt_Summary_In_Out_Sup_Pivot = new RT_Rpt_Summary_In_Out_Sup_Pivot();
                objRT_Rpt_Summary_In_Out_Sup_Pivot.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Summary_In_Out_Sup_Pivot>(ex, objRT_Rpt_Summary_In_Out_Sup_Pivot);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Summary_In_Out_Pivot> WA_Rpt_Summary_In_Out_Pivot(RQ_Rpt_Summary_In_Out_Pivot objRQ_Rpt_Summary_In_Out_Pivot)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Summary_In_Out_Pivot>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Summary_In_Out_Pivot.Tid);
            RT_Rpt_Summary_In_Out_Pivot objRT_Rpt_Summary_In_Out_Pivot = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Summary_In_Out_Pivot";
            string strErrorCodeDefault = "WA_Rpt_Summary_In_Out_Pivot";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_In_Out_Pivot.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Summary_In_Out_Pivot.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Summary_In_Out_Pivot(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Summary_In_Out_Pivot // objRQ_Rpt_Summary_In_Out_Pivot
                                                     ////
                    , out objRT_Rpt_Summary_In_Out_Pivot // objRT_Rpt_Summary_In_Out_Pivot
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
                objRT_Rpt_Summary_In_Out_Pivot.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Summary_In_Out_Pivot>(objRT_Rpt_Summary_In_Out_Pivot);
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
                if (objRT_Rpt_Summary_In_Out_Pivot == null) objRT_Rpt_Summary_In_Out_Pivot = new RT_Rpt_Summary_In_Out_Pivot();
                objRT_Rpt_Summary_In_Out_Pivot.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Summary_In_Out_Pivot>(ex, objRT_Rpt_Summary_In_Out_Pivot);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_InvBalLot_MaxExpiredDateByInv> WA_Rpt_InvBalLot_MaxExpiredDateByInv(RQ_Rpt_InvBalLot_MaxExpiredDateByInv objRQ_Rpt_InvBalLot_MaxExpiredDateByInv)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_InvBalLot_MaxExpiredDateByInv>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.Tid);
            RT_Rpt_InvBalLot_MaxExpiredDateByInv objRT_Rpt_InvBalLot_MaxExpiredDateByInv = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_InvBalLot_MaxExpiredDateByInv";
            string strErrorCodeDefault = "WA_Rpt_InvBalLot_MaxExpiredDateByInv";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_InvBalLot_MaxExpiredDateByInv_New20210925(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvBalLot_MaxExpiredDateByInv // objRQ_Rpt_InvBalLot_MaxExpiredDateByInv
                                                     ////
                    , out objRT_Rpt_InvBalLot_MaxExpiredDateByInv // objRT_Rpt_InvBalLot_MaxExpiredDateByInv
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
                objRT_Rpt_InvBalLot_MaxExpiredDateByInv.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_InvBalLot_MaxExpiredDateByInv>(objRT_Rpt_InvBalLot_MaxExpiredDateByInv);
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
                if (objRT_Rpt_InvBalLot_MaxExpiredDateByInv == null) objRT_Rpt_InvBalLot_MaxExpiredDateByInv = new RT_Rpt_InvBalLot_MaxExpiredDateByInv();
                objRT_Rpt_InvBalLot_MaxExpiredDateByInv.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_InvBalLot_MaxExpiredDateByInv>(ex, objRT_Rpt_InvBalLot_MaxExpiredDateByInv);
                #endregion
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<RT_Rpt_Summary_In_Out_Sup_PivotSpecial> WA_Rpt_Summary_In_Out_Sup_PivotSpecial(RQ_Rpt_Summary_In_Out_Sup_PivotSpecial objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial)
		{
			#region // Temp:Rpt_Summary_In_Out_Sup_PivotSpecial
			if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Summary_In_Out_Sup_PivotSpecial>();
			DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.Tid);
			RT_Rpt_Summary_In_Out_Sup_PivotSpecial objRT_Rpt_Summary_In_Out_Sup_PivotSpecial = null;
			DateTime dtimeSys = DateTime.Now;
			string strControllerName = "WA_Rpt_Summary_In_Out_Sup_PivotSpecial";
			string strErrorCodeDefault = "WA_Rpt_Summary_In_Out_Sup_PivotSpecial";

			ArrayList alParamsCoupleError = new ArrayList();
			alParamsCoupleError.AddRange(new object[] {
				"strControllerName", strControllerName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
			#endregion

			try
			{
				#region // CheckGatewayAuthentication:
				TUtils.CConnectionManager.CheckGatewayAuthentication(
					_biz._cf.nvcParams // nvcParams
					, ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.GwUserCode // strGwUserCode
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial.GwPassword // strGwPassword
					);
				#endregion

				#region // Process:
				//
				mdsReturn = _biz.WAS_Rpt_Summary_In_Out_Sup_PivotSpecial_New20201002(
					ref alParamsCoupleError // alParamsCoupleError
					, objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial // objRQ_Rpt_Summary_In_Out_Sup_PivotSpecial
														 ////
					, out objRT_Rpt_Summary_In_Out_Sup_PivotSpecial // objRT_Rpt_Summary_In_Out_Sup_PivotSpecial
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
				objRT_Rpt_Summary_In_Out_Sup_PivotSpecial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Success<RT_Rpt_Summary_In_Out_Sup_PivotSpecial>(objRT_Rpt_Summary_In_Out_Sup_PivotSpecial);
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
				if (objRT_Rpt_Summary_In_Out_Sup_PivotSpecial == null) objRT_Rpt_Summary_In_Out_Sup_PivotSpecial = new RT_Rpt_Summary_In_Out_Sup_PivotSpecial();
				objRT_Rpt_Summary_In_Out_Sup_PivotSpecial.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
				return Error<RT_Rpt_Summary_In_Out_Sup_PivotSpecial>(ex, objRT_Rpt_Summary_In_Out_Sup_PivotSpecial);
				#endregion
			}
		}

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inv_InventoryBalance_Extend> WA_Rpt_Inv_InventoryBalance_Extend(RQ_Rpt_Inv_InventoryBalance_Extend objRQ_Rpt_Inv_InventoryBalance_Extend)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InventoryBalance_Extend>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalance_Extend.Tid);
            RT_Rpt_Inv_InventoryBalance_Extend objRT_Rpt_Inv_InventoryBalance_Extend = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inv_InventoryBalance_Extend";
            string strErrorCodeDefault = "WA_Rpt_Inv_InventoryBalance_Extend";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_Extend.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Inv_InventoryBalance_Extend(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance_Extend // objRQ_Rpt_Inv_InventoryBalance_Extend
                                                     ////
                    , out objRT_Rpt_Inv_InventoryBalance_Extend // objRT_Rpt_Inv_InventoryBalance_Extend
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
                objRT_Rpt_Inv_InventoryBalance_Extend.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inv_InventoryBalance_Extend>(objRT_Rpt_Inv_InventoryBalance_Extend);
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
                if (objRT_Rpt_Inv_InventoryBalance_Extend == null) objRT_Rpt_Inv_InventoryBalance_Extend = new RT_Rpt_Inv_InventoryBalance_Extend();
                objRT_Rpt_Inv_InventoryBalance_Extend.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inv_InventoryBalance_Extend>(ex, objRT_Rpt_Inv_InventoryBalance_Extend);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_MapDeliveryOrder_ByInvFIOut> WA_Rpt_MapDeliveryOrder_ByInvFIOut(RQ_Rpt_MapDeliveryOrder_ByInvFIOut objRQ_Rpt_MapDeliveryOrder_ByInvFIOut)
        {
            #region // Temp:Rpt_MapDeliveryOrder_ByInvFIOut
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_MapDeliveryOrder_ByInvFIOut>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.Tid);
            RT_Rpt_MapDeliveryOrder_ByInvFIOut objRT_Rpt_MapDeliveryOrder_ByInvFIOut = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_MapDeliveryOrder_ByInvFIOut";
            string strErrorCodeDefault = "WA_Rpt_MapDeliveryOrder_ByInvFIOut";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.GwUserCode // strGwUserCode
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_MapDeliveryOrder_ByInvFIOut(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_MapDeliveryOrder_ByInvFIOut // objRQ_Rpt_MapDeliveryOrder_ByInvFIOut
                                                            ////
                    , out objRT_Rpt_MapDeliveryOrder_ByInvFIOut // objRT_Rpt_MapDeliveryOrder_ByInvFIOut
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
                objRT_Rpt_MapDeliveryOrder_ByInvFIOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_MapDeliveryOrder_ByInvFIOut>(objRT_Rpt_MapDeliveryOrder_ByInvFIOut);
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
                if (objRT_Rpt_MapDeliveryOrder_ByInvFIOut == null) objRT_Rpt_MapDeliveryOrder_ByInvFIOut = new RT_Rpt_MapDeliveryOrder_ByInvFIOut();
                objRT_Rpt_MapDeliveryOrder_ByInvFIOut.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_MapDeliveryOrder_ByInvFIOut>(ex, objRT_Rpt_MapDeliveryOrder_ByInvFIOut);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inv_InventoryBalance_ByPeriod> WA_Rpt_Inv_InventoryBalance_ByPeriod(RQ_Rpt_Inv_InventoryBalance_ByPeriod objRQ_Rpt_Inv_InventoryBalance_ByPeriod)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InventoryBalance_ByPeriod>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalance_ByPeriod.Tid);
            RT_Rpt_Inv_InventoryBalance_ByPeriod objRT_Rpt_Inv_InventoryBalance_ByPeriod = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inv_InventoryBalance_ByPeriod";
            string strErrorCodeDefault = "WA_Rpt_Inv_InventoryBalance_ByPeriod";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance_ByPeriod.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_ByPeriod.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Inv_InventoryBalance_ByPeriod(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance_ByPeriod // objRQ_Rpt_Inv_InventoryBalance_ByPeriod
                                                     ////
                    , out objRT_Rpt_Inv_InventoryBalance_ByPeriod // objRT_Rpt_Inv_InventoryBalance_ByPeriod
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
                objRT_Rpt_Inv_InventoryBalance_ByPeriod.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inv_InventoryBalance_ByPeriod>(objRT_Rpt_Inv_InventoryBalance_ByPeriod);
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
                if (objRT_Rpt_Inv_InventoryBalance_ByPeriod == null) objRT_Rpt_Inv_InventoryBalance_ByPeriod = new RT_Rpt_Inv_InventoryBalance_ByPeriod();
                objRT_Rpt_Inv_InventoryBalance_ByPeriod.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inv_InventoryBalance_ByPeriod>(ex, objRT_Rpt_Inv_InventoryBalance_ByPeriod);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inv_InventoryBalance> WA_Rpt_Inv_InventoryBalance_ByValue(RQ_Rpt_Inv_InventoryBalance objRQ_Rpt_Inv_InventoryBalance)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InventoryBalance>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalance.Tid);
            RT_Rpt_Inv_InventoryBalance objRT_Rpt_Inv_InventoryBalance = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inv_InventoryBalance_ByValue";
            string strErrorCodeDefault = "WA_Rpt_Inv_InventoryBalance_ByValue";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Inv_InventoryBalance_ByValue(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance // objRQ_Rpt_Inv_InventoryBalance
                                                     ////
                    , out objRT_Rpt_Inv_InventoryBalance // objRT_Rpt_Inv_InventoryBalance
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
                objRT_Rpt_Inv_InventoryBalance.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inv_InventoryBalance>(objRT_Rpt_Inv_InventoryBalance);
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
                if (objRT_Rpt_Inv_InventoryBalance == null) objRT_Rpt_Inv_InventoryBalance = new RT_Rpt_Inv_InventoryBalance();
                objRT_Rpt_Inv_InventoryBalance.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inv_InventoryBalance>(ex, objRT_Rpt_Inv_InventoryBalance);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_InvBalanceValuationPeriodMonth> WA_Rpt_InvBalanceValuationPeriodMonth(RQ_Rpt_InvBalanceValuationPeriodMonth objRQ_Rpt_InvBalanceValuationPeriodMonth)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_InvBalanceValuationPeriodMonth>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_InvBalanceValuationPeriodMonth.Tid);
            RT_Rpt_InvBalanceValuationPeriodMonth objRT_Rpt_InvBalanceValuationPeriodMonth = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_InvBalanceValuationPeriodMonth";
            string strErrorCodeDefault = "WA_Rpt_InvBalanceValuationPeriodMonth";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.GwUserCode // strGwUserCode
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_InvBalanceValuationPeriodMonth(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_InvBalanceValuationPeriodMonth // objRQ_Rpt_InvBalanceValuationPeriodMonth
                                                               ////
                    , out objRT_Rpt_InvBalanceValuationPeriodMonth // objRT_Rpt_InvBalanceValuationPeriodMonth
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
                objRT_Rpt_InvBalanceValuationPeriodMonth.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_InvBalanceValuationPeriodMonth>(objRT_Rpt_InvBalanceValuationPeriodMonth);
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
                if (objRT_Rpt_InvBalanceValuationPeriodMonth == null) objRT_Rpt_InvBalanceValuationPeriodMonth = new RT_Rpt_InvBalanceValuationPeriodMonth();
                objRT_Rpt_InvBalanceValuationPeriodMonth.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_InvBalanceValuationPeriodMonth>(ex, objRT_Rpt_InvBalanceValuationPeriodMonth);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inv_InventoryBalance_Minimum> WA_Rpt_Inv_InventoryBalance_Minimum(RQ_Rpt_Inv_InventoryBalance_Minimum objRQ_Rpt_Inv_InventoryBalance_Minimum)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InventoryBalance_Minimum>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalance_Minimum.Tid);
            RT_Rpt_Inv_InventoryBalance_Minimum objRT_Rpt_Inv_InventoryBalance_Minimum = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inv_InventoryBalance_Minimum";
            string strErrorCodeDefault = "WA_Rpt_Inv_InventoryBalance_Minimum";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Inv_InventoryBalance_Minimum(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance_Minimum // objRQ_Rpt_Inv_InventoryBalance_Minimum
                                                             ////
                    , out objRT_Rpt_Inv_InventoryBalance_Minimum // objRT_Rpt_Inv_InventoryBalance_Minimum
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
                objRT_Rpt_Inv_InventoryBalance_Minimum.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inv_InventoryBalance_Minimum>(objRT_Rpt_Inv_InventoryBalance_Minimum);
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
                if (objRT_Rpt_Inv_InventoryBalance_Minimum == null) objRT_Rpt_Inv_InventoryBalance_Minimum = new RT_Rpt_Inv_InventoryBalance_Minimum();
                objRT_Rpt_Inv_InventoryBalance_Minimum.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inv_InventoryBalance_Minimum>(ex, objRT_Rpt_Inv_InventoryBalance_Minimum);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_Rpt_Inv_InventoryBalance_StorageTime> WA_Rpt_Inv_InventoryBalance_StorageTime(RQ_Rpt_Inv_InventoryBalance_StorageTime objRQ_Rpt_Inv_InventoryBalance_StorageTime)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_Rpt_Inv_InventoryBalance_StorageTime>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Rpt_Inv_InventoryBalance_StorageTime.Tid);
            RT_Rpt_Inv_InventoryBalance_StorageTime objRT_Rpt_Inv_InventoryBalance_StorageTime = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_Rpt_Inv_InventoryBalance_StorageTime";
            string strErrorCodeDefault = "WA_Rpt_Inv_InventoryBalance_StorageTime";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Budget_ProjectPlan", TJson.JsonConvert.SerializeObject(objRQ_Budget_ProjectPlan)
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.GwUserCode // strGwUserCode
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_Rpt_Inv_InventoryBalance_StorageTime(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_Rpt_Inv_InventoryBalance_StorageTime // objRQ_Rpt_Inv_InventoryBalance_StorageTime
                                                                 ////
                    , out objRT_Rpt_Inv_InventoryBalance_StorageTime // objRT_Rpt_Inv_InventoryBalance_StorageTime
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
                objRT_Rpt_Inv_InventoryBalance_StorageTime.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Success<RT_Rpt_Inv_InventoryBalance_StorageTime>(objRT_Rpt_Inv_InventoryBalance_StorageTime);
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
                if (objRT_Rpt_Inv_InventoryBalance_StorageTime == null) objRT_Rpt_Inv_InventoryBalance_StorageTime = new RT_Rpt_Inv_InventoryBalance_StorageTime();
                objRT_Rpt_Inv_InventoryBalance_StorageTime.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_Rpt_Inv_InventoryBalance_StorageTime>(ex, objRT_Rpt_Inv_InventoryBalance_StorageTime);
                #endregion
            }
        }
    }

}