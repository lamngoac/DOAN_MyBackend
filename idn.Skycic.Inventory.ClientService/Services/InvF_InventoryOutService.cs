using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_InventoryOutService : ClientServiceBase<InvF_InventoryOut>
    {
        public static InvF_InventoryOutService Instance
        {
            get
            {
                return GetInstance<InvF_InventoryOutService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "InvFInventoryOut";
            }
        }

        public RT_InvF_InventoryOut InvF_InventoryOut_Save(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOut, RQ_InvF_InventoryOut>("InvFInventoryOut", "WA_InvF_InventoryOut_Save", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }

        public RT_InvF_InventoryOut WA_InvF_InventoryOut_Get(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOut, RQ_InvF_InventoryOut>("InvFInventoryOut", "WA_InvF_InventoryOut_Get", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryOut InvF_InventoryOut_Appr(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOut, RQ_InvF_InventoryOut>("InvFInventoryOut", "WA_InvF_InventoryOut_Appr", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }
        public RT_InvF_InventoryOut InvF_InventoryOut_Cancel(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOut, RQ_InvF_InventoryOut>("InvFInventoryOut", "WA_InvF_InventoryOut_Cancel", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }

        public RT_InvF_InventoryOut WA_InvF_InventoryOut_SaveAndAppr_Cheo(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOut, RQ_InvF_InventoryOut>("InvFInventoryOut", "WA_InvF_InventoryOut_SaveAndAppr_Cheo", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }

        public RT_InvF_InventoryOut WA_InvF_InventoryOut_UpdProfile(RQ_InvF_InventoryOut objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_InventoryOut, RQ_InvF_InventoryOut>("InvFInventoryOut", "WA_InvF_InventoryOut_UpdProfile", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }
    }
}
