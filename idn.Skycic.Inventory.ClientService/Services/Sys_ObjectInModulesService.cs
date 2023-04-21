using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Sys_ObjectInModulesService : ClientServiceBase<Sys_ObjectInModules>
    {
        public static Sys_ObjectInModulesService Instance
        {
            get
            {
                return GetInstance<Sys_ObjectInModulesService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysObjectInModules";
            }
        }

        public RT_Sys_ObjectInModules WA_Sys_ObjectInModules_Get(RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules, string addressAPIs)
        {
            var result = PostData<RT_Sys_ObjectInModules, RQ_Sys_ObjectInModules>("SysObjectInModules", "WA_Sys_ObjectInModules_Get", new { }, objRQ_Sys_ObjectInModules, addressAPIs);
            return result;
        }
        public RT_Sys_ObjectInModules WA_Sys_ObjectInModules_Save(RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules, string addressAPIs)
        {
            var result = PostData<RT_Sys_ObjectInModules, RQ_Sys_ObjectInModules>("SysObjectInModules", "WA_Sys_ObjectInModules_Save", new { }, objRQ_Sys_ObjectInModules, addressAPIs);
            return result;
        }

        public RT_Sys_ObjectInModules WA_RptSv_Sys_ObjectInModules_Get(RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules, string addressAPIs)
        {
            var result = PostData<RT_Sys_ObjectInModules, RQ_Sys_ObjectInModules>("SysObjectInModules", "WA_RptSv_Sys_ObjectInModules_Get", new { }, objRQ_Sys_ObjectInModules, addressAPIs);
            return result;
        }
        public RT_Sys_ObjectInModules WA_RptSv_Sys_ObjectInModules_Save(RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules, string addressAPIs)
        {
            var result = PostData<RT_Sys_ObjectInModules, RQ_Sys_ObjectInModules>("SysObjectInModules", "WA_RptSv_Sys_ObjectInModules_Save", new { }, objRQ_Sys_ObjectInModules, addressAPIs);
            return result;
        }
    }
}
