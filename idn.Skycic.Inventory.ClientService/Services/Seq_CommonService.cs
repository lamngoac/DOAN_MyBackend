using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Seq_CommonService : ClientServiceBase<Seq_Common>
    {
        public static Seq_CommonService Instance
        {
            get
            {
                return GetInstance<Seq_CommonService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Seq";
            }
        }

        public RT_Seq_Common WA_Seq_Common_Get(RQ_Seq_Common objRQ_Seq_Common, string addressAPIs)
        {
            var result = PostData<RT_Seq_Common, RQ_Seq_Common>("Seq", "WA_Seq_Common_Get", new { }, objRQ_Seq_Common, addressAPIs);
            return result;
        }
        public RT_Seq_Common WA_RptSv_Seq_MST_Get(RQ_Seq_Common objRQ_Seq_Common, string addressAPIs)
        {
            var result = PostData<RT_Seq_Common, RQ_Seq_Common>("ReportServer", "WA_RptSv_Seq_MST_Get", new { }, objRQ_Seq_Common, addressAPIs);
            return result;
        }
    }
}
