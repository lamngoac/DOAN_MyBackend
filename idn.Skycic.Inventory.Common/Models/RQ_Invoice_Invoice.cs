using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Invoice_Invoice : WARQBase
    {
        public string Rt_Cols_Invoice_Invoice { get; set; }
        public string Rt_Cols_Invoice_InvoiceDtl { get; set; }
        public string Rt_Cols_Invoice_InvoicePrd { get; set; }
        public string Rt_Cols_Invoice_InvoiceVAT { get; set; }
        public object FlagIsCheckInvoiceTotal { get; set; }
        public Invoice_Invoice Invoice_Invoice { get; set; }
        public List<Invoice_Invoice> Lst_Invoice_Invoice { get; set; }
        public List<Invoice_InvoiceDtl> Lst_Invoice_InvoiceDtl { get; set; }
        public List<Invoice_InvoicePrd> Lst_Invoice_InvoicePrd { get; set; }

        public Email_BatchSendEmail Email_BatchSendEmail { get; set; }
        public List<Email_BatchSendEmailTo> Lst_Email_BatchSendEmailTo { get; set; }
        public List<Email_BatchSendEmailCC> Lst_Email_BatchSendEmailCC { get; set; }
        public List<Email_BatchSendEmailBCC> Lst_Email_BatchSendEmailBCC { get; set; }
        public List<Email_BatchSendEmailFileAttach> Lst_Email_BatchSendEmailFileAttach { get; set; }
    }
}
