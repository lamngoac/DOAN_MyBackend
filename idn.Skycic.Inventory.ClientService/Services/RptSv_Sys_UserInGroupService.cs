using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class RptSv_Sys_UserInGroupService : ClientServiceBase<RptSv_Sys_UserInGroup>
    {
        public static RptSv_Sys_UserInGroupService Instance
        {
            get
            {
                return GetInstance<RptSv_Sys_UserInGroupService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "RptSvSysUserInGroup";
            }
        }

        public RT_RptSv_Sys_UserInGroup WA_RptSv_Sys_UserInGroup_Save(RQ_RptSv_Sys_UserInGroup objRQ_RptSv_Sys_UserInGroup, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_UserInGroup, RQ_RptSv_Sys_UserInGroup>("RptSvSysUserInGroup", "WA_RptSv_Sys_UserInGroup_Save", new { }, objRQ_RptSv_Sys_UserInGroup, addressAPIs);
            return result;
        }
    }
}
