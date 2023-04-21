using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;


namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_Ord_OrderPDService : ClientServiceBase<OS_Ord_OrderPD>
    {
        public static OS_Ord_OrderPDService Instance
        {
            get
            {
                return GetInstance<OS_Ord_OrderPDService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "OrdOrderPD";
            }
        }

        public RT_OS_Ord_OrderPD WA_Ord_OrderPD_Get(RQ_OS_Ord_OrderPD objRQ_OS_Ord_OrderPD, string addressAPIs)
        {
            var result = PostData<RT_OS_Ord_OrderPD, RQ_OS_Ord_OrderPD>("OrdOrderPD", "WA_Ord_OrderPD_Get", new { }, objRQ_OS_Ord_OrderPD, addressAPIs);
            return result;
        }
    }
}
