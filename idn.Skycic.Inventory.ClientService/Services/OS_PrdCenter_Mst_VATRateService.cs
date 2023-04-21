using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_VATRateService : ClientServiceBase<Mst_VATRate>
    {
        public static OS_PrdCenter_Mst_VATRateService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_VATRateService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstVATRate";
            }
        }

        public RT_Mst_VATRate WA_Mst_VATRate_Get(RQ_Mst_VATRate objRQ_Mst_VATRate, string addressAPIs)
        {
            var result = PostData<RT_Mst_VATRate, RQ_Mst_VATRate>("MstVATRate", "WA_Mst_VATRate_Get", new { }, objRQ_Mst_VATRate, addressAPIs);
            return result;
        }

        public RT_Mst_VATRate WA_Mst_VATRate_Create(RQ_Mst_VATRate objRQ_Mst_VATRate, string addressAPIs)
        {
            var result = PostData<RT_Mst_VATRate, RQ_Mst_VATRate>("MstVATRate", "WA_Mst_VATRate_Create", new { }, objRQ_Mst_VATRate, addressAPIs);
            return result;
        }

        public RT_Mst_VATRate WA_Mst_VATRate_Update(RQ_Mst_VATRate objRQ_Mst_VATRate, string addressAPIs)
        {
            var result = PostData<RT_Mst_VATRate, RQ_Mst_VATRate>("MstVATRate", "WA_Mst_VATRate_Update", new { }, objRQ_Mst_VATRate, addressAPIs);
            return result;
        }

        public RT_Mst_VATRate WA_Mst_VATRate_Delete(RQ_Mst_VATRate objRQ_Mst_VATRate, string addressAPIs)
        {
            var result = PostData<RT_Mst_VATRate, RQ_Mst_VATRate>("MstVATRate", "WA_Mst_VATRate_Delete", new { }, objRQ_Mst_VATRate, addressAPIs);
            return result;
        }
    }
}
