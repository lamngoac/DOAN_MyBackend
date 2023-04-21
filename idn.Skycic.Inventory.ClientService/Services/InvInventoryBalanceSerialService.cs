using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvInventoryBalanceSerialService : ClientServiceBase<Inv_InventoryBalanceSerial>
    {
        public static InvInventoryBalanceSerialService Instance
        {
            get
            {
                return GetInstance<InvInventoryBalanceSerialService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvInventoryBalanceSerial";
            }
        }

        public RT_Inv_InventoryBalanceSerial WA_Inv_InventoryBalanceSerial_Get(RQ_Inv_InventoryBalanceSerial objRQ_Inv_InventoryBalanceSerial, string addressAPIs)
        {
            var result = PostData<RT_Inv_InventoryBalanceSerial, RQ_Inv_InventoryBalanceSerial>("InvInventoryBalanceSerial", "WA_Inv_InventoryBalanceSerial_Get", new { }, objRQ_Inv_InventoryBalanceSerial, addressAPIs);
            return result;
        }
    }
}
