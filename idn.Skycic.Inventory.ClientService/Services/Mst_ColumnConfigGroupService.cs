using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_ColumnConfigGroupService : ClientServiceBase<Mst_ColumnConfigGroup>
    {
        public static Mst_ColumnConfigGroupService Instance
        {
            get
            {
                return GetInstance<Mst_ColumnConfigGroupService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstColumnConfigGroup";
            }
        }


        public RT_Mst_ColumnConfigGroup WA_Mst_ColumnConfigGroup_Get(RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_ColumnConfigGroup, RQ_Mst_ColumnConfigGroup>(ApiControllerName, "WA_Mst_ColumnConfigGroup_Get", new { }, objRQ_Mst_ColumnConfigGroup, addressAPIs);
            return result;
        }

        public RT_Mst_ColumnConfigGroup WA_Mst_ColumnConfigGroup_Update(RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_ColumnConfigGroup, RQ_Mst_ColumnConfigGroup>(ApiControllerName, "WA_Mst_ColumnConfigGroup_Update", new { }, objRQ_Mst_ColumnConfigGroup, addressAPIs);
            return result;
        }

        public RT_Mst_ColumnConfigGroup WA_Mst_ColumnConfigGroup_Create(RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_ColumnConfigGroup, RQ_Mst_ColumnConfigGroup>(ApiControllerName, "WA_Mst_ColumnConfigGroup_Create", new { }, objRQ_Mst_ColumnConfigGroup, addressAPIs);
            return result;
        }

        public RT_Mst_ColumnConfigGroup WA_Mst_ColumnConfigGroup_Delete(RQ_Mst_ColumnConfigGroup objRQ_Mst_ColumnConfigGroup, string addressAPIs)
        {
            var result = PostData<RT_Mst_ColumnConfigGroup, RQ_Mst_ColumnConfigGroup>(ApiControllerName, "WA_Mst_ColumnConfigGroup_Delete", new { }, objRQ_Mst_ColumnConfigGroup, addressAPIs);
            return result;
        }
    }
}
