using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Map_UserInNotifyTypeService : ClientServiceBase<RT_Map_UserInNotifyType>
    {
        public static Map_UserInNotifyTypeService Instance
        {
            get
            {
                return GetInstance<Map_UserInNotifyTypeService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MapUserInNotifyType";
            }
        }

        public RT_Map_UserInNotifyType WA_Map_UserInNotifyType_Get(RQ_Map_UserInNotifyType objRQ_Map_UserInNotifyType, string addressAPIs)
        {
            var result = PostData<RT_Map_UserInNotifyType, RQ_Map_UserInNotifyType>(ApiControllerName, "WA_Map_UserInNotifyType_Get", new { }, objRQ_Map_UserInNotifyType, addressAPIs);
            return result;
        }

        public RT_Map_UserInNotifyType WA_Map_UserInNotifyType_Save(RQ_Map_UserInNotifyType objRQ_Map_UserInNotifyType, string addressAPIs)
        {
            var result = PostData<RT_Map_UserInNotifyType, RQ_Map_UserInNotifyType>(ApiControllerName, "WA_Map_UserInNotifyType_Save", new { }, objRQ_Map_UserInNotifyType, addressAPIs);
            return result;
        }
    }
}
