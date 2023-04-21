using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_ColumnConfigService : ClientServiceBase<Mst_ColumnConfig>
    {
        public static Mst_ColumnConfigService Instance
        {
            get
            {
                return GetInstance<Mst_ColumnConfigService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstColumnConfig";
            }
        }

        public RT_Mst_ColumnConfig WA_Mst_ColumnConfig_Get(RQ_Mst_ColumnConfig objRQ_Mst_ColumnConfig, string addressAPIs)
        {
            var result = PostData<RT_Mst_ColumnConfig, RQ_Mst_ColumnConfig>(ApiControllerName, "WA_Mst_ColumnConfig_Get", new { }, objRQ_Mst_ColumnConfig, addressAPIs);
            return result;
        }

        public RT_Mst_ColumnConfig WA_Mst_ColumnConfig_Update(RQ_Mst_ColumnConfig objRQ_Mst_ColumnConfig, string addressAPIs)
        {
            var result = PostData<RT_Mst_ColumnConfig, RQ_Mst_ColumnConfig>(ApiControllerName, "WA_Mst_ColumnConfig_Update", new { }, objRQ_Mst_ColumnConfig, addressAPIs);
            return result;
        }
    }
}
