using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_UserMapInventoryService: ClientServiceBase<Mst_UserMapInventory>
    {
        public static Mst_UserMapInventoryService Instance
        {
            get
            {
                return GetInstance<Mst_UserMapInventoryService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstUserMapInventory";
            }
        }
        public RT_Mst_UserMapInventory WA_Mst_UserMapInventory_Get(RQ_Mst_UserMapInventory objRQ_Mst_UserMapInventory, string addressAPIs)
        {
            var result = PostData<RT_Mst_UserMapInventory, RQ_Mst_UserMapInventory> (ApiControllerName, "WA_Mst_UserMapInventory_Get", new { }, objRQ_Mst_UserMapInventory, addressAPIs);
            return result;
        }
        public RT_Mst_UserMapInventory WA_Mst_UserMapInventory_Save(RQ_Mst_UserMapInventory objRQ_Mst_UserMapInventory, string addressAPIs)
        {
            var result = PostData<RT_Mst_UserMapInventory, RQ_Mst_UserMapInventory>(ApiControllerName, "WA_Mst_UserMapInventory_Save", new { }, objRQ_Mst_UserMapInventory, addressAPIs);
            return result;
        }
    }
}
