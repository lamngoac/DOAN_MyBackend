using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_SpecPriceService : ClientServiceBase<Mst_SpecPrice>
    {
        public static OS_PrdCenter_Mst_SpecPriceService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecPriceService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecPrice";
            }
        }

        public RT_Mst_SpecPrice WA_Mst_SpecPrice_Get(RQ_Mst_SpecPrice objRQ_Mst_SpecPrice, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecPrice, RQ_Mst_SpecPrice>("MstSpecPrice", "WA_Mst_SpecPrice_Get", new { }, objRQ_Mst_SpecPrice, addressAPIs);
            return result;
        }

        public RT_Mst_SpecPrice WA_Mst_SpecPrice_Create(RQ_Mst_SpecPrice objRQ_Mst_SpecPrice, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecPrice, RQ_Mst_SpecPrice>("MstSpecPrice", "WA_Mst_SpecPrice_Create", new { }, objRQ_Mst_SpecPrice, addressAPIs);
            return result;
        }

        public RT_Mst_SpecPrice WA_Mst_SpecPrice_Update(RQ_Mst_SpecPrice objRQ_Mst_SpecPrice, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecPrice, RQ_Mst_SpecPrice>("MstSpecPrice", "WA_Mst_SpecPrice_Update", new { }, objRQ_Mst_SpecPrice, addressAPIs);
            return result;
        }

        public RT_Mst_SpecPrice WA_Mst_SpecPrice_Delete(RQ_Mst_SpecPrice objRQ_Mst_SpecPrice, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecPrice, RQ_Mst_SpecPrice>("MstSpecPrice", "WA_Mst_SpecPrice_Delete", new { }, objRQ_Mst_SpecPrice, addressAPIs);
            return result;
        }
    }
}
