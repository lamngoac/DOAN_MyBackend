using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Temp_PrintTempService : ClientServiceBase<Temp_PrintTemp>
    {
        public static Temp_PrintTempService Instance
        {
            get
            {
                return GetInstance<Temp_PrintTempService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "TempPrintTemp";
            }
        }
        public RT_Temp_PrintTemp WA_Temp_PrintTemp_Get(RQ_Temp_PrintTemp objRQ_Temp_PrintTemp, string addressAPIs)
        {
            var result = PostData<RT_Temp_PrintTemp, RQ_Temp_PrintTemp>("TempPrintTemp", "WA_Temp_PrintTemp_Get", new { }, objRQ_Temp_PrintTemp, addressAPIs);
            return result;
        }

        public RT_Temp_PrintTemp WA_Temp_PrintTemp_Save(RQ_Temp_PrintTemp objRQ_Temp_PrintTemp, string addressAPIs)
        {
            var result = PostData<RT_Temp_PrintTemp, RQ_Temp_PrintTemp>("TempPrintTemp", "WA_Temp_PrintTemp_Save", new { }, objRQ_Temp_PrintTemp, addressAPIs);
            return result;
        }

        public RT_Temp_PrintTemp WA_Temp_PrintTemp_Approved(RQ_Temp_PrintTemp objRQ_Temp_PrintTemp, string addressAPIs)
        {
            var result = PostData<RT_Temp_PrintTemp, RQ_Temp_PrintTemp>("TempPrintTemp", "WA_Temp_PrintTemp_Approved", new { }, objRQ_Temp_PrintTemp, addressAPIs);
            return result;
        }

        public RT_Temp_PrintTemp WA_Temp_PrintTemp_Cancel(RQ_Temp_PrintTemp objRQ_Temp_PrintTemp, string addressAPIs)
        {
            var result = PostData<RT_Temp_PrintTemp, RQ_Temp_PrintTemp>("TempPrintTemp", "WA_Temp_PrintTemp_Cancel", new { }, objRQ_Temp_PrintTemp, addressAPIs);
            return result;
        }
    }
}
