using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class MstSv_Mst_NetworkService : ClientServiceBase<MstSv_Mst_Network>
    {
        public static MstSv_Mst_NetworkService Instance
        {
            get
            {
                return GetInstance<MstSv_Mst_NetworkService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSvMstNetwork";
            }
        }

        public RT_MstSv_Mst_Network WA_MstSv_Mst_Network_Gen(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network, string apiaddresstype)
        {
            var result = PostData<RT_MstSv_Mst_Network, RQ_MstSv_Mst_Network>("MstSvMstNetwork", "WA_MstSv_Mst_Network_Gen", new { }, objRQ_MstSv_Mst_Network, apiaddresstype);
            return result;
        }
    }
}
