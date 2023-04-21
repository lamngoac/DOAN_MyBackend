using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_InventoryLevelTypeService : ClientServiceBase<Mst_InventoryLevelType>
    {
        public static Mst_InventoryLevelTypeService Instance
        {
            get
            {
                return GetInstance<Mst_InventoryLevelTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstInventoryLevelType";
            }
        }

        public RT_Mst_InventoryLevelType WA_Mst_InventoryLevelType_Get(RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InventoryLevelType, RQ_Mst_InventoryLevelType>("MstInventoryLevelType", "WA_Mst_InventoryLevelType_Get", new { }, objRQ_Mst_InventoryLevelType, addressAPIs);
            return result;
        }
        public RT_Mst_InventoryLevelType WA_Mst_InventoryLevelType_Create(RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InventoryLevelType, RQ_Mst_InventoryLevelType>("MstInventoryLevelType", "WA_Mst_InventoryLevelType_Create", new { }, objRQ_Mst_InventoryLevelType, addressAPIs);
            return result;
        }
        public RT_Mst_InventoryLevelType WA_Mst_InventoryLevelType_Update(RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InventoryLevelType, RQ_Mst_InventoryLevelType>("MstInventoryLevelType", "WA_Mst_InventoryLevelType_Update", new { }, objRQ_Mst_InventoryLevelType, addressAPIs);
            return result;
        }
        public RT_Mst_InventoryLevelType WA_Mst_InventoryLevelType_Delete(RQ_Mst_InventoryLevelType objRQ_Mst_InventoryLevelType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InventoryLevelType, RQ_Mst_InventoryLevelType>("MstInventoryLevelType", "WA_Mst_InventoryLevelType_Delete", new { }, objRQ_Mst_InventoryLevelType, addressAPIs);
            return result;
        }
    }
}
