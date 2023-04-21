using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvoiceInvoiceInputService : ClientServiceBase<Invoice_InvoiceInput>
    {
        public static InvoiceInvoiceInputService Instance
        {
            get
            {
                return GetInstance<InvoiceInvoiceInputService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvoiceInvoiceInput";
            }
        }

        public RT_Invoice_InvoiceInput WA_Invoice_InvoiceInput_Get(RQ_Invoice_InvoiceInput objWA_Invoice_InvoiceInput_Get, string addressAPIs)
        {
            var result = PostData<RT_Invoice_InvoiceInput, RQ_Invoice_InvoiceInput>("InvoiceInvoiceInput", "WA_Invoice_InvoiceInput_Get", new { }, objWA_Invoice_InvoiceInput_Get, addressAPIs);
            return result;
        }
    }
}
