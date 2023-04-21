using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
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

        //public RT_Sys_Object WA_Sys_Object_Get(RQ_Sys_Object objRQ_Sys_Object)
        //{
        //    var result = PostData<RT_Sys_Object, RQ_Sys_Object>("SysObject", "WA_Sys_Object_Get", new { }, objRQ_Sys_Object);
        //    return result;
        //}
    }
}
