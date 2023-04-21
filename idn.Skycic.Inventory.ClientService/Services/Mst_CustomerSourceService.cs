using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_CustomerSourceService : ClientServiceBase<RT_Mst_CustomerSource>
    {
        public static Mst_CustomerSourceService Instance
        {
            get
            {
                return GetInstance<Mst_CustomerSourceService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstCustomerSource";
            }
        }

        public RT_Mst_CustomerSource WA_Mst_CustomerSource_Get(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerSource, RQ_Mst_CustomerSource>(ApiControllerName, "WA_Mst_CustomerSource_Get", new { }, objRQ_Mst_CustomerSource, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerSource WA_Mst_CustomerSource_Create(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerSource, RQ_Mst_CustomerSource>(ApiControllerName, "WA_Mst_CustomerSource_Create", new { }, objRQ_Mst_CustomerSource, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerSource WA_Mst_CustomerSource_Update(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerSource, RQ_Mst_CustomerSource>(ApiControllerName, "WA_Mst_CustomerSource_Update", new { }, objRQ_Mst_CustomerSource, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerSource WA_Mst_CustomerSource_Delete(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerSource, RQ_Mst_CustomerSource>(ApiControllerName, "WA_Mst_CustomerSource_Delete", new { }, objRQ_Mst_CustomerSource, addressAPIs);
            return result;
        }
    }
}
