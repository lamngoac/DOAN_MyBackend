using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_OrgService : ClientServiceBase<RT_Mst_Org>
    {
        public static Mst_OrgService Instance
        {
            get
            {
                return GetInstance<Mst_OrgService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstOrg";
            }
        }

        public RT_Mst_Org WA_Mst_Org_Get(RQ_Mst_Org objRQ_Mst_Org, string addressAPIs)
        {
            var result = PostData<RT_Mst_Org, RQ_Mst_Org>(ApiControllerName, "WA_Mst_Org_Get", new { }, objRQ_Mst_Org, addressAPIs);
            return result;
        }

        public RT_Mst_Org WA_Mst_Org_Create(RQ_Mst_Org objRQ_Mst_Org, string addressAPIs)
        {
            var result = PostData<RT_Mst_Org, RQ_Mst_Org>(ApiControllerName, "WA_Mst_Org_Create", new { }, objRQ_Mst_Org, addressAPIs);
            return result;
        }
    }
}
