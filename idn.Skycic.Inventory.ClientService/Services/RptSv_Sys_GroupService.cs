using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class RptSv_Sys_GroupService : ClientServiceBase<RptSv_Sys_Group>
    {
        public static RptSv_Sys_GroupService Instance
        {
            get
            {
                return GetInstance<RptSv_Sys_GroupService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "RptSvSysGroup";
            }
        }

        public RT_RptSv_Sys_Group WA_RptSv_Sys_Group_Get(RQ_RptSv_Sys_Group objRQ_Sys_Group, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_Group, RQ_RptSv_Sys_Group>("RptSvSysGroup", "WA_RptSv_Sys_Group_Get", new { }, objRQ_Sys_Group, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_Group WA_RptSv_Sys_Group_Create(RQ_RptSv_Sys_Group objRQ_Sys_Group, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_Group, RQ_RptSv_Sys_Group>("RptSvSysGroup", "WA_RptSv_Sys_Group_Create", new { }, objRQ_Sys_Group, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_Group WA_RptSv_Sys_Group_Update(RQ_RptSv_Sys_Group objRQ_Sys_Group, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_Group, RQ_RptSv_Sys_Group>("RptSvSysGroup", "WA_RptSv_Sys_Group_Update", new { }, objRQ_Sys_Group, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_Group WA_RptSv_Sys_Group_Delete(RQ_RptSv_Sys_Group objRQ_Sys_Group, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_Group, RQ_RptSv_Sys_Group>("RptSvSysGroup", "WA_RptSv_Sys_Group_Delete", new { }, objRQ_Sys_Group, addressAPIs);
            return result;
        }
    }
}
