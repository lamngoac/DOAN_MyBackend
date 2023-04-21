using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_PartTypeService : ClientServiceBase<Mst_PartType>
    {
        public static Mst_PartTypeService Instance
        {
            get
            {
                return GetInstance<Mst_PartTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstPartType";
            }
        }

        public RT_Mst_PartType WA_Mst_PartType_Get(RQ_Mst_PartType objRQ_Mst_PartType, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartType, RQ_Mst_PartType>("MstPartType", "WA_Mst_PartType_Get", new { }, objRQ_Mst_PartType, addressAPIs);
            return result;
        }
        public RT_Mst_PartType WA_Mst_PartType_Create(RQ_Mst_PartType objRQ_Mst_PartType, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartType, RQ_Mst_PartType>("MstPartType", "WA_Mst_PartType_Create", new { }, objRQ_Mst_PartType, addressAPIs);
            return result;
        }
        public RT_Mst_PartType WA_Mst_PartType_Update(RQ_Mst_PartType objRQ_Mst_PartType, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartType, RQ_Mst_PartType>("MstPartType", "WA_Mst_PartType_Update", new { }, objRQ_Mst_PartType, addressAPIs);
            return result;
        }
        public RT_Mst_PartType WA_Mst_PartType_Delete(RQ_Mst_PartType objRQ_Mst_PartType, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartType, RQ_Mst_PartType>("MstPartType", "WA_Mst_PartType_Delete", new { }, objRQ_Mst_PartType, addressAPIs);
            return result;
        }
    }
}
