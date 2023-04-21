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
		#region // Invoice_Invoice:
		// Invoice_Invoice_Calc:
		//public const string Invoice_Invoice_Calc = "ErridnInventory.Invoice_Invoice_Calc"; //// //Invoice_Invoice_Calc
		public const string Invoice_Invoice_Calc_Input_InvoiceTblNotFound = "ErridnInventory.Invoice_Invoice_Calc_Input_InvoiceTblNotFound"; //// //Invoice_Invoice_Calc_Input_InvoiceTblNotFound

		public const string Invoice_Invoice_Calc_Input_InvoiceTblInvalid = "ErridnInventory.Invoice_Invoice_Calc_Input_InvoiceTblInvalid"; //// //Invoice_Invoice_Calc_Input_InvoiceTblInvalid

		public const string Invoice_Invoice_Calc_InvalidInvoiceCode = "ErridnInventory.Invoice_Invoice_Calc_InvalidInvoiceCode"; //// //Invoice_Invoice_Calc_InvalidInvoiceCode
		public const string Invoice_Invoice_Calc_InvalidInvoiceDateUTC = "ErridnInventory.Invoice_Invoice_Calc_InvalidInvoiceDateUTC"; //// //Invoice_Invoice_Calc_InvalidInvoiceDateUTC
		public const string Invoice_Invoice_Calc_InvalidCustomerNNTCode = "ErridnInventory.Invoice_Invoice_Calc_InvalidCustomerNNTCode"; //// //Invoice_Invoice_Calc_InvalidCustomerNNTCode
		public const string Invoice_Invoice_Calc_InvalidEmailSend = "ErridnInventory.Invoice_Invoice_Calc_InvalidEmailSend"; //// //Invoice_Invoice_Calc_InvalidEmailSend
		public const string Invoice_Invoice_Calc_InvalidPaymentMethodCode = "ErridnInventory.Invoice_Invoice_Calc_InvalidPaymentMethodCode"; //// //Invoice_Invoice_Calc_InvalidPaymentMethodCode
		public const string Invoice_Invoice_Calc_StatusNotMatched = "ErridnInventory.Invoice_Invoice_Calc_StatusNotMatched"; //// //Invoice_Invoice_Calc_StatusNotMatched
		public const string Invoice_Invoice_Calc_Input_InvoiceDtlTblNotFound = "ErridnInventory.Invoice_Invoice_Calc_Input_InvoiceDtlTblNotFound"; //// //Invoice_Invoice_Calc_Input_InvoiceDtlTblNotFound
		public const string Invoice_Invoice_Calc_Input_InvoiceDtlTblInvalid = "ErridnInventory.Invoice_Invoice_Calc_Input_InvoiceDtlTblInvalid"; //// //Invoice_Invoice_Calc_Input_InvoiceDtlTblInvalid
		public const string Invoice_Invoice_Calc_Input_InvoicePrdTblInvalid = "ErridnInventory.Invoice_Invoice_Calc_Input_InvoicePrdTblInvalid"; //// //Invoice_Invoice_Calc_Input_InvoicePrdTblInvalid
		public const string Invoice_Invoice_Calc_Input_InvoiceDtl_ProductIDDuplicate = "ErridnInventory.Invoice_Invoice_Calc_Input_InvoiceDtl_ProductIDDuplicate"; //// //Invoice_Invoice_Calc_Input_InvoiceDtl_ProductIDDuplicate
		public const string Invoice_Invoice_Calc_Input_InvoiceDtl_SpecCodeDuplicate = "ErridnInventory.Invoice_Invoice_Calc_Input_InvoiceDtl_SpecCodeDuplicate"; //// //Invoice_Invoice_Calc_Input_InvoiceDtl_SpecCodeDuplicate
		public const string Invoice_Invoice_Calc_InvoiceAdjTypeIsNotNull = "ErridnInventory.Invoice_Invoice_Calc_InvoiceAdjTypeIsNotNull"; //// //Invoice_Invoice_Calc_ExistInvoiceNo
		public const string Invoice_Invoice_Calc_ExistInvoiceNo = "ErridnInventory.Invoice_Invoice_Calc_ExistInvoiceNo"; //// //Invoice_Invoice_Calc_ExistInvoiceNo
		public const string Invoice_Invoice_Calc_NotDelete = "ErridnInventory.Invoice_Invoice_Calc_NotDelete"; //// //Invoice_Invoice_Calc_NotDelete
		public const string Invoice_Invoice_Calc_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC = "ErridnInventory.Invoice_Invoice_Calc_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC"; //// //Invoice_Invoice_Calc_InvalidInvoiceDateUTCBeforeLastInvoiceDateUTC
		public const string Invoice_Invoice_Calc_InvalidMSTOfNNTAndCustomer = "ErridnInventory.Invoice_Invoice_Calc_InvalidMSTOfNNTAndCustomer"; //// //Invoice_Invoice_Calc_InvalidMSTOfNNTAndCustomer
		public const string Invoice_Invoice_Calc_InvalidMSTOfNNTAndTempInvoice = "ErridnInventory.Invoice_Invoice_Calc_InvalidMSTOfNNTAndTempInvoice"; //// //Invoice_Invoice_Calc_InvalidMSTOfNNTAndTempInvoice
		public const string Invoice_Invoice_Calc_InvalidQtyIssueRemain = "ErridnInventory.Invoice_Invoice_Calc_InvalidQtyIssueRemain"; //// //Invoice_Invoice_Calc_InvalidQtyIssueRemain
		public const string Invoice_Invoice_Calc_InputInvoice_InvoicePrdTblNotFound = "ErridnInventory.Invoice_Invoice_Calc_InputInvoice_InvoicePrdTblNotFound"; //// //Invoice_Invoice_Calc_InputInvoice_InvoicePrdTblNotFound
		public const string Invoice_Invoice_Calc_Input_Invoice_InvoicePrdTblInvalid = "ErridnInventory.Invoice_Invoice_Calc_Input_Invoice_InvoicePrdTblInvalid"; //// //Invoice_Invoice_Calc_Input_Invoice_InvoicePrdTblInvalid
		public const string Invoice_Invoice_Calc_Input_Invoice_InvaliInvoiceDateUTCAfterSysDate = "ErridnInventory.Invoice_Invoice_Calc_Input_Invoice_InvaliInvoiceDateUTCAfterSysDate"; //// //Invoice_Invoice_Calc_Input_Invoice_InvaliInvoiceDateUTCAfterSysDate
		public const string Invoice_Invoice_Calc_Input_Invoice_InvalidRefNo = "ErridnInventory.Invoice_Invoice_Calc_Input_Invoice_InvalidRefNo"; //// //Invoice_Invoice_Calc_Input_Invoice_InvalidRefNo
		public const string Invoice_Invoice_Calc_Input_Invoice_InvalidInvoiceStatusRefNo = "ErridnInventory.Invoice_Invoice_Calc_Input_Invoice_InvalidInvoiceStatusRefNo"; //// //Invoice_Invoice_Calc_Input_Invoice_InvalidInvoiceStatusRefNo
		public const string Invoice_Invoice_Calc_Invalid_MstNNT_FlagActive = "ErridnInventory.Invoice_Invoice_Calc_Invalid_MstNNT_FlagActive"; //// //Invoice_Invoice_Calc_Invalid_MstNNT_FlagActive
		public const string Invoice_Invoice_Calc_Invalid_MstNNT_NotFound = "ErridnInventory.Invoice_Invoice_Calc_Invalid_MstNNT_NotFound"; //// //Invoice_Invoice_Calc_Invalid_MstNNT_NotFound
		public const string Invoice_Invoice_Calc_Invalid_PaymentMethods_NotFound = "ErridnInventory.Invoice_Invoice_Calc_Invalid_PaymentMethods_NotFound"; //// //Invoice_Invoice_Calc_Invalid_PaymentMethods_NotFound
		public const string Invoice_Invoice_Calc_Invalid_PaymentMethods_FlagActive = "ErridnInventory.Invoice_Invoice_Calc_Invalid_PaymentMethods_FlagActive"; //// //Invoice_Invoice_Calc_Invalid_PaymentMethods_FlagActive
		public const string Invoice_Invoice_Calc_Invalid_InvoiceType2_NotFound = "ErridnInventory.Invoice_Invoice_Calc_Invalid_InvoiceType2_NotFound"; //// //Invoice_Invoice_Calc_Invalid_InvoiceType2_NotFound
		public const string Invoice_Invoice_Calc_Invalid_InvoiceType2_FlagActive = "ErridnInventory.Invoice_Invoice_Calc_Invalid_InvoiceType2_FlagActive"; //// //Invoice_Invoice_Calc_Invalid_InvoiceType2_FlagActive
		public const string Invoice_Invoice_Calc_Invalid_SourceInvoice_NotFound = "ErridnInventory.Invoice_Invoice_Calc_Invalid_SourceInvoice_NotFound"; //// //Invoice_Invoice_Calc_Invalid_SourceInvoice_NotFound
		public const string Invoice_Invoice_Calc_Invalid_SourceInvoice_FlagActive = "ErridnInventory.Invoice_Invoice_Calc_Invalid_SourceInvoice_FlagActive"; //// //Invoice_Invoice_Calc_Invalid_SourceInvoice_FlagActive
																																						  ////
		public const string Invoice_Invoice_Calc_Invalid_MstCustomerNNT_FlagActive = "ErridnInventory.Invoice_Invoice_Calc_Invalid_MstCustomerNNT_FlagActive"; //// //Invoice_Invoice_Calc_Invalid_MstCustomerNNT_FlagActive
		public const string Invoice_Invoice_Calc_Invalid_MstCustomerNNT_NotFound = "ErridnInventory.Invoice_Invoice_Calc_Invalid_MstCustomerNNT_NotFound"; //// //Invoice_Invoice_Calc_Invalid_MstCustomerNNT_NotFound

		//public const string WAS_Invoice_Invoice_Calc = "ErridnInventory.WAS_Invoice_Invoice_Calc"; //// //WAS_Invoice_Invoice_Calc
		//public const string Invoice_Invoice_Calc = "ErridnInventory.Invoice_Invoice_Calc"; //// //Invoice_Invoice_Calc

		public const string Invoice_Invoice_Calc_Invalid_TempInvoice_NotFound = "ErridnInventory.Invoice_Invoice_Calc_Invalid_TempInvoice_NotFound"; //// //Invoice_Invoice_Calc_Invalid_TempInvoice_NotFound
		public const string Invoice_Invoice_Calc_Invalid_TempInvoice_StatusNotMatch = "ErridnInventory.Invoice_Invoice_Calc_Invalid_TempInvoice_StatusNotMatch"; //// //Invoice_Invoice_Calc_Invalid_TempInvoice_StatusNotMatch

		#endregion
	}
}
