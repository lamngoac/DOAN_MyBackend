using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class OS_PrdCenter_Mst_OrgService : ClientServiceBase<OS_PrdCenter_Mst_OrgService>
    {
        public static OS_PrdCenter_Mst_OrgService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_OrgService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstOrg";
            }
        }

        public RT_OS_PrdCenter_Mst_Org WA_OS_PrdCenter_Mst_Org_Get(RQ_OS_PrdCenter_Mst_Org objRQ_OS_PrdCenter_Mst_Org)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Org, RQ_OS_PrdCenter_Mst_Org>(TConst.UrlType.UrlPrdCenter, "MstOrg", "WA_Mst_Org_Get", new { }, objRQ_OS_PrdCenter_Mst_Org);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Org WA_OS_PrdCenter_Mst_Org_Create(RQ_OS_PrdCenter_Mst_Org objRQ_OS_PrdCenter_Mst_Org)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Org, RQ_OS_PrdCenter_Mst_Org>(TConst.UrlType.UrlPrdCenter, "MstOrg", "WA_Mst_Org_Create", new { }, objRQ_OS_PrdCenter_Mst_Org);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Org WA_OS_PrdCenter_Mst_Org_Update(RQ_OS_PrdCenter_Mst_Org objRQ_OS_PrdCenter_Mst_Org)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Org, RQ_OS_PrdCenter_Mst_Org>(TConst.UrlType.UrlPrdCenter, "MstOrg", "WA_Mst_Org_Update", new { }, objRQ_OS_PrdCenter_Mst_Org);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Org WA_OS_PrdCenter_Mst_Org_Delete(RQ_OS_PrdCenter_Mst_Org objRQ_OS_PrdCenter_Mst_Org)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Org, RQ_OS_PrdCenter_Mst_Org>(TConst.UrlType.UrlPrdCenter, "MstOrg", "WA_Mst_Org_Delete", new { }, objRQ_OS_PrdCenter_Mst_Org);
            return result;
        }
    }
}
