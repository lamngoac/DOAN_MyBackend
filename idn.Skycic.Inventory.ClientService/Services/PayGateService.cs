using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class PayGateService : ClientServicePayGate
    {
        public string WA_PmtOrd_PaymentOrderVnp_Add(string objPmtOrdPaymentOrderVnp)
        {
            var result = PmtOrd_PaymentOrderVnp_Add("PmtOrdPaymentOrderVnp", "WA_PmtOrd_PaymentOrderVnp_Add", new { }, objPmtOrdPaymentOrderVnp);
            return result;
        }
    }
}
