using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class RptSv_Sys_AccessService : ClientServiceBase<RptSv_Sys_Access>
    {
        public static RptSv_Sys_AccessService Instance
        {
            get
            {
                return GetInstance<RptSv_Sys_AccessService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "RptSvSysAccess";
            }
        }
        public RT_RptSv_Sys_Access WA_RptSv_Sys_Access_Get(RQ_RptSv_Sys_Access objRQ_Sys_Access, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_Access, RQ_RptSv_Sys_Access>("RptSvSysAccess", "WA_RptSv_Sys_Access_Get", new { }, objRQ_Sys_Access, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_Access WA_RptSv_Sys_Access_Save(RQ_RptSv_Sys_Access objRQ_Sys_Access, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_Access, RQ_RptSv_Sys_Access>("RptSvSysAccess", "WA_RptSv_Sys_Access_Save", new { }, objRQ_Sys_Access, addressAPIs);
            return result;
        }
    }
}
