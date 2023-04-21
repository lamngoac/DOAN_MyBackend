using idn.Skycic.Inventory.Common.Models.DMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services.DMS
{
    public class DMS_ReportService : ClientServiceBase<Rpt_OrderSummary_TotalProductForInv>
    {
        public static DMS_ReportService Instance
        {
            get
            {
                return GetInstance<DMS_ReportService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }
        public RT_Rpt_OrderSummary_TotalProductForInv WA_Rpt_OrderSummary_TotalProductForInv(RQ_Rpt_OrderSummary_TotalProductForInv objRQ_Rpt_OrderSummary_TotalProductForInv, string strUrl)
        {
            var result = PostData<RT_Rpt_OrderSummary_TotalProductForInv, RQ_Rpt_OrderSummary_TotalProductForInv>("Report", "WA_Rpt_OrderSummary_TotalProductForInv", new { }, objRQ_Rpt_OrderSummary_TotalProductForInv, strUrl);
            return result;
        }

        public RT_Rpt_OrderSummary_TotalProductForInvReturn WA_Rpt_OrderSummary_TotalProductForInvReturn(RQ_Rpt_OrderSummary_TotalProductForInvReturn objRQ_Rpt_OrderSummary_TotalProductForInvReturn, string strUrl)
        {
            var result = PostData<RT_Rpt_OrderSummary_TotalProductForInvReturn, RQ_Rpt_OrderSummary_TotalProductForInvReturn>("Report", "WA_Rpt_OrderSummary_TotalProductForInvReturn", new { }, objRQ_Rpt_OrderSummary_TotalProductForInvReturn, strUrl);
            return result;
        }
    }
}
