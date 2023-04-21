using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class View_GroupViewService : ClientServiceBase<View_GroupView>
    {
        public static View_GroupViewService Instance
        {
            get
            {
                return GetInstance<View_GroupViewService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "ViewGroupView";
            }
        }

        public RT_View_GroupView WA_View_GroupView_Get(RQ_View_GroupView objRQ_View_GroupView, string addressAPIs)
        {
            var result = PostData<RT_View_GroupView, RQ_View_GroupView>("ViewGroupView", "WA_View_GroupView_Get", new { }, objRQ_View_GroupView, addressAPIs);
            return result;
        }

        public RT_View_GroupView WA_View_GroupView_Create(RQ_View_GroupView objRQ_View_GroupView, string addressAPIs)
        {
            var result = PostData<RT_View_GroupView, RQ_View_GroupView>("ViewGroupView", "WA_View_GroupView_Create", new { }, objRQ_View_GroupView, addressAPIs);
            return result;
        }

        public RT_View_GroupView WA_View_GroupView_Update(RQ_View_GroupView objRQ_View_GroupView, string addressAPIs)
        {
            var result = PostData<RT_View_GroupView, RQ_View_GroupView>("ViewGroupView", "WA_View_GroupView_Update", new { }, objRQ_View_GroupView, addressAPIs);
            return result;
        }

        public RT_View_GroupView WA_View_GroupView_Delete(RQ_View_GroupView objRQ_View_GroupView, string addressAPIs)
        {
            var result = PostData<RT_View_GroupView, RQ_View_GroupView>("ViewGroupView", "WA_View_GroupView_Delete", new { }, objRQ_View_GroupView, addressAPIs);
            return result;
        }
    }
}
