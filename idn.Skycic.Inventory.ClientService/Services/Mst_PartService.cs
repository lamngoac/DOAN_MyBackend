using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_PartService : ClientServiceBase<Mst_Part>
    {
        public static Mst_PartService Instance
        {
            get
            {
                return GetInstance<Mst_PartService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstPart";
            }
        }

        public RT_Mst_Part WA_Mst_Part_Get(RQ_Mst_Part objRQ_Mst_Part, string addressAPIs)
        {
            var result = PostData<RT_Mst_Part, RQ_Mst_Part>("MstPart", "WA_Mst_Part_Get", new { }, objRQ_Mst_Part, addressAPIs);
            return result;
        }
        public RT_Mst_Part WA_Mst_Part_Create(RQ_Mst_Part objRQ_Mst_Part, string addressAPIs)
        {
            var result = PostData<RT_Mst_Part, RQ_Mst_Part>("MstPart", "WA_Mst_Part_Create", new { }, objRQ_Mst_Part, addressAPIs);
            return result;
        }
        public RT_Mst_Part WA_Mst_Part_Update(RQ_Mst_Part objRQ_Mst_Part, string addressAPIs)
        {
            var result = PostData<RT_Mst_Part, RQ_Mst_Part>("MstPart", "WA_Mst_Part_Update", new { }, objRQ_Mst_Part, addressAPIs);
            return result;
        }
        public RT_Mst_Part WA_Mst_Part_Delete(RQ_Mst_Part objRQ_Mst_Part, string addressAPIs)
        {
            var result = PostData<RT_Mst_Part, RQ_Mst_Part>("MstPart", "WA_Mst_Part_Delete", new { }, objRQ_Mst_Part, addressAPIs);
            return result;
        }
    }
}
