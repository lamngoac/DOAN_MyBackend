using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_MstSvTVAN_MstSv_Sys_UserService : ClientServiceBase<OS_MstSvTVAN_MstSv_Sys_User>
    {
        public static OS_MstSvTVAN_MstSv_Sys_UserService Instance
        {
            get
            {
                return GetInstance<OS_MstSvTVAN_MstSv_Sys_UserService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "OS_MstSvTVAN_MstSv_Sys_User";
            }
        }

        public RT_OS_MstSvTVAN_MstSv_Sys_User WA_OS_MstSvTVAN_MstSv_Sys_User_Login(RQ_OS_MstSvTVAN_MstSv_Sys_User objRQ_OS_MstSvTVAN_MstSv_Sys_User, string addressAPIs)
        {
            var result = PostData<RT_OS_MstSvTVAN_MstSv_Sys_User, RQ_OS_MstSvTVAN_MstSv_Sys_User>("OS_MstSvTVAN_MstSv_Sys_User", "WA_OS_MstSvTVAN_MstSv_Sys_User_Login", new { }, objRQ_OS_MstSvTVAN_MstSv_Sys_User, addressAPIs);
            return result;
        }
    }
}
