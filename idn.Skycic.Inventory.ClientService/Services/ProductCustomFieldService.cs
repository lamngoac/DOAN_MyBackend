using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class ProductCustomFieldService : ClientServiceBase<Product_CustomField>
    {
        public static ProductCustomFieldService Instance
        {
            get
            {
                return GetInstance<ProductCustomFieldService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "ProductCustomField";
            }
        }

        public RT_Product_CustomField WA_Product_CustomField_Get(RQ_Product_CustomField objRQ_Product_CustomField, string addressAPIs)
        {
            var result = PostData<RT_Product_CustomField, RQ_Product_CustomField>(ApiControllerName, "WA_Product_CustomField_Get", new { }, objRQ_Product_CustomField, addressAPIs);
            return result;
        }

        public RT_Product_CustomField WA_Product_CustomField_Update(RQ_Product_CustomField objRQ_Product_CustomField, string addressAPIs)
        {
            var result = PostData<RT_Product_CustomField, RQ_Product_CustomField>(ApiControllerName, "WA_Product_CustomField_Update", new { }, objRQ_Product_CustomField, addressAPIs);
            return result;
        }
    }
}
