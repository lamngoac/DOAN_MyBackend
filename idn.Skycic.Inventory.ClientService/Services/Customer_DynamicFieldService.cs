using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Customer_DynamicFieldService : ClientServiceBase<RT_Customer_DynamicField>
    {
        public static Customer_DynamicFieldService Instance
        {
            get
            {
                return GetInstance<Customer_DynamicFieldService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "CustomerDynamicField";
            }
        }

        public RT_Customer_DynamicField WA_Customer_DynamicField_Get(RQ_Customer_DynamicField objRQ_Customer_DynamicField, string addressAPIs)
        {
            var result = PostData<RT_Customer_DynamicField, RQ_Customer_DynamicField>(ApiControllerName, "WA_Customer_DynamicField_Get", new { }, objRQ_Customer_DynamicField, addressAPIs);
            return result;
        }

        public RT_Customer_DynamicField WA_Customer_DynamicField_Create(RQ_Customer_DynamicField objRQ_Customer_DynamicField, string addressAPIs)
        {
            var result = PostData<RT_Customer_DynamicField, RQ_Customer_DynamicField>(ApiControllerName, "WA_Customer_DynamicField_Create", new { }, objRQ_Customer_DynamicField, addressAPIs);
            return result;
        }

        public RT_Customer_DynamicField WA_Customer_DynamicField_Update(RQ_Customer_DynamicField objRQ_Customer_DynamicField, string addressAPIs)
        {
            var result = PostData<RT_Customer_DynamicField, RQ_Customer_DynamicField>(ApiControllerName, "WA_Customer_DynamicField_Update", new { }, objRQ_Customer_DynamicField, addressAPIs);
            return result;
        }

        public RT_Customer_DynamicField WA_Customer_DynamicField_Delete(RQ_Customer_DynamicField objRQ_Customer_DynamicField, string addressAPIs)
        {
            var result = PostData<RT_Customer_DynamicField, RQ_Customer_DynamicField>(ApiControllerName, "WA_Customer_DynamicField_Delete", new { }, objRQ_Customer_DynamicField, addressAPIs);
            return result;
        }
    }
}
