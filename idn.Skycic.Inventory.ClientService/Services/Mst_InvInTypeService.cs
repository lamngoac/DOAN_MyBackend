using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_InvInTypeService : ClientServiceBase<Mst_InvInType>
    {
        public static Mst_InvInTypeService Instance
        {
            get
            {
                return GetInstance<Mst_InvInTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstInvInType";
            }
        }

        public RT_Mst_InvInType WA_Mst_InvInType_Get(RQ_Mst_InvInType objRQ_Mst_InvInType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InvInType, RQ_Mst_InvInType>("MstInvInType", "WA_Mst_InvInType_Get", new { }, objRQ_Mst_InvInType, addressAPIs);
            return result;
        }
        public RT_Mst_InvInType WA_Mst_InvInType_Create(RQ_Mst_InvInType objRQ_Mst_InvInType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InvInType, RQ_Mst_InvInType>("MstInvInType", "WA_Mst_InvInType_Create", new { }, objRQ_Mst_InvInType, addressAPIs);
            return result;
        }
        public RT_Mst_InvInType WA_Mst_InvInType_Update(RQ_Mst_InvInType objRQ_Mst_InvInType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InvInType, RQ_Mst_InvInType>("MstInvInType", "WA_Mst_InvInType_Update", new { }, objRQ_Mst_InvInType, addressAPIs);
            return result;
        }
        public RT_Mst_InvInType WA_Mst_InvInType_Delete(RQ_Mst_InvInType objRQ_Mst_InvInType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InvInType, RQ_Mst_InvInType>("MstInvInType", "WA_Mst_InvInType_Delete", new { }, objRQ_Mst_InvInType, addressAPIs);
            return result;
        }
    }
}
