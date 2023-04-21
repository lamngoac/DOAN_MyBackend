using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Inv_InventorySecretService : ClientServiceBase<Inv_InventorySecret>
    {
        public static Inv_InventorySecretService Instance
        {
            get
            {
                return GetInstance<Inv_InventorySecretService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvInventorySecret";
            }
        }

        public RT_Inv_InventorySecret WA_Inv_InventorySecret_Get(RQ_Inv_InventorySecret objRQ_Inv_InventorySecret, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventorySecret, RQ_Inv_InventorySecret>("InvInventorySecret", "WA_Inv_InventorySecret_Get", new { }, objRQ_Inv_InventorySecret, addressAPIs);
            return result;
        }

        public RT_Inv_InventorySecret WA_Inv_InventorySecret_UpdateFlagUsed(RQ_Inv_InventorySecret objRQ_Inv_InventorySecret, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventorySecret, RQ_Inv_InventorySecret>("InvInventorySecret", "WA_Inv_InventorySecret_UpdateFlagUsed", new { }, objRQ_Inv_InventorySecret, addressAPIs);
            return result;
        }
    }
}
