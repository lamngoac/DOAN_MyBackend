using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_Invoice_InvoiceTemp : WARQBase
    {
        public string Rt_Cols_OS_Invoice_InvoiceTemp { get; set; }
        public string Rt_Cols_OS_Invoice_InvoiceTempDtl { get; set; }
        public OS_Invoice_InvoiceTemp OS_Invoice_InvoiceTemp { get; set; }
        public List<OS_Invoice_InvoiceTemp> Lst_OS_Invoice_InvoiceTemp { get; set; }
        public List<OS_Invoice_InvoiceTempDtl> Lst_OS_Invoice_InvoiceTempDtl { get; set; }
    }
}
