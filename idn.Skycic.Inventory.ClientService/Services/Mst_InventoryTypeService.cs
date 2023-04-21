using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_InventoryTypeService : ClientServiceBase<Mst_InventoryType>
    {
        public static Mst_InventoryTypeService Instance
        {
            get
            {
                return GetInstance<Mst_InventoryTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstInventoryType";
            }
        }

        public RT_Mst_InventoryType WA_Mst_InventoryType_Get(RQ_Mst_InventoryType objRQ_Mst_InventoryType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InventoryType, RQ_Mst_InventoryType>(ApiControllerName, "WA_Mst_InventoryType_Get", new { }, objRQ_Mst_InventoryType, addressAPIs);
            return result;
        }
        public RT_Mst_InventoryType WA_Mst_InventoryType_Create(RQ_Mst_InventoryType objRQ_Mst_InventoryType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InventoryType, RQ_Mst_InventoryType>(ApiControllerName, "WA_Mst_InventoryType_Create", new { }, objRQ_Mst_InventoryType, addressAPIs);
            return result;
        }
        public RT_Mst_InventoryType WA_Mst_InventoryType_Update(RQ_Mst_InventoryType objRQ_Mst_InventoryType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InventoryType, RQ_Mst_InventoryType>(ApiControllerName, "WA_Mst_InventoryType_Update", new { }, objRQ_Mst_InventoryType, addressAPIs);
            return result;
        }
        public RT_Mst_InventoryType WA_Mst_InventoryType_Delete(RQ_Mst_InventoryType objRQ_Mst_InventoryType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InventoryType, RQ_Mst_InventoryType>(ApiControllerName, "WA_Mst_InventoryType_Delete", new { }, objRQ_Mst_InventoryType, addressAPIs);
            return result;
        }
    }
}
