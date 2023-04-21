using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Sys_UserInGroupService : ClientServiceBase<Sys_UserInGroup>
    {
        public static Sys_UserInGroupService Instance
        {
            get
            {
                return GetInstance<Sys_UserInGroupService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Sys_UserInGroup";
            }
        }

        public RT_Sys_UserInGroup WA_Sys_UserInGroup_Save(RQ_Sys_UserInGroup objRQ_Sys_UserInGroup, string addressAPIs)
        {
            var result = PostData<RT_Sys_UserInGroup, RQ_Sys_UserInGroup>("Sys_UserInGroup", "WA_Sys_UserInGroup_Save", new { }, objRQ_Sys_UserInGroup, addressAPIs);
            return result;
        }
    }
}
