using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_SpecCustomFieldService : ClientServiceBase<Mst_SpecCustomField>
    {
        public static OS_PrdCenter_Mst_SpecCustomFieldService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecCustomFieldService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecCustomField";
            }
        }

        public RT_Mst_SpecCustomField WA_Mst_SpecCustomField_Get(RQ_Mst_SpecCustomField objRQ_Mst_SpecCustomField, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecCustomField, RQ_Mst_SpecCustomField>("MstSpecCustomField", "WA_Mst_SpecCustomField_Get", new { }, objRQ_Mst_SpecCustomField, addressAPIs);
            return result;
        }

        public RT_Mst_SpecCustomField WA_Mst_SpecCustomField_Update(RQ_Mst_SpecCustomField objRQ_Mst_SpecCustomField, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecCustomField, RQ_Mst_SpecCustomField>("MstSpecCustomField", "WA_Mst_SpecCustomField_Update", new { }, objRQ_Mst_SpecCustomField, addressAPIs);
            return result;
        }
    }
}
