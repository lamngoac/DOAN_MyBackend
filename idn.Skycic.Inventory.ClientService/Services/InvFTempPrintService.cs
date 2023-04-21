using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService
{
    public class InvFTempPrintService : ClientServiceBase<InvF_TempPrint>
    {
        public static InvFTempPrintService Instance
        {
            get
            {
                return GetInstance<InvFTempPrintService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvFTempPrint";
            }
        }

        public RT_InvF_TempPrint WA_InvF_TempPrint_Get(RQ_InvF_TempPrint objRQ_InvF_TempPrint, string addressAPIs)
        {
            var result = PostData<RT_InvF_TempPrint, RQ_InvF_TempPrint>("InvFTempPrint", "WA_InvF_TempPrint_Get", new { }, objRQ_InvF_TempPrint, addressAPIs);
            return result;
        }
        public RT_InvF_TempPrint WA_InvF_TempPrint_Save(RQ_InvF_TempPrint objRQ_InvF_TempPrint, string addressAPIs)
        {
            var result = PostData<RT_InvF_TempPrint, RQ_InvF_TempPrint>("InvFTempPrint", "WA_InvF_TempPrint_Save", new { }, objRQ_InvF_TempPrint, addressAPIs);
            return result;
        }
    }
}
