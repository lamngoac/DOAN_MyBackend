using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_AttributeService : ClientServiceBase<RT_Mst_Attribute>
    {
        public static Mst_AttributeService Instance
        {
            get
            {
                return GetInstance<Mst_AttributeService>();
            } 
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstAttribute";
            }
        }

        public RT_Mst_Attribute WA_Mst_Attribute_Get(RQ_Mst_Attribute objRQ_Mst_Attribute, string addressAPIs)
        {
            var result = PostData<RT_Mst_Attribute, RQ_Mst_Attribute>(ApiControllerName, "WA_Mst_Attribute_Get", new { }, objRQ_Mst_Attribute, addressAPIs);
            return result;
        }

        public RT_Mst_Attribute WA_Mst_Attribute_Create(RQ_Mst_Attribute objRQ_Mst_Attribute, string addressAPIs)
        {
            var result = PostData<RT_Mst_Attribute, RQ_Mst_Attribute>(ApiControllerName, "WA_Mst_Attribute_Create", new { }, objRQ_Mst_Attribute, addressAPIs);
            return result;
        }

        public RT_Mst_Attribute WA_Mst_Attribute_Update(RQ_Mst_Attribute objRQ_Mst_Attribute, string addressAPIs)
        {
            var result = PostData<RT_Mst_Attribute, RQ_Mst_Attribute>(ApiControllerName, "WA_Mst_Attribute_Update", new { }, objRQ_Mst_Attribute, addressAPIs);
            return result;
        }

        public RT_Mst_Attribute WA_Mst_Attribute_Delete(RQ_Mst_Attribute objRQ_Mst_Attribute, string addressAPIs)
        {
            var result = PostData<RT_Mst_Attribute, RQ_Mst_Attribute>(ApiControllerName, "WA_Mst_Attribute_Delete", new { }, objRQ_Mst_Attribute, addressAPIs);
            return result;
        }
    }
}
