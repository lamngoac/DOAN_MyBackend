using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class RptSv_Sys_ObjectService : ClientServiceBase<RptSv_Sys_Object>
    {
        public static RptSv_Sys_ObjectService Instance
        {
            get
            {
                return GetInstance<RptSv_Sys_ObjectService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysObject";
            }
        }

        public RT_RptSv_Sys_Object WA_RptSv_Sys_Object_Get(RQ_RptSv_Sys_Object objRQ_Sys_Object, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_Object, RQ_RptSv_Sys_Object>("SysObject", "WA_RptSv_Sys_Object_Get", new { }, objRQ_Sys_Object, addressAPIs);
            return result;
        }
    }
}
