using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class OS_SkyCICService : ClientServiceBase<GeneneralNotificationBatch>
    {
        public static OS_SkyCICService Instance
        {
            get
            {
                return GetInstance<OS_SkyCICService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "OSSkyCICService";
            }
        }

        public GeneneralNotificationBatch WA_SkyCIC_NotificationApi_SendGeneralNotification(string strNetWorkUrl, string strAuthorization, object model)
        {
            string strPath = "agent/NotificationApi/SendGeneralNotification";

            var result = SkyCICPostData<GeneneralNotificationBatch, object>(strNetWorkUrl, strPath, strAuthorization, new { }, model);
            return result;
        }
    }
}
