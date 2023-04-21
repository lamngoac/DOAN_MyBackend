using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.Inbrand;

namespace idn.Skycic.Inventory.ClientService.Services.Inbrand
{
    public class Report_InbrandService : ClientServiceBase<Rpt_Inv_InventoryVerifiedIDForSearch>
    {
        public static Report_InbrandService Instance
        {
            get
            {
                return GetInstance<Report_InbrandService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }
        public RT_Rpt_Inv_InventoryVerifiedIDForSearch Rpt_Inv_InventoryVerifiedIDForSearch(string strUrl, RQ_Rpt_Inv_InventoryVerifiedIDForSearch objRQ_Rpt_Inv_InventoryVerifiedIDForSearch)
        {
            var result = PostData<RT_Rpt_Inv_InventoryVerifiedIDForSearch, RQ_Rpt_Inv_InventoryVerifiedIDForSearch>("Report", "WA_Rpt_Inv_InventoryVerifiedIDForSearch", new { }, objRQ_Rpt_Inv_InventoryVerifiedIDForSearch, strUrl);
            return result;
        }
    }
}
