using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_SpecType1Service : ClientServiceBase<Mst_SpecType1>
    {
        public static OS_PrdCenter_Mst_SpecType1Service Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecType1Service>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecType1";
            }
        }

        public RT_Mst_SpecType1 WA_Mst_SpecType1_Get(RQ_Mst_SpecType1 objRQ_Mst_SpecType1, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecType1, RQ_Mst_SpecType1>("MstSpecType1", "WA_Mst_SpecType1_Get", new { }, objRQ_Mst_SpecType1, addressAPIs);
            return result;
        }

        public RT_Mst_SpecType1 WA_Mst_SpecType1_Create(RQ_Mst_SpecType1 objRQ_Mst_SpecType1, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecType1, RQ_Mst_SpecType1>("MstSpecType1", "WA_Mst_SpecType1_Create", new { }, objRQ_Mst_SpecType1, addressAPIs);
            return result;
        }

        public RT_Mst_SpecType1 WA_Mst_SpecType1_Update(RQ_Mst_SpecType1 objRQ_Mst_SpecType1, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecType1, RQ_Mst_SpecType1>("MstSpecType1", "WA_Mst_SpecType1_Update", new { }, objRQ_Mst_SpecType1, addressAPIs);
            return result;
        }

        public RT_Mst_SpecType1 WA_Mst_SpecType1_Delete(RQ_Mst_SpecType1 objRQ_Mst_SpecType1, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecType1, RQ_Mst_SpecType1>("MstSpecType1", "WA_Mst_SpecType1_Delete", new { }, objRQ_Mst_SpecType1, addressAPIs);
            return result;
        }
    }
}
