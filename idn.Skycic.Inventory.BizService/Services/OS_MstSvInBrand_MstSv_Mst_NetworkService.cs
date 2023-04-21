using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class OS_MstSvInBrand_MstSv_Mst_NetworkService : ClientServiceBase<OS_MstSvInBrand_MstSv_Mst_NetworkService>
    {
        public static OS_MstSvInBrand_MstSv_Mst_NetworkService Instance
        {
            get
            {
                return GetInstance<OS_MstSvInBrand_MstSv_Mst_NetworkService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSvMstNetwork";
            }
        }

        public RT_OS_MstSvInBrand_MstSv_Mst_Network WA_OS_MstSvInBrand_MstSv_Mst_Network_GetByMST(RQ_OS_MstSvInBrand_MstSv_Mst_Network objRQ_OS_MstSvInBrand_MstSv_Mst_Network, string strUrlMstSvSolution)
        {
            var result = MstSvRoute_PostData<RT_OS_MstSvInBrand_MstSv_Mst_Network, RQ_OS_MstSvInBrand_MstSv_Mst_Network>(strUrlMstSvSolution, "MstSvMstNetwork", "WA_MstSv_Mst_Network_GetByMST", new { }, objRQ_OS_MstSvInBrand_MstSv_Mst_Network);
            return result;
        }
    }
}
