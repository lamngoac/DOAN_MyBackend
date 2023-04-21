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
    public class Sys_UserService : ClientServiceBase<Sys_User>
    {
        public static Sys_UserService Instance
        {
            get
            {
                return GetInstance<Sys_UserService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "SysUser";
            }
        }

        public RT_Sys_User WA_Sys_User_Login(RQ_Sys_User objRQ_Sys_User)
        {
            var result = PostData<RT_Sys_User, RQ_Sys_User>(TConst.UrlType.UrlPrdCenter, "SysUser", "WA_Sys_User_Login", new { }, objRQ_Sys_User);
            return result;
        }
        public RT_Sys_User WA_Sys_User_Get(RQ_Sys_User objRQ_Sys_User)
        {
            var result = PostData<RT_Sys_User, RQ_Sys_User>(TConst.UrlType.UrlPrdCenter, "SysUser", "WA_Sys_User_Get", new { }, objRQ_Sys_User);
            return result;
        }
        public RT_Sys_User WA_Sys_User_GetForCurrentUser(RQ_Sys_User objRQ_Sys_User)
        {
            var result = PostData<RT_Sys_User, RQ_Sys_User>(TConst.UrlType.UrlPrdCenter, "SysUser", "WA_Sys_User_GetForCurrentUser", new { }, objRQ_Sys_User);
            return result;
        }
        public RT_Sys_User WA_Sys_User_ChangePassword(RQ_Sys_User objRQ_Sys_User)
        {
            var result = PostData<RT_Sys_User, RQ_Sys_User>(TConst.UrlType.UrlPrdCenter, "SysUser", "WA_Sys_User_ChangePassword", new { }, objRQ_Sys_User);
            return result;
        }
        public RT_Sys_User WA_Sys_User_Create(RQ_Sys_User objRQ_Sys_User)
        {
            var result = PostData<RT_Sys_User, RQ_Sys_User>(TConst.UrlType.UrlPrdCenter, "SysUser", "WA_Sys_User_Create", new { }, objRQ_Sys_User);
            return result;
        }
        public RT_Sys_User WA_Sys_User_Update(RQ_Sys_User objRQ_Sys_User)
        {
            var result = PostData<RT_Sys_User, RQ_Sys_User>(TConst.UrlType.UrlPrdCenter, "SysUser", "WA_Sys_User_Update", new { }, objRQ_Sys_User);
            return result;
        }
        public RT_Sys_User WA_Sys_User_Delete(RQ_Sys_User objRQ_Sys_User)
        {
            var result = PostData<RT_Sys_User, RQ_Sys_User>(TConst.UrlType.UrlPrdCenter, "SysUser", "WA_Sys_User_Delete", new { }, objRQ_Sys_User);
            return result;
        }
    }
}
