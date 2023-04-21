using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_BizSizeService : ClientServiceBase<iNOS_Mst_BizSize>
    {
        public static Mst_BizSizeService Instance
        {
            get
            {
                return GetInstance<Mst_BizSizeService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "iNOSMstBizSize";
            }
        }

        public RT_iNOS_Mst_BizSize WA_iNOS_Mst_BizSize_Get(RQ_iNOS_Mst_BizSize objRQ_iNOS_Mst_BizSize, string addressAPIs)
        {
            var result = PostData<RT_iNOS_Mst_BizSize, RQ_iNOS_Mst_BizSize>("iNOSMstBizSize", "WA_iNOS_Mst_BizSize_Get", new { }, objRQ_iNOS_Mst_BizSize, addressAPIs);
            return result;
        }

        // ReportServer
        public RT_iNOS_Mst_BizSize WA_RptSv_iNOS_Mst_BizSize_Get(RQ_iNOS_Mst_BizSize objRQ_iNOS_Mst_BizSize, string addressAPIs)
        {
            var result = PostData<RT_iNOS_Mst_BizSize, RQ_iNOS_Mst_BizSize>("iNOSMstBizSize", "WA_RptSv_iNOS_Mst_BizSize_Get", new { }, objRQ_iNOS_Mst_BizSize, addressAPIs);
            return result;
        }
    }
}
