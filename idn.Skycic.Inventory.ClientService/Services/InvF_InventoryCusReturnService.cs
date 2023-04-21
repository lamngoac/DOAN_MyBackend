using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_InventoryCusReturnService : ClientServiceBase<InvF_InventoryCusReturn>
    {
        public static InvF_InventoryCusReturnService Instance
        {
            get
            {
                return GetInstance<InvF_InventoryCusReturnService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "InvFInventoryCusReturn";
            }
        }

        public RT_InvF_InventoryCusReturn WA_InvF_InventoryCusReturn_Save(RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryCusReturn, RQ_InvF_InventoryCusReturn>("InvFInventoryCusReturn", "WA_InvF_InventoryCusReturn_Save", new { }, objRQ_InvF_InventoryCusReturn, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryCusReturn WA_InvF_InventoryCusReturn_Appr(RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryCusReturn, RQ_InvF_InventoryCusReturn>("InvFInventoryCusReturn", "WA_InvF_InventoryCusReturn_Appr", new { }, objRQ_InvF_InventoryCusReturn, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryCusReturn WA_InvF_InventoryCusReturn_Cancel(RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryCusReturn, RQ_InvF_InventoryCusReturn>("InvFInventoryCusReturn", "WA_InvF_InventoryCusReturn_Cancel", new { }, objRQ_InvF_InventoryCusReturn, addressAPIs);
            return result;
        }
        
        public RT_InvF_InventoryCusReturn WA_InvF_InventoryCusReturn_Get(RQ_InvF_InventoryCusReturn objRQ_InvF_InventoryCusReturn, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryCusReturn, RQ_InvF_InventoryCusReturn>("InvFInventoryCusReturn", "WA_InvF_InventoryCusReturn_Get", new { }, objRQ_InvF_InventoryCusReturn, addressAPIs);
            return result;
        }
    }
}
