using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_DealerService : ClientServiceBase<Mst_Dealer>
    {
        public static Mst_DealerService Instance
        {
            get
            {
                return GetInstance<Mst_DealerService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstDealer";
            }
        }

        public RT_Mst_Dealer WA_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Dealer, RQ_Mst_Dealer>("MstDealer", "WA_Mst_Dealer_Get", new { }, objRQ_Mst_Dealer, addressAPIs);
            return result;
        }

        public RT_Mst_Dealer WA_Mst_Dealer_Create(RQ_Mst_Dealer objRQ_Mst_Dealer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Dealer, RQ_Mst_Dealer>("MstDealer", "WA_Mst_Dealer_Create", new { }, objRQ_Mst_Dealer, addressAPIs);
            return result;
        }

        public RT_Mst_Dealer WA_Mst_Dealer_Update(RQ_Mst_Dealer objRQ_Mst_Dealer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Dealer, RQ_Mst_Dealer>("MstDealer", "WA_Mst_Dealer_Update", new { }, objRQ_Mst_Dealer, addressAPIs);
            return result;
        }

        public RT_Mst_Dealer WA_Mst_Dealer_Delete(RQ_Mst_Dealer objRQ_Mst_Dealer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Dealer, RQ_Mst_Dealer>("MstDealer", "WA_Mst_Dealer_Delete", new { }, objRQ_Mst_Dealer, addressAPIs);
            return result;
        }
        public RT_Mst_Dealer WA_RptSv_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Dealer, RQ_Mst_Dealer>("MstDealer", "WA_RptSv_Mst_Dealer_Get", new { }, objRQ_Mst_Dealer, addressAPIs);
            return result;
        }

        public RT_Mst_Dealer WA_RptSv_Mst_Dealer_Create(RQ_Mst_Dealer objRQ_Mst_Dealer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Dealer, RQ_Mst_Dealer>("MstDealer", "WA_RptSv_Mst_Dealer_Create", new { }, objRQ_Mst_Dealer, addressAPIs);
            return result;
        }

        public RT_Mst_Dealer WA_RptSv_Mst_Dealer_Update(RQ_Mst_Dealer objRQ_Mst_Dealer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Dealer, RQ_Mst_Dealer>("MstDealer", "WA_RptSv_Mst_Dealer_Update", new { }, objRQ_Mst_Dealer, addressAPIs);
            return result;
        }

        public RT_Mst_Dealer WA_RptSv_Mst_Dealer_Delete(RQ_Mst_Dealer objRQ_Mst_Dealer, string addressAPIs)
        {
            var result = PostData<RT_Mst_Dealer, RQ_Mst_Dealer>("MstDealer", "WA_RptSv_Mst_Dealer_Delete", new { }, objRQ_Mst_Dealer, addressAPIs);
            return result;
        }
    }
}
