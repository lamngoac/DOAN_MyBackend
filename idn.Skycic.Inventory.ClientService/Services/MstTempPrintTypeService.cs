using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class MstTempPrintTypeService : ClientServiceBase<Mst_TempPrintType>
    {
        public static MstTempPrintTypeService Instance
        {
            get
            {
                return GetInstance<MstTempPrintTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstTempPrintType";
            }
        }

        public RT_Mst_TempPrintType WA_Mst_TempPrintType_Get(RQ_Mst_TempPrintType objRQ_Mst_TempPrintType, string addressAPIs)
        {
            var result = PostData<RT_Mst_TempPrintType, RQ_Mst_TempPrintType>("MstTempPrintType", "WA_Mst_TempPrintType_Get", new { }, objRQ_Mst_TempPrintType, addressAPIs);
            return result;
        }
    }
}
