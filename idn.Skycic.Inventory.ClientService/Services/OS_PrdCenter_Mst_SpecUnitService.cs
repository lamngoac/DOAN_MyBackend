using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_SpecUnitService : ClientServiceBase<Mst_SpecUnit>
    {
        public static OS_PrdCenter_Mst_SpecUnitService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecUnitService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecUnit";
            }
        }

        public RT_Mst_SpecUnit WA_Mst_SpecUnit_Get(RQ_Mst_SpecUnit objRQ_Mst_SpecUnit, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecUnit, RQ_Mst_SpecUnit>("MstSpecUnit", "WA_Mst_SpecUnit_Get", new { }, objRQ_Mst_SpecUnit, addressAPIs);
            return result;
        }

        public RT_Mst_SpecUnit WA_Mst_SpecUnit_Create(RQ_Mst_SpecUnit objRQ_Mst_SpecUnit, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecUnit, RQ_Mst_SpecUnit>("MstSpecUnit", "WA_Mst_SpecUnit_Create", new { }, objRQ_Mst_SpecUnit, addressAPIs);
            return result;
        }
        public RT_Mst_SpecUnit WA_Mst_SpecUnit_Update(RQ_Mst_SpecUnit objRQ_Mst_SpecUnit, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecUnit, RQ_Mst_SpecUnit>("MstSpecUnit", "WA_Mst_SpecUnit_Update", new { }, objRQ_Mst_SpecUnit, addressAPIs);
            return result;
        }
        public RT_Mst_SpecUnit WA_Mst_SpecUnit_Delete(RQ_Mst_SpecUnit objRQ_Mst_SpecUnit, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecUnit, RQ_Mst_SpecUnit>("MstSpecUnit", "WA_Mst_SpecUnit_Delete", new { }, objRQ_Mst_SpecUnit, addressAPIs);
            return result;
        }
    }
}
