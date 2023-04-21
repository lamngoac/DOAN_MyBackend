using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_Invoice_InvoiceTempService : ClientServiceBase<OS_Invoice_InvoiceTemp>
    {
        public static OS_Invoice_InvoiceTempService Instance
        {
            get
            {
                return GetInstance<OS_Invoice_InvoiceTempService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "OSInvoiceInvoiceTemp";
            }
        }

        public RT_OS_Invoice_InvoiceTemp WA_OS_Invoice_InvoiceTemp_Get(RQ_OS_Invoice_InvoiceTemp objRQ_OS_Invoice_InvoiceTemp, string addressAPIs)
        {
            var result = PostData<RT_OS_Invoice_InvoiceTemp, RQ_OS_Invoice_InvoiceTemp>("OSInvoiceInvoiceTemp", "WA_OS_Invoice_InvoiceTemp_Get", new { }, objRQ_OS_Invoice_InvoiceTemp, addressAPIs);
            return result;
        }
        public RT_OS_Invoice_InvoiceTemp WA_OS_Invoice_InvoiceTemp_UpdMultiSignStatus(RQ_OS_Invoice_InvoiceTemp objRQ_OS_Invoice_InvoiceTemp, string addressAPIs)
        {
            var result = PostData<RT_OS_Invoice_InvoiceTemp, RQ_OS_Invoice_InvoiceTemp>("OSInvoiceInvoiceTemp", "WA_OS_Invoice_InvoiceTemp_UpdMultiSignStatus", new { }, objRQ_OS_Invoice_InvoiceTemp, addressAPIs);
            return result;
        }

    }
}
