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
    public class OS_MstSvTVAN_Map_Network_SysOutSideService : ClientServiceBase<OS_MstSvTVAN_Map_Network_SysOutSideService>
    {
        public static OS_MstSvTVAN_Map_Network_SysOutSideService Instance
        {
            get
            {
                return GetInstance<OS_MstSvTVAN_Map_Network_SysOutSideService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MasterServer";
            }
        }

        public RT_OS_MstSvTVAN_Map_Network_SysOutSide WA_Map_Network_SysOutSide_GetBySysOS(RQ_OS_MstSvTVAN_Map_Network_SysOutSide objRQ_OS_MstSvTVAN_Map_Network_SysOutSide)
        {
            var result = PostData<RT_OS_MstSvTVAN_Map_Network_SysOutSide, RQ_OS_MstSvTVAN_Map_Network_SysOutSide>(TConst.UrlType.UrlMstSvSolution, "MasterServer", "WA_Map_Network_SysOutSide_GetBySysOS", new { }, objRQ_OS_MstSvTVAN_Map_Network_SysOutSide);
            return result;
        }
    }
}
