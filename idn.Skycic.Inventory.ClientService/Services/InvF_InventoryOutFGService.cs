using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_InventoryOutFGService : ClientServiceBase<InvF_InventoryOutFG>
    {
        public static InvF_InventoryOutFGService Instance
        {
            get
            {
                return GetInstance<InvF_InventoryOutFGService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvFInventoryOutFG";
            }
        }
        public RT_InvF_InventoryOutFG WA_InvF_InventoryOutFG_Get(RQ_InvF_InventoryOutFG objRQ_InvF_InventoryOutFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOutFG, RQ_InvF_InventoryOutFG>("InvFInventoryOutFG", "WA_InvF_InventoryOutFG_Get", new { }, objRQ_InvF_InventoryOutFG, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryOutFG WA_InvF_InventoryOutFG_Approve(RQ_InvF_InventoryOutFG objRQ_InvF_InventoryOutFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOutFG, RQ_InvF_InventoryOutFG>("InvFInventoryOutFG", "WA_InvF_InventoryOutFG_Approve", new { }, objRQ_InvF_InventoryOutFG, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryOutFG WA_InvF_InventoryOutFG_Save(RQ_InvF_InventoryOutFG objRQ_InvF_InventoryOutFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOutFG, RQ_InvF_InventoryOutFG>("InvFInventoryOutFG", "WA_InvF_InventoryOutFG_Save", new { }, objRQ_InvF_InventoryOutFG, addressAPIs);
            return result;
        }
        
            public RT_InvF_InventoryOutFG WA_InvF_InventoryOutFGInstSerial_Get(RQ_InvF_InventoryOutFG objRQ_InvF_InventoryOutFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOutFG, RQ_InvF_InventoryOutFG>("InvFInventoryOutFG", "WA_InvF_InventoryOutFGInstSerial_Get", new { }, objRQ_InvF_InventoryOutFG, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryOutFGInstSerial WA_InvF_InventoryOutFGInstSerial_Get(RQ_InvF_InventoryOutFGInstSerial objRQ_InvF_InventoryOutFG, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOutFGInstSerial, RQ_InvF_InventoryOutFGInstSerial>("InvFInventoryOutFG", "WA_InvF_InventoryOutFGInstSerial_Get", new { }, objRQ_InvF_InventoryOutFG, addressAPIs);
            return result;
        }
    }
}
