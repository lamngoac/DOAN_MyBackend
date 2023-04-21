using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class RptSv_Rpt_InvoiceSummary_01Service : ClientServiceBase<RptSv_Rpt_InvoiceSummary_01>
    {
        public static RptSv_Rpt_InvoiceSummary_01Service Instance
        {
            get
            {
                return GetInstance<RptSv_Rpt_InvoiceSummary_01Service>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "RptSvRptInvoiceSummary01";
            }
        }

        public RT_RptSv_Rpt_InvoiceSummary_01 WA_RptSv_Rpt_InvoiceSummary_01_Get(RQ_RptSv_Rpt_InvoiceSummary_01 objRptSv_Rpt_InvoiceSummary_01, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Rpt_InvoiceSummary_01, RQ_RptSv_Rpt_InvoiceSummary_01>("RptSvRptInvoiceSummary01", "WA_RptSv_Rpt_InvoiceSummary_01_Get", new { }, objRptSv_Rpt_InvoiceSummary_01, addressAPIs);
            return result;
        }
    }
}
