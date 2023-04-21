using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class View_ColumnViewService : ClientServiceBase<View_ColumnView>
    {
        public static View_ColumnViewService Instance
        {
            get
            {
                return GetInstance<View_ColumnViewService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "ViewColumnView";
            }
        }

        public RT_View_ColumnView WA_View_ColumnView_Get(RQ_View_ColumnView objRQ_View_ColumnView, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnView, RQ_View_ColumnView>("ViewColumnView", "WA_View_ColumnView_Get", new { }, objRQ_View_ColumnView, addressAPIs);
            return result;
        }

        public RT_View_ColumnView WA_View_ColumnView_Create(RQ_View_ColumnView objRQ_View_ColumnView, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnView, RQ_View_ColumnView>("ViewColumnView", "WA_View_ColumnView_Create", new { }, objRQ_View_ColumnView, addressAPIs);
            return result;
        }

        public RT_View_ColumnView WA_View_ColumnView_Update(RQ_View_ColumnView objRQ_View_ColumnView, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnView, RQ_View_ColumnView>("ViewColumnView", "WA_View_ColumnView_Update", new { }, objRQ_View_ColumnView, addressAPIs);
            return result;
        }

        public RT_View_ColumnView WA_View_ColumnView_Delete(RQ_View_ColumnView objRQ_View_ColumnView, string addressAPIs)
        {
            var result = PostData<RT_View_ColumnView, RQ_View_ColumnView>("ViewColumnView", "WA_View_ColumnView_Delete", new { }, objRQ_View_ColumnView, addressAPIs);
            return result;
        }
    }
}
