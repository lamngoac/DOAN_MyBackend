using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_WardService : ClientServiceBase<RT_Mst_Ward>
    {
        public static Mst_WardService Instance
        {
            get
            {
                return GetInstance<Mst_WardService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstWard";
            }
        }

        public RT_Mst_Ward WA_Mst_Ward_Get(RQ_Mst_Ward objRQ_Mst_Ward, string addressAPIs)
        {
            var result = PostData<RT_Mst_Ward, RQ_Mst_Ward>(ApiControllerName, "WA_Mst_Ward_Get", new { }, objRQ_Mst_Ward, addressAPIs);
            return result;
        }

        public RT_Mst_Ward WA_Mst_Ward_Create(RQ_Mst_Ward objRQ_Mst_Ward, string addressAPIs)
        {
            var result = PostData<RT_Mst_Ward, RQ_Mst_Ward>(ApiControllerName, "WA_Mst_Ward_Create", new { }, objRQ_Mst_Ward, addressAPIs);
            return result;
        }

        public RT_Mst_Ward WA_Mst_Ward_Update(RQ_Mst_Ward objRQ_Mst_Ward, string addressAPIs)
        {
            var result = PostData<RT_Mst_Ward, RQ_Mst_Ward>(ApiControllerName, "WA_Mst_Ward_Update", new { }, objRQ_Mst_Ward, addressAPIs);
            return result;
        }

        public RT_Mst_Ward WA_Mst_Ward_Delete(RQ_Mst_Ward objRQ_Mst_Ward, string addressAPIs)
        {
            var result = PostData<RT_Mst_Ward, RQ_Mst_Ward>(ApiControllerName, "WA_Mst_Ward_Delete", new { }, objRQ_Mst_Ward, addressAPIs);
            return result;
        }
    }
}
