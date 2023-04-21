using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class OSDMS_Mst_PrintOrderService : ClientServiceBase<OSDMS_Mst_PrintOrderService>
    {
        public static OSDMS_Mst_PrintOrderService Instance
        {
            get
            {
                return GetInstance<OSDMS_Mst_PrintOrderService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstPrintOrder";
            }
        }

        public RT_Mst_PrintOrder WA_Mst_PrintOrder_Update(string strNetWorkUrl, RQ_Mst_PrintOrder objRQ_Mst_PrintOrder)
        {

            var result = MstSvRoute_PostData<RT_Mst_PrintOrder, RQ_Mst_PrintOrder>(strNetWorkUrl, "MstPrintOrder", "WA_Mst_PrintOrder_Update", new { }, objRQ_Mst_PrintOrder);
            return result;
        }
        public RT_Mst_PrintOrder WA_Mst_PrintOrder_Get(string strNetWorkUrl, RQ_Mst_PrintOrder objRQ_Mst_PrintOrder)
        {

            var result = MstSvRoute_PostData<RT_Mst_PrintOrder, RQ_Mst_PrintOrder>(strNetWorkUrl, "MstPrintOrder", "WA_Mst_PrintOrder_Get", new { }, objRQ_Mst_PrintOrder);
            return result;
        }
    }
}
