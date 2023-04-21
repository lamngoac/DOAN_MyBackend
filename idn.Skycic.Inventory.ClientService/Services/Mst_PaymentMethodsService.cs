using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_PaymentMethodsService : ClientServiceBase<Mst_PaymentMethods>
    {
        public static Mst_PaymentMethodsService Instance
        {
            get
            {
                return GetInstance<Mst_PaymentMethodsService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstPaymentMethods";
            }
        }

        public RT_Mst_PaymentMethods WA_Mst_PaymentMethods_Get(RQ_Mst_PaymentMethods objRQ_Mst_PaymentMethods, string addressAPIs)
        {
            var result = PostData<RT_Mst_PaymentMethods, RQ_Mst_PaymentMethods>("MstPaymentMethods", "WA_Mst_PaymentMethods_Get", new { }, objRQ_Mst_PaymentMethods, addressAPIs);
            return result;
        }
    }
}
