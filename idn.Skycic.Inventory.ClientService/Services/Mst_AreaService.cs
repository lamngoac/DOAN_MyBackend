using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_AreaService : ClientServiceBase<RT_Mst_Area>
    {
        public static Mst_AreaService Instance
        {
            get
            {
                return GetInstance<Mst_AreaService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstArea";
            }
        }

        public RT_Mst_Area WA_Mst_Area_Get(RQ_Mst_Area objRQ_Mst_Area, string addressAPIs)
        {
            var result = PostData<RT_Mst_Area, RQ_Mst_Area>(ApiControllerName, "WA_Mst_Area_Get", new { }, objRQ_Mst_Area, addressAPIs);
            return result;
        }

        public RT_Mst_Area WA_Mst_Area_Create(RQ_Mst_Area objRQ_Mst_Area, string addressAPIs)
        {
            var result = PostData<RT_Mst_Area, RQ_Mst_Area>(ApiControllerName, "WA_Mst_Area_Create", new { }, objRQ_Mst_Area, addressAPIs);
            return result;
        }

        public RT_Mst_Area WA_Mst_Area_Update(RQ_Mst_Area objRQ_Mst_Area, string addressAPIs)
        {
            var result = PostData<RT_Mst_Area, RQ_Mst_Area>(ApiControllerName, "WA_Mst_Area_Update", new { }, objRQ_Mst_Area, addressAPIs);
            return result;
        }

        public RT_Mst_Area WA_Mst_Area_Delete(RQ_Mst_Area objRQ_Mst_Area, string addressAPIs)
        {
            var result = PostData<RT_Mst_Area, RQ_Mst_Area>(ApiControllerName, "WA_Mst_Area_Delete", new { }, objRQ_Mst_Area, addressAPIs);
            return result;
        }
    }
}
