using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_Sys_ConfigService : ClientServiceBase<Mst_Sys_Config>
    {
        public static Mst_Sys_ConfigService Instance
        {
            get
            {
                return GetInstance<Mst_Sys_ConfigService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSysConfig";
            }
        }



        public RT_Mst_Sys_Config WA_Mst_Sys_Config_Get(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config, string addressAPIs)
        {
            var result = PostData<RT_Mst_Sys_Config, RQ_Mst_Sys_Config>(ApiControllerName, "WA_Mst_Sys_Config_Get", new { }, objRQ_Mst_Sys_Config, addressAPIs);
            return result;
        }

        public RT_Mst_Sys_Config WA_Mst_Sys_Config_Update(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config, string addressAPIs)
        {
            var result = PostData<RT_Mst_Sys_Config, RQ_Mst_Sys_Config>(ApiControllerName, "WA_Mst_Sys_Config_Update", new { }, objRQ_Mst_Sys_Config, addressAPIs);
            return result;
        }
    }
}
