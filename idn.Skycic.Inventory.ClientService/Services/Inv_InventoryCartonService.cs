using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Inv_InventoryCartonService : ClientServiceBase<Inv_InventoryCarton>
    {
        public static Inv_InventoryCartonService Instance
        {
            get
            {
                return GetInstance<Inv_InventoryCartonService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvInventoryCarton";
            }
        }
        public RT_Inv_InventoryCarton WA_Inv_InventoryCarton_Get(RQ_Inv_InventoryCarton objRQ_Inv_InventoryCarton, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventoryCarton, RQ_Inv_InventoryCarton>("InvInventoryCarton", "WA_Inv_InventoryCarton_Get", new { }, objRQ_Inv_InventoryCarton, addressAPIs);
            return result;
        }
        public RT_Inv_InventoryCarton WA_Inv_InventoryCarton_UpdateFlagUsed(RQ_Inv_InventoryCarton objRQ_Inv_InventoryCarton, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventoryCarton, RQ_Inv_InventoryCarton>("InvInventoryCarton", "WA_Inv_InventoryCarton_UpdateFlagUsed", new { }, objRQ_Inv_InventoryCarton, addressAPIs);
            return result;
        }
    }
}
