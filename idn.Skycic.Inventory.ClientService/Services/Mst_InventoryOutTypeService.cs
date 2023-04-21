using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_InventoryOutTypeService : ClientServiceBase<Mst_InvOutType>
    {
        public static Mst_InventoryOutTypeService Instance
        {
            get
            {
                return GetInstance<Mst_InventoryOutTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstInvOutType";
            }
        }

        public RT_Mst_InvOutType WA_Mst_InvOutType_Get(RQ_Mst_InvOutType objRQ_Mst_InvOutType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InvOutType, RQ_Mst_InvOutType>("MstInvOutType", "WA_Mst_InvOutType_Get", new { }, objRQ_Mst_InvOutType, addressAPIs);
            return result;
        }
    }
}
