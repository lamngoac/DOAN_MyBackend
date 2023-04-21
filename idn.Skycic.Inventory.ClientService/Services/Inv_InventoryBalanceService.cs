using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Inv_InventoryBalanceService : ClientServiceBase<Inv_InventoryBalance>
    {
        public static Inv_InventoryBalanceService Instance
        {
            get
            {
                return GetInstance<Inv_InventoryBalanceService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvInventoryBalance";
            }
        }
        public RT_Inv_InventoryBalance WA_Inv_InventoryBalance_Get(RQ_Inv_InventoryBalance objRQ_Inv_InventoryBalance, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventoryBalance, RQ_Inv_InventoryBalance>("InvInventoryBalance", "WA_Inv_InventoryBalance_Get", new { }, objRQ_Inv_InventoryBalance, addressAPIs);
            return result;
        }
    }
}
