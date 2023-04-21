using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Map_DealerDiscountService : ClientServiceBase<Map_DealerDiscount>
    {
        public static Map_DealerDiscountService Instance
        {
            get
            {
                return GetInstance<Map_DealerDiscountService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "RptSvMapDealerDiscount";
            }
        }
        public RT_Map_DealerDiscount WA_RptSv_Map_DealerDiscount_Get(RQ_Map_DealerDiscount objRQ_Map_DealerDiscount, string addressAPIs)
        {
            var result = PostData<RT_Map_DealerDiscount, RQ_Map_DealerDiscount>("RptSvMapDealerDiscount", "WA_RptSv_Map_DealerDiscount_Get", new { }, objRQ_Map_DealerDiscount, addressAPIs);
            return result;
        }
        public RT_Map_DealerDiscount WA_Map_DealerDiscount_Get(RQ_Map_DealerDiscount objRQ_Map_DealerDiscount, string addressAPIs)
        {
            var result = PostData<RT_Map_DealerDiscount, RQ_Map_DealerDiscount>("MapDealerDiscount", "WA_Map_DealerDiscount_Get", new { }, objRQ_Map_DealerDiscount, addressAPIs);
            return result;
        }
    }
}
