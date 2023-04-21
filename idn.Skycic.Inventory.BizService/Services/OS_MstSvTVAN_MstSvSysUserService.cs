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
    public class OS_MstSvTVAN_MstSvSysUserService : ClientServiceBase<OS_MstSvTVAN20_MstSv_Seq_Common>
    {
        public static OS_MstSvTVAN_MstSvSysUserService Instance
        {
            get
            {
                return GetInstance<OS_MstSvTVAN_MstSvSysUserService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MasterServer";
            }
        }

        public RT_OS_MstSvTVAN_MstSv_Sys_User WA_OS_MstSvTVAN_MstSv_Sys_User_Login(RQ_OS_MstSvTVAN_MstSv_Sys_User objRQ_OS_MstSvTVAN20_Seq_Common)
        {
            var result = PostData<RT_OS_MstSvTVAN_MstSv_Sys_User, RQ_OS_MstSvTVAN_MstSv_Sys_User>(TConst.UrlType.UrlMstSvSolution, "MasterServer", "WA_MstSv_Sys_User_Login", new { }, objRQ_OS_MstSvTVAN20_Seq_Common);
            return result;
        }

		public RT_MstSv_Sys_User WA_OS_MstSv_Sys_User_GetAccessToken(RQ_MstSv_Sys_User objRQ_OS_MstSvTVAN_MstSv_Sys_User)
		{
			var result = PostData<RT_MstSv_Sys_User, RQ_MstSv_Sys_User>(TConst.UrlType.UrlMstSvSolution, "MasterServer", "WA_MstSv_Sys_User_GetAccessToken", new { }, objRQ_OS_MstSvTVAN_MstSv_Sys_User);
			return result;
		}
	}
}
