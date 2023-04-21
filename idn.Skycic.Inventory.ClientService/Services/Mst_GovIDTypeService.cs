using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_GovIDTypeService : ClientServiceBase<Mst_GovIDType>
    {
        public static Mst_GovIDTypeService Instance
        {
            get
            {
                return GetInstance<Mst_GovIDTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstGovIDType";
            }
        }

        public RT_Mst_GovIDType WA_Mst_GovIDType_Get(RQ_Mst_GovIDType objRQ_Mst_GovIDType, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovIDType, RQ_Mst_GovIDType>("MstGovIDType", "WA_Mst_GovIDType_Get", new { }, objRQ_Mst_GovIDType, addressAPIs);
            return result;
        }
        //ReportServer
        public RT_Mst_GovIDType WA_RptSv_Mst_GovIDType_Get(RQ_Mst_GovIDType objRQ_Mst_GovIDType, string addressAPIs)
        {
            var result = PostData<RT_Mst_GovIDType, RQ_Mst_GovIDType>("MstGovIDType", "WA_RptSv_Mst_GovIDType_Get", new { }, objRQ_Mst_GovIDType, addressAPIs);
            return result;
        }
    }
}
