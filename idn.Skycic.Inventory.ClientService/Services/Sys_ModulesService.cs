using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Sys_ModulesService : ClientServiceBase<Sys_Modules>
    {
        public static Sys_ModulesService Instance
        {
            get
            {
                return GetInstance<Sys_ModulesService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysModules";
            }
        }

        public RT_Sys_Modules WA_Sys_Modules_Get(RQ_Sys_Modules objRQ_Sys_Modules, string addressAPIs)
        {
            var result = PostData<RT_Sys_Modules, RQ_Sys_Modules>("SysModules", "WA_Sys_Modules_Get", new { }, objRQ_Sys_Modules, addressAPIs);
            return result;
        }

        public RT_Sys_Modules WA_Sys_Modules_Create(RQ_Sys_Modules objRQ_Sys_Modules, string addressAPIs)
        {
            var result = PostData<RT_Sys_Modules, RQ_Sys_Modules>("SysModules", "WA_Sys_Modules_Create", new { }, objRQ_Sys_Modules, addressAPIs);
            return result;
        }

        public RT_Sys_Modules WA_Sys_Modules_Update(RQ_Sys_Modules objRQ_Sys_Modules, string addressAPIs)
        {
            var result = PostData<RT_Sys_Modules, RQ_Sys_Modules>("SysModules", "WA_Sys_Modules_Update", new { }, objRQ_Sys_Modules, addressAPIs);
            return result;
        }

        public RT_Sys_Modules WA_Sys_Modules_Delete(RQ_Sys_Modules objRQ_Sys_Modules, string addressAPIs)
        {
            var result = PostData<RT_Sys_Modules, RQ_Sys_Modules>("SysModules", "WA_Sys_Modules_Delete", new { }, objRQ_Sys_Modules, addressAPIs);
            return result;
        }

        //
        public RT_Sys_Modules WA_RptSv_Sys_Modules_Get(RQ_Sys_Modules objRQ_Sys_Modules, string addressAPIs)
        {
            var result = PostData<RT_Sys_Modules, RQ_Sys_Modules>("SysModules", "WA_RptSv_Sys_Modules_Get", new { }, objRQ_Sys_Modules, addressAPIs);
            return result;
        }

        public RT_Sys_Modules WA_RptSv_Sys_Modules_Create(RQ_Sys_Modules objRQ_Sys_Modules, string addressAPIs)
        {
            var result = PostData<RT_Sys_Modules, RQ_Sys_Modules>("SysModules", "WA_RptSv_Sys_Modules_Create", new { }, objRQ_Sys_Modules, addressAPIs);
            return result;
        }

        public RT_Sys_Modules WA_RptSv_Sys_Modules_Update(RQ_Sys_Modules objRQ_Sys_Modules, string addressAPIs)
        {
            var result = PostData<RT_Sys_Modules, RQ_Sys_Modules>("SysModules", "WA_RptSv_Sys_Modules_Update", new { }, objRQ_Sys_Modules, addressAPIs);
            return result;
        }

        public RT_Sys_Modules WA_RptSv_Sys_Modules_Delete(RQ_Sys_Modules objRQ_Sys_Modules, string addressAPIs)
        {
            var result = PostData<RT_Sys_Modules, RQ_Sys_Modules>("SysModules", "WA_RptSv_Sys_Modules_Delete", new { }, objRQ_Sys_Modules, addressAPIs);
            return result;
        }
    }
}
