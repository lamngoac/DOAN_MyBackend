using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_MoveOrdTypeService : ClientServiceBase<Mst_MoveOrdType>
    {
        public static Mst_MoveOrdTypeService Instance
        {
            get
            {
                return GetInstance<Mst_MoveOrdTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstMoveOrdType";
            }
        }

        public RT_Mst_MoveOrdType WA_Mst_MoveOrdType_Get(RQ_Mst_MoveOrdType objRQ_Mst_MoveOrdType, string addressAPIs)
        {
            var result = PostData<RT_Mst_MoveOrdType, RQ_Mst_MoveOrdType>("MstMoveOrdType", "WA_Mst_MoveOrdType_Get", new { }, objRQ_Mst_MoveOrdType, addressAPIs);
            return result;
        }

       
    }
}
