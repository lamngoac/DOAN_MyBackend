using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_PartMaterialTypeService : ClientServiceBase<Mst_PartMaterialType>
    {
        public static Mst_PartMaterialTypeService Instance
        {
            get
            {
                return GetInstance<Mst_PartMaterialTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstPartMaterialType";
            }
        }

        public RT_Mst_PartMaterialType WA_Mst_PartMaterialType_Get(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartMaterialType, RQ_Mst_PartMaterialType>("MstPartMaterialType", "WA_Mst_PartMaterialType_Get", new { }, objRQ_Mst_PartMaterialType, addressAPIs);
            return result;
        }
        public RT_Mst_PartMaterialType WA_Mst_PartMaterialType_Create(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartMaterialType, RQ_Mst_PartMaterialType>("MstPartMaterialType", "WA_Mst_PartMaterialType_Create", new { }, objRQ_Mst_PartMaterialType, addressAPIs);
            return result;
        }
        public RT_Mst_PartMaterialType WA_Mst_PartMaterialType_Update(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartMaterialType, RQ_Mst_PartMaterialType>("MstPartMaterialType", "WA_Mst_PartMaterialType_Update", new { }, objRQ_Mst_PartMaterialType, addressAPIs);
            return result;
        }
        public RT_Mst_PartMaterialType WA_Mst_PartMaterialType_Delete(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType, string addressAPIs)
        {
            var result = PostData<RT_Mst_PartMaterialType, RQ_Mst_PartMaterialType>("MstPartMaterialType", "WA_Mst_PartMaterialType_Delete", new { }, objRQ_Mst_PartMaterialType, addressAPIs);
            return result;
        }
    }
}
