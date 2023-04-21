using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class View_ColumnInGroupService : ClientServiceBase<View_ColumnInGroup>
    {
        public static View_ColumnInGroupService Instance
        {
            get
            {
                return GetInstance<View_ColumnInGroupService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "ViewColumnInGroup";
            }
        }

        public RT_View_ColumnInGroup WA_View_ColumnInGroup_Get(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnInGroup, RQ_View_ColumnInGroup>("ViewColumnInGroup", "WA_View_ColumnInGroup_Get", new { }, objRQ_View_ColumnInGroup, addressAPIs);
            return result;
        }

        public RT_View_ColumnInGroup WA_View_ColumnInGroup_Create(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnInGroup, RQ_View_ColumnInGroup>("ViewColumnInGroup", "WA_View_ColumnInGroup_Create", new { }, objRQ_View_ColumnInGroup, addressAPIs);
            return result;
        }

        public RT_View_ColumnInGroup WA_View_ColumnInGroup_Update(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnInGroup, RQ_View_ColumnInGroup>("ViewColumnInGroup", "WA_View_ColumnInGroup_Update", new { }, objRQ_View_ColumnInGroup, addressAPIs);
            return result;
        }

        public RT_View_ColumnInGroup WA_View_ColumnInGroup_Delete(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnInGroup, RQ_View_ColumnInGroup>("ViewColumnInGroup", "WA_View_ColumnInGroup_Delete", new { }, objRQ_View_ColumnInGroup, addressAPIs);
            return result;
        }
        public RT_View_ColumnInGroup WA_View_ColumnInGroup_Save(RQ_View_ColumnInGroup objRQ_View_ColumnInGroup, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnInGroup, RQ_View_ColumnInGroup>("ViewColumnInGroup", "WA_View_ColumnInGroup_Save", new { }, objRQ_View_ColumnInGroup, addressAPIs);
            return result;
        }
    }
}
