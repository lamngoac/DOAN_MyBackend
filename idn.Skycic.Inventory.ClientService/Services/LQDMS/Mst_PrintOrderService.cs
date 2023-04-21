using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services.LQDMS
{
    public class Mst_PrintOrderService : ClientServiceBase<Mst_PrintOrder>
    {
        public static Mst_PrintOrderService Instance
        {
            get
            {
                return GetInstance<Mst_PrintOrderService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "MstPrintOrder";
            }
        }
        public RT_Mst_PrintOrder WA_Mst_PrintOrder_Get(RQ_Mst_PrintOrder objRQ_Mst_PrintOrder, string strUrl)
        {
            var result = PostData<RT_Mst_PrintOrder, RQ_Mst_PrintOrder>("MstPrintOrder", "WA_Mst_PrintOrder_Get", new { }, objRQ_Mst_PrintOrder, strUrl);
            return result;
        }
    }
}
