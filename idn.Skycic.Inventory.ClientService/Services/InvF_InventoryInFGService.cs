using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_InventoryInFGService : ClientServiceBase<InvF_InventoryInFG>
    {
        public static InvF_InventoryInFGService Instance
        {
            get
            {
                return GetInstance<InvF_InventoryInFGService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvFInventoryInFG";
            }
        }
        public RT_InvF_InventoryInFG WA_InvF_InventoryInFG_Get(RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryInFG, RQ_InvF_InventoryInFG>("InvFInventoryInFG", "WA_InvF_InventoryInFG_Get", new { }, objRQ_InvF_InventoryInFG, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryInFG WA_InvF_InventoryInFG_Approve(RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryInFG, RQ_InvF_InventoryInFG>("InvFInventoryInFG", "WA_InvF_InventoryInFG_Approve", new { }, objRQ_InvF_InventoryInFG, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryInFG WA_InvF_InventoryInFG_Save(RQ_InvF_InventoryInFG objRQ_InvF_InventoryInFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryInFG, RQ_InvF_InventoryInFG>("InvFInventoryInFG", "WA_InvF_InventoryInFG_Save", new { }, objRQ_InvF_InventoryInFG, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryInFGInstSerial WA_InvF_InventoryInFGInstSerial_Get(RQ_InvF_InventoryInFGInstSerial objRQ_InvF_InventoryInFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryInFGInstSerial, RQ_InvF_InventoryInFGInstSerial>("InvFInventoryInFG", "WA_InvF_InventoryInFGInstSerial_Get", new { }, objRQ_InvF_InventoryInFG, addressAPIs);
            return result;
        }
    }
}
