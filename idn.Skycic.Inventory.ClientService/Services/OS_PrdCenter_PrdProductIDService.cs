using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Prd_ProductIDService : ClientServiceBase<Prd_ProductID>
    {
        public static OS_PrdCenter_Prd_ProductIDService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Prd_ProductIDService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "PrdProductID";
            }
        }

        public RT_Prd_ProductID WA_Prd_ProductID_Get(RQ_Prd_ProductID objRQ_Prd_ProductID, string addressAPIs)
        {
            var result = PostData<RT_Prd_ProductID, RQ_Prd_ProductID>("PrdProductID", "WA_Prd_ProductID_Get", new { }, objRQ_Prd_ProductID, addressAPIs);
            return result;
        }

        public RT_Prd_ProductID WA_Prd_ProductID_Update(RQ_Prd_ProductID objRQ_Prd_ProductID, string addressAPIs)
        {
            var result = PostData<RT_Prd_ProductID, RQ_Prd_ProductID>("PrdProductID", "WA_Prd_ProductID_Update", new { }, objRQ_Prd_ProductID, addressAPIs);
            return result;
        }

        public RT_Prd_ProductID WA_Prd_ProductID_Create(RQ_Prd_ProductID objRQ_Prd_ProductID, string addressAPIs)
        {
            var result = PostData<RT_Prd_ProductID, RQ_Prd_ProductID>("PrdProductID", "WA_Prd_ProductID_Create", new { }, objRQ_Prd_ProductID, addressAPIs);
            return result;
        }

        public RT_Prd_ProductID WA_Prd_ProductID_Delete(RQ_Prd_ProductID objRQ_Prd_ProductID, string addressAPIs)
        {
            var result = PostData<RT_Prd_ProductID, RQ_Prd_ProductID>("PrdProductID", "WA_Prd_ProductID_Delete", new { }, objRQ_Prd_ProductID, addressAPIs);
            return result;
        }
    }
}
