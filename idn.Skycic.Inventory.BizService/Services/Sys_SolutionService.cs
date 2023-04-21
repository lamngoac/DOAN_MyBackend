using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class Sys_SolutionService : ClientServiceBase<Sys_Solution>
    {
        public static Sys_SolutionService Instance
        {
            get
            {
                return GetInstance<Sys_SolutionService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysSolution";
            }
        }

        //public RT_Sys_Solution WA_Sys_Solution_Get(RQ_Sys_Solution objRQ_Sys_Solution)
        //{
        //    var result = PostData<RT_Sys_Solution, RQ_Sys_Solution>("SysSolution", "WA_Sys_Solution_Get", new { }, objRQ_Sys_Solution);
        //    return result;
        //}
    }
}
