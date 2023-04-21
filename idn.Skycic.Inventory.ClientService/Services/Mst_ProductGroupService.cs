using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_ProductGroupService : ClientServiceBase<RT_Mst_ProductGroup>
    {
        public static Mst_ProductGroupService Instance
        {
            get
            {
                return GetInstance<Mst_ProductGroupService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstProductGroup";
            }
        }

        public RT_Mst_ProductGroup WA_Mst_ProductGroup_Get(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_ProductGroup, RQ_Mst_ProductGroup>(ApiControllerName, "WA_Mst_ProductGroup_Get", new { }, objRQ_Mst_ProductGroup, addressAPIs);
            return result;
        }

        public RT_Mst_ProductGroup WA_Mst_ProductGroup_Create(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_ProductGroup, RQ_Mst_ProductGroup>(ApiControllerName, "WA_Mst_ProductGroup_Create", new { }, objRQ_Mst_ProductGroup, addressAPIs);
            return result;
        }

        public RT_Mst_ProductGroup WA_Mst_ProductGroup_Update(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_ProductGroup, RQ_Mst_ProductGroup>(ApiControllerName, "WA_Mst_ProductGroup_Update", new { }, objRQ_Mst_ProductGroup, addressAPIs);
            return result;
        }

        public RT_Mst_ProductGroup WA_Mst_ProductGroup_Delete(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_ProductGroup, RQ_Mst_ProductGroup>(ApiControllerName, "WA_Mst_ProductGroup_Delete", new { }, objRQ_Mst_ProductGroup, addressAPIs);
            return result;
        }

    }
}
