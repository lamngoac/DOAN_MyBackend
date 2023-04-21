using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class RptSv_Sys_UserService : ClientServiceBase<RptSv_Sys_User>
    {
        public static RptSv_Sys_UserService Instance
        {
            get
            {
                return GetInstance<RptSv_Sys_UserService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "RptSvSysUser";
            }
        }

        public RT_RptSv_Sys_User WA_RptSv_Sys_User_Login(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_User, RQ_RptSv_Sys_User>("RptSvSysUser", "WA_RptSv_Sys_User_Login", new { }, objRQ_RptSv_Sys_User, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_User WA_RptSv_Sys_User_Get(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_User, RQ_RptSv_Sys_User>("RptSvSysUser", "WA_RptSv_Sys_User_Get", new { }, objRQ_RptSv_Sys_User, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_User WA_RptSv_Sys_User_GetForCurrentUser(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_User, RQ_RptSv_Sys_User>("RptSvSysUser", "WA_RptSv_Sys_User_GetForCurrentUser", new { }, objRQ_RptSv_Sys_User, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_User WA_RptSv_Sys_User_ChangePassword(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_User, RQ_RptSv_Sys_User>("RptSvSysUser", "WA_RptSv_Sys_User_ChangePassword", new { }, objRQ_RptSv_Sys_User, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_User WA_RptSv_Sys_User_Create(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_User, RQ_RptSv_Sys_User>("RptSvSysUser", "WA_RptSv_Sys_User_Create", new { }, objRQ_RptSv_Sys_User, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_User WA_RptSv_Sys_User_Update(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_User, RQ_RptSv_Sys_User>("RptSvSysUser", "WA_RptSv_Sys_User_Update", new { }, objRQ_RptSv_Sys_User, addressAPIs);
            return result;
        }

        public RT_RptSv_Sys_User WA_RptSv_Sys_User_Delete(RQ_RptSv_Sys_User objRQ_RptSv_Sys_User, string addressAPIs)
        {
            var result = PostData<RT_RptSv_Sys_User, RQ_RptSv_Sys_User>("RptSvSysUser", "WA_RptSv_Sys_User_Delete", new { }, objRQ_RptSv_Sys_User, addressAPIs);
            return result;
        }
    }
}
