using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class RptSv_Sys_SolutionService : ClientServiceBase<Sys_Solution>
    {
        public static RptSv_Sys_SolutionService Instance
        {
            get
            {
                return GetInstance<RptSv_Sys_SolutionService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysSolution";
            }
        }

        public RT_Sys_Solution WA_RptSv_Sys_Solution_Get(RQ_Sys_Solution objRQ_Sys_Solution, string addressAPIs)
        {
            var result = PostData<RT_Sys_Solution, RQ_Sys_Solution>("SysSolution", "WA_RptSv_Sys_Solution_Get", new { }, objRQ_Sys_Solution, addressAPIs);
            return result;
        }
    }
}
