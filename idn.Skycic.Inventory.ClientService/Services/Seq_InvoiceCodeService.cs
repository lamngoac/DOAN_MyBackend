using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Seq_InvoiceCodeService: ClientServiceBase<Seq_InvoiceCode>
    {
        public static Seq_InvoiceCodeService Instance
        {
            get
            {
                return GetInstance<Seq_InvoiceCodeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Seq";
            }
        }

        public RT_Seq_InvoiceCode WA_Seq_InvoiceCode_Get(RQ_Seq_InvoiceCode objRQ_Seq_InvoiceCode, string addressAPIs)
        {
            var result = PostData<RT_Seq_InvoiceCode, RQ_Seq_InvoiceCode>("Seq", "WA_Seq_InvoiceCode_Get", new { }, objRQ_Seq_InvoiceCode, addressAPIs);
            return result;
        }

        public RT_Seq_InvoiceCode WA_Seq_InvoiceCode_GetByAmount(RQ_Seq_InvoiceCode objRQ_Seq_InvoiceCode, string addressAPIs)
        {
            var result = PostData<RT_Seq_InvoiceCode, RQ_Seq_InvoiceCode>("Seq", "WA_Seq_InvoiceCode_GetByAmount", new { }, objRQ_Seq_InvoiceCode, addressAPIs);
            return result;
        }
    }
}
