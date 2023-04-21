using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_UnitService : ClientServiceBase<Mst_Unit>
    {
        public static OS_PrdCenter_Mst_UnitService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_UnitService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstUnit";
            }
        }

        public RT_Mst_Unit WA_Mst_Unit_Get(RQ_Mst_Unit objRQ_Mst_Unit, string addressAPIs)
        {
            var result = PostData<RT_Mst_Unit, RQ_Mst_Unit>("MstUnit", "WA_Mst_Unit_Get", new { }, objRQ_Mst_Unit, addressAPIs);
            return result;
        }

        public RT_Mst_Unit WA_Mst_Unit_Create(RQ_Mst_Unit objRQ_Mst_Unit, string addressAPIs)
        {
            var result = PostData<RT_Mst_Unit, RQ_Mst_Unit>("MstUnit", "WA_Mst_Unit_Create", new { }, objRQ_Mst_Unit, addressAPIs);
            return result;
        }

        public RT_Mst_Unit WA_Mst_Unit_Update(RQ_Mst_Unit objRQ_Mst_Unit, string addressAPIs)
        {
            var result = PostData<RT_Mst_Unit, RQ_Mst_Unit>("MstUnit", "WA_Mst_Unit_Update", new { }, objRQ_Mst_Unit, addressAPIs);
            return result;
        }

        public RT_Mst_Unit WA_Mst_Unit_Delete(RQ_Mst_Unit objRQ_Mst_Unit, string addressAPIs)
        {
            var result = PostData<RT_Mst_Unit, RQ_Mst_Unit>("MstUnit", "WA_Mst_Unit_Delete", new { }, objRQ_Mst_Unit, addressAPIs);
            return result;
        }
    }
}
