using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_BizFieldService : ClientServiceBase<iNOS_Mst_BizField>
    {
        public static Mst_BizFieldService Instance
        {
            get
            {
                return GetInstance<Mst_BizFieldService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "iNOSMstBizField";
            }
        }

        public RT_iNOS_Mst_BizField WA_iNOS_Mst_BizField_Get(RQ_iNOS_Mst_BizField objRQ_iNOS_Mst_BizField, string addressAPIs)
        {
            var result = PostData<RT_iNOS_Mst_BizField, RQ_iNOS_Mst_BizField>("iNOSMstBizField", "WA_iNOS_Mst_BizField_Get", new { }, objRQ_iNOS_Mst_BizField, addressAPIs);
            return result;
        }

        // ReportServer
        public RT_iNOS_Mst_BizField WA_RptSv_iNOS_Mst_BizField_Get(RQ_iNOS_Mst_BizField objRQ_iNOS_Mst_BizField, string addressAPIs)
        {
            var result = PostData<RT_iNOS_Mst_BizField, RQ_iNOS_Mst_BizField>("iNOSMstBizField", "WA_RptSv_iNOS_Mst_BizField_Get", new { }, objRQ_iNOS_Mst_BizField, addressAPIs);
            return result;
        }
    }
}
