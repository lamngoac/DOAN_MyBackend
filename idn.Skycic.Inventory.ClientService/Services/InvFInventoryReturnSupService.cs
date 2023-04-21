using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvFInventoryReturnSupService : ClientServiceBase<InvF_InventoryReturnSup>
    {
        public static InvFInventoryReturnSupService Instance
        {
            get
            {
                return GetInstance<InvFInventoryReturnSupService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvFInventoryReturnSup";
            }
        }

        public RT_InvF_InventoryReturnSup WA_InvF_InventoryReturnSup_Get(RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSup, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryReturnSup, RQ_InvF_InventoryReturnSup>("InvFInventoryReturnSup", "WA_InvF_InventoryReturnSup_Get", new { }, objRQ_InvF_InventoryReturnSup, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryReturnSup WA_InvF_InventoryReturnSup_Save(RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSup, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryReturnSup, RQ_InvF_InventoryReturnSup>("InvFInventoryReturnSup", "WA_InvF_InventoryReturnSup_Save", new { }, objRQ_InvF_InventoryReturnSup, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryReturnSup WA_InvF_InventoryReturnSup_Appr(RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSup, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryReturnSup, RQ_InvF_InventoryReturnSup>("InvFInventoryReturnSup", "WA_InvF_InventoryReturnSup_Appr", new { }, objRQ_InvF_InventoryReturnSup, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryReturnSup WA_InvF_InventoryReturnSup_Cancel(RQ_InvF_InventoryReturnSup objRQ_InvF_InventoryReturnSup, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryReturnSup, RQ_InvF_InventoryReturnSup>("InvFInventoryReturnSup", "WA_InvF_InventoryReturnSup_Cancel", new { }, objRQ_InvF_InventoryReturnSup, addressAPIs);
            return result;
        }
    }
}
