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
    public class OS_MstSvTVAN_MstSv_Mst_NetworkService : ClientServiceBase<OS_MstSvTVAN_MstSv_Mst_NetworkService>
    {
        public static OS_MstSvTVAN_MstSv_Mst_NetworkService Instance
        {
            get
            {
                return GetInstance<OS_MstSvTVAN_MstSv_Mst_NetworkService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSvMstNetwork";
            }
        }

        public RT_OS_MstSvTVAN_MstSv_Mst_Network WA_OS_MstSvTVAN_MstSv_Mst_Network_GetByMST(RQ_OS_MstSvTVAN_MstSv_Mst_Network objRQ_OS_MstSvTVAN_MstSv_Mst_Network)
        {
            var result = PostData<RT_OS_MstSvTVAN_MstSv_Mst_Network, RQ_OS_MstSvTVAN_MstSv_Mst_Network>(TConst.UrlType.UrlMstSvSolution, "MstSvMstNetwork", "WA_MstSv_Mst_Network_GetByMST", new { }, objRQ_OS_MstSvTVAN_MstSv_Mst_Network);
            return result;
        }
    }
}
