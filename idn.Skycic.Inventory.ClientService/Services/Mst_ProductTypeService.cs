using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_ProductTypeService: ClientServiceBase<RT_Mst_ProductType>
    {
        public static Mst_ProductTypeService Instance
        {
            get
            {
                return GetInstance<Mst_ProductTypeService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstProductType";
            }
        }

        public RT_Mst_ProductType WA_Mst_ProductType_Get(RQ_Mst_ProductType objRQ_Mst_ProductType, string addressAPIs)
        {
            var result = PostData<RT_Mst_ProductType, RQ_Mst_ProductType>(ApiControllerName, "WA_Mst_ProductType_Get", new { }, objRQ_Mst_ProductType, addressAPIs);
            return result;
        }
    }
}
