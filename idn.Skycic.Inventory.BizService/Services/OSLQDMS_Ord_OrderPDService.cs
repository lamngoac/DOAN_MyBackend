using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class OSLQDMS_Ord_OrderPDService : ClientServiceBase<OSLQDMS_Ord_OrderPDService>
    {
        public static OSLQDMS_Ord_OrderPDService Instance
        {
            get
            {
                return GetInstance<OSLQDMS_Ord_OrderPDService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "OrdOrderPD";
            }
        }

        public RT_OS_Ord_OrderPD WA_OS_Ord_OrderPD_Get(string strNetWorkUrl, RQ_OS_Ord_OrderPD objRQ_OS_Ord_OrderPD)
        {
            //strNetWorkUrl = @"http://localhost:1800/";
            var result = MstSvRoute_PostData<RT_OS_Ord_OrderPD, RQ_OS_Ord_OrderPD>(strNetWorkUrl, ApiControllerName, "WA_Ord_OrderPD_Get", new { }, objRQ_OS_Ord_OrderPD);
            return result;
        }

        public RT_OS_Ord_OrderPD WA_OS_Ord_OrderPD_Update(string strNetWorkUrl, RQ_OS_Ord_OrderPD objRQ_OS_Ord_OrderPD)
        {
            //strNetWorkUrl = @"http://localhost:1800/";
            var result = MstSvRoute_PostData<RT_OS_Ord_OrderPD, RQ_OS_Ord_OrderPD>(strNetWorkUrl, ApiControllerName, "WA_Ord_OrderPD_UpdPGInvIn", new { }, objRQ_OS_Ord_OrderPD);
            return result;
        }
    }
}
