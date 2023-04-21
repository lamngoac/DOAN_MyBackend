using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_InvoiceTypeService : ClientServiceBase<Mst_InvoiceType>
    {
        public static Mst_InvoiceTypeService Instance
        {
            get
            {
                return GetInstance<Mst_InvoiceTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstInvoiceType";
            }
        }

        public RT_Mst_InvoiceType WA_Mst_InvoiceType_Get(RQ_Mst_InvoiceType objRQ_Mst_InvoiceType, string addressAPIs)
        {
            var result = PostData<RT_Mst_InvoiceType, RQ_Mst_InvoiceType>("MstInvoiceType", "WA_Mst_InvoiceType_Get", new { }, objRQ_Mst_InvoiceType, addressAPIs);
            return result;
        }
    }
}
