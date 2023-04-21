using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_CurrencyExService : ClientServiceBase<Mst_CurrencyEx>
    {
        public static OS_PrdCenter_Mst_CurrencyExService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_CurrencyExService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstCurrencyEx";
            }
        }

        public RT_Mst_CurrencyEx WA_Mst_CurrencyEx_Get(RQ_Mst_CurrencyEx objRQ_Mst_CurrencyEx, string addressAPIs)
        {
            var result = PostData<RT_Mst_CurrencyEx, RQ_Mst_CurrencyEx>("MstCurrencyEx", "WA_Mst_CurrencyEx_Get", new { }, objRQ_Mst_CurrencyEx, addressAPIs);
            return result;
        }
        public RT_Mst_CurrencyEx WA_Mst_CurrencyEx_Create(RQ_Mst_CurrencyEx objRQ_Mst_CurrencyEx, string addressAPIs)
        {
            var result = PostData<RT_Mst_CurrencyEx, RQ_Mst_CurrencyEx>("MstCurrencyEx", "WA_Mst_CurrencyEx_Create", new { }, objRQ_Mst_CurrencyEx, addressAPIs);
            return result;
        }
        public RT_Mst_CurrencyEx WA_Mst_CurrencyEx_Update(RQ_Mst_CurrencyEx objRQ_Mst_CurrencyEx, string addressAPIs)
        {
            var result = PostData<RT_Mst_CurrencyEx, RQ_Mst_CurrencyEx>("MstCurrencyEx", "WA_Mst_CurrencyEx_Update", new { }, objRQ_Mst_CurrencyEx, addressAPIs);
            return result;
        }
        public RT_Mst_CurrencyEx WA_Mst_CurrencyEx_Delete(RQ_Mst_CurrencyEx objRQ_Mst_CurrencyEx, string addressAPIs)
        {
            var result = PostData<RT_Mst_CurrencyEx, RQ_Mst_CurrencyEx>("MstCurrencyEx", "WA_Mst_CurrencyEx_Delete", new { }, objRQ_Mst_CurrencyEx, addressAPIs);
            return result;
        }
    }
}
