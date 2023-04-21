using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Rpt_InvF_WarehouseCardService : ClientServiceBase<Rpt_InvF_WarehouseCard>
    {
        public static Rpt_InvF_WarehouseCardService Instance
        {
            get
            {
                return GetInstance<Rpt_InvF_WarehouseCardService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }

        public RT_Rpt_InvF_WarehouseCard WA_Rpt_InvF_WarehouseCard(RQ_Rpt_InvF_WarehouseCard objRQ_Rpt_InvF_WarehouseCard, string addressAPIs)
        {
            var result = PostData<RT_Rpt_InvF_WarehouseCard, RQ_Rpt_InvF_WarehouseCard>(ApiControllerName, "WA_Rpt_InvF_WarehouseCard", new { }, objRQ_Rpt_InvF_WarehouseCard, addressAPIs);
            return result;
        }
    }
}