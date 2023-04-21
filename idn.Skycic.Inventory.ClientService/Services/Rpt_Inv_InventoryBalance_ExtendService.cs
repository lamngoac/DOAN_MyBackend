using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Rpt_Inv_InventoryBalance_ExtendService : ClientServiceBase<Rpt_Inv_InventoryBalance_Extend>
    {
        public static Rpt_Inv_InventoryBalance_ExtendService Instance
        {
            get
            {
                return GetInstance<Rpt_Inv_InventoryBalance_ExtendService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }

        public RT_Rpt_Inv_InventoryBalance_Extend WA_Rpt_Inv_InventoryBalance_Extend(RQ_Rpt_Inv_InventoryBalance_Extend objRQ_Rpt_Inv_InventoryBalance_Extend, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Inv_InventoryBalance_Extend, RQ_Rpt_Inv_InventoryBalance_Extend>(ApiControllerName, "WA_Rpt_Inv_InventoryBalance_Extend", new { }, objRQ_Rpt_Inv_InventoryBalance_Extend, addressAPIs);
            return result;
        }
    }
}
