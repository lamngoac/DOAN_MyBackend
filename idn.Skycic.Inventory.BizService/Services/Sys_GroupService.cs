using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;
using TUtils = idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class Sys_GroupService : ClientServiceBase<Sys_Group>
    {
        public static Sys_GroupService Instance
        {
            get
            {
                return GetInstance<Sys_GroupService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysGroup";
            }
        }

        //public RT_Sys_Group WA_Sys_Group_Get(RQ_Sys_Group objRQ_Sys_Group)
        //{
        //    var result = PostData<RT_Sys_Group, RQ_Sys_Group>("SysGroup", "WA_Sys_Group_Get", new { }, objRQ_Sys_Group);
        //    return result;
        //}

        //public RT_Sys_Group WA_Sys_Group_Create(RQ_Sys_Group objRQ_Sys_Group)
        //{
        //    var result = PostData<RT_Sys_Group, RQ_Sys_Group>("SysGroup", "WA_Sys_Group_Create", new { }, objRQ_Sys_Group);
        //    return result;
        //}

        //public RT_Sys_Group WA_Sys_Group_Update(RQ_Sys_Group objRQ_Sys_Group)
        //{
        //    var result = PostData<RT_Sys_Group, RQ_Sys_Group>("SysGroup", "WA_Sys_Group_Update", new { }, objRQ_Sys_Group);
        //    return result;
        //}

        //public RT_Sys_Group WA_Sys_Group_Delete(RQ_Sys_Group objRQ_Sys_Group)
        //{
        //    var result = PostData<RT_Sys_Group, RQ_Sys_Group>("SysGroup", "WA_Sys_Group_Delete", new { }, objRQ_Sys_Group);
        //    return result;
        //}
    }
}
