using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Prd_DynamicFieldService : ClientServiceBase<RT_Prd_DynamicField>
    {
        public static Prd_DynamicFieldService Instance
        {
            get
            {
                return GetInstance<Prd_DynamicFieldService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "PrdDynamicField";
            }
        }

        public RT_Prd_DynamicField WA_Prd_DynamicField_Get(RQ_Prd_DynamicField objRQ_Prd_DynamicField, string addressAPIs)
        {
            var result = PostData<RT_Prd_DynamicField, RQ_Prd_DynamicField>(ApiControllerName, "WA_Prd_DynamicField_Get", new { }, objRQ_Prd_DynamicField, addressAPIs);
            return result;
        }

        public RT_Prd_DynamicField WA_Prd_DynamicField_Create(RQ_Prd_DynamicField objRQ_Prd_DynamicField, string addressAPIs)
        {
            var result = PostData<RT_Prd_DynamicField, RQ_Prd_DynamicField>(ApiControllerName, "WA_Prd_DynamicField_Create", new { }, objRQ_Prd_DynamicField, addressAPIs);
            return result;
        }

        public RT_Prd_DynamicField WA_Prd_DynamicField_Update(RQ_Prd_DynamicField objRQ_Prd_DynamicField, string addressAPIs)
        {
            var result = PostData<RT_Prd_DynamicField, RQ_Prd_DynamicField>(ApiControllerName, "WA_Prd_DynamicField_Update", new { }, objRQ_Prd_DynamicField, addressAPIs);
            return result;
        }

        public RT_Prd_DynamicField WA_Prd_DynamicField_Delete(RQ_Prd_DynamicField objRQ_Prd_DynamicField, string addressAPIs)
        {
            var result = PostData<RT_Prd_DynamicField, RQ_Prd_DynamicField>(ApiControllerName, "WA_Prd_DynamicField_Delete", new { }, objRQ_Prd_DynamicField, addressAPIs);
            return result;
        }
    }
}
