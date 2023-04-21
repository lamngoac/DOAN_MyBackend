using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services.Inbrand
{
    public class OS_MstSv_InBrandServices : ClientServiceBase<MstSv_Sys_User>
    {
        public static OS_MstSv_InBrandServices Instance
        {
            get
            {
                return GetInstance<OS_MstSv_InBrandServices>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MasterServer";
            }
        }
        public RT_MstSv_Sys_User WA_OS_MstSv_InBrand_MstSv_Sys_User_Login(string strUrl, RQ_MstSv_Sys_User objRQ_MstSv_Sys_User)
        {
            var result = PostData<RT_MstSv_Sys_User, RQ_MstSv_Sys_User>("MasterServer", "WA_MstSv_Sys_User_Login", new { }, objRQ_MstSv_Sys_User, strUrl);
            return result;
        }   
    }
}
