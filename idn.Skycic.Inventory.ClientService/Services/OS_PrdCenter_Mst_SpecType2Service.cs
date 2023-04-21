using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class OS_PrdCenter_Mst_SpecType2Service : ClientServiceBase<Mst_SpecType2>
    {
        public static OS_PrdCenter_Mst_SpecType2Service Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecType2Service>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecType2";
            }
        }

        public RT_Mst_SpecType2 WA_Mst_SpecType2_Get(RQ_Mst_SpecType2 objRQ_Mst_SpecType2, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecType2, RQ_Mst_SpecType2>("MstSpecType2", "WA_Mst_SpecType2_Get", new { }, objRQ_Mst_SpecType2, addressAPIs);
            return result;
        }

        public RT_Mst_SpecType2 WA_Mst_SpecType2_Create(RQ_Mst_SpecType2 objRQ_Mst_SpecType2, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecType2, RQ_Mst_SpecType2>("MstSpecType2", "WA_Mst_SpecType2_Create", new { }, objRQ_Mst_SpecType2, addressAPIs);
            return result;
        }

        public RT_Mst_SpecType2 WA_Mst_SpecType2_Update(RQ_Mst_SpecType2 objRQ_Mst_SpecType2, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecType2, RQ_Mst_SpecType2>("MstSpecType2", "WA_Mst_SpecType2_Update", new { }, objRQ_Mst_SpecType2, addressAPIs);
            return result;
        }

        public RT_Mst_SpecType2 WA_Mst_SpecType2_Delete(RQ_Mst_SpecType2 objRQ_Mst_SpecType2, string addressAPIs)
        {
            var result = PostData<RT_Mst_SpecType2, RQ_Mst_SpecType2>("MstSpecType2", "WA_Mst_SpecType2_Delete", new { }, objRQ_Mst_SpecType2, addressAPIs);
            return result;
        }
    }
}
