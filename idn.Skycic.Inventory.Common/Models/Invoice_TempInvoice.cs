using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Invoice_TempInvoice
    {
        public object TInvoiceCode { get; set; }

        public object NetworkID { get; set; }

        public object MST { get; set; }

        public object PaymentMethodCode { get; set; }

        public object InvoiceType { get; set; }

        public object TInvoiceName { get; set; }

        public object FormNo { get; set; }

        public object Sign { get; set; }

        public object EffDateStart { get; set; }

        public object EffDateEnd { get; set; }

        public object InvoiceCode { get; set; }

        public object NNTName { get; set; }

        public object NNTAddress { get; set; }

        public object NNTPhone { get; set; }

        public object NNTFax { get; set; }

        public object NNTEmail { get; set; }

        public object NNTWebsite { get; set; }

        public object NNTAccNo { get; set; }

        public object NNTBankName { get; set; }
        
        public object LogoFilePath { get; set; }

        public object TInvoiceBody { get; set; }

        public object WatermarkFilePath { get; set; }

        public object InvoiceTGroupCode { get; set; }

        public object StartInvoiceNo { get; set; }

        public object EndInvoiceNo { get; set; }

        public object QtyUsed { get; set; }

        public object CreateDTimeUTC { get; set; }

        public object CreateBy { get; set; }

        public object LUDTimeUTC { get; set; }

        public object LUBy { get; set; }

        public object Remark { get; set; }

        public object TInvoiceStatus { get; set; }

        public object FlagActive { get; set; }

        public object UpdQtyInvoiceNoDTimeUTC { get; set; }

        public object UpdQtyInvoiceBy { get; set; }

        public object IssuedDTimeUTC { get; set; }

        public object Issuedby { get; set; }

        public object InActiveDTimeUTC { get; set; }

        public object InActiveBy { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        public object TInvoiceFilePathXML { get; set; }

        ////
        public object mgtid_GovTaxID { get; set; }
        public object mgtid_GovTaxName { get; set; }
        /// <summary>
        public object mit_InvoiceType { get; set; }
        public object mit_InvoiceTypeName { get; set; }
        /// <summary>
        public object itg_InvoiceTGroupCode { get; set; }
        public object itg_Spec_Prd_Type { get; set; }

        public object itg_InvoiceTGroupName { get; set; }

        public object itg_VATType { get; set; }
    }
}
