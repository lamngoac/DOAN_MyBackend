using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Prd_PrdIDCustomFieldService : ClientServiceBase<Prd_PrdIDCustomField>
    {
        public static OS_PrdCenter_Prd_PrdIDCustomFieldService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Prd_PrdIDCustomFieldService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "PrdPrdIDCustomField";
            }
        }

        public RT_Prd_PrdIDCustomField WA_Prd_PrdIDCustomField_Get(RQ_Prd_PrdIDCustomField objRQ_Prd_PrdIDCustomField, string addressAPIs)
        {
            var result = PostData<RT_Prd_PrdIDCustomField, RQ_Prd_PrdIDCustomField>("PrdPrdIDCustomField", "WA_Prd_PrdIDCustomField_Get", new { }, objRQ_Prd_PrdIDCustomField, addressAPIs);
            return result;
        }

        public RT_Prd_PrdIDCustomField WA_Prd_PrdIDCustomField_Update(RQ_Prd_PrdIDCustomField objRQ_Prd_PrdIDCustomField, string addressAPIs)
        {
            var result = PostData<RT_Prd_PrdIDCustomField, RQ_Prd_PrdIDCustomField>("PrdPrdIDCustomField", "WA_Prd_PrdIDCustomField_Update", new { }, objRQ_Prd_PrdIDCustomField, addressAPIs);
            return result;
        }
    }
}
