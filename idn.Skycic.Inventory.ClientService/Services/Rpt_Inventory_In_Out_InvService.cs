using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Rpt_Inventory_In_Out_InvService : ClientServiceBase<Rpt_Inventory_In_Out_Inv>
    {
        public static Rpt_Inventory_In_Out_InvService Instance
        {
            get
            {
                return GetInstance<Rpt_Inventory_In_Out_InvService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }

        public RT_Rpt_Inventory_In_Out_Inv WA_Rpt_Inventory_In_Out_Inv(RQ_Rpt_Inventory_In_Out_Inv objRQ_Rpt_Inventory_In_Out_Inv, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Inventory_In_Out_Inv, RQ_Rpt_Inventory_In_Out_Inv>(ApiControllerName, "WA_Rpt_Inventory_In_Out_Inv", new { }, objRQ_Rpt_Inventory_In_Out_Inv, addressAPIs);
            return result;
        }
    }
}
