using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_CustomerTypeService : ClientServiceBase<RT_Mst_CustomerType>
    {
        public static Mst_CustomerTypeService Instance
        {
            get
            {
                return GetInstance<Mst_CustomerTypeService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstCustomerType";
            }
        }

        public RT_Mst_CustomerType WA_Mst_CustomerType_Get(RQ_Mst_CustomerType objRQ_Mst_CustomerType, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerType, RQ_Mst_CustomerType>(ApiControllerName, "WA_Mst_CustomerType_Get", new { }, objRQ_Mst_CustomerType, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerType WA_Mst_CustomerType_Create(RQ_Mst_CustomerType objRQ_Mst_CustomerType, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerType, RQ_Mst_CustomerType>(ApiControllerName, "WA_Mst_CustomerType_Create", new { }, objRQ_Mst_CustomerType, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerType WA_Mst_CustomerType_Update(RQ_Mst_CustomerType objRQ_Mst_CustomerType, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerType, RQ_Mst_CustomerType>(ApiControllerName, "WA_Mst_CustomerType_Update", new { }, objRQ_Mst_CustomerType, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerType WA_Mst_CustomerType_Delete(RQ_Mst_CustomerType objRQ_Mst_CustomerType, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerType, RQ_Mst_CustomerType>(ApiControllerName, "WA_Mst_CustomerType_Delete", new { }, objRQ_Mst_CustomerType, addressAPIs);
            return result;
        }
    }
}
