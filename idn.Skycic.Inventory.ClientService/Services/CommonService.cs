using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class CommonService : ClientServiceBase<Common.Models.Common>
    {
        public static CommonService Instance
        {
            get
            {
                return GetInstance<CommonService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Common";
            }
        }

        public RT_Common WA_Common_GetDTime(RQ_Common objRQ_Common, string addressAPIs)
        {
            var result = PostData<RT_Common, RQ_Common>("Common", "WA_Common_GetDTime", new { }, objRQ_Common, addressAPIs);
            return result;
        }
    }
}
