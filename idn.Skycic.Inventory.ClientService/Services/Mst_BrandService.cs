using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_BrandService : ClientServiceBase<RT_Mst_Brand>
    {
        public static Mst_BrandService Instance
        {
            get
            {
                return GetInstance<Mst_BrandService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstBrand";
            }
        }

        public RT_Mst_Brand WA_Mst_Brand_Get(RQ_Mst_Brand objRQ_Mst_Brand, string addressAPIs)
        {
            var result = PostData<RT_Mst_Brand, RQ_Mst_Brand>(ApiControllerName, "WA_Mst_Brand_Get", new { }, objRQ_Mst_Brand, addressAPIs);
            return result;
        }

        public RT_Mst_Brand WA_Mst_Brand_Create(RQ_Mst_Brand objRQ_Mst_Brand, string addressAPIs)
        {
            var result = PostData<RT_Mst_Brand, RQ_Mst_Brand>(ApiControllerName, "WA_Mst_Brand_Create", new { }, objRQ_Mst_Brand, addressAPIs);
            return result;
        }

        public RT_Mst_Brand WA_Mst_Brand_Update(RQ_Mst_Brand objRQ_Mst_Brand, string addressAPIs)
        {
            var result = PostData<RT_Mst_Brand, RQ_Mst_Brand>(ApiControllerName, "WA_Mst_Brand_Update", new { }, objRQ_Mst_Brand, addressAPIs);
            return result;
        }

        public RT_Mst_Brand WA_Mst_Brand_Delete(RQ_Mst_Brand objRQ_Mst_Brand, string addressAPIs)
        {
            var result = PostData<RT_Mst_Brand, RQ_Mst_Brand>(ApiControllerName, "WA_Mst_Brand_Delete", new { }, objRQ_Mst_Brand, addressAPIs);
            return result;
        }
    }
}
