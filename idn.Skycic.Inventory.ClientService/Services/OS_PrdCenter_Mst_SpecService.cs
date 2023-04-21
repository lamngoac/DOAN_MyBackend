using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_SpecService : ClientServiceBase<Mst_Spec>
    {
        public static OS_PrdCenter_Mst_SpecService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpec";
            }
        }

        public RT_Mst_Spec WA_Mst_Spec_Get(RQ_Mst_Spec objRQ_Mst_Spec, string addressAPIs)
        {
            var result = PostData<RT_Mst_Spec, RQ_Mst_Spec>("MstSpec", "WA_Mst_Spec_Get", new { }, objRQ_Mst_Spec, addressAPIs);
            return result;
        }

        public RT_Mst_Spec WA_Mst_Spec_Add(RQ_Mst_Spec objRQ_Mst_Spec, string addressAPIs)
        {
            var result = PostData<RT_Mst_Spec, RQ_Mst_Spec>("MstSpec", "WA_Mst_Spec_Add", new { }, objRQ_Mst_Spec, addressAPIs);
            return result;
        }

        public RT_Mst_Spec WA_Mst_Spec_Upd(RQ_Mst_Spec objRQ_Mst_Spec, string addressAPIs)
        {
            var result = PostData<RT_Mst_Spec, RQ_Mst_Spec>("MstSpec", "WA_Mst_Spec_Upd", new { }, objRQ_Mst_Spec, addressAPIs);
            return result;
        }

        public RT_Mst_Spec WA_Mst_Spec_Del(RQ_Mst_Spec objRQ_Mst_Spec, string addressAPIs)
        {
            var result = PostData<RT_Mst_Spec, RQ_Mst_Spec>("MstSpec", "WA_Mst_Spec_Del", new { }, objRQ_Mst_Spec, addressAPIs);
            return result;
        }
    }
}
