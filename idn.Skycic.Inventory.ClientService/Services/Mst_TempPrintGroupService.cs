using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_TempPrintGroupService : ClientServiceBase<Mst_TempPrintGroup>
    {
        public static Mst_TempPrintGroupService Instance
        {
            get
            {
                return GetInstance<Mst_TempPrintGroupService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstTempPrintGroup";
            }
        }

        public RT_Mst_TempPrintGroup WA_Mst_TempPrintGroup_Get(RQ_Mst_TempPrintGroup objRQ_Mst_TempPrintGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_TempPrintGroup, RQ_Mst_TempPrintGroup>("MstTempPrintGroup", "WA_Mst_TempPrintGroup_Get", new { }, objRQ_Mst_TempPrintGroup, addressAPIs);
            return result;
        }
        public RT_Mst_TempPrintGroup WA_Mst_TempPrintGroup_Save(RQ_Mst_TempPrintGroup objRQ_Mst_TempPrintGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_TempPrintGroup, RQ_Mst_TempPrintGroup>("MstTempPrintGroup", "WA_Mst_TempPrintGroup_Save", new { }, objRQ_Mst_TempPrintGroup, addressAPIs);
            return result;
        }
    }
}
