using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Invoice_CustomFieldService : ClientServiceBase<Invoice_CustomField>
    {
        public static Invoice_CustomFieldService Instance
        {
            get
            {
                return GetInstance<Invoice_CustomFieldService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvoiceCustomField";
            }
        }

        public RT_Invoice_CustomField WA_Invoice_CustomField_Get(RQ_Invoice_CustomField objRQ_Invoice_CustomField, string addressAPIs)
        {
            var result = PostData<RT_Invoice_CustomField, RQ_Invoice_CustomField>("InvoiceCustomField", "WA_Invoice_CustomField_Get", new { }, objRQ_Invoice_CustomField, addressAPIs);
            return result;
        }
        public RT_Invoice_CustomField WA_Invoice_CustomField_Create(RQ_Invoice_CustomField objRQ_Invoice_CustomField, string addressAPIs)
        {
            var result = PostData<RT_Invoice_CustomField, RQ_Invoice_CustomField>("InvoiceCustomField", "WA_Invoice_CustomField_Create", new { }, objRQ_Invoice_CustomField, addressAPIs);
            return result;
        }
        public RT_Invoice_CustomField WA_Invoice_CustomField_Update(RQ_Invoice_CustomField objRQ_Invoice_CustomField, string addressAPIs)
        {
            var result = PostData<RT_Invoice_CustomField, RQ_Invoice_CustomField>("InvoiceCustomField", "WA_Invoice_CustomField_Update", new { }, objRQ_Invoice_CustomField, addressAPIs);
            return result;
        }
        public RT_Invoice_CustomField WA_Invoice_CustomField_Delete(RQ_Invoice_CustomField objRQ_Invoice_CustomField, string addressAPIs)
        {
            var result = PostData<RT_Invoice_CustomField, RQ_Invoice_CustomField>("InvoiceCustomField", "WA_Invoice_CustomField_Delete", new { }, objRQ_Invoice_CustomField, addressAPIs);
            return result;
        }
        public RT_Invoice_CustomField WA_RptSv_Invoice_CustomField_Get(RQ_Invoice_CustomField objRQ_Invoice_CustomField, string addressAPIs)
        {
            var result = PostData<RT_Invoice_CustomField, RQ_Invoice_CustomField>("InvoiceCustomField", "WA_RptSv_Invoice_CustomField_Get", new { }, objRQ_Invoice_CustomField, addressAPIs);
            return result;
        }
    }
}
