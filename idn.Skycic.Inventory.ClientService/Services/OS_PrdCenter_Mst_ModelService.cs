using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_ModelService : ClientServiceBase<Mst_Model>
    {
        public static OS_PrdCenter_Mst_ModelService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_ModelService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstModel";
            }
        }

        public RT_Mst_Model WA_Mst_Model_Get(RQ_Mst_Model objRQ_Mst_Model, string addressAPIs)
        {
            var result = PostData<RT_Mst_Model, RQ_Mst_Model>("MstModel", "WA_Mst_Model_Get", new { }, objRQ_Mst_Model, addressAPIs);
            return result;
        }

        public RT_Mst_Model WA_Mst_Model_Create(RQ_Mst_Model objRQ_Mst_Model, string addressAPIs)
        {
            var result = PostData<RT_Mst_Model, RQ_Mst_Model>("MstModel", "WA_Mst_Model_Create", new { }, objRQ_Mst_Model, addressAPIs);
            return result;
        }

        public RT_Mst_Model WA_Mst_Model_Update(RQ_Mst_Model objRQ_Mst_Model, string addressAPIs)
        {
            var result = PostData<RT_Mst_Model, RQ_Mst_Model>("MstModel", "WA_Mst_Model_Update", new { }, objRQ_Mst_Model, addressAPIs);
            return result;
        }

        public RT_Mst_Model WA_Mst_Model_Delete(RQ_Mst_Model objRQ_Mst_Model, string addressAPIs)
        {
            var result = PostData<RT_Mst_Model, RQ_Mst_Model>("MstModel", "WA_Mst_Model_Delete", new { }, objRQ_Mst_Model, addressAPIs);
            return result;
        }
    }
}
