using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_CustomerGroupService : ClientServiceBase<RT_Mst_CustomerGroup>
    {
        public static Mst_CustomerGroupService Instance
        {
            get
            {
                return GetInstance<Mst_CustomerGroupService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstCustomerGroup";
            }
        }

        public RT_Mst_CustomerGroup WA_Mst_CustomerGroup_Get(RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerGroup, RQ_Mst_CustomerGroup>(ApiControllerName, "WA_Mst_CustomerGroup_Get", new { }, objRQ_Mst_CustomerGroup, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerGroup WA_Mst_CustomerGroup_Create(RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerGroup, RQ_Mst_CustomerGroup>(ApiControllerName, "WA_Mst_CustomerGroup_Create", new { }, objRQ_Mst_CustomerGroup, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerGroup WA_Mst_CustomerGroup_Update(RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerGroup, RQ_Mst_CustomerGroup>(ApiControllerName, "WA_Mst_CustomerGroup_Update", new { }, objRQ_Mst_CustomerGroup, addressAPIs);
            return result;
        }

        public RT_Mst_CustomerGroup WA_Mst_CustomerGroup_Delete(RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_CustomerGroup, RQ_Mst_CustomerGroup>(ApiControllerName, "WA_Mst_CustomerGroup_Delete", new { }, objRQ_Mst_CustomerGroup, addressAPIs);
            return result;
        }
    }
}
