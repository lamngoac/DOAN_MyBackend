using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Inv_InventoryBalanceLotService : ClientServiceBase<Inv_InventoryBalanceLot>
    {
        public static Inv_InventoryBalanceLotService Instance
        {
            get
            {
                return GetInstance<Inv_InventoryBalanceLotService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvInventoryBalanceLot";
            }
        }

        public RT_Inv_InventoryBalanceLot WA_Inv_InventoryBalanceLot_Get(RQ_Inv_InventoryBalanceLot objRQ_Inv_InventoryBalanceLot, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventoryBalanceLot, RQ_Inv_InventoryBalanceLot>("InvInventoryBalanceLot", "WA_Inv_InventoryBalanceLot_Get", new { }, objRQ_Inv_InventoryBalanceLot, addressAPIs);
            return result;
        }
    }
}
