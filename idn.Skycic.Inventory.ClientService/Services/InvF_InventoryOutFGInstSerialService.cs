using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_InventoryOutFGInstSerialService : ClientServiceBase<InvF_InventoryOutFGInstSerial>
    {
        public static InvF_InventoryOutFGInstSerialService Instance
        {
            get
            {
                return GetInstance<InvF_InventoryOutFGInstSerialService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvFInventoryOutFGInstSerial";
            }
        }
        
        public RT_InvF_InventoryOutFGInstSerial WA_InvF_InventoryOutFGInstSerial_Get(RQ_InvF_InventoryOutFGInstSerial objRQ_InvF_InventoryOutFGInstSerial, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOutFGInstSerial, RQ_InvF_InventoryOutFGInstSerial>("InvFInventoryOutFGInstSerial", "WA_InvF_InventoryOutFGInstSerial_Get", new { }, objRQ_InvF_InventoryOutFGInstSerial, addressAPIs);
            return result;
        }
    }
}
