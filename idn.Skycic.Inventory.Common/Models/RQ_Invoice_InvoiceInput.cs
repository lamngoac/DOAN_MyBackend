using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Invoice_InvoiceInput : WARQBase
    {
        public string Rt_Cols_Invoice_InvoiceInput { get; set; }
        public string Rt_Cols_Invoice_InvoiceInputDtl { get; set; }
        public Invoice_InvoiceInput Invoice_InvoiceInput { get; set; }
        public List<Invoice_InvoiceInput> Lst_Invoice_InvoiceInput { get; set; }
        public List<Invoice_InvoiceInputDtl> Lst_Invoice_InvoiceInputDtl { get; set; }
    }
}
