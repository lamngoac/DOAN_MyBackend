using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Inv_InventoryBoxService : ClientServiceBase<Inv_InventoryBox>
    {
        public static Inv_InventoryBoxService Instance
        {
            get
            {
                return GetInstance<Inv_InventoryBoxService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvInventoryBox";
            }
        }
        public RT_Inv_InventoryBox WA_Inv_InventoryBox_Get(RQ_Inv_InventoryBox objRQ_Inv_InventoryBox, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventoryBox, RQ_Inv_InventoryBox>("InvInventoryBox", "WA_Inv_InventoryBox_Get", new { }, objRQ_Inv_InventoryBox, addressAPIs);
            return result;
        }
        public RT_Inv_InventoryBox WA_Inv_InventoryBox_UpdateFlagUsed(RQ_Inv_InventoryBox objRQ_Inv_InventoryBox, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventoryBox, RQ_Inv_InventoryBox>("InvInventoryBox", "WA_Inv_InventoryBox_UpdateFlagUsed", new { }, objRQ_Inv_InventoryBox, addressAPIs);
            return result;
        }
    }
}
