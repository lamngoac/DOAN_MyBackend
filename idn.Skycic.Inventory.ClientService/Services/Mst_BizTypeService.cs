using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_BizTypeService : ClientServiceBase<iNOS_Mst_BizType>
    {
        public static Mst_BizTypeService Instance
        {
            get
            {
                return GetInstance<Mst_BizTypeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "iNOSMstBizType";
            }
        }

        public RT_iNOS_Mst_BizType WA_iNOS_Mst_BizType_Get(RQ_iNOS_Mst_BizType objRQ_iNOS_Mst_BizType, string addressAPIs)
        {
            var result = PostData<RT_iNOS_Mst_BizType, RQ_iNOS_Mst_BizType>("iNOSMstBizType", "WA_iNOS_Mst_BizType_Get", new { }, objRQ_iNOS_Mst_BizType, addressAPIs);
            return result;
        }

        // ReportServer
        
        public RT_iNOS_Mst_BizType WA_RptSv_iNOS_Mst_BizType_Get(RQ_iNOS_Mst_BizType objRQ_iNOS_Mst_BizType, string addressAPIs)
        {
            var result = PostData<RT_iNOS_Mst_BizType, RQ_iNOS_Mst_BizType>("iNOSMstBizType", "WA_RptSv_iNOS_Mst_BizType_Get", new { }, objRQ_iNOS_Mst_BizType, addressAPIs);
            return result;
        }
    }
}
