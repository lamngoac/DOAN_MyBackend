using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Rpt_InvoiceInvoice_ResultUsedService: ClientServiceBase<Rpt_InvoiceInvoice_ResultUsed>
    {
        public static Rpt_InvoiceInvoice_ResultUsedService Instance
        {
            get
            {
                return GetInstance<Rpt_InvoiceInvoice_ResultUsedService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }

        public RT_Rpt_InvoiceInvoice_ResultUsed WA_Rpt_InvoiceInvoice_ResultUsed(RQ_Rpt_InvoiceInvoice_ResultUsed objRpt_InvoiceInvoice_ResultUsed, string addressAPIs)
        {
            var result = PostData<RT_Rpt_InvoiceInvoice_ResultUsed, RQ_Rpt_InvoiceInvoice_ResultUsed>("Report", "WA_Rpt_InvoiceInvoice_ResultUsed", new { }, objRpt_InvoiceInvoice_ResultUsed, addressAPIs);
            return result;
        }
    }
}
