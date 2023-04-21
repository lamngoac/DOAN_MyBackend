using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvoiceInvoiceService: ClientServiceBase<Invoice_Invoice>
    {
        public static InvoiceInvoiceService Instance
        {
            get
            {
                return GetInstance<InvoiceInvoiceService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvoiceInvoice";
            }
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Get(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Get", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Save_Replace(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Save_Replace", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }


        public RT_Invoice_Invoice WA_Invoice_Invoice_Save_Adj(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Save_Adj", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Save_Root(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Save_Root", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Deleted(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Deleted", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Issued(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Issued", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Approved(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Approved", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Cancel(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Cancel", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_AllocatedInv(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_AllocatedInv", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_AllocatedAndApprovedAndIssued(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_AllocatedAndApprovedAndIssued", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }
        public RT_Invoice_Invoice WA_Invoice_Invoice_Change(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Change", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_UpdAfterAllocated(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_UpdAfterAllocated", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Calc(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Calc", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_SaveAndAllocatedInv(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_SaveAndAllocatedInv", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_ApprovedAndIssued(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_ApprovedAndIssued", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_UpdMailSentDTimeUTC(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_UpdMailSentDTimeUTC", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }

        public RT_Invoice_Invoice WA_Invoice_Invoice_Upd(RQ_Invoice_Invoice objRQ_Invoice_Invoice, string addressAPIs)
        {
            var result = PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>("InvoiceInvoice", "WA_Invoice_Invoice_Upd", new { }, objRQ_Invoice_Invoice, addressAPIs);
            return result;
        }
    }
}
