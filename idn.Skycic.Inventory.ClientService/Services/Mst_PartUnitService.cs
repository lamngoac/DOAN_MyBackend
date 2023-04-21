using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_PartUnitService : ClientServiceBase<Mst_PartUnit>
    {
        public static Mst_PartUnitService Instance
        {
            get
            {
                return GetInstance<Mst_PartUnitService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstPartUnit";
            }
        }

        public RT_Mst_PartUnit WA_Mst_PartUnit_Get(RQ_Mst_PartUnit objRQ_Mst_PartUnit, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartUnit, RQ_Mst_PartUnit>("MstPartUnit", "WA_Mst_PartUnit_Get", new { }, objRQ_Mst_PartUnit, addressAPIs);
            return result;
        }
        public RT_Mst_PartUnit WA_Mst_PartUnit_Create(RQ_Mst_PartUnit objRQ_Mst_PartUnit, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartUnit, RQ_Mst_PartUnit>("MstPartUnit", "WA_Mst_PartUnit_Create", new { }, objRQ_Mst_PartUnit, addressAPIs);
            return result;
        }
        public RT_Mst_PartUnit WA_Mst_PartUnit_Update(RQ_Mst_PartUnit objRQ_Mst_PartUnit, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartUnit, RQ_Mst_PartUnit>("MstPartUnit", "WA_Mst_PartUnit_Update", new { }, objRQ_Mst_PartUnit, addressAPIs);
            return result;
        }
        public RT_Mst_PartUnit WA_Mst_PartUnit_Delete(RQ_Mst_PartUnit objRQ_Mst_PartUnit, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartUnit, RQ_Mst_PartUnit>("MstPartUnit", "WA_Mst_PartUnit_Delete", new { }, objRQ_Mst_PartUnit, addressAPIs);
            return result;
        }
    }
}
