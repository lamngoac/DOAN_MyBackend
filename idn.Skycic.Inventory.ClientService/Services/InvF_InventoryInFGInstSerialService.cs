using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_InventoryInFGInstSerialService : ClientServiceBase<InvF_InventoryInFGInstSerial>
    {
        public static InvF_InventoryInFGInstSerialService Instance
        {
            get
            {
                return GetInstance<InvF_InventoryInFGInstSerialService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvFInventoryInFGInstSerial";
            }
        }

        public RT_InvF_InventoryInFGInstSerial WA_InvF_InventoryInFGInstSerial_Get(RQ_InvF_InventoryInFGInstSerial objRQ_InvF_InventoryInFGInstSerial, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryInFGInstSerial, RQ_InvF_InventoryInFGInstSerial>("InvFInventoryInFGInstSerial", "WA_InvF_InventoryInFGInstSerial_Get", new { }, objRQ_InvF_InventoryInFGInstSerial, addressAPIs);
            return result;
        }

    }
}
