using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_InventoryInService : ClientServiceBase<InvF_InventoryIn>
    {
        public static InvF_InventoryInService Instance
        {
            get
            {
                return GetInstance<InvF_InventoryInService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "InvFInventoryIn";
            }
        }

        public RT_InvF_InventoryIn WA_InvF_InventoryIn_Save(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryIn, RQ_InvF_InventoryIn>("InvFInventoryIn", "WA_InvF_InventoryIn_Save", new { }, objRQ_InvF_InventoryIn, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryIn WA_InvF_InventoryIn_Appr(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryIn, RQ_InvF_InventoryIn>("InvFInventoryIn", "WA_InvF_InventoryIn_Appr", new { }, objRQ_InvF_InventoryIn, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryIn WA_InvF_InventoryIn_Cancel(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryIn, RQ_InvF_InventoryIn>("InvFInventoryIn", "WA_InvF_InventoryIn_Cancel", new { }, objRQ_InvF_InventoryIn, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryIn WA_InvF_InventoryIn_UpdAfterAppr(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryIn, RQ_InvF_InventoryIn>("InvFInventoryIn", "WA_InvF_InventoryIn_UpdAfterAppr", new { }, objRQ_InvF_InventoryIn, addressAPIs);
            return result;
        }

        public RT_InvF_InventoryIn WA_InvF_InventoryIn_Get(RQ_InvF_InventoryIn objRQ_InvF_InventoryIn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryIn, RQ_InvF_InventoryIn>("InvFInventoryIn", "WA_InvF_InventoryIn_Get", new { }, objRQ_InvF_InventoryIn, addressAPIs);
            return result;
        }
    }
}
