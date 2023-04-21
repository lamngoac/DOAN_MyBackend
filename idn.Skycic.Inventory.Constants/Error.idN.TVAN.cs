using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace idn.Skycic.Inventory.Errors
{
	public partial class ErridnInventory
    {
        #region // Report:
        // Rpt_InvoiceForDashboard:
        public const string Rpt_InvoiceForDashboard = "ErridnInventory.Rpt_InvoiceForDashboard"; //// //Rpt_InvoiceForDashboard

        // WAS_Rpt_InvoiceForDashboard:
        public const string WAS_Rpt_InvoiceForDashboard = "ErridnInventory.WAS_Rpt_InvoiceForDashboard"; //// //WAS_Rpt_InvoiceForDashboard
		#endregion

		#region // Sys_User:
		//
		public const string Sys_User_Login_InvalidFlagBG = "ErridnInventory.Sys_User_Login_InvalidFlagBG"; //// //Sys_User_Login_InvalidFlagBG
		public const string Sys_User_Login_InvalidInosUser = "ErridnInventory.Sys_User_Login_InvalidInosUser"; //// //Sys_User_Login_InvalidInosUser
		public const string Sys_User_Login_InvalidAccessToken = "ErridnInventory.Sys_User_Login_InvalidAccessToken"; //// //Sys_User_Login_InvalidAccessToken

		// WAS_Sys_User_RefreshToken:
		public const string WAS_Sys_User_RefreshToken = "ErridnInventory.WAS_Sys_User_RefreshToken"; //// //WAS_Sys_User_RefreshToken

        #endregion

        #region // Invoice_Invoice:
        // Invoice_Invoice_CheckDB:        
        public const string Invoice_Invoice_CheckDB_InvoiceNotFound = "ErridnInventory.Invoice_Invoice_CheckDB_InvoiceNotFound"; //// //Invoice_Invoice_CheckDB_InvoiceNotFound
        public const string Invoice_Invoice_CheckDB_InvoiceExist = "ErridnInventory.Invoice_Invoice_CheckDB_InvoiceExist"; //// //Invoice_Invoice_CheckDB_InvoiceExist
        public const string Invoice_Invoice_CheckDB_CheckDB_InvoiceStatusNotMatched = "ErridnInventory.Invoice_Invoice_CheckDB_CheckDB_InvoiceStatusNotMatched"; //// //Invoice_Invoice_CheckDB_CheckDB_InvoiceStatusNotMatched

        // Invoice_Invoice_Get:
        public const string Invoice_Invoice_Get = "ErridnInventory.Invoice_Invoice_Get"; //// //Invoice_Invoice_Get

        // Invoice_Invoice_GetNoSession:
        public const string Invoice_Invoice_GetNoSession = "ErridnInventory.Invoice_Invoice_GetNoSession"; //// //Invoice_Invoice_GetNoSession

        // WAS_Invoice_Invoice_GetNoSession:
        public const string WAS_Invoice_Invoice_GetNoSession = "ErridnInventory.WAS_Invoice_Invoice_GetNoSession"; //// //WAS_Invoice_Invoice_GetNoSession

        // WAS_OS3A_Invoice_Invoice_GetNoSession:
        public const string WAS_OS3A_Invoice_Invoice_GetNoSession = "ErridnInventory.WAS_OS3A_Invoice_Invoice_GetNoSession"; //// //WAS_OS3A_Invoice_Invoice_GetNoSession

        // WAS_OS3A_InvoiceInvoice_Get:
        public const string WAS_OS3A_TVAN_InvoiceInvoice_Get = "ErridnInventory.WAS_OS3A_TVAN_InvoiceInvoice_Get"; //// //WAS_OS3A_TVAN_InvoiceInvoice_Get

        // Invoice_Invoice_SaveX:
        public const string Invoice_Invoice_SaveX = "ErridnInventory.Invoice_Invoice_SaveX"; //// //Invoice_Invoice_SaveX
        public const string Invoice_Invoice_SaveX_Input_InvoiceTblNotFound = "ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceTblNotFound"; //// //Invoice_Invoice_SaveX_Input_InvoiceTblNotFound

        public const string Invoice_Invoice_SaveX_Input_InvoiceTblInvalid = "ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceTblInvalid"; //// //Invoice_Invoice_SaveX_Input_InvoiceTblInvalid

        public const string Invoice_Invoice_SaveX_InvalidInvoiceCode = "ErridnInventory.Invoice_Invoice_SaveX_InvalidInvoiceCode"; //// //Invoice_Invoice_SaveX_InvalidInvoiceCode
        public const string Invoice_Invoice_SaveX_StatusNotMatched = "ErridnInventory.Invoice_Invoice_SaveX_StatusNotMatched"; //// //Invoice_Invoice_SaveX_StatusNotMatched
        public const string Invoice_Invoice_SaveX_Input_InvoiceDtlTblNotFound = "ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceDtlTblNotFound"; //// //Invoice_Invoice_SaveX_Input_InvoiceDtlTblNotFound
        public const string Invoice_Invoice_SaveX_Input_InvoiceDtlTblInvalid = "ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceDtlTblInvalid"; //// //Invoice_Invoice_SaveX_Input_InvoiceDtlTblInvalid
        public const string Invoice_Invoice_SaveX_Input_InvoicePrdTblInvalid = "ErridnInventory.Invoice_Invoice_SaveX_Input_InvoicePrdTblInvalid"; //// //Invoice_Invoice_SaveX_Input_InvoicePrdTblInvalid
        public const string Invoice_Invoice_SaveX_Input_InvoiceDtl_ProductIDDuplicate = "ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceDtl_ProductIDDuplicate"; //// //Invoice_Invoice_SaveX_Input_InvoiceDtl_ProductIDDuplicate
        public const string Invoice_Invoice_SaveX_Input_InvoiceDtl_SpecCodeDuplicate = "ErridnInventory.Invoice_Invoice_SaveX_Input_InvoiceDtl_SpecCodeDuplicate"; //// //Invoice_Invoice_SaveX_Input_InvoiceDtl_SpecCodeDuplicate
        public const string Invoice_Invoice_SaveX_InvoiceAdjTypeIsNotNull = "ErridnInventory.Invoice_Invoice_SaveX_InvoiceAdjTypeIsNotNull"; //// //Invoice_Invoice_SaveX_ExistInvoiceNo
        public const string Invoice_Invoice_SaveX_ExistInvoiceNo = "ErridnInventory.Invoice_Invoice_SaveX_ExistInvoiceNo"; //// //Invoice_Invoice_SaveX_ExistInvoiceNo
        public const string Invoice_Invoice_SaveX_NotDelete = "ErridnInventory.Invoice_Invoice_SaveX_NotDelete"; //// //Invoice_Invoice_SaveX_NotDelete
        public const string Invoice_Invoice_SaveX_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC = "ErridnInventory.Invoice_Invoice_SaveX_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC"; //// //Invoice_Invoice_SaveX_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC
        public const string Invoice_Invoice_SaveX_InvalidMSTOfNNTAndCustomer = "ErridnInventory.Invoice_Invoice_SaveX_InvalidMSTOfNNTAndCustomer"; //// //Invoice_Invoice_SaveX_InvalidMSTOfNNTAndCustomer
        public const string Invoice_Invoice_SaveX_InvalidMSTOfNNTAndTempInvoice = "ErridnInventory.Invoice_Invoice_SaveX_InvalidMSTOfNNTAndTempInvoice"; //// //Invoice_Invoice_SaveX_InvalidMSTOfNNTAndTempInvoice
        public const string Invoice_Invoice_SaveX_InvalidQtyIssueRemain = "ErridnInventory.Invoice_Invoice_SaveX_InvalidQtyIssueRemain"; //// //Invoice_Invoice_SaveX_InvalidQtyIssueRemain
        public const string Invoice_Invoice_SaveX_InputInvoice_InvoicePrdTblNotFound = "ErridnInventory.Invoice_Invoice_SaveX_InputInvoice_InvoicePrdTblNotFound"; //// //Invoice_Invoice_SaveX_InputInvoice_InvoicePrdTblNotFound
        public const string Invoice_Invoice_SaveX_Input_Invoice_InvoicePrdTblInvalid = "ErridnInventory.Invoice_Invoice_SaveX_Input_Invoice_InvoicePrdTblInvalid"; //// //Invoice_Invoice_SaveX_Input_Invoice_InvoicePrdTblInvalid
        public const string Invoice_Invoice_SaveX_Input_Invoice_InvaliInvoiceDateUTCAfterSysDate = "ErridnInventory.Invoice_Invoice_SaveX_Input_Invoice_InvaliInvoiceDateUTCAfterSysDate"; //// //Invoice_Invoice_SaveX_Input_Invoice_InvaliInvoiceDateUTCAfterSysDate
        public const string Invoice_Invoice_SaveX_Input_Invoice_InvalidRefNo = "ErridnInventory.Invoice_Invoice_SaveX_Input_Invoice_InvalidRefNo"; //// //Invoice_Invoice_SaveX_Input_Invoice_InvalidRefNo
        public const string Invoice_Invoice_SaveX_Input_Invoice_InvalidInvoiceStatusRefNo = "ErridnInventory.Invoice_Invoice_SaveX_Input_Invoice_InvalidInvoiceStatusRefNo"; //// //Invoice_Invoice_SaveX_Input_Invoice_InvalidInvoiceStatusRefNo
        public const string Invoice_Invoice_SaveX_Invalid_MstNNT_FlagActive = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_MstNNT_FlagActive"; //// //Invoice_Invoice_SaveX_Invalid_MstNNT_FlagActive
        public const string Invoice_Invoice_SaveX_Invalid_MstNNT_NotFound = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_MstNNT_NotFound"; //// //Invoice_Invoice_SaveX_Invalid_MstNNT_NotFound
        public const string Invoice_Invoice_SaveX_Invalid_PaymentMethods_NotFound = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_PaymentMethods_NotFound"; //// //Invoice_Invoice_SaveX_Invalid_PaymentMethods_NotFound
        public const string Invoice_Invoice_SaveX_Invalid_PaymentMethods_FlagActive = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_PaymentMethods_FlagActive"; //// //Invoice_Invoice_SaveX_Invalid_PaymentMethods_FlagActive
        public const string Invoice_Invoice_SaveX_Invalid_InvoiceType2_NotFound = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_InvoiceType2_NotFound"; //// //Invoice_Invoice_SaveX_Invalid_InvoiceType2_NotFound
        public const string Invoice_Invoice_SaveX_Invalid_InvoiceType2_FlagActive = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_InvoiceType2_FlagActive"; //// //Invoice_Invoice_SaveX_Invalid_InvoiceType2_FlagActive
        public const string Invoice_Invoice_SaveX_Invalid_SourceInvoice_NotFound = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_SourceInvoice_NotFound"; //// //Invoice_Invoice_SaveX_Invalid_SourceInvoice_NotFound
        public const string Invoice_Invoice_SaveX_Invalid_SourceInvoice_FlagActive = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_SourceInvoice_FlagActive"; //// //Invoice_Invoice_SaveX_Invalid_SourceInvoice_FlagActive
        ////
        public const string Invoice_Invoice_SaveX_Invalid_MstCustomerNNT_FlagActive = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_MstCustomerNNT_FlagActive"; //// //Invoice_Invoice_SaveX_Invalid_MstCustomerNNT_FlagActive
        public const string Invoice_Invoice_SaveX_Invalid_MstCustomerNNT_NotFound = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_MstCustomerNNT_NotFound"; //// //Invoice_Invoice_SaveX_Invalid_MstCustomerNNT_NotFound

        public const string WAS_Invoice_Invoice_Calc = "ErridnInventory.WAS_Invoice_Invoice_Calc"; //// //WAS_Invoice_Invoice_Calc
        public const string Invoice_Invoice_Calc = "ErridnInventory.Invoice_Invoice_Calc"; //// //Invoice_Invoice_Calc

        public const string Invoice_Invoice_SaveX_Invalid_TempInvoice_NotFound = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_TempInvoice_NotFound"; //// //Invoice_Invoice_SaveX_Invalid_TempInvoice_NotFound
        public const string Invoice_Invoice_SaveX_Invalid_TempInvoice_StatusNotMatch = "ErridnInventory.Invoice_Invoice_SaveX_Invalid_TempInvoice_StatusNotMatch"; //// //Invoice_Invoice_SaveX_Invalid_TempInvoice_StatusNotMatch

        public const string Invoice_Invoice_SaveX_InvalidEmailSend = "ErridnInventory.Invoice_Invoice_SaveX_InvalidEmailSend"; //// //Invoice_Invoice_SaveX_InvalidEmailSend
        // Invoice_Invoice_Save_Replace:
        public const string Invoice_Invoice_Save_Replace = "ErridnInventory.Invoice_Invoice_Save_Replace"; //// //Invoice_Invoice_Save_Replace

        // WAS_Invoice_Invoice_Save_Root:
        public const string WAS_Invoice_Invoice_Save_Root = "ErridnInventory.WAS_Invoice_Invoice_Save_Root"; //// //WAS_Invoice_Invoice_Save_Root

        // WAS_Invoice_Invoice_Save_Adj:
        public const string WAS_Invoice_Invoice_Save_Adj = "ErridnInventory.WAS_Invoice_Invoice_Save_Adj"; //// //WAS_Invoice_Invoice_Save_Adj

        // WAS_Invoice_Invoice_Save_Replace:
        public const string WAS_Invoice_Invoice_Save_Replace = "ErridnInventory.WAS_Invoice_Invoice_Save_Replace"; //// //WAS_Invoice_Invoice_Save_Replace

        // Invoice_Invoice_Save_Adj:
        public const string Invoice_Invoice_Save_Adj = "ErridnInventory.Invoice_Invoice_Save_Adj"; //// //Invoice_Invoice_Save_Adj

        // Invoice_Invoice_Save_Root:
        public const string Invoice_Invoice_Save_Root = "ErridnInventory.Invoice_Invoice_Save_Root"; //// //Invoice_Invoice_Save_Root

        // WAS_Invoice_Invoice_Cancel:
        public const string WAS_Invoice_Invoice_Cancel = "ErridnInventory.WAS_Invoice_Invoice_Cancel"; //// //WAS_Invoice_Invoice_Cancel

        // Invoice_Invoice_Cancel:
        public const string Invoice_Invoice_Cancel = "ErridnInventory.Invoice_Invoice_Cancel"; //// //Invoice_Invoice_Cancel
        public const string Invoice_Invoice_Cancel_InvoiceDtlNotFound = "ErridnInventory.Invoice_Invoice_Cancel_InvoiceDtlNotFound"; //// //Invoice_Invoice_Cancel_InvoiceDtlNotFound
        public const string Invoice_Invoice_Cancel_InvoiceNoIsNotNull = "ErridnInventory.Invoice_Invoice_Cancel_InvoiceNoIsNotNull"; //// //Invoice_Invoice_Cancel_InvoiceNoIsNotNull
        public const string Invoice_Invoice_Cancel_InvalidMST = "ErridnInventory.Invoice_Invoice_Cancel_InvalidMST"; //// //Invoice_Invoice_Cancel_InvalidMST

        // WAS_Invoice_Invoice_Approved:
        public const string WAS_Invoice_Invoice_Approved = "ErridnInventory.WAS_Invoice_Invoice_Approved"; //// //WAS_Invoice_Invoice_Approved

        // Invoice_Invoice_Approved:
        public const string Invoice_Invoice_Approved = "ErridnInventory.Invoice_Invoice_Approved"; //// //Invoice_Invoice_Approved
        public const string Invoice_Invoice_Approved_InvoiceDtlNotFound = "ErridnInventory.Invoice_Invoice_Approved_InvoiceDtlNotFound"; //// //Invoice_Invoice_Approved_InvoiceDtlNotFound
        public const string Invoice_Invoice_Approved_InvoiceNoIsNotNull = "ErridnInventory.Invoice_Invoice_Approved_InvoiceNoIsNotNull"; //// //Invoice_Invoice_Approved_InvoiceNoIsNotNull
        public const string Invoice_Invoice_Approved_InvalidMST = "ErridnInventory.Invoice_Invoice_Approved_InvalidMST"; //// //Invoice_Invoice_Approved_InvalidMST
        public const string Invoice_Invoice_ApprovedMultiX_Input_InvoiceTblNotFound = "ErridnInventory.Invoice_Invoice_ApprovedX_Input_InvoiceTblNotFound"; //// //Invoice_Invoice_ApprovedX_Input_InvoiceTblNotFound
        public const string Invoice_Invoice_ApprovedMultiX_Input_InvoiceTblInvalid = "ErridnInventory.Invoice_Invoice_ApprovedX_Input_InvoiceTblInvalid"; //// //Invoice_Invoice_ApprovedX_Input_InvoiceTblInvalid
        public const string Invoice_Invoice_ApprovedMultiX_InvoiceNoIsNotNull = "ErridnInventory.Invoice_Invoice_Approved_InvoiceNoIsNotNull"; //// //Invoice_Invoice_Approved_InvoiceNoIsNotNull
        public const string Invoice_Invoice_ApprovedMultiX_InvalidMST = "ErridnInventory.Invoice_Invoice_ApprovedMultiX_InvalidMST"; //// //Invoice_Invoice_ApprovedMultiX_InvalidMST
        // WAS_Invoice_Invoice_Issued:
        public const string WAS_Invoice_Invoice_Issued = "ErridnInventory.WAS_Invoice_Invoice_Issued"; //// //WAS_Invoice_Invoice_Issued

        // Invoice_Invoice_Issued:
        public const string Invoice_Invoice_Issued = "ErridnInventory.Invoice_Invoice_Issued"; //// //Invoice_Invoice_Issued
        public const string Invoice_Invoice_Issued_InvoiceDtlNotFound = "ErridnInventory.Invoice_Invoice_Issued_InvoiceDtlNotFound"; //// //Invoice_Invoice_Issued_InvoiceDtlNotFound
        public const string Invoice_Invoice_Issued_InvalidMST = "ErridnInventory.Invoice_Invoice_Issued_InvalidMST"; //// //Invoice_Invoice_Issued_InvalidMST

        // OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSite:
        public const string OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSite = "ErridnInventory.OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSite"; //// //OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSite

        // WAS_Invoice_Invoice_Deleted:
        public const string WAS_Invoice_Invoice_Deleted = "ErridnInventory.WAS_Invoice_Invoice_Deleted"; //// //WAS_Invoice_Invoice_Deleted

        // Invoice_Invoice_Deleted:
        public const string Invoice_Invoice_Deleted = "ErridnInventory.Invoice_Invoice_Deleted"; //// //Invoice_Invoice_Deleted
        public const string Invoice_Invoice_Deleted_InvoiceDtlNotFound = "ErridnInventory.Invoice_Invoice_Deleted_InvoiceDtlNotFound"; //// //Invoice_Invoice_Deleted_InvoiceDtlNotFound
        public const string Invoice_Invoice_Deleted_InvalidMST = "ErridnInventory.Invoice_Invoice_Deleted_InvalidMST"; //// //Invoice_Invoice_Deleted_InvalidMST

        // WAS_Invoice_Invoice_AllocatedInv:
        public const string WAS_Invoice_Invoice_AllocatedInv = "ErridnInventory.WAS_Invoice_Invoice_AllocatedInv"; //// //WAS_Invoice_Invoice_AllocatedInv
        
        // Invoice_Invoice_AllocatedInv:
        public const string Invoice_Invoice_AllocatedInv = "ErridnInventory.Invoice_Invoice_AllocatedInv"; //// //Invoice_Invoice_AllocatedInv
        public const string Invoice_Invoice_AllocatedInv_InvoiceDtlNotFound = "ErridnInventory.Invoice_Invoice_AllocatedInv_InvoiceDtlNotFound"; //// //Invoice_Invoice_AllocatedInv_InvoiceDtlNotFound
        public const string Invoice_Invoice_AllocatedInv_NotAllowAllocatedInv = "ErridnInventory.Invoice_Invoice_AllocatedInv_NotAllowAllocatedInv"; //// //Invoice_Invoice_AllocatedInv_NotAllowAllocatedInv
        public const string Invoice_Invoice_AllocatedInv_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC = "ErridnInventory.Invoice_Invoice_AllocatedInv_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC"; //// //Invoice_Invoice_AllocatedInv_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC
        public const string Invoice_Invoice_AllocatedInv_InvalidInvoiceDateUTCBeforeEffDateStart = "ErridnInventory.Invoice_Invoice_AllocatedInv_InvalidInvoiceDateUTCBeforeEffDateStart"; //// //Invoice_Invoice_AllocatedInv_InvalidInvoiceDateUTCBeforeEffDateStart
        public const string Invoice_Invoice_AllocatedInv_InvoiceDateUTCIsNotNull = "ErridnInventory.Invoice_Invoice_AllocatedInv_InvoiceDateUTCIsNotNull"; //// //Invoice_Invoice_AllocatedInv_InvoiceDateUTCIsNotNull
        public const string Invoice_Invoice_AllocatedInv_InvalidMST = "ErridnInventory.Invoice_Invoice_AllocatedInv_InvalidMST"; //// //Invoice_Invoice_AllocatedInv_InvalidMST
        public const string Invoice_Invoice_AllocatedInv_InvaliInvoiceDateUTCAfterSysDate = "ErridnInventory.Invoice_Invoice_AllocatedInv_InvaliInvoiceDateUTCAfterSysDate"; //// //Invoice_Invoice_AllocatedInv_InvaliInvoiceDateUTCAfterSysDate

        // WAS_Invoice_Invoice_AllocatedAndApprovedAndIssued:
        public const string WAS_Invoice_Invoice_AllocatedAndApprovedAndIssued = "ErridnInventory.WAS_Invoice_Invoice_AllocatedAndApprovedAndIssued"; //// //WAS_Invoice_Invoice_AllocatedAndApprovedAndIssued

        // Invoice_Invoice_AllocatedAndApprovedAndIssued:
        public const string Invoice_Invoice_AllocatedAndApprovedAndIssued = "ErridnInventory.Invoice_Invoice_AllocatedAndApprovedAndIssued"; //// //Invoice_Invoice_AllocatedAndApprovedAndIssued

        // WAS_Invoice_Invoice_Change:
        public const string WAS_Invoice_Invoice_Change = "ErridnInventory.WAS_Invoice_Invoice_Change"; //// //WAS_Invoice_Invoice_Change

        // WAS_OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSite:
        public const string WAS_OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSite = "ErridnInventory.WAS_OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSite"; //// //WAS_OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSite

        // WAS_OS3A_Invoice_Invoice_Issued_UpdFlagPushOutSiteX:
        public const string Invoice_Invoice_Issued_UpdFlagPushOutSiteX_ExistFlagPushOutSite = "ErridnInventory.Invoice_Invoice_Issued_UpdFlagPushOutSiteX_ExistFlagPushOutSite"; //// //Invoice_Invoice_Issued_UpdFlagPushOutSiteX_ExistFlagPushOutSite

        // WAS_Invoice_Invoice_Change:
        public const string Invoice_Invoice_Change = "ErridnInventory.Invoice_Invoice_Change"; //// //Invoice_Invoice_Change
        public const string Invoice_Invoice_Change_InvoiceDtlNotFound = "ErridnInventory.Invoice_Invoice_Change_InvoiceDtlNotFound"; //// //Invoice_Invoice_Change_InvoiceDtlNotFound
        public const string Invoice_Invoice_Change_InvalidFlagChange = "ErridnInventory.Invoice_Invoice_Change_InvalidFlagChange"; //// //Invoice_Invoice_Change_InvalidFlagChange

        // myCheck_Invoice_TempInvoice_Invoice:
        public const string myCheck_Invoice_TempInvoice_Invoice_InvalidValue = "ErridnInventory.myCheck_Invoice_TempInvoice_Invoice_InvalidValue"; //// //myCheck_Invoice_TempInvoice_Invoice_InvalidValue
                     
        // myCheck_Invoice_Invoice_Invoice:
        public const string myCheck_Invoice_Invoice_InvoiceNoNotUnique = "ErridnInventory.myCheck_Invoice_Invoice_InvoiceNoNotUnique"; //// //myCheck_Invoice_Invoice_InvoiceNoNotUnique

        // myCheck_Invoice_Invoice_Invoice:
        public const string myCheck_Invoice_Invoice_RefNo_ExistRefNo = "ErridnInventory.myCheck_Invoice_Invoice_RefNo_ExistRefNo"; //// //myCheck_Invoice_Invoice_RefNo_ExistRefNo

        // myCheck_Invoice_Invoice_Total_InvalidValue:
        public const string myCheck_Invoice_Invoice_Total_InvalidValue = "ErridnInventory.myCheck_Invoice_Invoice_Total_InvalidValue"; //// //myCheck_Invoice_Invoice_Total_InvalidValue

        // myCheck_Invoice_InvoiceDtl_Total_InvalidValue:
        public const string myCheck_Invoice_InvoiceDtl_Total_InvalidValue = "ErridnInventory.myCheck_Invoice_InvoiceDtl_Total_InvalidValue"; //// //myCheck_Invoice_InvoiceDtl_Total_InvalidValue

        // OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued:
        public const string OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued = "ErridnInventory.OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued"; //// //OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued

        // OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued:
        public const string OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued = "ErridnInventory.OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued"; //// //OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued

        // OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued:
        public const string OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued = "ErridnInventory.OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued"; //// //OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued

        // WAS_OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued:
        public const string WAS_OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued = "ErridnInventory.WAS_OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued"; //// //WAS_OSDMS_Invoice_Invoice_SaveAndAllocatedAndApprovedAndIssued

        // WAS_OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued:
        public const string WAS_OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued = "ErridnInventory.WAS_OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued"; //// //WAS_OSDMS_Invoice_Invoice_SaveReplaceAndAllocatedAndApprovedAndIssued

        // WAS_OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued:
        public const string WAS_OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued = "ErridnInventory.WAS_OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued"; //// //WAS_OSDMS_Invoice_Invoice_SaveAdjAndAllocatedAndApprovedAndIssued

        // WAS_OSDMS_Invoice_Invoice_Calc:
        public const string WAS_OSDMS_Invoice_Invoice_Calc = "ErridnInventory.WAS_OSDMS_Invoice_Invoice_Calc"; //// //WAS_OSDMS_Invoice_Invoice_Calc

        // OSDMS_Invoice_Invoice_Calc:
        public const string OSDMS_Invoice_Invoice_Calc = "ErridnInventory.OSDMS_Invoice_Invoice_Calc"; //// //OSDMS_Invoice_Invoice_Calc

        // Invoice_Invoice_SaveAndAllocatedInv
        public const string WAS_Invoice_Invoice_SaveAndAllocatedInv = "ErridnInventory.WAS_Invoice_Invoice_SaveAndAllocatedInv"; //WAS_Invoice_Invoice_SaveAndAllocatedInv
        public const string Invoice_Invoice_SaveAndAllocatedInv = "ErridnInventory.Invoice_Invoice_SaveAndAllocatedInv"; //WAS_Invoice_Invoice_SaveAndAllocatedInv

        // WAS_Invoice_Invoice_ApprovedAndIssued
        public const string WAS_Invoice_Invoice_ApprovedAndIssued = "ErridnInventory.WAS_Invoice_Invoice_ApprovedAndIssued"; //// // WAS_Invoice_Invoice_ApprovedAndIssued
        public const string Invoice_Invoice_ApprovedAndIssued = "ErridnInventory.Invoice_Invoice_ApprovedAndIssued"; //// // Invoice_Invoice_ApprovedAndIssued
        public const string Invoice_Invoice_IssuedXMulti_Input_InvoiceTblNotFound = "ErridnInventory.Invoice_Invoice_IssuedXMulti_Input_InvoiceTblNotFound"; //// // Invoice_Invoice_IssuedXMulti_Input_InvoiceTblNotFound
        public const string Invoice_Invoice_IssuedMultiX_Input_InvoiceTblInvalid = "ErridnInventory.Invoice_Invoice_IssuedMultiX_Input_InvoiceTblInvalid";//// // Invoice_Invoice_IssuedMultiX_Input_InvoiceTblInvalid
        // Invoice_Invoice_UpdMailSentDTimeUTC
        public const string Invoice_Invoice_UpdMailSentDTimeUTC = "ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTC"; // Invoice_Invoice_UpdMailSentDTimeUTC
        // Invoice_Invoice_UpdMailSentDTimeUTCX
        public const string Invoice_Invoice_UpdMailSentDTimeUTCX_InvoiceNoIsNotNull  = "ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTCX_InvoiceNoIsNotNull"; // Invoice_Invoice_UpdMailSentDTimeUTCX_InvoiceNoIsNotNull
        public const string Invoice_Invoice_UpdMailSentDTimeUTCX_Input_InvoiceTblNotFound = "ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTCX_InvoiceNoIsNotNull"; // Invoice_Invoice_UpdMailSentDTimeUTCX_InvoiceNoIsNotNull
        public const string Invoice_Invoice_UpdMailSentDTimeUTCX_Input_InvoiceTblInvalid = "ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTCX_InvoiceNoIsNotNull"; // Invoice_Invoice_UpdMailSentDTimeUTCX_InvoiceNoIsNotNull
        public const string Invoice_Invoice_UpdMailSentDTimeUTCX_Invalid = "ErridnInventory.Invoice_Invoice_UpdMailSentDTimeUTCX_Invalid"; //// Invoice_Invoice_UpdMailSentDTimeUTCX_Invalid
        // WAS_Invoice_Invoice_UpdMailSentDTimeUTC
        public const string WAS_Invoice_Invoice_UpdMailSentDTimeUTC = "ErridnInventory.WAS_Invoice_Invoice_UpdMailSentDTimeUTC";//// //WAS_Invoice_Invoice_UpdMailSentDTimeUTC
        
        #endregion

        #region // Invoice_InvoiceInput:
        // Invoice_InvoiceInput_CheckDB:        
        public const string Invoice_InvoiceInput_CheckDB_InvoiceNotFound = "ErridnInventory.Invoice_InvoiceInput_CheckDB_InvoiceNotFound"; //// //Invoice_InvoiceInput_CheckDB_InvoiceNotFound
        public const string Invoice_InvoiceInput_CheckDB_InvoiceExist = "ErridnInventory.Invoice_InvoiceInput_CheckDB_InvoiceExist"; //// //Invoice_InvoiceInput_CheckDB_InvoiceExist
        public const string Invoice_InvoiceInput_CheckDB_InvoiceStatusNotMatched = "ErridnInventory.Invoice_InvoiceInput_CheckDB_InvoiceStatusNotMatched"; //// //Invoice_InvoiceInput_CheckDB_InvoiceStatusNotMatched

        // Invoice_InvoiceInput_Get:
        public const string Invoice_InvoiceInput_Get = "ErridnInventory.Invoice_InvoiceInput_Get"; //// //Invoice_InvoiceInput_Get

        // WAS_Invoice_InvoiceInput_Get:
        public const string WAS_Invoice_InvoiceInput_Get = "ErridnInventory.WAS_Invoice_InvoiceInput_Get"; //// //WAS_Invoice_InvoiceInput_Get

        // Invoice_InvoiceInput_Save:
        public const string Invoice_InvoiceInput_Save = "ErridnInventory.Invoice_InvoiceInput_Save"; //// //Invoice_InvoiceInput_Save

        // Invoice_InvoiceInput_Delete:
        public const string Invoice_InvoiceInput_Delete = "ErridnInventory.Invoice_InvoiceInput_Delete"; //// //Invoice_InvoiceInput_Delete

        // WAS_Invoice_InvoiceInput_Save:
        public const string WAS_Invoice_InvoiceInput_Save = "ErridnInventory.WAS_Invoice_InvoiceInput_Save"; //// //WAS_Invoice_InvoiceInput_Save

        // WAS_Invoice_InvoiceInput_Delete:
        public const string WAS_Invoice_InvoiceInput_Delete = "ErridnInventory.WAS_Invoice_InvoiceInput_Delete"; //// //WAS_Invoice_InvoiceInput_Delete

        // Invoice_InvoiceInput_SaveX:
        public const string Invoice_InvoiceInput_SaveX = "ErridnInventory.Invoice_InvoiceInput_SaveX"; //// //Invoice_InvoiceInput_SaveX
        public const string Invoice_InvoiceInput_SaveX_Input_InvoiceTblNotFound = "ErridnInventory.Invoice_InvoiceInput_SaveX_Input_InvoiceTblNotFound"; //// //Invoice_InvoiceInput_SaveX_Input_InvoiceTblNotFound

        public const string Invoice_InvoiceInput_SaveX_Input_InvoiceTblInvalid = "ErridnInventory.Invoice_InvoiceInput_SaveX_Input_InvoiceTblInvalid"; //// //Invoice_InvoiceInput_SaveX_Input_InvoiceTblInvalid

        public const string Invoice_InvoiceInput_SaveX_InvalidInvoiceCode = "ErridnInventory.Invoice_InvoiceInput_SaveX_InvalidInvoiceCode"; //// //Invoice_InvoiceInput_SaveX_InvalidInvoiceCode
        public const string Invoice_InvoiceInput_SaveX_StatusNotMatched = "ErridnInventory.Invoice_InvoiceInput_SaveX_StatusNotMatched"; //// //Invoice_InvoiceInput_SaveX_StatusNotMatched
        public const string Invoice_InvoiceInput_SaveX_Input_InvoiceDtlTblNotFound = "ErridnInventory.Invoice_InvoiceInput_SaveX_Input_InvoiceDtlTblNotFound"; //// //Invoice_InvoiceInput_SaveX_Input_InvoiceDtlTblNotFound
        public const string Invoice_InvoiceInput_SaveX_Input_InvoiceDtlTblInvalid = "ErridnInventory.Invoice_InvoiceInput_SaveX_Input_InvoiceDtlTblInvalid"; //// //Invoice_InvoiceInput_SaveX_Input_InvoiceDtlTblInvalid
        
        public const string Invoice_InvoiceInput_SaveX_Invalid_TempInvoice_NotFound = "ErridnInventory.Invoice_InvoiceInput_SaveX_Invalid_TempInvoice_NotFound"; //// //Invoice_InvoiceInput_SaveX_Invalid_TempInvoice_NotFound
        public const string Invoice_InvoiceInput_SaveX_Invalid_TempInvoice_StatusNotMatch = "ErridnInventory.Invoice_InvoiceInput_SaveX_Invalid_TempInvoice_StatusNotMatch"; //// //Invoice_InvoiceInput_SaveX_Invalid_TempInvoice_StatusNotMatch

        #endregion

        #region // OS_Invoice_InvoiceTemp:
        // OS_Invoice_InvoiceTemp_CheckDB:        
        public const string OS_Invoice_InvoiceTemp_CheckDB_InvoiceNotFound = "ErridnInventory.OS_Invoice_InvoiceTemp_CheckDB_InvoiceNotFound"; //// //OS_Invoice_InvoiceTemp_CheckDB_InvoiceNotFound
        public const string OS_Invoice_InvoiceTemp_CheckDB_InvoiceExist = "ErridnInventory.OS_Invoice_InvoiceTemp_CheckDB_InvoiceExist"; //// //OS_Invoice_InvoiceTemp_CheckDB_InvoiceExist
        public const string OS_Invoice_InvoiceTemp_CheckDB_InvoiceStatusNotMatched = "ErridnInventory.OS_Invoice_InvoiceTemp_CheckDB_InvoiceStatusNotMatched"; //// //OS_Invoice_InvoiceTemp_CheckDB_InvoiceStatusNotMatched

        // OS_Invoice_InvoiceTemp_Get:
        public const string OS_Invoice_InvoiceTemp_Get = "ErridnInventory.OS_Invoice_InvoiceTemp_Get"; //// //OS_Invoice_InvoiceTemp_Get

        // WAS_OS_Invoice_InvoiceTemp_Get:
        public const string WAS_OS_Invoice_InvoiceTemp_Get = "ErridnInventory.WAS_OS_Invoice_InvoiceTemp_Get"; //// //WAS_OS_Invoice_InvoiceTemp_Get

        // OS_Invoice_InvoiceTemp_Save:
        public const string OS_Invoice_InvoiceTemp_Save = "ErridnInventory.OS_Invoice_InvoiceTemp_Save"; //// //OS_Invoice_InvoiceTemp_Save

        // OS_Invoice_InvoiceTemp_Delete:
        public const string OS_Invoice_InvoiceTemp_Delete = "ErridnInventory.OS_Invoice_InvoiceTemp_Delete"; //// //OS_Invoice_InvoiceTemp_Delete

        // WAS_OS_Invoice_InvoiceTemp_Save:
        public const string WAS_OS_Invoice_InvoiceTemp_Save = "ErridnInventory.WAS_OS_Invoice_InvoiceTemp_Save"; //// //WAS_OS_Invoice_InvoiceTemp_Save

        // WAS_OS_Invoice_InvoiceTemp_Delete:
        public const string WAS_OS_Invoice_InvoiceTemp_Delete = "ErridnInventory.WAS_OS_Invoice_InvoiceTemp_Delete"; //// //WAS_OS_Invoice_InvoiceTemp_Delete

        // OS_Invoice_InvoiceTemp_SaveX:
        public const string OS_Invoice_InvoiceTemp_SaveX = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX"; //// //OS_Invoice_InvoiceTemp_SaveX
        public const string OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceTblNotFound = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceTblNotFound"; //// //OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceTblNotFound

        public const string OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceTblInvalid = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceTblInvalid"; //// //OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceTblInvalid

        public const string OS_Invoice_InvoiceTemp_SaveX_InvalidInvoiceCode = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_InvalidInvoiceCode"; //// //OS_Invoice_InvoiceTemp_SaveX_InvalidInvoiceCode
        public const string OS_Invoice_InvoiceTemp_SaveX_StatusNotMatched = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_StatusNotMatched"; //// //OS_Invoice_InvoiceTemp_SaveX_StatusNotMatched
        public const string OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceDtlTblNotFound = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceDtlTblNotFound"; //// //OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceDtlTblNotFound
        public const string OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceDtlTblInvalid = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceDtlTblInvalid"; //// //OS_Invoice_InvoiceTemp_SaveX_Input_InvoiceDtlTblInvalid

        public const string OS_Invoice_InvoiceTemp_SaveX_Invalid_TempInvoice_NotFound = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_Invalid_TempInvoice_NotFound"; //// //OS_Invoice_InvoiceTemp_SaveX_Invalid_TempInvoice_NotFound
        public const string OS_Invoice_InvoiceTemp_SaveX_Invalid_TempInvoice_StatusNotMatch = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_Invalid_TempInvoice_StatusNotMatch"; //// //OS_Invoice_InvoiceTemp_SaveX_Invalid_TempInvoice_StatusNotMatch

        public const string OS_Invoice_InvoiceTemp_SaveX_InvalidSignStatus = "ErridnInventory.OS_Invoice_InvoiceTemp_SaveX_InvalidSignStatus"; //// //OS_Invoice_InvoiceTemp_SaveX_InvalidSignStatus

        // OS_Invoice_InvoiceTemp_UpdMultiSignStatus:
        public const string WAS_OS_Invoice_InvoiceTemp_UpdMultiSignStatus = "ErridnInventory.WAS_OS_Invoice_InvoiceTemp_UpdMultiSignStatus"; //// //WAS_OS_Invoice_InvoiceTemp_UpdMultiSignStatus
        public const string OS_Invoice_InvoiceTemp_UpdMultiSignStatus = "ErridnInventory.OS_Invoice_InvoiceTemp_UpdMultiSignStatus"; //// //OS_Invoice_InvoiceTemp_UpdMultiSignStatus
        public const string OS_Invoice_InvoiceTemp_UpdMultiSignStatus_Input_OS_Invoice_InvoiceTemptblNotFound = "ErridnInventory.OS_Invoice_InvoiceTemp_UpdMultiSignStatus_Input_OS_Invoice_InvoiceTemptblNotFound"; //// //OS_Invoice_InvoiceTemp_UpdMultiSignStatus_Input_OS_Invoice_InvoiceTemptblNotFound
        public const string OS_Invoice_InvoiceTemp_UpdMultiSignStatus_Input_OS_Invoice_InvoiceTemptblInvalid = "ErridnInventory.OS_Invoice_InvoiceTemp_UpdMultiSignStatus_Input_OS_Invoice_InvoiceTemptblInvalid"; //// //OS_Invoice_InvoiceTemp_UpdMultiSignStatus_Input_OS_Invoice_InvoiceTemptblInvalid

        #endregion

        #region // Mst_SourceInvoice:
        // Mst_SourceInvoice_CheckDB:        
        public const string Mst_SourceInvoice_CheckDB_SourceInvoiceNotFound = "ErridnInventory.Mst_SourceInvoice_CheckDB_SourceInvoiceNotFound"; //// //Mst_SourceInvoice_CheckDB_SourceInvoiceNotFound
        public const string Mst_SourceInvoice_CheckDB_SourceInvoiceExist = "ErridnInventory.Mst_SourceInvoice_CheckDB_SourceInvoiceExist"; //// //Mst_SourceInvoice_CheckDB_SourceInvoiceExist
        public const string Mst_SourceInvoice_CheckDB_CheckDB_InvoiceStatusNotMatched = "ErridnInventory.Mst_SourceInvoice_CheckDB_CheckDB_InvoiceStatusNotMatched"; //// //Mst_SourceInvoice_CheckDB_CheckDB_InvoiceStatusNotMatched
        #endregion

        #region // myCache_ViewAbility_CheckAccessMSTWrite:
        // myCache_ViewAbility_CheckAccessMSTWrite:
        public const string myCache_ViewAbility_CheckAccessMSTWrite = "ErridnInventory.myCache_ViewAbility_CheckAccessMSTWrite"; //// //myCache_ViewAbility_CheckAccessMSTWrite
        #endregion

        #region // Invoice_TempInvoice:
        // Invoice_TempInvoice_CheckDB:
        public const string Invoice_TempInvoice_CheckDB_TempInvoiceNotFound = "ErridnInventory.Invoice_TempInvoice_CheckDB_TempInvoiceNotFound"; //// //Invoice_TempInvoice_CheckDB_TempInvoiceNotFound
        public const string Invoice_TempInvoice_CheckDB_TempInvoiceExist = "ErridnInventory.Invoice_TempInvoice_CheckDB_TempInvoiceExist"; //// //Invoice_TempInvoice_CheckDB_TempInvoiceExist
        public const string Invoice_TempInvoice_CheckDB_FlagActiveNotMatched = "ErridnInventory.Invoice_TempInvoice_CheckDB_FlagActiveNotMatched"; //// //Invoice_TempInvoice_CheckDB_FlagActiveNotMatched
        public const string Invoice_TempInvoice_CheckDB_StatusNotMatched = "ErridnInventory.Invoice_TempInvoice_CheckDB_StatusNotMatched"; //// //Invoice_TempInvoice_CheckDB_StatusNotMatched
        public const string Invoice_TempInvoiceCheckDB_CustomFieldNotFound = "ErridnInventory.Invoice_TempInvoiceCheckDB_CustomFieldNotFound"; //// // Invoice_TempInvoiceCheckDB_CustomFieldNotFound

        //// Invoice_TempInvoice_CheckFormNo
        public const string Invoice_TempInvoice_CheckFormNo_ExistFormNo = "ErridnInventory.Invoice_TempInvoice_CheckFormNo_ExistFormNo"; //// //Invoice_TempInvoice_CheckFormNo_ExistFormNo

        //// Invoice_license_TotalQtyIssued
        public const string Invoice_license_TotalQtyIssued = "ErridnInventory.Invoice_license_TotalQtyIssued"; //// //Invoice_license_TotalQtyIssued
        public const string Invoice_license_TotalQtyIssued_InvalidValue = "ErridnInventory.Invoice_license_TotalQtyIssued_InvalidValue"; //// //Invoice_license_TotalQtyIssued_InvalidValue

        //// Invoice_license_TotalQtyUsed
        public const string Invoice_license_TotalQtyUsed = "ErridnInventory.Invoice_license_TotalQtyUsed"; //// //Invoice_license_TotalQtyUsed
        public const string Invoice_license_TotalQtyUsed_InvalidValue = "ErridnInventory.Invoice_license_TotalQtyUsed_InvalidValue"; //// //Invoice_license_TotalQtyUsed_InvalidValue

        //// Invoice_TempInvoice_CheckFormNoFormat
        public const string Invoice_TempInvoice_CheckFormNoFormat = "ErridnInventory.Invoice_TempInvoice_CheckFormNoFormat"; //// //Invoice_TempInvoice_CheckFormNoFormat

        //// Invoice_TempInvoice_CheckFormatSign
        public const string Invoice_TempInvoice_CheckFormatSign = "ErridnInventory.Invoice_TempInvoice_CheckFormatSign"; //// //

        //// Seq_FormNo_Get
        public const string Seq_FormNo_Get = "ErridnInventory.Seq_FormNo_Get"; //// //Seq_FormNo_Get

        // Invoice_TempInvoice_Get:
        public const string Invoice_TempInvoice_Get = "ErridnInventory.Invoice_TempInvoice_Get"; //// //Invoice_TempInvoice_Get

        // Invoice_TempInvoice_SaveX:
        public const string Invoice_TempInvoice_SaveX = "ErridnInventory.Invoice_TempInvoice_SaveX"; //// //Invoice_TempInvoice_SaveX
        public const string Invoice_TempInvoice_SaveX_InvalidTInvoiceCode = "ErridnInventory.Invoice_TempInvoice_SaveX_InvalidTInvoiceCode"; //// //Invoice_TempInvoice_SaveX_InvalidTInvoiceCode
        public const string Invoice_TempInvoice_SaveX_StatusNotMatched = "ErridnInventory.Invoice_TempInvoice_SaveX_StatusNotMatched"; //// //Invoice_TempInvoice_SaveX_StatusNotMatched
        public const string Invoice_TempInvoice_SaveX_InvalidFormNo = "ErridnInventory.Invoice_TempInvoice_SaveX_InvalidFormNo"; //// //Invoice_TempInvoice_SaveX_InvalidFormNo
        public const string Invoice_TempInvoice_SaveX_InvalidSign = "ErridnInventory.Invoice_TempInvoice_SaveX_InvalidSign"; //// //Invoice_TempInvoice_SaveX_InvalidSign
        public const string Invoice_TempInvoice_SaveX_NNTNotUsedInvoiceTGroupCode = "ErridnInventory.Invoice_TempInvoice_SaveX_NNTNotUsedInvoiceTGroupCode"; //// //Invoice_TempInvoice_SaveX_NNTNotUsedInvoiceTGroupCode
        
        public const string Invoice_TempInvoice_SaveX_InvalidDBFieldName = "ErridnInventory.Invoice_TempInvoice_SaveX_InvalidDBFieldName"; //// // Invoice_TempInvoice_SaveX_InvalidDBFieldName
        public const string Invoice_TempInvoice_SaveX_InvalidTCFType = "ErridnInventory.Invoice_TempInvoice_SaveX_InvalidTCFType"; //// // Invoice_TempInvoice_SaveX_InvalidTCFType
        public const string Invoice_TempInvoice_SaveX_InvalidDisplayName = "ErridnInventory.Invoice_TempInvoice_SaveX_InvalidDisplayName"; //// // Invoice_TempInvoice_SaveX_InvalidDisplayName

        // Invoice_TempInvoice_Save:
        public const string Invoice_TempInvoice_Save = "ErridnInventory.Invoice_TempInvoice_Save"; //// //Invoice_TempInvoice_Save

        // Invoice_TempInvoice_UpdQtyInvoiceNo:
        public const string Invoice_TempInvoice_UpdQtyInvoiceNo = "ErridnInventory.Invoice_TempInvoice_UpdQtyInvoiceNo"; //// //Invoice_TempInvoice_UpdQtyInvoiceNo

        // Invoice_TempInvoice_UpdQtyInvoiceNoX:
        public const string Invoice_TempInvoice_UpdQtyInvoiceNoX_InvalidValue = "ErridnInventory.Invoice_TempInvoice_UpdQtyInvoiceNoX_InvalidValue"; //// //Invoice_TempInvoice_UpdQtyInvoiceNoX_InvalidValue

        // Invoice_TempInvoice_Issued:
        public const string Invoice_TempInvoice_Issued = "ErridnInventory.Invoice_TempInvoice_Issued"; //// //Invoice_TempInvoice_Issued

        // Invoice_TempInvoice_InActive:
        public const string Invoice_TempInvoice_InActive = "ErridnInventory.Invoice_TempInvoice_InActive"; //// //Invoice_TempInvoice_InActive

        // WAS_Invoice_TempInvoice_Save:
        public const string WAS_Invoice_TempInvoice_Save = "ErridnInventory.Invoice_TempInvoice_Save"; //// //WAS_Invoice_TempInvoice_Save

        // WAS_Invoice_TempInvoice_UpdQtyInvoiceNo:
        public const string WAS_Invoice_TempInvoice_UpdQtyInvoiceNo = "ErridnInventory.WAS_Invoice_TempInvoice_UpdQtyInvoiceNo"; //// //WAS_Invoice_TempInvoice_UpdQtyInvoiceNo

        // WAS_Invoice_TempInvoice_Issued:
        public const string WAS_Invoice_TempInvoice_Issued = "ErridnInventory.WAS_Invoice_TempInvoice_Issued"; //// //WAS_Invoice_TempInvoice_Issued

        // Invoice_TempInvoice_IssuedX:
        public const string Invoice_TempInvoice_IssuedX = "ErridnInventory.Invoice_TempInvoice_IssuedX"; //// //Invoice_TempInvoice_IssuedX
        public const string Invoice_TempInvoice_IssuedX_InvalidEndInvoiceNoAndStatrInvoiceNo = "ErridnInventory.Invoice_TempInvoice_IssuedX_InvalidEndInvoiceNoAndStatrInvoiceNo"; //// //Invoice_TempInvoice_IssuedX_InvalidEndInvoiceNoAndStatrInvoiceNo
        public const string Invoice_TempInvoice_IssuedX_InvalidIEffDateStartAfterSysDate = "ErridnInventory.Invoice_TempInvoice_IssuedX_InvalidIEffDateStartAfterSysDate"; //// //Invoice_TempInvoice_IssuedX_InvalidIEffDateStartAfterSysDate

        // WAS_Invoice_TempInvoice_InActive:
        public const string WAS_Invoice_TempInvoice_InActive = "ErridnInventory.WAS_Invoice_TempInvoice_InActive"; //// //WAS_Invoice_TempInvoice_InActive
        #endregion

        #region // Mst_InvoiceType:
        // Mst_InvoiceType_CheckDB:
        public const string Mst_InvoiceType_CheckDB_InvoiceTypeNotFound = "ErridnInventory.Mst_InvoiceType_CheckDB_TempInvoiceNotFound"; //// //Mst_InvoiceType_CheckDB_TempInvoiceNotFound
        public const string Mst_InvoiceType_CheckDB_InvoiceTypeExist = "ErridnInventory.Mst_InvoiceType_CheckDB_TempInvoiceExist"; //// //Mst_InvoiceType_CheckDB_TempInvoiceExist
        public const string Mst_InvoiceType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_InvoiceType_CheckDB_FlagActiveNotMatched"; //// //Mst_InvoiceType_CheckDB_FlagActiveNotMatched
        public const string Mst_InvoiceType_CheckDB_StatusNotMatched = "ErridnInventory.Mst_InvoiceType_CheckDB_StatusNotMatched"; //// //Mst_InvoiceType_CheckDB_StatusNotMatched

        // Mst_InvoiceType_Get:
        public const string Mst_InvoiceType_Get = "ErridnInventory.Mst_InvoiceType_Get"; //// //Mst_InvoiceType_Get

        // WAS_Mst_InvoiceType_Get:
        public const string WAS_Mst_InvoiceType_Get = "ErridnInventory.WAS_Mst_InvoiceType_Get"; //// //WAS_Mst_InvoiceType_Get
        #endregion

        #region // Mst_PaymentMethods:
        // Mst_PaymentMethods_CheckDB:
        public const string Mst_PaymentMethods_CheckDB_PaymentMethodsNotFound = "ErridnInventory.Mst_PaymentMethods_CheckDB_PaymentMethodsNotFound"; //// //Mst_PaymentMethods_CheckDB_PaymentMethodsNotFound
        public const string Mst_PaymentMethods_CheckDB_PaymentMethodsExist = "ErridnInventory.Mst_PaymentMethods_CheckDB_PaymentMethodsExist"; //// //Mst_PaymentMethods_CheckDB_PaymentMethodsExist
        public const string Mst_PaymentMethods_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_PaymentMethods_CheckDB_FlagActiveNotMatched"; //// //Mst_PaymentMethods_CheckDB_FlagActiveNotMatched
        public const string Mst_PaymentMethods_CheckDB_StatusNotMatched = "ErridnInventory.Mst_PaymentMethods_CheckDB_StatusNotMatched"; //// //Mst_PaymentMethods_CheckDB_StatusNotMatched

        // Mst_PaymentMethods_Get:
        public const string Mst_PaymentMethods_Get = "ErridnInventory.Mst_PaymentMethods_Get"; //// //Mst_PaymentMethods_Get

        // WAS_Mst_PaymentMethods_Get:
        public const string WAS_Mst_PaymentMethods_Get = "ErridnInventory.WAS_Mst_PaymentMethods_Get"; //// //WAS_Mst_PaymentMethods_Get
        #endregion

        #region // Mst_VATRate:
        // Mst_VATRate_CheckDB:
        public const string Mst_VATRate_CheckDB_VATRateNotFound = "ErridnInventory.Mst_VATRate_CheckDB_VATRateNotFound"; //// //Mst_VATRate_CheckDB_VATRateNotFound
        public const string Mst_VATRate_CheckDB_VATRateExist = "ErridnInventory.Mst_VATRate_CheckDB_VATRateExist"; //// //Mst_VATRate_CheckDB_VATRateExist
        public const string Mst_VATRate_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_VATRate_CheckDB_FlagActiveNotMatched"; //// //Mst_VATRate_CheckDB_FlagActiveNotMatched

        // Mst_VATRate_Get:
        public const string Mst_VATRate_Get = "ErridnInventory.Mst_VATRate_Get"; //// //Mst_VATRate_Get

        // WAS_Mst_VATRate_Get:
        public const string WAS_Mst_VATRate_Get = "ErridnInventory.WAS_Mst_VATRate_Get"; //// //WAS_Mst_VATRate_Get

        #endregion

        #region // Mst_GovIDType:
        // Mst_GovIDType_CheckDB:
        public const string Mst_GovIDType_CheckDB_GovIDTypeNotFound = "ErridnInventory.Mst_GovIDType_CheckDB_GovIDTypeNotFound"; //// //Mst_GovIDType_CheckDB_GovIDTypeNotFound
        public const string Mst_GovIDType_CheckDB_GovIDTypeExist = "ErridnInventory.Mst_GovIDType_CheckDB_GovIDTypeExist"; //// //Mst_GovIDType_CheckDB_GovIDTypeExist
        public const string Mst_GovIDType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_GovIDType_CheckDB_FlagActiveNotMatched"; //// //Mst_GovIDType_CheckDB_FlagActiveNotMatched

        // Mst_GovIDType_Get:
        public const string Mst_GovIDType_Get = "ErridnInventory.Mst_GovIDType_Get"; //// //Mst_GovIDType_Get

        // WAS_Mst_GovIDType_Get:
        public const string WAS_Mst_GovIDType_Get = "ErridnInventory.WAS_Mst_GovIDType_Get"; //// //WAS_Mst_GovIDType_Get

        #endregion

        #region // Mst_CustomerNNT:
        // Mst_CustomerNNT_CheckDB:
        public const string Mst_CustomerNNT_CheckDB_CustomerNNTNotFound = "ErridnInventory.Mst_CustomerNNT_CheckDB_CustomerNNTNotFound"; //// //Mst_CustomerNNT_CheckDB_CustomerNNTNotFound
        public const string Mst_CustomerNNT_CheckDB_CustomerNNTExist = "ErridnInventory.Mst_CustomerNNT_CheckDB_CustomerNNTExist"; //// //Mst_CustomerNNT_CheckDB_CustomerNNTExist
        public const string Mst_CustomerNNT_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CustomerNNT_CheckDB_FlagActiveNotMatched"; //// //Mst_CustomerNNT_CheckDB_FlagActiveNotMatched

        // Mst_CustomerNNT_Get:
        public const string Mst_CustomerNNT_Get = "ErridnInventory.Mst_CustomerNNT_Get"; //// //Mst_CustomerNNT_Get

        // Mst_CustomerNNT_Create:
        public const string Mst_CustomerNNT_Create = "ErridnInventory.Mst_CustomerNNT_Create"; //// //Mst_CustomerNNT_Create
        public const string Mst_CustomerNNT_Create_InvalidCustomerNNTCode = "ErridnInventory.Mst_CustomerNNT_Create_InvalidCustomerNNTCode"; //// //Mst_CustomerNNT_Create_InvalidCustomerNNTCode
        public const string Mst_CustomerNNT_Create_InvalidMST = "ErridnInventory.Mst_CustomerNNT_Create_InvalidMST"; //// //Mst_CustomerNNT_Create_InvalidMST
        public const string Mst_CustomerNNT_Create_InvalidCustomerNNTName = "ErridnInventory.Mst_CustomerNNT_Create_InvalidCustomerNNTName"; //// //Mst_CustomerNNT_Create_InvalidCustomerNNTName
        public const string Mst_CustomerNNT_Create_InvalidCustomerNNTType = "ErridnInventory.Mst_CustomerNNT_Create_InvalidCustomerNNTType"; //// //Mst_CustomerNNT_Create_InvalidCustomerNNTType
        public const string Mst_CustomerNNT_Create_InvalidContactEmail = "ErridnInventory.Mst_CustomerNNT_Create_InvalidContactEmail"; //// //Mst_CustomerNNT_Create_InvalidContactEmail

        // Mst_CustomerNNT_Update:
        public const string Mst_CustomerNNT_Update = "ErridnInventory.Mst_CustomerNNT_Update"; //// //Mst_CustomerNNT_Update
        public const string Mst_CustomerNNT_Update_InvalidCustomerNNTName = "ErridnInventory.Mst_CustomerNNT_Update_InvalidCustomerNNTName"; //// //Mst_CustomerNNT_Update_InvalidCustomerNNTName
        public const string Mst_CustomerNNT_Update_InvalidCustomerNNTType = "ErridnInventory.Mst_CustomerNNT_Update_InvalidCustomerNNTType"; //// //Mst_CustomerNNT_Update_InvalidCustomerNNTType
        public const string Mst_CustomerNNT_Update_InvalidContactEmail = "ErridnInventory.Mst_CustomerNNT_Update_InvalidContactEmail"; //// //Mst_CustomerNNT_Update_InvalidContactEmail

        // Mst_CustomerNNT_Delete:
        public const string Mst_CustomerNNT_Delete = "ErridnInventory.Mst_CustomerNNT_Delete"; //// //Mst_CustomerNNT_Delete

        // WAS_Mst_CustomerNNT_Get:
        public const string WAS_Mst_CustomerNNT_Get = "ErridnInventory.WAS_Mst_CustomerNNT_Get"; //// //WAS_Mst_CustomerNNT_Get

        // WAS_Mst_CustomerNNT_Create:
        public const string WAS_Mst_CustomerNNT_Create = "ErridnInventory.WAS_Mst_CustomerNNT_Create"; //// //WAS_Mst_CustomerNNT_Create

        // WAS_Mst_CustomerNNT_Update:
        public const string WAS_Mst_CustomerNNT_Update = "ErridnInventory.WAS_Mst_CustomerNNT_Update"; //// //WAS_Mst_CustomerNNT_Update

        // WAS_Mst_CustomerNNT_Delete:
        public const string WAS_Mst_CustomerNNT_Delete = "ErridnInventory.WAS_Mst_CustomerNNT_Delete"; //// //WAS_Mst_CustomerNNT_Delete


        #endregion

        #region // Invoice_CustomField:
        // Invoice_CustomField_CheckDB:
        public const string Invoice_CustomField_CheckDB_CustomFieldNotFound = "ErridnInventory.Invoice_CustomField_CheckDB_CustomFieldNotFound"; //// // Invoice_CustomField_CheckDB_CustomFieldNotFound
        public const string Invoice_CustomField_CheckDB_CustomFieldExist = "ErridnInventory.Invoice_CustomField_CheckDB_CustomFieldExist"; //// // Invoice_CustomField_CheckDB_CustomFieldExist
        public const string Invoice_CustomField_CheckDB_FlagActiveNotMatched = "ErridnInventory.Invoice_CustomField_CheckDB_FlagActiveNotMatched"; //// // Invoice_CustomField_CheckDB_FlagActiveNotMatched

        // Invoice_CustomField_Get:
        public const string Invoice_CustomField_Get = "ErridnInventory.Invoice_CustomField_Get"; //// // Invoice_CustomField_Get

        // Invoice_CustomField_Create:
        public const string Invoice_CustomField_Create = "ErridnInventory.Invoice_CustomField_Create"; //// // Invoice_CustomField_Create
        public const string Invoice_CustomField_Create_InvalidOrgID = "ErridnInventory.Invoice_CustomField_Create_InvalidOrgID"; //// // Invoice_CustomField_Create_InvalidOrgID
        public const string Invoice_CustomField_Create_InvalidInvoiceCustomFieldCode = "ErridnInventory.Invoice_CustomField_Create_InvalidInvoiceCustomFieldCode"; //// // Invoice_CustomField_Create_InvalidInvoiceCustomFieldCode

        // Invoice_CustomField_Update:
        public const string Invoice_CustomField_Update = "ErridnInventory.Invoice_CustomField_Update"; //// // Invoice_CustomField_Update
        public const string Invoice_CustomField_Update_InvalidInvoiceCustomFieldName = "ErridnInventory.Invoice_CustomField_Update_InvalidInvoiceCustomFieldName"; //// // Invoice_CustomField_Update_InvalidInvoiceCustomFieldName
        public const string Invoice_CustomField_Update_CustomFieldblNotFound = "ErridnInventory.Invoice_CustomField_Update_CustomFieldblNotFound"; //// // Invoice_CustomField_Update_CustomFieldblNotFound
        public const string Invoice_CustomField_Update_Input_CustomFieldblInvalid = "ErridnInventory.Invoice_CustomField_Update_Input_CustomFieldblInvalid"; //// // Invoice_CustomField_Update_Input_CustomFieldblInvalid

        // Invoice_CustomField_Delete:
        public const string Invoice_CustomField_Delete = "ErridnInventory.Invoice_CustomField_Delete"; //// // Invoice_CustomField_Delete

        // WAS_Invoice_CustomField_Get:
        public const string WAS_Invoice_CustomField_Get = "ErridnInventory.WAS_Invoice_CustomField_Get"; //// // WAS_Invoice_CustomField_Get

        // WAS_Invoice_CustomField_Create:
        public const string WAS_Invoice_CustomField_Create = "ErridnInventory.WAS_Invoice_CustomField_Create"; //// // WAS_Invoice_CustomField_Create

        // WAS_Invoice_CustomField_Update:
        public const string WAS_Invoice_CustomField_Update = "ErridnInventory.WAS_Invoice_CustomField_Update"; //// // WAS_Invoice_CustomField_Update

        // WAS_Invoice_CustomField_Delete:
        public const string WAS_Invoice_CustomField_Delete = "ErridnInventory.WAS_Invoice_CustomField_Delete"; //// // WAS_Invoice_CustomField_Delete
        #endregion

        #region // Invoice_DtlCustomField:
        // Invoice_DtlCustomField_CheckDB:
        public const string Invoice_DtlCustomField_CheckDB_CustomFieldNotFound = "ErridnInventory.Invoice_DtlCustomField_CheckDB_CustomFieldNotFound"; //// // Invoice_DtlCustomField_CheckDB_CustomFieldNotFound
        public const string Invoice_DtlCustomField_CheckDB_CustomFieldExist = "ErridnInventory.Invoice_DtlCustomField_CheckDB_CustomFieldExist"; //// // Invoice_DtlCustomField_CheckDB_CustomFieldExist
        public const string Invoice_DtlCustomField_CheckDB_FlagActiveNotMatched = "ErridnInventory.Invoice_DtlCustomField_CheckDB_FlagActiveNotMatched"; //// // Invoice_DtlCustomField_CheckDB_FlagActiveNotMatched

        // Invoice_DtlCustomField_Get:
        public const string Invoice_DtlCustomField_Get = "ErridnInventory.Invoice_DtlCustomField_Get"; //// // Invoice_DtlCustomField_Get

        // Invoice_DtlCustomField_Create:
        public const string Invoice_DtlCustomField_Create = "ErridnInventory.Invoice_DtlCustomField_Create"; //// // Invoice_DtlCustomField_Create
        public const string Invoice_DtlCustomField_Create_InvalidOrgID = "ErridnInventory.Invoice_DtlCustomField_Create_InvalidOrgID"; //// // Invoice_DtlCustomField_Create_InvalidOrgID
        public const string Invoice_DtlCustomField_Create_InvalidInvoiceDtlCustomFieldCode = "ErridnInventory.Invoice_DtlCustomField_Create_InvalidInvoiceDtlCustomFieldCode"; //// // Invoice_DtlCustomField_Create_InvalidInvoiceDtlCustomFieldCode

        // Invoice_DtlCustomField_Update:
        public const string Invoice_DtlCustomField_Update = "ErridnInventory.Invoice_DtlCustomField_Update"; //// // Invoice_DtlCustomField_Update
        public const string Invoice_DtlCustomField_Update_CustomFieldblNotFound = "ErridnInventory.Invoice_DtlCustomField_Update_CustomFieldblNotFound"; //// // Invoice_DtlCustomField_Update_CustomFieldblNotFound
        public const string Invoice_DtlCustomField_Update_Input_CustomFieldblInvalid = "ErridnInventory.Invoice_DtlCustomField_Update_Input_CustomFieldblInvalid"; //// // Invoice_DtlCustomField_Update_Input_CustomFieldblInvalid
        public const string Invoice_DtlCustomField_Update_InvalidInvoiceDtlCustomFieldName = "ErridnInventory.Invoice_DtlCustomField_Update_InvalidInvoiceDtlCustomFieldName"; //// // Invoice_DtlCustomField_Update_InvalidInvoiceDtlCustomFieldName

        // Invoice_DtlCustomField_Delete:
        public const string Invoice_DtlCustomField_Delete = "ErridnInventory.Invoice_DtlCustomField_Delete"; //// // Invoice_DtlCustomField_Delete

        // WAS_Invoice_DtlCustomField_Get:
        public const string WAS_Invoice_DtlCustomField_Get = "ErridnInventory.WAS_Invoice_DtlCustomField_Get"; //// // WAS_Invoice_DtlCustomField_Get

        // WAS_Invoice_DtlCustomField_Create:
        public const string WAS_Invoice_DtlCustomField_Create = "ErridnInventory.WAS_Invoice_DtlCustomField_Create"; //// // WAS_Invoice_DtlCustomField_Create

        // WAS_Invoice_DtlCustomField_Update:
        public const string WAS_Invoice_DtlCustomField_Update = "ErridnInventory.WAS_Invoice_DtlCustomField_Update"; //// // WAS_Invoice_DtlCustomField_Update

        // WAS_Invoice_DtlCustomField_Delete:
        public const string WAS_Invoice_DtlCustomField_Delete = "ErridnInventory.WAS_Invoice_DtlCustomField_Delete"; //// // WAS_Invoice_DtlCustomField_Delete
        #endregion

        #region // Invoice_TempCustomField:
        // Invoice_TempCustomField_CheckDB:
        public const string Invoice_TempCustomField_CheckDB_CustomFieldNotFound = "ErridnInventory.Invoice_TempCustomField_CheckDB_CustomFieldNotFound"; //// // Invoice_TempCustomField_CheckDB_CustomFieldNotFound
        public const string Invoice_TempCustomField_CheckDB_CustomFieldExist = "ErridnInventory.Invoice_TempCustomField_CheckDB_CustomFieldExist"; //// // Invoice_TempCustomField_CheckDB_CustomFieldExist
        public const string Invoice_TempCustomField_CheckDB_FlagActiveNotMatched = "ErridnInventory.Invoice_TempCustomField_CheckDB_FlagActiveNotMatched"; //// // Invoice_TempCustomField_CheckDB_FlagActiveNotMatched

        // Invoice_TempCustomField_Get:
        public const string Invoice_TempCustomField_Get = "ErridnInventory.Invoice_TempCustomField_Get"; //// // Invoice_TempCustomField_Get

        //// Invoice_TempCustomField_Create:
        //public const string Invoice_TempInvoice_Create = "ErridnInventory.Invoice_TempCustomField_Create"; //// // Invoice_TempCustomField_Create
        //public const string Invoice_TempInvoice_Create_InvalidOrgID = "ErridnInventory.Invoice_TempCustomField_Create_InvalidOrgID"; //// // Invoice_TempCustomField_Create_InvalidOrgID
        //public const string Invoice_TempInvoice_Create_InvalidDBFieldName = "ErridnInventory.Invoice_TempCustomField_Create_InvalidDBFieldName"; //// // Invoice_TempCustomField_Create_InvalidDBFieldName
        //public const string Invoice_TempInvoice_Create_InvalidTCFType = "ErridnInventory.Invoice_TempCustomField_Create_InvalidTCFType"; //// // Invoice_TempCustomField_Create_InvalidTCFType
        //public const string Invoice_TempInvoice_Create_InvalidDisplayName = "ErridnInventory.Invoice_TempCustomField_Create_InvalidDisplayName"; //// // Invoice_TempCustomField_Create_InvalidDisplayName

        //// Invoice_TempCustomField_Update:
        //public const string Invoice_TempInvoice_Update = "ErridnInventory.Invoice_TempCustomField_Update"; //// // Invoice_TempCustomField_Update
        //public const string Invoice_TempInvoice_Update_InvalidInvoiceTempCustomFieldName = "ErridnInventory.Invoice_TempCustomField_Update_InvalidInvoiceTempCustomFieldName"; //// // Invoice_TempCustomField_Update_InvalidInvoiceTempCustomFieldName
        //public const string Invoice_TempInvoice_Update_CustomFieldblNotFound = "ErridnInventory.Invoice_TempCustomField_Update_CustomFieldblNotFound"; //// // Invoice_TempCustomField_Update_CustomFieldblNotFound
        //public const string Invoice_TempInvoice_Update_Input_CustomFieldblInvalid = "ErridnInventory.Invoice_TempCustomField_Update_Input_CustomFieldblInvalid"; //// // Invoice_TempCustomField_Update_Input_CustomFieldblInvalid

        //// Invoice_TempCustomField_Delete:
        //public const string Invoice_TempCustomField_Delete = "ErridnInventory.Invoice_TempCustomField_Delete"; //// // Invoice_TempCustomField_Delete

        //// WAS_Invoice_TempCustomField_Get:
        //public const string WAS_Invoice_TempCustomField_Get = "ErridnInventory.WAS_Invoice_TempCustomField_Get"; //// // WAS_Invoice_TempCustomField_Get

        //// WAS_Invoice_TempCustomField_Create:
        //public const string WAS_Invoice_TempCustomField_Create = "ErridnInventory.WAS_Invoice_TempCustomField_Create"; //// // WAS_Invoice_TempCustomField_Create

        //// WAS_Invoice_TempCustomField_Update:
        //public const string WAS_Invoice_TempCustomField_Update = "ErridnInventory.WAS_Invoice_TempCustomField_Update"; //// // WAS_Invoice_TempCustomField_Update

        //// WAS_Invoice_TempCustomField_Delete:
        //public const string WAS_Invoice_TempCustomField_Delete = "ErridnInventory.WAS_Invoice_TempCustomField_Delete"; //// // WAS_Invoice_TempCustomField_Delete
        #endregion

        #region // Mst_InvoiceType2:
        // Mst_InvoiceType2_CheckDB:
        public const string Mst_InvoiceType2_CheckDB_InvoiceType2NotFound = "ErridnInventory.Mst_InvoiceType2_CheckDB_InvoiceType2NotFound"; //// // Mst_InvoiceType2_CheckDB_InvoiceType2NotFound
        public const string Mst_InvoiceType2_CheckDB_InvoiceType2Exist = "ErridnInventory.Mst_InvoiceType2_CheckDB_InvoiceType2Exist"; //// // Mst_InvoiceType2_CheckDB_InvoiceType2Exist
        public const string Mst_InvoiceType2_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_InvoiceType2_CheckDB_FlagActiveNotMatched"; //// // Mst_InvoiceType2_CheckDB_FlagActiveNotMatched

        // Mst_InvoiceType2_Get:
        public const string Mst_InvoiceType2_Get = "ErridnInventory.Mst_InvoiceType2_Get"; //// // Mst_InvoiceType2_Get

        // WAS_Mst_InvoiceType2_Get:
        public const string WAS_Mst_InvoiceType2_Get = "ErridnInventory.WAS_Mst_InvoiceType2_Get"; //// // WAS_Mst_InvoiceType2_Get

        #endregion

        #region // Invoice_license:
        // Invoice_license_CheckDB:
        public const string Invoice_license_CheckDB_licenseNotFound = "ErridnInventory.Invoice_license_CheckDB_licenseNotFound"; //// //Invoice_license_CheckDB_licenseNotFound
        public const string Invoice_license_CheckDB_licenseExist = "ErridnInventory.Invoice_license_CheckDB_licenseExist"; //// //Invoice_license_CheckDB_licenseExist
        public const string Invoice_license_CheckDB_FlagActiveNotMatched = "ErridnInventory.Invoice_license_CheckDB_FlagActiveNotMatched"; //// //Invoice_license_CheckDB_FlagActiveNotMatched

        // Invoice_license_Get:
        public const string Invoice_license_Get = "ErridnInventory.Invoice_license_Get"; //// //Invoice_license_Get

        // WAS_Invoice_license_Get:
        public const string WAS_Invoice_license_Get = "ErridnInventory.WAS_Invoice_license_Get"; //// //WAS_Invoice_license_Get

        // Invoice_license_IncreaseQty:
        public const string Invoice_license_IncreaseQty = "ErridnInventory.Invoice_license_IncreaseQty"; //// //Invoice_license_IncreaseQty
        public const string Invoice_license_IncreaseQtyX_InvalidQty = "ErridnInventory.Invoice_license_IncreaseQtyX_InvalidQty"; //// //Invoice_license_IncreaseQtyX_InvalidQty

        // WAS_Invoice_license_IncreaseQty:
        public const string WAS_Invoice_license_IncreaseQty = "ErridnInventory.WAS_Invoice_license_IncreaseQty"; //// //WAS_Invoice_license_IncreaseQty


        #endregion

        #region // Invoice_licenseCreHist:
        // WAS_Invoice_licenseCreHist_Get:
        public const string WAS_Invoice_licenseCreHist_Get = "ErridnInventory.WAS_Invoice_licenseCreHist_Get"; //// //WAS_Invoice_licenseCreHist_Get

        // Invoice_licenseCreHist_GetAndSave:
        public const string Invoice_licenseCreHist_Get = "ErridnInventory.Invoice_licenseCreHist_Get"; //// //Invoice_licenseCreHist_Get

        // WAS_Invoice_licenseCreHist_GetAndSave:
        public const string WAS_Invoice_licenseCreHist_GetAndSave = "ErridnInventory.WAS_Invoice_licenseCreHist_GetAndSave"; //// //WAS_Invoice_licenseCreHist_GetAndSave

        // Invoice_licenseCreHist_GetAndSave:
        public const string Invoice_licenseCreHist_GetAndSave = "ErridnInventory.Invoice_licenseCreHist_GetAndSave"; //// //Invoice_licenseCreHist_GetAndSave

        #endregion

        #region // iNOS_Mst_BizType:
        // iNOS_Mst_BizType_Get:
        public const string iNOS_Mst_BizType_Get = "ErridnInventory.iNOS_Mst_BizType_Get"; //// //iNOS_Mst_BizType_Get

        // WAS_iNOS_Mst_BizType_Get:
        public const string WAS_iNOS_Mst_BizType_Get = "ErridnInventory.WAS_iNOS_Mst_BizType_Get"; //// //WAS_iNOS_Mst_BizType_Get

        // RptSv_iNOS_Mst_BizType_Get:
        public const string RptSv_iNOS_Mst_BizType_Get = "ErridnInventory.RptSv_iNOS_Mst_BizType_Get"; //// //RptSv_iNOS_Mst_BizType_Get

        // WAS_RptSv_iNOS_Mst_BizType_Get:
        public const string WAS_RptSv_iNOS_Mst_BizType_Get = "ErridnInventory.WAS_RptSv_iNOS_Mst_BizType_Get"; //// //WAS_RptSv_iNOS_Mst_BizType_Get

        #endregion

        #region // iNOS_Mst_BizSize:
        // iNOS_Mst_BizSize_Get:
        public const string iNOS_Mst_BizSize_Get = "ErridnInventory.iNOS_Mst_BizSize_Get"; //// //iNOS_Mst_BizSize_Get

        // WAS_iNOS_Mst_BizSize_Get:
        public const string WAS_iNOS_Mst_BizSize_Get = "ErridnInventory.WAS_iNOS_Mst_BizSize_Get"; //// //WAS_iNOS_Mst_BizSize_Get

        // RptSv_iNOS_Mst_BizSize_Get:
        public const string RptSv_iNOS_Mst_BizSize_Get = "ErridnInventory.RptSv_iNOS_Mst_BizSize_Get"; //// //RptSv_iNOS_Mst_BizSize_Get

        // WAS_RptSv_iNOS_Mst_BizSize_Get:
        public const string WAS_RptSv_iNOS_Mst_BizSize_Get = "ErridnInventory.WAS_RptSv_iNOS_Mst_BizSize_Get"; //// //WAS_RptSv_iNOS_Mst_BizSize_Get

        #endregion

        #region // iNOS_Mst_BizField:
        // iNOS_Mst_BizField_Get:
        public const string iNOS_Mst_BizField_Get = "ErridnInventory.iNOS_Mst_BizField_Get"; //// //iNOS_Mst_BizField_Get

        // WAS_iNOS_Mst_BizField_Get:
        public const string WAS_iNOS_Mst_BizField_Get = "ErridnInventory.WAS_iNOS_Mst_BizField_Get"; //// //WAS_iNOS_Mst_BizField_Get

        // RptSv_iNOS_Mst_BizField_Get:
        public const string RptSv_iNOS_Mst_BizField_Get = "ErridnInventory.RptSv_iNOS_Mst_BizField_Get"; //// //RptSv_iNOS_Mst_BizField_Get

        // WAS_RptSv_iNOS_Mst_BizField_Get:
        public const string WAS_RptSv_iNOS_Mst_BizField_Get = "ErridnInventory.WAS_RptSv_iNOS_Mst_BizField_Get"; //// //WAS_RptSv_iNOS_Mst_BizField_Get

        #endregion

        #region // Report:
        // Rpt_InvFInventoryInFGSum:
        public const string Rpt_InvFInventoryInFGSum = "ErridnInventory.Rpt_InvFInventoryInFGSum"; //// //Rpt_InvFInventoryInFGSum

        // WAS_Rpt_InvFInventoryInFGSum:
        public const string WAS_Rpt_InvFInventoryInFGSum = "ErridnInventory.WAS_Rpt_InvFInventoryInFGSum"; //// //WAS_Rpt_InvFInventoryInFGSum


        // Rpt_InvFInventoryOutFGSum:
        public const string Rpt_InvFInventoryOutFGSum = "ErridnInventory.Rpt_InvFInventoryOutFGSum"; //// //Rpt_InvFInventoryOutFGSum

        // WAS_Rpt_InvFInventoryOutFGSum:
        public const string WAS_Rpt_InvFInventoryOutFGSum = "ErridnInventory.WAS_Rpt_InvFInventoryOutFGSum"; //// //WAS_Rpt_InvFInventoryOutFGSum

        // WAS_Inv_InventoryBalance_Get:
        public const string WAS_Inv_InventoryBalance_Get = "ErridnInventory.WAS_Inv_InventoryBalance_Get"; //// //WAS_Inv_InventoryBalance_Get

        // WAS_Rpt_InvInventoryBalanceMonth:
        public const string WAS_Rpt_InvInventoryBalanceMonth = "ErridnInventory.WAS_Rpt_InvInventoryBalanceMonth"; //// //WAS_Rpt_InvInventoryBalanceMonth
        #endregion 

        #region // Mst_Organ:
        // Mst_Organ_CheckDB:
        public const string Mst_Organ_CheckDB_OrganNotFound = "ErridnInventory.Mst_Organ_CheckDB_OrganNotFound"; //// //Mst_Organ_CheckDB_OrganNotFound
        public const string Mst_Organ_CheckDB_OrganExist = "ErridnInventory.Mst_Organ_CheckDB_OrganExist"; //// //Mst_Organ_CheckDB_OrganExist
        public const string Mst_Organ_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Organ_CheckDB_FlagActiveNotMatched"; //// //Mst_Organ_CheckDB_FlagActiveNotMatched

        // Mst_Organ_Get:
        public const string Mst_Organ_Get = "ErridnInventory.Mst_Organ_Get"; //// //Mst_Organ_Get

        // WAS_Mst_Organ_Get:
        public const string WAS_Mst_Organ_Get = "ErridnInventory.WAS_Mst_Organ_Get"; //// //WAS_Mst_Organ_Get

        // Mst_Organ_GetForCurrentUser:
        public const string Mst_Organ_GetForCurrentUser = "ErridnInventory.Mst_Organ_GetForCurrentUser"; //// //Mst_Organ_GetForCurrentUser

        // WAS_Mst_Organ_GetForCurrentUser:
        public const string WAS_Mst_Organ_GetForCurrentUser = "ErridnInventory.WAS_Mst_Organ_GetForCurrentUser"; //// //WAS_Mst_Organ_GetForCurrentUser

        // WAS_Mst_Organ_Create:
        public const string WAS_Mst_Organ_Create = "ErridnInventory.WAS_Mst_Organ_Create"; //// //WAS_Mst_Organ_Create

        // Mst_Organ_Create:
        public const string Mst_Organ_Create = "ErridnInventory.Mst_Organ_Create"; //// //Mst_Organ_Create
        public const string Mst_Organ_Create_InvalidOrganCode = "ErridnInventory.Mst_Organ_Create_InvalidOrganCode"; //// //Mst_Organ_Create_InvalidOrganCode
        public const string Mst_Organ_Create_InvalidOrganName = "ErridnInventory.Mst_Organ_Create_InvalidOrganName"; //// //Mst_Organ_Create_InvalidOrganName

        // Mst_Organ_Update:
        public const string Mst_Organ_Update = "ErridnInventory.Mst_Organ_Update"; //// //Mst_Organ_Update

        // WAS_Mst_Organ_Update:
        public const string WAS_Mst_Organ_Update = "ErridnInventory.WAS_Mst_Organ_Update"; //// //WAS_Mst_Organ_Update

        // Mst_Organ_UpdateX:
        public const string Mst_Organ_UpdateX = "ErridnInventory.Mst_Organ_UpdateX"; //// //Mst_Organ_UpdateX
        public const string Mst_Organ_UpdateX_InvalidOrganName = "ErridnInventory.Mst_Organ_UpdateX_InvalidOrganName"; //// //Mst_Organ_UpdateX_InvalidOrganName

        // WAS_Mst_Organ_Delete:
        public const string WAS_Mst_Organ_Delete = "ErridnInventory.WAS_Mst_Organ_Delete"; //// //WAS_Mst_Organ_Delete

        // Mst_Organ_Delete:
        public const string Mst_Organ_Delete = "ErridnInventory.Mst_Organ_Delete"; //// //Mst_Organ_Delete
		#endregion

		#region // Mst_Org:
		// Mst_Org_CheckDB:
		public const string Mst_Org_CheckDB_OrgNotFound = "ErridnInventory.Mst_Org_CheckDB_OrgNotFound"; //// //Mst_Org_CheckDB_OrgNotFound
		public const string Mst_Org_CheckDB_OrgExist = "ErridnInventory.Mst_Org_CheckDB_OrgExist"; //// //Mst_Org_CheckDB_OrgExist
		public const string Mst_Org_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Org_CheckDB_FlagActiveNotMatched"; //// //Mst_Org_CheckDB_FlagActiveNotMatched
        // Mst_Org_Create
        public const string Mst_Org_Create = "ErridnInventory.Mst_Org_Create";//// //Mst_Org_Create
        public const string Mst_Org_Create_InvalidOrgID = "ErridnInventory.Mst_Org_Create_InvalidOrgID";//// // Mst_Org_Create_InvalidOrgID
        #endregion

        #region // Mst_Department:
        // Mst_Department_CheckDB:
        public const string Mst_Department_CheckDB_DepartmentNotFound = "ErridnInventory.Mst_Department_CheckDB_DepartmentNotFound"; //// //Mst_Department_CheckDB_DepartmentNotFound
        public const string Mst_Department_CheckDB_DepartmentExist = "ErridnInventory.Mst_Department_CheckDB_DepartmentExist"; //// //Mst_Department_CheckDB_DepartmentExist
        public const string Mst_Department_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Department_CheckDB_FlagActiveNotMatched"; //// //Mst_Department_CheckDB_FlagActiveNotMatched

        // Mst_Department_Get:
        public const string Mst_Department_Get = "ErridnInventory.Mst_Department_Get"; //// //Mst_Department_Get

        // WAS_Mst_Department_Get:
        public const string WAS_Mst_Department_Get = "ErridnInventory.WAS_Mst_Department_Get"; //// //WAS_Mst_Department_Get

        // WAS_Mst_Department_Create:
        public const string WAS_Mst_Department_Create = "ErridnInventory.WAS_Mst_Department_Create"; //// //WAS_Mst_Department_Create

        // Mst_Department_Create:
        public const string Mst_Department_Create = "ErridnInventory.Mst_Department_Create"; //// //Mst_Department_Create
        public const string Mst_Department_Create_InvalidDepartmentCode = "ErridnInventory.Mst_Department_Create_InvalidDepartmentCode"; //// //Mst_Department_Create_InvalidDepartmentCode
        public const string Mst_Department_Create_InvalidDepartmentName = "ErridnInventory.Mst_Department_Create_InvalidDepartmentName"; //// //Mst_Department_Create_InvalidDepartmentName
        public const string Mst_Department_Create_InvalidOrganCode = "ErridnInventory.Mst_Department_Create_InvalidOrganCode"; //// //Mst_Department_Create_InvalidOrganCode

        // WAS_Mst_Department_Update:
        public const string WAS_Mst_Department_Update = "ErridnInventory.WAS_Mst_Department_Update"; //// //WAS_Mst_Department_Update

        // Mst_Department_Update:
        public const string Mst_Department_Update = "ErridnInventory.Mst_Department_Update"; //// //Mst_Department_Update

        // WAS_Mst_Department_Delete:
        public const string WAS_Mst_Department_Delete = "ErridnInventory.WAS_Mst_Department_Delete"; //// //WAS_Mst_Department_Delete

        // Mst_Department_Delete:
        public const string Mst_Department_Delete = "ErridnInventory.Mst_Department_Delete"; //// //Mst_Department_Delete

        // Mst_Department_UpdateX:
        public const string Mst_Department_UpdateX = "ErridnInventory.Mst_Department_UpdateX"; //// //Mst_Department_UpdateX
        public const string Mst_Department_UpdateX_InvalidDepartmentName = "ErridnInventory.Mst_Department_UpdateX_InvalidDepartmentName"; //// //Mst_Department_UpdateX_InvalidDepartmentName
        public const string Mst_Department_UpdateX_InvalidOrganCode = "ErridnInventory.Mst_Department_UpdateX_InvalidOrganCode"; //// //Mst_Department_UpdateX_InvalidOrganCode
        #endregion

        #region // Mst_PaymentPartnerType:
        // Mst_PaymentPartnerType_CheckDB:
        public const string Mst_PaymentPartnerType_CheckDB_PaymentPartnerTypeNotFound = "ErridnInventory.Mst_PaymentPartnerType_CheckDB_PaymentPartnerTypeNotFound"; //// //Mst_PaymentPartnerType_CheckDB_PaymentPartnerTypeNotFound
        public const string Mst_PaymentPartnerType_CheckDB_PaymentPartnerTypeExist = "ErridnInventory.Mst_PaymentPartnerType_CheckDB_PaymentPartnerTypeExist"; //// //Mst_PaymentPartnerType_CheckDB_PaymentPartnerTypeExist
        public const string Mst_PaymentPartnerType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_PaymentPartnerType_CheckDB_FlagActiveNotMatched"; //// //Mst_PaymentPartnerType_CheckDB_FlagActiveNotMatched

        // Mst_PaymentPartnerType_Get:
        public const string Mst_PaymentPartnerType_Get = "ErridnInventory.Mst_PaymentPartnerType_Get"; //// //Mst_PaymentPartnerType_Get

        // WAS_Mst_PaymentPartnerType_Get:
        public const string WAS_Mst_PaymentPartnerType_Get = "ErridnInventory.WAS_Mst_PaymentPartnerType_Get"; //// //WAS_Mst_PaymentPartnerType_Get

        // WAS_Mst_PaymentPartnerType_Create:
        public const string WAS_Mst_PaymentPartnerType_Create = "ErridnInventory.WAS_Mst_PaymentPartnerType_Create"; //// //WAS_Mst_PaymentPartnerType_Create

        // Mst_PaymentPartnerType_Create:
        public const string Mst_PaymentPartnerType_Create = "ErridnInventory.Mst_PaymentPartnerType_Create"; //// //Mst_PaymentPartnerType_Create

        //// Mst_PaymentPartnerType_CreateX:
        public const string Mst_PaymentPartnerType_CreateX = "ErridnInventory.Mst_PaymentPartnerType_CreateX"; //// //Mst_PaymentPartnerType_CreateX
        public const string Mst_PaymentPartnerType_CreateX_InvalidPaymentPartnerType = "ErridnInventory.Mst_PaymentPartnerType_CreateX_InvalidPaymentPartnerType"; //// //Mst_PaymentPartnerType_CreateX_InvalidPaymentPartnerType
        public const string Mst_PaymentPartnerType_CreateX_InvalidPaymentPartnerTypeName = "ErridnInventory.Mst_PaymentPartnerType_CreateX_InvalidPaymentPartnerTypeName"; //// //Mst_PaymentPartnerType_CreateX_InvalidPaymentPartnerTypeName

        // WAS_Mst_PaymentPartnerType_Update:
        public const string WAS_Mst_PaymentPartnerType_Update = "ErridnInventory.WAS_Mst_PaymentPartnerType_Update"; //// //WAS_Mst_PaymentPartnerType_Update

        // Mst_PaymentPartnerType_Update:
        public const string Mst_PaymentPartnerType_Update = "ErridnInventory.Mst_PaymentPartnerType_Update"; //// //Mst_PaymentPartnerType_Update

        //// Mst_PaymentPartnerType_UpdateX:
        public const string Mst_PaymentPartnerType_UpdateX = "ErridnInventory.Mst_PaymentPartnerType_UpdateX"; //// //Mst_PaymentPartnerType_UpdateX
        public const string Mst_PaymentPartnerType_UpdateX_InvalidPaymentPartnerTypeName = "ErridnInventory.Mst_PaymentPartnerType_UpdateX_InvalidPaymentPartnerTypeName"; //// //Mst_PaymentPartnerType_UpdateX_InvalidPaymentPartnerTypeName

        // WAS_Mst_PaymentPartnerType_Delete:
        public const string WAS_Mst_PaymentPartnerType_Delete = "ErridnInventory.WAS_Mst_PaymentPartnerType_Delete"; //// //WAS_Mst_PaymentPartnerType_Delete

        // Mst_PaymentPartnerType_Delete:
        public const string Mst_PaymentPartnerType_Delete = "ErridnInventory.Mst_PaymentPartnerType_Delete"; //// //Mst_PaymentPartnerType_Delete

        //// Mst_PaymentPartnerType_DeleteX:
        public const string Mst_PaymentPartnerType_DeleteX = "ErridnInventory.Mst_PaymentPartnerType_DeleteX"; //// //Mst_PaymentPartnerType_DeleteX
        #endregion

        #region // Mst_PaymentPartner:
        // Mst_PaymentPartnerType_CheckDB:
        public const string Mst_PaymentPartner_CheckDB_PaymentPartnerNotFound = "ErridnInventory.Mst_PaymentPartner_CheckDB_PaymentPartnerNotFound"; //// //Mst_PaymentPartner_CheckDB_PaymentPartnerNotFound
        public const string Mst_PaymentPartner_CheckDB_PaymentPartnerExist = "ErridnInventory.Mst_PaymentPartner_CheckDB_PaymentPartnerExist"; //// //Mst_PaymentPartner_CheckDB_PaymentPartnerExist
        public const string Mst_PaymentPartner_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_PaymentPartner_CheckDB_FlagActiveNotMatched"; //// //Mst_PaymentPartner_CheckDB_FlagActiveNotMatched

        // WAS_Mst_PaymentPartner_Get:
        public const string WAS_Mst_PaymentPartner_Get = "ErridnInventory.WAS_Mst_PaymentPartner_Get"; //// //WAS_Mst_PaymentPartner_Get

        // Mst_PaymentPartner_Get:
        public const string Mst_PaymentPartner_Get = "ErridnInventory.Mst_PaymentPartner_Get"; //// //Mst_PaymentPartner_Get

        // WAS_Mst_PaymentPartner_Create:
        public const string WAS_Mst_PaymentPartner_Create = "ErridnInventory.WAS_Mst_PaymentPartner_Create"; //// //WAS_Mst_PaymentPartner_Create

        // Mst_PaymentPartner_Create:
        public const string Mst_PaymentPartner_Create = "ErridnInventory.Mst_PaymentPartner_Create"; //// //Mst_PaymentPartner_Create

        //// Mst_PaymentPartner_CreateX:
        public const string Mst_PaymentPartner_CreateX = "ErridnInventory.Mst_PaymentPartner_CreateX"; //// //Mst_PaymentPartner_CreateX
        public const string Mst_PaymentPartner_CreateX_InvalidPaymentPartnerCode = "ErridnInventory.Mst_PaymentPartner_CreateX_InvalidPaymentPartnerCode"; //// //Mst_PaymentPartner_CreateX_InvalidPaymentPartnerCode
        public const string Mst_PaymentPartner_CreateX_InvalidPaymentPartnerName = "ErridnInventory.Mst_PaymentPartner_CreateX_InvalidPaymentPartnerName"; //// //Mst_PaymentPartner_CreateX_InvalidPaymentPartnerName

        // WAS_Mst_PaymentPartner_Update:
        public const string WAS_Mst_PaymentPartner_Update = "ErridnInventory.WAS_Mst_PaymentPartner_Update"; //// //WAS_Mst_PaymentPartner_Update

        // Mst_PaymentPartner_Update:
        public const string Mst_PaymentPartner_Update = "ErridnInventory.Mst_PaymentPartner_Update"; //// //Mst_PaymentPartner_Update

        //// Mst_PaymentPartner_UpdateX:
        public const string Mst_PaymentPartner_UpdateX = "ErridnInventory.Mst_PaymentPartner_UpdateX"; //// //Mst_PaymentPartner_UpdateX
        public const string Mst_PaymentPartner_UpdateX_InvalidPaymentPartnerName = "ErridnInventory.Mst_PaymentPartner_UpdateX_InvalidPaymentPartnerName"; //// //Mst_PaymentPartner_UpdateX_InvalidPaymentPartnerName

        // WAS_Mst_PaymentPartner_Delete:
        public const string WAS_Mst_PaymentPartner_Delete = "ErridnInventory.WAS_Mst_PaymentPartner_Delete"; //// //WAS_Mst_PaymentPartner_Delete

        // Mst_PaymentPartner_Delete:
        public const string Mst_PaymentPartner_Delete = "ErridnInventory.Mst_PaymentPartner_Delete"; //// //Mst_PaymentPartner_Delete

        //// Mst_PaymentPartner_DeleteX:
        public const string Mst_PaymentPartner_DeleteX = "ErridnInventory.Mst_PaymentPartner_DeleteX"; //// //Mst_PaymentPartner_DeleteX
        #endregion

        #region // Mst_Representative:
        // Mst_Representative_CheckDB:
        public const string Mst_Representative_CheckDB_RepresentativeNotFound = "ErridnInventory.Mst_Representative_CheckDB_RepresentativeNotFound"; //// //Mst_Representative_CheckDB_RepresentativeNotFound
        public const string Mst_Representative_CheckDB_RepresentativeExist = "ErridnInventory.Mst_Representative_CheckDB_RepresentativeExist"; //// //Mst_Representative_CheckDB_RepresentativeExist
        public const string Mst_Representative_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Representative_CheckDB_FlagActiveNotMatched"; //// //Mst_Representative_CheckDB_FlagActiveNotMatched

        // WAS_Mst_Representative_Get:
        public const string WAS_Mst_Representative_Get = "ErridnInventory.WAS_Mst_Representative_Get"; //// //WAS_Mst_Representative_Get

        // Mst_Representative_Get:
        public const string Mst_Representative_Get = "ErridnInventory.Mst_Representative_Get"; //// //Mst_Representative_Get

        // Mst_Representative_Create:
        public const string Mst_Representative_Create = "ErridnInventory.Mst_Representative_Create"; //// //Mst_Representative_Create

        // WAS_Mst_Representative_Create:
        public const string WAS_Mst_Representative_Create = "ErridnInventory.WAS_Mst_Representative_Create"; //// //WAS_Mst_Representative_Create

        // Mst_Representative_CreateX:
        public const string Mst_Representative_CreateX = "ErridnInventory.Mst_Representative_CreateX"; //// //Mst_Representative_CreateX
        public const string Mst_Representative_CreateX_InvalidRepresentativeCode = "ErridnInventory.Mst_Representative_CreateX_InvalidRepresentativeCode"; //// //Mst_Representative_CreateX_InvalidRepresentativeCode
        public const string Mst_Representative_CreateX_InvalidRepresentativeName = "ErridnInventory.Mst_Representative_CreateX_InvalidRepresentativeName"; //// //Mst_Representative_CreateX_InvalidRepresentativeName
        public const string Mst_Representative_CreateX_InvalidOrganCode = "ErridnInventory.Mst_Representative_CreateX_InvalidOrganCode"; //// //Mst_Representative_CreateX_InvalidOrganCode
        public const string Mst_Representative_CreateX_IDCardDateIssueAfterIDCardDateExpiry = "ErridnInventory.Mst_Representative_CreateX_IDCardDateIssueAfterIDCardDateExpiry"; //// //Mst_Representative_CreateX_IDCardDateIssueAfterIDCardDateExpiry
        public const string Mst_Representative_CreateX_ProxyDateIssueAfterProxyDateExpiry = "ErridnInventory.Mst_Representative_CreateX_ProxyDateIssueAfterProxyDateExpiry"; //// //Mst_Representative_CreateX_ProxyDateIssueAfterProxyDateExpiry
        public const string Mst_Representative_CreateX_InputRepresentativeFileUploadTblNotFound = "ErridnInventory.Mst_Representative_CreateX_InputRepresentativeFileUploadTblNotFound"; //// //Mst_Representative_CreateX_InputRepresentativeFileUploadTblNotFound
        public const string Mst_Representative_CreateX_InputRepresentativeFileUploadTblInvalid = "ErridnInventory.Mst_Representative_CreateX_InputRepresentativeFileUploadTblInvalid"; //// //Mst_Representative_CreateX_InputRepresentativeFileUploadTblInvalid

        // Mst_Representative_Update:
        public const string Mst_Representative_Update = "ErridnInventory.Mst_Representative_Update"; //// //Mst_Representative_Update

        // WAS_Mst_Representative_Update:
        public const string WAS_Mst_Representative_Update = "ErridnInventory.WAS_Mst_Representative_Update"; //// //WAS_Mst_Representative_Update

        // Mst_Representative_UpdateX:
        public const string Mst_Representative_UpdateX = "ErridnInventory.Mst_Representative_UpdateX"; //// //Mst_Representative_UpdateX
        public const string Mst_Representative_UpdateX_InvalidRepresentativeCode = "ErridnInventory.Mst_Representative_UpdateX_InvalidRepresentativeCode"; //// //Mst_Representative_UpdateX_InvalidRepresentativeCode
        public const string Mst_Representative_UpdateX_InvalidRepresentativeName = "ErridnInventory.Mst_Representative_UpdateX_InvalidRepresentativeName"; //// //Mst_Representative_UpdateX_InvalidRepresentativeName
        public const string Mst_Representative_UpdateX_IDCardDateIssueAfterIDCardDateExpiry = "ErridnInventory.Mst_Representative_UpdateX_IDCardDateIssueAfterIDCardDateExpiry"; //// //Mst_Representative_UpdateX_IDCardDateIssueAfterIDCardDateExpiry
        public const string Mst_Representative_UpdateX_ProxyDateIssueAfterProxyDateExpiry = "ErridnInventory.Mst_Representative_UpdateX_ProxyDateIssueAfterProxyDateExpiry"; //// //Mst_Representative_UpdateX_ProxyDateIssueAfterProxyDateExpiry
        public const string Mst_Representative_UpdateX_InputRepresentativeFileUploadTblNotFound = "ErridnInventory.Mst_Representative_UpdateX_InputRepresentativeFileUploadTblNotFound"; //// //Mst_Representative_UpdateX_InputRepresentativeFileUploadTblNotFound
        public const string Mst_Representative_UpdateX_InputRepresentativeFileUploadTblInvalid = "ErridnInventory.Mst_Representative_UpdateX_InputRepresentativeFileUploadTblInvalid"; //// //Mst_Representative_UpdateX_InputRepresentativeFileUploadTblInvalid

        // Mst_Representative_Delete:
        public const string Mst_Representative_Delete = "ErridnInventory.Mst_Representative_Delete"; //// //Mst_Representative_Delete

        // WAS_Mst_Representative_Delete:
        public const string WAS_Mst_Representative_Delete = "ErridnInventory.WAS_Mst_Representative_Delete"; //// //WAS_Mst_Representative_Delete
        #endregion

        #region // Mst_RepresentativeFileUpload:
        // Mst_RepresentativeFileUpload_CheckDB:
        public const string Mst_RepresentativeFileUpload_CheckDB_AutoIdNotFound = "ErridnInventory.Mst_RepresentativeFileUpload_CheckDB_AutoIdNotFound"; //// //Mst_RepresentativeFileUpload_CheckDB_AutoIdNotFound
        public const string Mst_RepresentativeFileUpload_CheckDB_AutoIdExist = "ErridnInventory.Mst_RepresentativeFileUpload_CheckDB_AutoIdExist"; //// //Mst_RepresentativeFileUpload_CheckDB_AutoIdExist
        public const string Mst_RepresentativeFileUpload_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_RepresentativeFileUpload_CheckDB_FlagActiveNotMatched"; //// //Mst_RepresentativeFileUpload_CheckDB_FlagActiveNotMatched

        // WAS_Mst_RepresentativeFileUpload_Get:
        public const string WAS_Mst_RepresentativeFileUpload_Get = "ErridnInventory.WAS_Mst_RepresentativeFileUpload_Get"; //// //WAS_Mst_RepresentativeFileUpload_Get

        // Mst_RepresentativeFileUpload_Get:
        public const string Mst_RepresentativeFileUpload_Get = "ErridnInventory.Mst_RepresentativeFileUpload_Get"; //// //Mst_RepresentativeFileUpload_Get

        // WAS_Mst_RepresentativeFileUpload_Create:
        public const string WAS_Mst_RepresentativeFileUpload_Create = "ErridnInventory.WAS_Mst_RepresentativeFileUpload_Create"; //// //WAS_Mst_RepresentativeFileUpload_Create

        // Mst_RepresentativeFileUpload_Create:
        public const string Mst_RepresentativeFileUpload_Create = "ErridnInventory.Mst_RepresentativeFileUpload_Create"; //// //Mst_RepresentativeFileUpload_Create
        public const string Mst_RepresentativeFileUpload_Create_InvalidFilePath = "ErridnInventory.Mst_RepresentativeFileUpload_Create_InvalidFilePath"; //// //Mst_RepresentativeFileUpload_Create_InvalidFilePath

        // Mst_RepresentativeFileUpload_Update:
        public const string Mst_RepresentativeFileUpload_Update = "ErridnInventory.Mst_RepresentativeFileUpload_Update"; //// //Mst_RepresentativeFileUpload_Update

        // WAS_Mst_RepresentativeFileUpload_Update:
        public const string WAS_Mst_RepresentativeFileUpload_Update = "ErridnInventory.WAS_Mst_RepresentativeFileUpload_Update"; //// //WAS_Mst_RepresentativeFileUpload_Update

        // Mst_RepresentativeFileUpload_UpdateX:
        public const string Mst_RepresentativeFileUpload_UpdateX = "ErridnInventory.Mst_RepresentativeFileUpload_UpdateX"; //// //Mst_RepresentativeFileUpload_UpdateX
        public const string Mst_RepresentativeFileUpload_UpdateX_InvalidFilePath = "ErridnInventory.Mst_RepresentativeFileUpload_UpdateX_InvalidFilePath"; //// //Mst_RepresentativeFileUpload_UpdateX_InvalidFilePath

        // Mst_RepresentativeFileUpload_Delete:
        public const string Mst_RepresentativeFileUpload_Delete = "ErridnInventory.Mst_RepresentativeFileUpload_Delete"; //// //Mst_RepresentativeFileUpload_Delete

        // WAS_Mst_RepresentativeFileUpload_Delete:
        public const string WAS_Mst_RepresentativeFileUpload_Delete = "ErridnInventory.WAS_Mst_RepresentativeFileUpload_Delete"; //// //WAS_Mst_RepresentativeFileUpload_Delete
        #endregion

        #region // Report:
        // OS3A_InvoiceInvoice_Get:
        public const string OS3A_TVAN_InvoiceInvoice_Get = "ErridnInventory.OS3A_TVAN_InvoiceInvoice_Get"; //// //OS3A_TVAN_InvoiceInvoice_Get
        #endregion

        #region // Report:
        // Rpt_InvoiceInvoice_ResultUsed:
        public const string Rpt_InvoiceInvoice_ResultUsed = "ErridnInventory.Rpt_InvoiceInvoice_ResultUsed"; //// //Rpt_InvoiceInvoice_ResultUsed

        // WAS_Rpt_InvoiceInvoice_ResultUsed:
        public const string WAS_Rpt_InvoiceInvoice_ResultUsed = "ErridnInventory.WAS_Rpt_InvoiceInvoice_ResultUsed"; //// //WAS_Rpt_InvoiceInvoice_ResultUsed

        // Rpt_Budget_ProjectPlan:
        public const string Rpt_Budget_ProjectPlan = "ErridnInventory.Rpt_Budget_ProjectPlan"; //// //Rpt_Budget_ProjectPlan

        // WAS_Rpt_Budget_ProjectPlan:
        public const string WAS_Rpt_Budget_ProjectPlan = "ErridnInventory.WAS_Rpt_Budget_ProjectPlan"; //// //WAS_Rpt_Budget_ProjectPlan

        // Rpt_OrganDebt:
        public const string Rpt_OrganDebt = "ErridnInventory.Rpt_OrganDebt"; //// //Rpt_OrganDebt

        // WAS_Rpt_OrganDebt:
        public const string WAS_Rpt_OrganDebt = "ErridnInventory.WAS_Rpt_OrganDebt"; //// //WAS_Rpt_OrganDebt

        // _Rpt_KUNN_KUNNDtl_01:
        public const string _Rpt_KUNN_KUNNDtl_01 = "ErridnInventory._Rpt_KUNN_KUNNDtl_01"; //// //_Rpt_KUNN_KUNNDtl_01

        // WAS__Rpt_KUNN_KUNNDtl_01:
        public const string WAS_Rpt_KUNN_KUNNDtl_01 = "ErridnInventory.WAS_Rpt_KUNN_KUNNDtl_01"; //// //WAS_Rpt_KUNN_KUNNDtl_01

        // Rpt_KUNN_CalcValLaiPeriod:
        public const string Rpt_KUNN_CalcValLaiPeriod = "ErridnInventory.Rpt_KUNN_CalcValLaiPeriod"; //// //Rpt_KUNN_CalcValLaiPeriod

        // WAS_Rpt_KUNN_CalcValLaiPeriod:
        public const string WAS_Rpt_KUNN_CalcValLaiPeriod = "ErridnInventory.WAS_Rpt_KUNN_CalcValLaiPeriod"; //// //WAS_Rpt_KUNN_CalcValLaiPeriod
        #endregion

        #region // Seq_Common:
        // Seq_Common:
        public const string Seq_Common = "Erridn.Skycic.Inventory.Seq_Common"; //// //Seq_Common

        // WAS_Seq_Common_Get:
        public const string WAS_Seq_Common_Get = "Erridn.Skycic.Inventory.WAS_Seq_Common_Get"; //// //WAS_Seq_Common_Get

        // WAS_Seq_FormNo_Get:
        public const string WAS_Seq_FormNo_Get = "Erridn.Skycic.Inventory.WAS_Seq_FormNo_Get"; //// //WAS_Seq_FormNo_Get


        // Seq_TCTTranNo_Get:
        public const string Seq_TCTTranNo_Get = "Erridn.Skycic.Inventory.Seq_TCTTranNo_Get"; //// //Seq_TCTTranNo_Get

        // Seq_InvoiceCode_Get:
        public const string Seq_InvoiceCode_Get = "Erridn.Skycic.Inventory.Seq_InvoiceCode_Get"; //// //Seq_InvoiceCode_Get

        // WAS_Seq_TCTTranNo_Get:
        public const string WAS_Seq_TCTTranNo_Get = "Erridn.Skycic.Inventory.WAS_Seq_TCTTranNo_Get"; //// //WAS_Seq_TCTTranNo_Get

        // WAS_Seq_InvoiceCode_Get:
        public const string WAS_Seq_InvoiceCode_Get = "Erridn.Skycic.Inventory.WAS_Seq_InvoiceCode_Get"; //// //WAS_Seq_InvoiceCode_Get

        // Seq_TVANTranNo_Get:
        public const string Seq_TVANTranNo_Get = "Erridn.Skycic.Inventory.Seq_TVANTranNo_Get"; //// //Seq_TVANTranNo_Get

        // WAS_Seq_TVANTranNo_Get:
        public const string WAS_Seq_TVANTranNo_Get = "Erridn.Skycic.Inventory.WAS_Seq_TVANTranNo_Get"; //// //WAS_Seq_TVANTranNo_Get

        // WAS_Seq_GenEngine_Get:
        public const string WAS_Seq_GenEngine_Get = "Erridn.Skycic.Inventory.WAS_Seq_GenEngine_Get"; //// //WAS_Seq_GenEngine_Get

        //WAS_Seq_GenObjCode_Get
        public const string WAS_Seq_GenObjCode_Get = "Erridn.Skycic.Inventory.WAS_Seq_GenObjCode_Get";

        //RptSv_Seq_MST_Get
        public const string RptSv_Seq_MST_Get = "Erridn.Skycic.Inventory.RptSv_Seq_MST_Get";

        //WAS_RptSv_Seq_MST_Get
        public const string WAS_RptSv_Seq_MST_Get = "Erridn.Skycic.Inventory.WAS_RptSv_Seq_MST_Get";
        #endregion

        #region // TCT:
        // Seq_Common:
        public const string TCT_Err = "Erridn.Skycic.Inventory.TCT_Err"; //// //TCT_Err

        // TCT_NhanHSoThue_BaseWCF:
        public const string TCT_NhanHSoThue_BaseWCF = "ErridnInventory.TCT_NhanHSoThue_BaseWCF"; //// //TCT_NhanHSoThue_BaseWCF

        // WAS_TCT_NhanHSoThue_BaseWCF:
        public const string WAS_TCT_NhanHSoThue_BaseWCF = "ErridnInventory.WAS_TCT_NhanHSoThue_BaseWCF"; //// //WAS_TCT_NhanHSoThue_BaseWCF

        // TCT_TraKQuaGDich_BaseHTTPReq:
        public const string TCT_TraKQuaGDich_BaseHTTPReq = "ErridnInventory.TCT_TraKQuaGDich_BaseHTTPReq"; //// //TCT_TraKQuaGDich_BaseHTTPReq

        // WAS_TCT_TraKQuaGDich_BaseHTTPReq:
        public const string WAS_TCT_TraKQuaGDich_BaseHTTPReq = "ErridnInventory.WAS_TCT_TraKQuaGDich_BaseHTTPReq"; //// //WAS_TCT_TraKQuaGDich_BaseHTTPReq

        // TCT_TraTTinMaSoThue_BaseHTTPReq:
        public const string TCT_TraTTinMaSoThue_BaseHTTPReq = "ErridnInventory.TCT_TraTTinMaSoThue_BaseHTTPReq"; //// //TCT_TraTTinMaSoThue_BaseHTTPReq

        // WAS_TCT_TraTTinMaSoThue_BaseHTTPReq:
        public const string WAS_TCT_TraTTinMaSoThue_BaseHTTPReq = "ErridnInventory.WAS_TCT_TraTTinMaSoThue_BaseHTTPReq"; //// //WAS_TCT_TraTTinMaSoThue_BaseHTTPReq

        // TCT_TraTThaiMaSoThue_BaseHTTPReq:
        public const string TCT_TraTThaiMaSoThue_BaseHTTPReq = "ErridnInventory.TCT_TraTThaiMaSoThue_BaseHTTPReq"; //// //TCT_TraTThaiMaSoThue_BaseHTTPReq

        // WAS_TCT_TraTThaiMaSoThue_BaseHTTPReq:
        public const string WAS_TCT_TraTThaiMaSoThue_BaseHTTPReq = "ErridnInventory.WAS_TCT_TraTThaiMaSoThue_BaseHTTPReq"; //// //WAS_TCT_TraTThaiMaSoThue_BaseHTTPReq


        // WAS_TCT_NhanHSoXML_BaseWCF:
        public const string WAS_TCT_NhanHSoXML_BaseWCF = "ErridnInventory.WAS_TCT_NhanHSoXML_BaseWCF"; //// //WAS_TCT_NhanHSoXML_BaseWCF

        // TCT_NhanHSoXML_BaseWCF:
        public const string TCT_NhanHSoXML_BaseWCF = "ErridnInventory.TCT_NhanHSoXML_BaseWCF"; //// //TCT_NhanHSoXML_BaseWCF

        // TCT_NhanPLucHSoThue_BaseWCF:
        public const string TCT_NhanPLucHSoThue_BaseWCF = "ErridnInventory.TCT_NhanPLucHSoThue_BaseWCF"; //// //TCT_NhanPLucHSoThue_BaseWCF

        // WAS_TCT_NhanPLucHSoThue_BaseWCF:
        public const string WAS_TCT_NhanPLucHSoThue_BaseWCF = "ErridnInventory.WAS_TCT_NhanPLucHSoThue_BaseWCF"; //// //WAS_TCT_NhanPLucHSoThue_BaseWCF


        // WAS_TCT_NhanHSoXML_BaseHTTPReq:
        public const string WAS_TCT_NhanHSoXML_BaseHTTPReq = "ErridnInventory.WAS_TCT_NhanHSoXML_BaseHTTPReq"; //// //WAS_TCT_NhanHSoXML_BaseHTTPReq

        // TCT_NhanHSoXML_BaseHTTPReq:
        public const string TCT_NhanHSoXML_BaseHTTPReq = "ErridnInventory.TCT_NhanHSoXML_BaseHTTPReq"; //// //TCT_NhanHSoXML_BaseHTTPReq


        // TCT_TraKQuaPhienGDich_BaseHTTPReq:
        public const string TCT_TraKQuaPhienGDich_BaseHTTPReq = "ErridnInventory.TCT_TraKQuaPhienGDich_BaseHTTPReq"; //// //TCT_TraKQuaPhienGDich_BaseHTTPReq

        // WAS_TCT_TraKQuaPhienGDich_BaseHTTPReq:
        public const string WAS_TCT_TraKQuaPhienGDich_BaseHTTPReq = "ErridnInventory.WAS_TCT_TraKQuaPhienGDich_BaseHTTPReq"; //// //WAS_TCT_TraKQuaPhienGDich_BaseHTTPReq
        #endregion

        #region // Email_BatchSendEmail:
        // Email_BatchSendEmail_CheckDB:
        public const string Email_BatchSendEmail_CheckDB_BatchSendEmailNotFound = "ErridnInventory.Email_BatchSendEmail_CheckDB_BatchSendEmailNotFound"; //// //Email_BatchSendEmail_CheckDB_BatchSendEmailNotFound
        public const string Email_BatchSendEmail_CheckDB_BatchSendEmailExist = "ErridnInventory.Email_BatchSendEmail_CheckDB_BatchSendEmailExist"; //// //Email_BatchSendEmail_CheckDB_BatchSendEmailExist
        public const string Email_BatchSendEmail_CheckDB_StatusNotMatched = "ErridnInventory.Email_BatchSendEmail_CheckDB_StatusNotMatched"; //// //Email_BatchSendEmail_CheckDB_StatusNotMatched

        // Email_BatchSendEmail_Send:
        public const string Email_BatchSendEmail_Send = "ErridnInventory.Email_BatchSendEmail_Send"; //// //Email_BatchSendEmail_Send

        // Email_BatchSendEmail_SaveAndSend:
        public const string Email_BatchSendEmail_SaveAndSend = "ErridnInventory.Email_BatchSendEmail_SaveAndSend"; //// //Email_BatchSendEmail_SaveAndSend

        // Email_BatchSendEmail_MstSv_Inos_User_Send:
        public const string Email_BatchSendEmail_MstSv_Inos_User_Send = "ErridnInventory.Email_BatchSendEmail_MstSv_Inos_User_Send"; //// //Email_BatchSendEmail_MstSv_Inos_User_Send

		// Email_BatchSendEmail_Sys_User_Send:
		public const string Email_BatchSendEmail_Sys_User_Send = "ErridnInventory.Email_BatchSendEmail_Sys_User_Send"; //// //Email_BatchSendEmail_Sys_User_Send

		// WAS_Email_BatchSendEmail_SaveAndSend:
		public const string WAS_Email_BatchSendEmail_SaveAndSend = "ErridnInventory.WAS_Email_BatchSendEmail_SaveAndSend"; //// //WAS_Email_BatchSendEmail_SaveAndSend

        // WAS_Email_BatchSendEmail_MstSv_Inos_User_Send:
        public const string WAS_Email_BatchSendEmail_MstSv_Inos_User_Send = "ErridnInventory.WAS_Email_BatchSendEmail_MstSv_Inos_User_Send"; //// //WAS_Email_BatchSendEmail_MstSv_Inos_User_Send

        // Email_BatchSendEmail_SaveX:
        public const string Email_BatchSendEmail_SaveX = "ErridnInventory.Email_BatchSendEmail_SaveX"; //// //Email_BatchSendEmail_SaveX
        public const string Email_BatchSendEmail_SaveX_InvalidDlrSignStatus = "ErridnInventory.Email_BatchSendEmail_SaveX_InvalidDlrSignStatus"; //// //Email_BatchSendEmail_SaveX_InvalidDlrSignStatus
        public const string Email_BatchSendEmail_SaveX_InvalidValue = "ErridnInventory.Email_BatchSendEmail_SaveX_InvalidValue"; //// //Email_BatchSendEmail_SaveX_InvalidValue
        public const string Email_BatchSendEmail_SaveX_InvalidT24_Status = "ErridnInventory.Email_BatchSendEmail_SaveX_InvalidT24_Status"; //// //Email_BatchSendEmail_SaveX_InvalidT24_Status
        public const string Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailToTblNotFound = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailToTblNotFound"; //// //Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailToTblNotFound
        public const string Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailToTblInvalid = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailToTblInvalid"; //// //Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailToTblInvalid
        public const string Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailCCTblNotFound = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailCCTblNotFound"; //// //Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailCCTblNotFound
        public const string Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailCCTblInvalid = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailCCTblInvalid"; //// //Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailCCTblInvalid
        public const string Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailBCCTblNotFound = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailBCCTblNotFound"; //// //Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailBCCTblNotFound
        public const string Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailBCCTblInvalid = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailBCCTblInvalid"; //// //Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailBCCTblInvalid
        public const string Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailFileAttachTblNotFound = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailFileAttachTblNotFound"; //// //Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailFileAttachTblNotFound
        public const string Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailFileAttachTblInvalid = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailFileAttachTblInvalid"; //// //Email_BatchSendEmail_SaveX_Input_Email_BatchSendEmailFileAttachTblInvalid
        public const string Email_BatchSendEmail_SaveX_Input_DMS40_CT_DealerContractDtlTbl_InvalidT24_CIFKH = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_DMS40_CT_DealerContractDtlTbl_InvalidT24_CIFKH"; //// //Email_BatchSendEmail_SaveX_Input_DMS40_CT_DealerContractDtlTbl_InvalidT24_CIFKH
        public const string Email_BatchSendEmail_SaveX_Input_InvalidPaymentType = "ErridnInventory.Email_BatchSendEmail_SaveX_Input_InvalidPaymentType"; //// //Email_BatchSendEmail_SaveX_Input_InvalidPaymentType
        public const string Email_BatchSendEmail_SaveX_InvalidOneAssemblyStatus = "ErridnInventory.Email_BatchSendEmail_SaveX_InvalidOneAssemblyStatus"; //// //Email_BatchSendEmail_SaveX_InvalidOneAssemblyStatus
        public const string Email_BatchSendEmail_SaveX_InvalidOneSOType = "ErridnInventory.Email_BatchSendEmail_SaveX_InvalidOneSOType"; //// //Email_BatchSendEmail_SaveX_InvalidOneSOType

        // Email_BatchSendEmail_SendForDealerContractUpdBankCodeMD:
        public const string Email_BatchSendEmail_SendForDealerContractUpdBankCodeMD = "ErridnInventory.Email_BatchSendEmail_SendForDealerContractUpdBankCodeMD"; //// //Email_BatchSendEmail_SendForDealerContractUpdBankCodeMD
        public const string Email_BatchSendEmail_SendForDealerContractUpdBankCodeMDX = "ErridnInventory.Email_BatchSendEmail_SendForDealerContractUpdBankCodeMDX"; //// //Email_BatchSendEmail_SendForDealerContractUpdBankCodeMDX
        #endregion

        #region // Invoice_TempGroup:
        // Invoice_TempGroup_CheckDB:
        public const string Invoice_TempGroup_CheckDB_InvoiceTGroupCodeNotFound = "ErridnInventory.Invoice_TempGroup_CheckDB_InvoiceTGroupCodeNotFound"; //// // Invoice_TempGroup_CheckDB_InvoiceTGroupCodeExist
        public const string Invoice_TempGroup_CheckDB_InvoiceTGroupCodeExist = "ErridnInventory.Invoice_TempGroup_CheckDB_InvoiceTGroupCodeExist"; //// // Invoice_TempGroup_CheckDB_InvoiceTGroupCodeExist
        public const string Invoice_TempGroup_CheckDB_FlagActiveNotMatched = "ErridnInventory.Invoice_TempGroup_CheckDB_FlagActiveNotMatched"; //// // Invoice_TempGroup_CheckDB_FlagActiveNotMatched

        // Invoice_TempGroup_Get:
        public const string Invoice_TempGroup_Get = "ErridnInventory.Invoice_TempGroup_Get"; //// // Invoice_TempGroup_Get

        // Invoice_TempGroup_Create:
        public const string Invoice_TempGroup_Create = "ErridnInventory.Invoice_TempGroup_Create"; //// // Invoice_TempGroup_Create
        public const string Invoice_TempGroup_Create_InvalidInvoiceTGroupCode = "ErridnInventory.Invoice_TempGroup_Create_InvalidInvoiceTGroupCode"; //// // Invoice_TempGroup_Create_InvalidInvoiceTGroupCode
        public const string Invoice_TempGroup_Create_InvalidVATType = "ErridnInventory.Invoice_TempGroup_Create_InvalidVATType"; //// // Invoice_TempGroup_Create_InvalidVATType
        public const string Invoice_TempGroup_Create_InvalidInvoiceTGroupName = "ErridnInventory.Invoice_TempGroup_Create_InvalidInvoiceTGroupName"; //// // Invoice_TempGroup_Create_InvalidInvoiceTGroupName
        public const string Invoice_TempGroup_Create_InvalidSpec_Prd_Type = "ErridnInventory.Invoice_TempGroup_Create_InvalidSpec_Prd_Type"; //// // Invoice_TempGroup_Create_InvalidSpec_Prd_Type
        public const string Invoice_TempGroup_Create_TempGroupFieldNotFound = "ErridnInventory.Invoice_TempGroup_Create_TempGroupFieldNotFound"; //// // Invoice_TempGroup_Create_TempGroupFieldNotFound
        public const string Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidDBFieldName = "ErridnInventory.Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidDBFieldName"; //// // Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidDBFieldName
        public const string Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType = "ErridnInventory.Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType"; //// // Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType

        // Invoice_TempGroup_Update:
        public const string Invoice_TempGroup_Update = "ErridnInventory.Invoice_TempGroup_Update"; //// // Invoice_TempGroup_Update
        public const string Invoice_TempGroup_Update_InvalidProvinceName = "ErridnInventory.Invoice_TempGroup_Update_InvalidProvinceName"; //// // Invoice_TempGroup_Update_InvalidProvinceName
        public const string Invoice_TempGroup_Update_InvalidSpec_Prd_Type = "ErridnInventory.Invoice_TempGroup_Update_InvalidSpec_Prd_Type"; //// // Invoice_TempGroup_Update_InvalidSpec_Prd_Type
        public const string Invoice_TempGroup_Update_InvalidSpec_TempGroupFieldNotFound = "ErridnInventory.Invoice_TempGroup_Update_InvalidSpec_TempGroupFieldNotFound"; //// // Invoice_TempGroup_Update_InvalidSpec_TempGroupFieldNotFound
        public const string Invoice_TempGroup_Update_TempGroupFieldTbl_InvalidDBFieldName = "ErridnInventory.Invoice_TempGroup_Update_TempGroupFieldTbl_InvalidDBFieldName"; //// // Invoice_TempGroup_Update_TempGroupFieldTbl_InvalidDBFieldName

        // Invoice_TempGroup_Delete:
        public const string Invoice_TempGroup_Delete = "ErridnInventory.Invoice_TempGroup_Delete"; //// // Invoice_TempGroup_Delete

        // WAS_Invoice_TempGroup_Get:
        public const string WAS_Invoice_TempGroup_Get = "ErridnInventory.WAS_Invoice_TempGroup_Get"; //// // WAS_Invoice_TempGroup_Get

        // WAS_Invoice_TempGroup_Create:
        public const string WAS_Invoice_TempGroup_Create = "ErridnInventory.WAS_Invoice_TempGroup_Create"; //// // WAS_Invoice_TempGroup_Create

        // WAS_Invoice_TempGroup_Update:
        public const string WAS_Invoice_TempGroup_Update = "ErridnInventory.WAS_Invoice_TempGroup_Update"; //// // WAS_Invoice_TempGroup_Update

        // WAS_Invoice_TempGroup_Delete:
        public const string WAS_Invoice_TempGroup_Delete = "ErridnInventory.WAS_Invoice_TempGroup_Delete"; //// // WAS_Invoice_TempGroup_Delete
		#endregion

		#region // OS_Sys:
		// WAC_OS_Sys_AT_3A_Invoice_Delete_HasError:
		public const string WAC_OS_Sys_AT_3A_Invoice_Delete_HasError = "ErridnInventory.WAC_OS_Sys_AT_3A_Invoice_Delete_HasError"; //// // WAC_OS_Sys_AT_3A_Invoice_Delete_HasError
        #endregion

        #region // Seq_Box_Carton:
        //Seq_Box_Carton_Get:
        public const string Seq_Box_Carton_Get = "Erridn.Skycic.Inventory.Seq_Box_Carton_Get";
        #endregion 
    }
}
