using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_MoveOrdService : ClientServiceBase<InvF_MoveOrd>
    {
        public static InvF_MoveOrdService Instance
        {
            get
            {
                return GetInstance<InvF_MoveOrdService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "InvFMoveOrd";
            }
        }

        public RT_InvF_MoveOrd WA_InvF_MoveOrd_Get(RQ_InvF_MoveOrd objRQ_InvF_MoveOrd, string addressAPIs)
        {
            var result = PostData<RT_InvF_MoveOrd, RQ_InvF_MoveOrd>("InvFMoveOrd", "WA_InvF_MoveOrd_Get", new { }, objRQ_InvF_MoveOrd, addressAPIs);
            return result;
        }
        public RT_InvF_MoveOrd WA_InvF_MoveOrd_Save(RQ_InvF_MoveOrd objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_MoveOrd, RQ_InvF_MoveOrd>("InvFMoveOrd", "WA_InvF_MoveOrd_Save", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }
        public RT_InvF_MoveOrd WA_InvF_MoveOrd_Appr(RQ_InvF_MoveOrd objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_MoveOrd, RQ_InvF_MoveOrd>("InvFMoveOrd", "WA_InvF_MoveOrd_Appr", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }
        public RT_InvF_MoveOrd WA_InvF_MoveOrd_Cancel(RQ_InvF_MoveOrd objRQ_InvF_InventoryOut, string addressAPIs)
        {
            var result = PostData<RT_InvF_MoveOrd, RQ_InvF_MoveOrd>("InvFMoveOrd", "WA_InvF_MoveOrd_Cancel", new { }, objRQ_InvF_InventoryOut, addressAPIs);
            return result;
        }
    }
}
