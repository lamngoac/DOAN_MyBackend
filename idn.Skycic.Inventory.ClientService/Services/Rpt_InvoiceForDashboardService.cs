using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Rpt_InvoiceForDashboardService : ClientServiceBase<Rpt_InvoiceForDashboard>
    {
        public static Rpt_InvoiceForDashboardService Instance
        {
            get
            {
                return GetInstance<Rpt_InvoiceForDashboardService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }

        public RT_Rpt_InvoiceForDashboard WA_Rpt_InvoiceForDashboard(RQ_Rpt_InvoiceForDashboard objRQ_Rpt_InvoiceForDashboard, string addressAPIs)
        {
            var result = PostData<RT_Rpt_InvoiceForDashboard, RQ_Rpt_InvoiceForDashboard>("Report", "WA_Rpt_InvoiceForDashboard", new { }, objRQ_Rpt_InvoiceForDashboard, addressAPIs);
            return result;
        }
    }
}
