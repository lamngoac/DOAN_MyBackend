using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUtils = idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class Sys_AccessService : ClientServiceBase<Sys_Access>
    {
        public static Sys_AccessService Instance
        {
            get
            {
                return GetInstance<Sys_AccessService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysAccess";
            }
        }

        //public RT_Sys_Access WA_Sys_Access_Get(RQ_Sys_Access objRQ_Sys_Access)
        //{
        //    var result = PostData<RT_Sys_Access, RQ_Sys_Access>("SysAccess", "WA_Sys_Access_Get", new { }, objRQ_Sys_Access);
        //    return result;
        //}

        //public RT_Sys_Access WA_Sys_Access_Save(RQ_Sys_Access objRQ_Sys_Access)
        //{
        //    var result = PostData<RT_Sys_Access, RQ_Sys_Access>("SysAccess", "WA_Sys_Access_Save", new { }, objRQ_Sys_Access);
        //    return result;
        //}
    }
}
