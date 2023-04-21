using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class OS_TVAN_Invoice_InvoiceInputService : ClientServiceBase<OS_TVAN_Invoice_InvoiceInputService>
    {
        public static OS_TVAN_Invoice_InvoiceInputService Instance
        {
            get
            {
                return GetInstance<OS_TVAN_Invoice_InvoiceInputService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvoiceInvoiceInput";
            }
        }

        public RT_Invoice_InvoiceInput WA_OS_TVAN_Invoice_InvoiceInput_Save(string strNetWorkUrl, RQ_Invoice_InvoiceInput objRQ_Invoice_InvoiceInput)
        {
            var result = MstSvRoute_PostData<RT_Invoice_InvoiceInput, RQ_Invoice_InvoiceInput>(strNetWorkUrl, "InvoiceInvoiceInput", "WA_Invoice_InvoiceInput_Save", new { }, objRQ_Invoice_InvoiceInput);
            return result;
        }

        public RT_Invoice_InvoiceInput WA_OS_TVAN_Invoice_InvoiceInput_Delete(string strNetWorkUrl, RQ_Invoice_InvoiceInput objRQ_Invoice_InvoiceInput)
        {
            var result = MstSvRoute_PostData<RT_Invoice_InvoiceInput, RQ_Invoice_InvoiceInput>(strNetWorkUrl, "InvoiceInvoiceInput", "WA_Invoice_InvoiceInput_Delete", new { }, objRQ_Invoice_InvoiceInput);
            return result;
        }
    }
}
