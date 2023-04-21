using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Rpt_Inv_InventoryBalanceService : ClientServiceBase<Rpt_Inv_InventoryBalance>
    {
        public static Rpt_Inv_InventoryBalanceService Instance
        {
            get
            {
                return GetInstance<Rpt_Inv_InventoryBalanceService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }

        public RT_Rpt_Inv_InventoryBalance WA_Rpt_Inv_InventoryBalance(RQ_Rpt_Inv_InventoryBalance objRQ_Rpt_Inv_InventoryBalance, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Inv_InventoryBalance, RQ_Rpt_Inv_InventoryBalance>(ApiControllerName, "WA_Rpt_Inv_InventoryBalance", new { }, objRQ_Rpt_Inv_InventoryBalance, addressAPIs);
            return result;
        }
    }
}
