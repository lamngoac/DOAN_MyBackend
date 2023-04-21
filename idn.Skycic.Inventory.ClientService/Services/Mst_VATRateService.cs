using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_VATRateService: ClientServiceBase<Mst_VATRate>
    {
        public static Mst_VATRateService Instance
        {
            get
            {
                return GetInstance<Mst_VATRateService>();
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
    }
}
