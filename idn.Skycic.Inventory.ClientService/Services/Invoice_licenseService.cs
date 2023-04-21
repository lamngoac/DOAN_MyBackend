using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Invoice_licenseService: ClientServiceBase<Invoice_license>
    {
        public static Invoice_licenseService Instance
        {
            get
            {
                return GetInstance<Invoice_licenseService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Invoicelicense";
            }
        }

        public RT_Invoice_license WA_Invoice_license_Get(RQ_Invoice_license objRQ_Invoice_license, string addressAPIs)
        {
            var result = PostData<RT_Invoice_license, RQ_Invoice_license>("Invoicelicense", "WA_Invoice_license_Get", new { }, objRQ_Invoice_license, addressAPIs);
            return result;
        }

        public RT_Invoice_license WA_Invoice_license_IncreaseQty(RQ_Invoice_license objRQ_Invoice_license, string addressAPIs)
        {
            var result = PostData<RT_Invoice_license, RQ_Invoice_license>("Invoicelicense", "WA_Invoice_license_IncreaseQty", new { }, objRQ_Invoice_license, addressAPIs);
            return result;
        }

        public RT_Invoice_licenseCreHist WA_Invoice_licenseCreHist_Get(RQ_Invoice_licenseCreHist objRQ_Invoice_license, string addressAPIs)
        {
            var result = PostData<RT_Invoice_licenseCreHist, RQ_Invoice_licenseCreHist>("Invoicelicense", "WA_Invoice_licenseCreHist_GetAndSave", new { }, objRQ_Invoice_license, addressAPIs);
            return result;
        }
    }
}
