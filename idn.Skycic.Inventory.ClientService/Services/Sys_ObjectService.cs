using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Sys_ObjectService : ClientServiceBase<Sys_Object>
    {
        public static Sys_ObjectService Instance
        {
            get
            {
                return GetInstance<Sys_ObjectService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysObject";
            }
        }

        public RT_Sys_Object WA_Sys_Object_Get(RQ_Sys_Object objRQ_Sys_Object, string addressAPIs)
        {
            var result = PostData<RT_Sys_Object, RQ_Sys_Object>("SysObject", "WA_Sys_Object_Get", new { }, objRQ_Sys_Object, addressAPIs);
            return result;
        }

        public RT_Sys_Object WA_RptSv_Sys_Object_Get(RQ_Sys_Object objRQ_Sys_Object, string addressAPIs)
        {
            var result = PostData<RT_Sys_Object, RQ_Sys_Object>("SysObject", "WA_RptSv_Sys_Object_Get", new { }, objRQ_Sys_Object, addressAPIs);
            return result;
        }
    }
}
