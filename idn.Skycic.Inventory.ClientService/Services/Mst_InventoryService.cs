using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_InventoryService : ClientServiceBase<Mst_Inventory>
    {
        public static Mst_InventoryService Instance
        {
            get
            {
                return GetInstance<Mst_InventoryService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstInventory";
            }
        }

        public RT_Mst_Inventory WA_Mst_Inventory_Get(RQ_Mst_Inventory objRQ_Mst_Inventory, string addressAPIs)
        {
            var result = PostData<RT_Mst_Inventory, RQ_Mst_Inventory>("MstInventory", "WA_Mst_Inventory_Get", new { }, objRQ_Mst_Inventory, addressAPIs);
            return result;
        }

        public RT_Mst_Inventory WA_Mst_Inventory_Create(RQ_Mst_Inventory objRQ_Mst_Inventory, string addressAPIs)
        {
            var result = PostData<RT_Mst_Inventory, RQ_Mst_Inventory>("MstInventory", "WA_Mst_Inventory_Create", new { }, objRQ_Mst_Inventory, addressAPIs);
            return result;
        }

        public RT_Mst_Inventory WA_Mst_Inventory_Update(RQ_Mst_Inventory objRQ_Mst_Inventory, string addressAPIs)
        {
            var result = PostData<RT_Mst_Inventory, RQ_Mst_Inventory>("MstInventory", "WA_Mst_Inventory_Update", new { }, objRQ_Mst_Inventory, addressAPIs);
            return result;
        }

        public RT_Mst_Inventory WA_Mst_Inventory_Delete(RQ_Mst_Inventory objRQ_Mst_Inventory, string addressAPIs)
        {
            var result = PostData<RT_Mst_Inventory, RQ_Mst_Inventory>("MstInventory", "WA_Mst_Inventory_Delete", new { }, objRQ_Mst_Inventory, addressAPIs);
            return result;
        }

        public RT_Mst_Inventory WA_Mst_Inventory_GetForUserMapInv(RQ_Mst_Inventory objRQ_Mst_Inventory, string addressAPIs)
        {
            var result = PostData<RT_Mst_Inventory, RQ_Mst_Inventory>("MstInventory", "WA_Mst_Inventory_GetForUserMapInv", new { }, objRQ_Mst_Inventory, addressAPIs);
            return result;
        }
    }
}
