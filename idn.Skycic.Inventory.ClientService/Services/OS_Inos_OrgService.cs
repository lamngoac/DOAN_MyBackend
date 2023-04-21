using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_Inos_OrgService : ClientServiceBase<OS_Inos_Org>
    {
        public static OS_Inos_OrgService Instance
        {
            get
            {
                return GetInstance<OS_Inos_OrgService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "OSInos";
            }
        }

        //Dùng link API Report Server 
        // 2
        public RT_OS_Inos_Org WA_OS_Inos_Org_GetMyOrgList(RQ_OS_Inos_Org objRQ_OS_Inos_Org, string apiaddresstype)
        {
            var result = PostData<RT_OS_Inos_Org, RQ_OS_Inos_Org>("OSInos", "WA_OS_Inos_Org_GetMyOrgList", new { }, objRQ_OS_Inos_Org, apiaddresstype);
            return result;
        }
    }
}
